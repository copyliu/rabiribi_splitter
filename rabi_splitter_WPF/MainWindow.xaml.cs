using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace rabi_splitter_WPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainContext mainContext;
        private DebugContext debugContext;
        private PracticeModeContext practiceModeContext;
        private static TcpClient tcpclient;
        private static NetworkStream networkStream;
        private readonly Regex titleReg = new Regex(@"ver.*?(\d+\.?\d+.*)$");
        private readonly Thread memoryThread;
        private void ReadMemory()
        {
            practiceModeContext.ResetSendTriggers();
            
            var processlist = Process.GetProcessesByName("rabiribi");
            if (processlist.Length > 0)
            {

                Process process = processlist[0];
                if (process.MainWindowTitle != mainContext.oldtitle)
                {
                    var result = titleReg.Match(process.MainWindowTitle);
                    string rabiver;
                    if (result.Success)
                    {

                         rabiver = result.Groups[1].Value;
                        mainContext.veridx = Array.IndexOf(StaticData.VerNames, rabiver);
                        if (mainContext.veridx < 0)
                        {
                            mainContext.GameVer = rabiver + " Running (not supported)";
                            
                            return;
                        }



                    }
                    else
                    {
                        mainContext.veridx = -1;
                        mainContext.GameVer = "Running (Unknown version)";

                        return;
                    }
                    mainContext.GameVer = rabiver + " Running";
                    mainContext.oldtitle = process.MainWindowTitle;
                    
                }


                if (mainContext.veridx < 0) return;


                #region read igt

                int igt = MemoryHelper.GetMemoryValue<int>(process, StaticData.IGTAddr[mainContext.veridx]);
                if (igt > 0 && mainContext.Igt)
                {
                    sendigt((float)igt / 60);
                }

                #endregion

                #region Detect Reload
                
                bool reloaded = false;
                {
                    // When reloading, the frame numbers look like this:
                    // Case 1: (PLAYTIME frame steps down briefly before going to 0)
                    //         1108, 1109, 1110, 1110, 1110, 540, 0, 0, 0, 0, 0, 540, 540, 541, 542, 544,
                    // Case 2: (PLAYTIME frame goes straight to 0)
                    //         1108, 1109, 1110, 1110, 1110, 0, 0, 0, 0, 0, 540, 540, 541, 542, 544,
                    // This can sometimes cause reloads to be detected twice (which is usually not a problem though, but ocd lol)
                    // So we use a switch to "prime" the canReload flag whenever it detects the PLAYTIME increasing.
                    // the canReload flag is unset when a reload is detected, and remains unset until PLAYTIME starts increasing again.

                    int playtime = MemoryHelper.GetMemoryValue<int>(process, StaticData.PlaytimeAddr[mainContext.veridx]);
                    reloaded = playtime < mainContext.lastplaytime;

                    if (playtime > mainContext.lastplaytime) mainContext.canReload = true;
                    if (mainContext.canReload && playtime < mainContext.lastplaytime)
                    {
                        PracticeModeSendTrigger(SplitTrigger.Reload);
                        DebugLog("Reload Game!");
                        mainContext.canReload = false;
                    }
                    mainContext.lastplaytime = playtime;
                }

                #endregion


                #region CheckMoney

                if (mainContext.Computer)
                {
                    var newmoney = MemoryHelper.GetMemoryValue<int>(process, StaticData.MoneyAddress[mainContext.veridx]);
                    if (newmoney - mainContext.lastmoney == 17500)
                    {
                        SpeedrunSendSplit();
                        DebugLog("get 17500 en, split");
                    }
                    mainContext.lastmoney = newmoney;
                }

                #endregion

                int mapid = MemoryHelper.GetMemoryValue<int>(process, StaticData.MapAddress[mainContext.veridx]);
                if (mainContext.lastmapid != mapid)
                {
                    PracticeModeMapChangeTrigger(mainContext.lastmapid, mapid);
                    DebugLog("newmap: " + mapid + ":" + StaticData.GetMapName(mapid));
                    mainContext.lastmapid = mapid;
                }


                #region checkTM



                #endregion

                #region Music

                int musicaddr = StaticData.MusicAddr[mainContext.veridx];
                int musicid = MemoryHelper.GetMemoryValue<int>(process, musicaddr);

                #region Detect Start Game

                if (musicid == 53){
                    int blackness = MemoryHelper.GetMemoryValue<int>(process, StaticData.BlacknessAddr[mainContext.veridx]);
                    if (mainContext.previousBlackness == 0 && blackness >= 100000)
                    {
                        // Sudden increase by 100000
                        // Have to be careful, though. I don't know whether anything else causes blackness to increase by 100000
                        if (mainContext.AutoStart) SpeedrunSendStartTimer();
                        DebugLog("Start Game!");
                        mainContext.LastBossEnd=DateTime.Now;
                    }
                    mainContext.previousBlackness = blackness;
                }

                #endregion


                if (musicid > 0)
                {
                    if (mainContext.lastmusicid != musicid)
                    {
                        PracticeModeMusicChangeTrigger(mainContext.lastmusicid, musicid);
                        DebugLog("new music:" + musicid + ":" + StaticData.GetMusicName(musicid));
                        mainContext.GameMusic = StaticData.GetMusicName(musicid);

                        if ((musicid == 45 || musicid == 46 || musicid == 53) && mainContext.AutoReset)
                        {
                            DebugLog("Title music, reset");
                            //reset
                            SpeedrunSendReset();
                            mainContext.Alius1 = true;
                            mainContext.Noah1Reload = false;
                            mainContext.Bossbattle = false;
                            mainContext.LastBossEnd = null;
                            mainContext.LastBossStart = null;
                        }

                        else
                        {
                            var bossmusicflag = StaticData.IsBossMusic(musicid);
                            if (bossmusicflag)
                            {
                                if (mainContext.Bossbattle)
                                {
                                    if (mainContext.Noah1Reload && (mainContext.lastmusicid == 52 || musicid == 52))
                                    {
                                        DebugLog("noah 1 reload? ignore");
                                    }
                                    else
                                    {
                                        if (mainContext.MusicStart || mainContext.MusicEnd)
                                        {
                                            SpeedrunSendSplit();
                                            DebugLog("new boss music, split");

                                        }
                                        if (musicid == 37)
                                        {
                                            mainContext.Noah1Reload = true;
                                            DebugLog("noah1 music start, ignore MR forever");
                                        }
                                    }

                                    mainContext.lastmusicid = musicid;
                                    return;
                                }
                            }
                            if (!mainContext.Bossbattle)
                            {

                                if (musicid == 54 && mainContext.Alius1 && !mainContext.ForceAlius1)
                                {
                                    mainContext.Bossbattle = false;
                                    mainContext.Alius1 = false;
                                    DebugLog("Alius music, ignore once");

                                }
                                else if (musicid == 42 && mapid == 1 && mainContext.Irisu1)
                                {
                                    mainContext.Bossbattle = false;
                                    DebugLog("Irisu P1, ignore");

                                }
                                else
                                {
                                    if (bossmusicflag)
                                    {
                                        if (mapid == 5 && musicid == 44 && mainContext.SideCh)
                                        {
                                            mainContext.Bossbattle = false;
                                            DebugLog("sidechapter, ignore");

                                        }
                                        else
                                        {
                                            PracticeModeSendTrigger(SplitTrigger.BossStart);
                                            mainContext.Bossbattle = true;
                                            mainContext.lastbosslist = new List<int>();
                                            mainContext.lastnoah3hp = -1;
                                            if (musicid == 37)
                                            {
                                                mainContext.Noah1Reload = true;
                                                DebugLog("noah1 music start, ignore MR forever");
                                            }
                                            if (mainContext.MusicStart)
                                            {
                                                SpeedrunSendSplit();
                                                DebugLog("music start, split");

                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (!bossmusicflag) //boss music end!
                                {
                                    mainContext.Bossbattle = false;
                                    if (mainContext.MusicEnd)
                                    {
                                        if (!mainContext.DontSplitOnReload || !reloaded) SpeedrunSendSplit();
                                        if (!reloaded) PracticeModeSendTrigger(SplitTrigger.BossEnd);
                                        DebugLog(reloaded ? "music end, don't split (reload)" : "music end, split");

                                    }
                                }
                            }
                        }
                        mainContext.lastmusicid = musicid;

                    }
                }
                else
                {
                    mainContext.GameMusic = "N/A";
                }

                #endregion Music

                #region SpecialBOSS

                if (mainContext.Bossbattle)
                {
                    if (mainContext.MiruDe || false)//todo noah3 option
                    {
                        int Noah3HP = -1;

                        if (StaticData.IsValidMap(mapid))
                        {
                            int ptr = MemoryHelper.GetMemoryValue<int>(process, StaticData.EnemyPtrAddr[mainContext.veridx]);
                            List<int> bosses = new List<int>();
                            for (var i = 0; i < 50; i++)
                            {
                                ptr = ptr + StaticData.EnemyEntitySize[mainContext.veridx];

                                var emyid = MemoryHelper.GetMemoryValue<int>(process,
                                    ptr + StaticData.EnemyEntityIDOffset[mainContext.veridx], false);
                                if (StaticData.IsBoss(emyid))
                                {
                                    bosses.Add(emyid);
                                    if (emyid == 1053)
                                    {
                                        Noah3HP = MemoryHelper.GetMemoryValue<int>(process,
                                            ptr + StaticData.EnemyEntityHPOffset[mainContext.veridx], false);
                                    }

                                }

                            }
                            if (mainContext.MiruDe && mapid==8)
                            {
                                foreach (var boss in mainContext.lastbosslist)
                                {

                                    if (boss == 1043)
                                    {
                                        if (!bosses.Contains(boss)) //despawn
                                        {
                                            SpeedrunSendSplit();
                                            DebugLog("miru despawn, split");
                                            mainContext.Bossbattle = false;

                                        }
                                    }
                                }
                            }

//                            if (cbBoss3.Checked)
//                            {
//                                if (bosses.Contains(1053) && Noah3HP < lastnoah3hp && Noah3HP == 1)
//                                {
//                                    sendsplit();
//                                    DebugLog("noah3 hp 1, split");
//                                    bossbattle = false;
//                                }
//                            }
                            if (mainContext.Tm2 && musicid == 8)
                            {
                                bool f = true;
                                foreach (var boss in mainContext.lastbosslist)
                                {

                                    if (boss == 1024)
                                    {
                                        if (!bosses.Contains(boss)) //despawn
                                        {
                                            SpeedrunSendSplit();
                                            DebugLog("nixie despawn, split");
                                            mainContext.Bossbattle = false;
                                            f = false;
                                            break;
                                        }
                                    }
                                }

                                int newTM = MemoryHelper.GetMemoryValue<int>(process, StaticData.TownMemberAddr[mainContext.veridx]);
                                if (newTM - mainContext.lastTM == 1 && f) //for after 1.71 , 1.71 isn't TM+2 at once when skip Nixie, it's TM+1 twice

                                {
                                    if (DateTime.Now - mainContext.LastTMAddTime < TimeSpan.FromSeconds(1))
                                    {
                                        var d = DateTime.Now - mainContext.LastTMAddTime;
                                        mainContext.Bossbattle = false;
                                        SpeedrunSendSplit();
                                        DebugLog("TM+2 in " + d.TotalMilliseconds + " ms, split");
                                    }
                                    mainContext.LastTMAddTime = DateTime.Now;
                                }
                                else if (newTM - mainContext.lastTM == 2 && f)//for 1.65-1.70
                                {
                                    mainContext.Bossbattle = false;
                                    SpeedrunSendSplit();
                                    DebugLog("TM+2, split");
                                }
                                mainContext.lastTM = newTM;
                            }
                            mainContext.lastbosslist = bosses;
                            mainContext.lastnoah3hp = Noah3HP;


                        }


                    }
                }


                #endregion SpecialBOSS

                if (mainContext.DebugArea)
                {
                    int ptr = MemoryHelper.GetMemoryValue<int>(process, StaticData.EnemyPtrAddr[mainContext.veridx]);
                    //                    List<int> bosses = new List<int>();
                    //                    List<int> HPS = new List<int>();
//                    this.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => debugContext.BossList.Clear()));
//                    ptr += StaticData.EnemyEntitySize[mainContext.veridx] * 3;
                    for (var i = 0; i < 50; i++)
                    {
                        ptr += StaticData.EnemyEntitySize[mainContext.veridx];
                        debugContext.BossList[i].BossID = MemoryHelper.GetMemoryValue<int>(process,
                            ptr + StaticData.EnemyEntityIDOffset[mainContext.veridx], false);
                        debugContext.BossList[i].BossHP = MemoryHelper.GetMemoryValue<int>(process,
                            ptr + StaticData.EnemyEntityHPOffset[mainContext.veridx], false);



                    }
                   
                }
                debugContext.BossEvent = mainContext.Bossbattle;
            }
            else
            {

                mainContext.oldtitle = "";

                mainContext.GameVer = "Not Found";
                mainContext.GameMusic = "N/A";

            }
            mainContext.NotifyTimer();
            SendPracticeModeMessages();
        }

        private void DebugLog(string log)
        {
            this.debugContext.DebugLog += log + "\n";
        }

        private void SpeedrunSendSplit()
        {
            if (!mainContext.PracticeMode) SendMessage("split\r\n");
        }

        private void SpeedrunSendReset()
        {
            if (!mainContext.PracticeMode) SendMessage("reset\r\n");
        }

        private void SpeedrunSendStartTimer()
        {
            if (!mainContext.PracticeMode) SendMessage("starttimer\r\n");
        }
        
        private void sendigt(float time)
        {
            SendMessage($"setgametime {time}\r\n");
        }

        private void PracticeModeSendTrigger(SplitTrigger trigger)
        {
            if (mainContext.PracticeMode) DebugLog("Practice Mode Trigger " + (trigger.ToString()));
            practiceModeContext.SendTrigger(SplitCondition.Trigger(trigger));
        }

        private void PracticeModeMapChangeTrigger(int oldMapId, int newMapId)
        {
            if (mainContext.PracticeMode) DebugLog("Practice Mode Trigger Map Change " + oldMapId + " -> " + newMapId);
            practiceModeContext.SendTrigger(SplitCondition.MapChange(oldMapId, newMapId));
        }

        private void PracticeModeMusicChangeTrigger(int oldMusicId, int newMusicId)
        {
            if (mainContext.PracticeMode) DebugLog("Practice Mode Trigger Music Change " + oldMusicId + " -> " + newMusicId);
            practiceModeContext.SendTrigger(SplitCondition.MusicChange(oldMusicId, newMusicId));
        }

        private void SendPracticeModeMessages()
        {
            if (!mainContext.PracticeMode) return;
            if (practiceModeContext.SendStartTimerThisFrame())
            {
                SendMessage("starttimer\r\n");
            }
            if (practiceModeContext.SendSplitTimerThisFrame())
            {
                SendMessage("split\r\n");
            }
            if (practiceModeContext.SendResetTimerThisFrame())
            {
                SendMessage("reset\r\n");
            }
        }

        private void SendMessage(string message)
        {
            if (tcpclient != null && tcpclient.Connected)
            {
                try
                {
                    var b = Encoding.UTF8.GetBytes(message);
                    networkStream.Write(b, 0, b.Length);
                }
                catch (Exception)
                {

                    disconnect();
                }
            }
        }
        void disconnect()
        {
            tcpclient = null;
            this.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                BtnConnect.IsEnabled = true;
            }));


        }

        public MainWindow()
        {
            InitializeComponent();
            mainContext=new MainContext();
            debugContext=new DebugContext();
            practiceModeContext = new PracticeModeContext();
            this.DataContext = mainContext;
            DebugPanel.DataContext = debugContext;
            this.Grid.ItemsSource = debugContext.BossList;
            BossEventDebug.DataContext = debugContext;
            this.PracticeModePanel.DataContext = practiceModeContext;
            memoryThread = new Thread(() =>
            {
                while (true)
                {
                    try
                    {
                        ReadMemory();
                    }
                    catch (Exception e)
                    {
                        DebugLog(e.ToString());
                    }
                   
                    Thread.Sleep(1000 / 60);
                }

            });
            memoryThread.IsBackground = true;
            memoryThread.Start();
        }

        private void BtnConnect_Click(object sender, RoutedEventArgs e)
        {
            if (tcpclient != null && tcpclient.Connected) return;
            try
            {
                tcpclient = new TcpClient("127.0.0.1", Convert.ToInt32(mainContext.ServerPort));
                networkStream = tcpclient.GetStream();
                this.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
                {
                    BtnConnect.IsEnabled = false;
                }));
            }
            catch (Exception)
            {
                tcpclient = null;
                networkStream = null;
                MessageBox.Show(this, "Connect Failed");

            }
        }

        private void TextBlock_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://github.com/copyliu/rabiribi_splitter");
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var s = sender as TextBox;
            if (s != null)
            {
                s.ScrollToEnd();
            }
        }
    }
}
