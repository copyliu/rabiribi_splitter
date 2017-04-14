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
        private static TcpClient tcpclient;
        private static NetworkStream networkStream;
        private readonly Regex titleReg = new Regex(@"ver.*?(\d+\.?\d+.*)$");
        private readonly Thread memoryThread;
        private void ReadMemory()
        {

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
                            mainContext.GameVer = rabiver + " Running (not support)";
                            
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
                    int playtime = MemoryHelper.GetMemoryValue<int>(process, StaticData.PlaytimeAddr[mainContext.veridx]);
                    reloaded = playtime != 0 && playtime < mainContext.lastplaytime;
                    if (reloaded) DebugLog("Reload Game!");
                    mainContext.lastplaytime = playtime;
                }

                #endregion

                #region Detect Start Game

                {
                    int blackness = MemoryHelper.GetMemoryValue<int>(process, StaticData.BlacknessAddr[mainContext.veridx]);
                    if (mainContext.previousBlackness == 0 && blackness >= 100000)
                    {
                        // Sudden increase by 100000
                        // Have to be careful, though. I don't know whether anything else causes blackness to increase by 100000
                        if (mainContext.AutoStart) sendstarttimer();
                        DebugLog("Start Game!");
                    }
                    mainContext.previousBlackness = blackness;
                }

                #endregion

                #region CheckMoney

                if (mainContext.Computer)
                {
                    var newmoney = MemoryHelper.GetMemoryValue<int>(process, StaticData.MoneyAddress[mainContext.veridx]);
                    if (newmoney - mainContext.lastmoney == 17500)
                    {
                        sendsplit();
                        DebugLog("get 17500 en, split");
                    }
                    mainContext.lastmoney = newmoney;
                }

                #endregion

                int mapid = MemoryHelper.GetMemoryValue<int>(process, StaticData.MapAddress[mainContext.veridx]);
                if (mainContext.lastmapid != mapid)
                {
                    DebugLog("newmap: " + mapid + ":" + StaticData.MapNames[mapid]);
                    mainContext.lastmapid = mapid;
                }


                #region checkTM



                #endregion

                #region Music

                int musicaddr = StaticData.MusicAddr[mainContext.veridx];
                int musicid = MemoryHelper.GetMemoryValue<int>(process, musicaddr);
                if (musicid > 0 && musicid < StaticData.MusicNames.Length)
                {
                    if (mainContext.lastmusicid != musicid)
                    {
                        DebugLog("new music:" + musicid + ":" + StaticData.MusicNames[musicid]);
                        mainContext.GameMusic = StaticData.MusicNames[musicid];

                        if ((musicid == 45 || musicid == 46 || musicid == 53) && mainContext.AutoReset)
                        {
                            DebugLog("Title music, reset");
                            //reset
                            sendreset();
                            mainContext.AliusI = true;
                            mainContext.Noah1Reload = false;
                            mainContext.bossbattle = false;
                        }

                        else
                        {
                            var bossmusicflag = StaticData.BossMusics.Contains(musicid);
                            if (bossmusicflag)
                            {
                                if (mainContext.bossbattle)
                                {
                                    if (mainContext.Noah1Reload && (mainContext.lastmusicid == 52 || musicid == 52))
                                    {
                                        DebugLog("noah 1 reload? ignore");
                                    }
                                    else
                                    {
                                        if (mainContext.MusicStart || mainContext.MusicEnd)
                                        {
                                            sendsplit();
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
                            if (!mainContext.bossbattle)
                            {

                                if (musicid == 54 && mainContext.AliusI)
                                {
                                    mainContext.bossbattle = false;
                                    mainContext.AliusI = false;
                                    DebugLog("Alius music, ignore once");

                                }
                                else if (musicid == 42 && mapid == 1 && mainContext.Irisu1)
                                {
                                    mainContext.bossbattle = false;
                                    DebugLog("Irisu P1, ignore");

                                }
                                else
                                {
                                    if (bossmusicflag)
                                    {
                                        if (mapid == 5 && musicid == 44 && mainContext.SideCh)
                                        {
                                            mainContext.bossbattle = false;
                                            DebugLog("sidechapter, ignore");

                                        }
                                        else
                                        {
                                            mainContext.bossbattle = true;
                                            mainContext.lastbosslist = new List<int>();
                                            mainContext.lastnoah3hp = -1;
                                            if (musicid == 37)
                                            {
                                                mainContext.Noah1Reload = true;
                                                DebugLog("noah1 music start, ignore MR forever");
                                            }
                                            if (mainContext.MusicStart)
                                            {
                                                sendsplit();
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
                                    mainContext.bossbattle = false;
                                    if (mainContext.MusicEnd)
                                    {
                                        if (!mainContext.DontSplitOnReload || !reloaded) sendsplit();
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

                if (mainContext.bossbattle)
                {
                    if (mainContext.MiruDe || false)//todo noah3 option
                    {
                        int Noah3HP = -1;

                        if (mapid >= 0 && mapid < StaticData.MapNames.Length)
                        {
                            int ptr = MemoryHelper.GetMemoryValue<int>(process, StaticData.EnenyPtrAddr[mainContext.veridx]);
                            List<int> bosses = new List<int>();
                            for (var i = 0; i < 50; i++)
                            {
                                ptr = ptr + StaticData.EnenyEntitySize[mainContext.veridx];

                                var emyid = MemoryHelper.GetMemoryValue<int>(process,
                                    ptr + StaticData.EnenyEnitiyIDOffset[mainContext.veridx], false);
                                if (StaticData.BossNames.ContainsKey(emyid))
                                {
                                    bosses.Add(emyid);
                                    if (emyid == 1053)
                                    {
                                        Noah3HP = MemoryHelper.GetMemoryValue<int>(process,
                                            ptr + StaticData.EnenyEnitiyHPOffset[mainContext.veridx], false);
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
                                            sendsplit();
                                            DebugLog("miru despawn, split");
                                            mainContext.bossbattle = false;

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
                                            sendsplit();
                                            DebugLog("nixie despawn, split");
                                            mainContext.bossbattle = false;
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
                                        mainContext.bossbattle = false;
                                        sendsplit();
                                        DebugLog("TM+2 in " + d.TotalMilliseconds + " ms, split");
                                    }
                                    mainContext.LastTMAddTime = DateTime.Now;
                                }
                                else if (newTM - mainContext.lastTM == 2 && f)//for 1.65-1.70
                                {
                                    mainContext.bossbattle = false;
                                    sendsplit();
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
                    int ptr = MemoryHelper.GetMemoryValue<int>(process, StaticData.EnenyPtrAddr[mainContext.veridx]);
                    //                    List<int> bosses = new List<int>();
                    //                    List<int> HPS = new List<int>();
//                    this.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() => debugContext.BossList.Clear()));
//                    ptr += StaticData.EnenyEntitySize[mainContext.veridx] * 3;
                    for (var i = 0; i < 50; i++)
                    {
                        ptr += StaticData.EnenyEntitySize[mainContext.veridx];
                        debugContext.BossList[i].BossID = MemoryHelper.GetMemoryValue<int>(process,
                            ptr + StaticData.EnenyEnitiyIDOffset[mainContext.veridx], false);
                        debugContext.BossList[i].BossHP = MemoryHelper.GetMemoryValue<int>(process,
                            ptr + StaticData.EnenyEnitiyHPOffset[mainContext.veridx], false);



                    }
                   
                }
                debugContext.BossEvent = mainContext.bossbattle;
            }
            else
            {

                mainContext.oldtitle = "";

                mainContext.GameVer = "Not Found";
                mainContext.GameMusic = "N/A";

            }
        }

        private void DebugLog(string log)
        {
            this.debugContext.DebugLog += log + "\n";
        }

        private void sendsplit()
        {
            SendMessage("split\r\n");
        }

        private void sendreset()
        {
            SendMessage("reset\r\n");
        }

        private void sendstarttimer()
        {
            SendMessage("starttimer\r\n");
        }

        private void sendigt(float time)
        {
            SendMessage($"setgametime {time}\r\n");
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
            this.DataContext = mainContext;
            DebugPanel.DataContext = debugContext;
            this.Grid.ItemsSource = debugContext.BossList;
            BossEventDebug.DataContext = debugContext;
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
