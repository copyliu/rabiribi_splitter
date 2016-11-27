using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Timers;
using System.Windows.Forms;

namespace rabiribi_splitter
{
    public partial class Form1 : Form
    {
        private static TcpClient tcpclient;
        private static NetworkStream networkStream;
        private static System.Timers.Timer timer;
        private bool bossbattle = false;
        private int lastmusicid;
        private Regex titleReg = new Regex(@"ver.*?(\d+\.?\d+.*)$");
        private Thread memoryThread;
        private int lastmoney;
        private bool rabiribiready;
        private string rabiribititle;
        private string rabiver;
        private int veridx;
        private List<int> lastbosslist = new List<int>();
        private int lastnoah3hp = -1;
        private int lastmapid;
        private int lastTM;
        private DateTime LastTMAddTime=DateTime.MinValue;
        void DebugLog(string log)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    this.debugLog.AppendText(log + "\r\n");
                }));
            }
            else
            {
                this.debugLog.AppendText(log + "\r\n");
            }
        }
        public Form1()
        {
            InitializeComponent();
            LinkLabel.Link link = new LinkLabel.Link();
            link.LinkData = "https://github.com/copyliu/rabiribi_splitter";
            linkLabel1.Links.Add(link);
            linkLabel1.LinkClicked += LinkLabel1_LinkClicked;
            memoryThread = new Thread(() =>
            {
                while (true)
                {
                    readmemory();
                    Thread.Sleep(10);
                }

            });
            memoryThread.IsBackground = true;
            memoryThread.Start();
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Send the URL to the operating system.
            Process.Start(e.Link.LinkData as string);
        }


        private void readmemory()
        {

            var processlist = Process.GetProcessesByName("rabiribi");
            if (processlist.Length > 0)
            {

                Process process = processlist[0];
                if (process.MainWindowTitle != rabiribititle)
                {
                    var result = titleReg.Match(process.MainWindowTitle);
                    if (result.Success)
                    {

                        rabiver = result.Groups[1].Value;
                        veridx = Array.IndexOf(StaticData.VerNames, rabiver);
                        if (veridx < 0)
                        {
                            this.Invoke(new Action(() =>
                            {
                                rbStatus.Text = rabiver + " Running (not support)";
                                this.musicLabel.Text = "N/A";
                            }));


                            return;
                        }



                    }
                    else
                    {
                        veridx = -1;
                        this.Invoke(new Action(() =>
                        {
                            rbStatus.Text = rabiver + " Running (not support)";
                            this.musicLabel.Text = "N/A";
                        }));
                        return;
                    }
                    this.Invoke(new Action(() => rbStatus.Text = rabiver + " Running"));
                    rabiribititle = process.MainWindowTitle;
                }


                if (veridx < 0) return;


               

                #region CheckMoney

                if (cbComputer.Checked)
                {
                    var newmoney = MemoryHelper.GetMemoryValue<int>(process, StaticData.MoneyAddress[veridx]);
                    if (newmoney - lastmoney == 17500)
                    {
                        sendsplit();
                        DebugLog("get 17500 en, split");
                    }
                    lastmoney = newmoney;
                }

                #endregion

                int mapid = MemoryHelper.GetMemoryValue<int>(process, StaticData.MapAddress[veridx]);
                if (lastmapid != mapid)
                {
                    DebugLog("newmap: "+mapid+":"+StaticData.MapNames[mapid]);
                    lastmapid = mapid;
                }


                #region checkTM

              

                #endregion

                #region Music

                int musicaddr = StaticData.MusicAddr[veridx];
                int musicid = MemoryHelper.GetMemoryValue<int>(process, musicaddr);
                if (musicid > 0 && musicid < StaticData.MusicNames.Length)
                {
                    if (lastmusicid != musicid)
                    {
                        DebugLog("new music:"+musicid+":"+StaticData.MusicNames[musicid]);
                        this.Invoke(new Action(() => this.musicLabel.Text = StaticData.MusicNames[musicid]));

                        var bossmusicflag = StaticData.BossMusics.Contains(musicid);
                        if (bossmusicflag)
                        {
                            if (bossbattle)
                            {
                                //直接换boss曲
                                if (cbBossStart.Checked || cbBossEnd.Checked)
                                {
                                    sendsplit();
                                    DebugLog("new boss music, split");
                                }

                                this.Invoke(new Action(() => cbBoss.Checked = bossbattle));
                                lastmusicid = musicid;
                                return;
                            }
                        }
                        if (!bossbattle)
                        {

                            if (cbASG.Checked && musicid == 54)
                            {
                                bossbattle = false;
                                DebugLog("Alius music, ignore once");
                                this.Invoke(new Action(() => cbASG.Checked = false));
                            }
                            else
                            {
                                if (bossmusicflag)
                                {
                                    if (mapid == 5 && musicid == 44 && cbSideCh.Checked )
                                    {
                                        bossbattle = false;
                                        DebugLog("sidechapter, ignore");

                                    }
                                    else
                                    {
                                        bossbattle = true;
                                        lastbosslist = new List<int>();
                                        lastnoah3hp = -1;
                                        if (cbBossStart.Checked)
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
                                bossbattle = false;
                                if (cbBossEnd.Checked)
                                {
                                    sendsplit();
                                    DebugLog("music end, split");
                                }
                            }
                        }
                        lastmusicid = musicid;

                    }
                }
                else
                {
                    this.Invoke(new Action(() => this.musicLabel.Text = "N/A"));
                }

                #endregion Music

                #region SpecialBOSS

                if (bossbattle)
                {
                    if (cbBoss1.Checked || cbBoss3.Checked)
                    {
                        int Noah3HP = -1;
                        
                        if (mapid >= 0 && mapid < StaticData.MapNames.Length)
                        {
                            int ptr = MemoryHelper.GetMemoryValue<int>(process, StaticData.EnenyPtrAddr[veridx]);
                            List<int> bosses = new List<int>();
                            for (var i = 0; i < 50; i++)
                            {
                                ptr = ptr + StaticData.EnenyEntitySize[veridx];

                                var emyid = MemoryHelper.GetMemoryValue<int>(process,
                                    ptr + StaticData.EnenyEnitiyIDOffset[veridx], false);
                                if (StaticData.BossNames.ContainsKey(emyid))
                                {
                                    bosses.Add(emyid);
                                    if (emyid == 1053)
                                    {
                                        Noah3HP = MemoryHelper.GetMemoryValue<int>(process,
                                            ptr + StaticData.EnenyEnitiyHPOffset[veridx], false);
                                    }

                                }

                            }
                            if (cbBoss1.Checked)
                            {
                                foreach (var boss in lastbosslist)
                                {

                                    if (boss == 1043)
                                    {
                                        if (!bosses.Contains(boss)) //despawn
                                        {
                                            sendsplit();
                                            DebugLog("miru despawn, split");
                                            bossbattle = false;

                                        }
                                    }
                                }
                            }

                            if (cbBoss3.Checked)
                            {
                                if (bosses.Contains(1053) && Noah3HP < lastnoah3hp && Noah3HP == 1)
                                {
                                    sendsplit();
                                    DebugLog("noah3 hp 1, split");
                                    bossbattle = false;
                                }
                            }
                            if (cbTM.Checked && musicid==8)
                            {
                                bool f = true;
                                foreach (var boss in lastbosslist)
                                {

                                    if (boss == 1024)
                                    {
                                        if (!bosses.Contains(boss)) //despawn
                                        {
                                            sendsplit();
                                            DebugLog("nixie despawn, split");
                                            bossbattle = false;
                                            f = false;
                                            break;
                                        }
                                    }
                                }
                                
                                int newTM = MemoryHelper.GetMemoryValue<int>(process, StaticData.TownMemberAddr[veridx]);
                                if (newTM - lastTM == 1 && f) //for after 1.71 , 1.71 isn't TM+2 at once when skip Nixie, it's TM+1 twice

                                {
                                    if (DateTime.Now- LastTMAddTime  < TimeSpan.FromSeconds(1))
                                    {
                                        var d = DateTime.Now - LastTMAddTime;
                                        bossbattle = false;
                                        sendsplit();
                                        DebugLog("TM+2 in "+d.TotalMilliseconds+" ms, split");
                                    }
                                    LastTMAddTime = DateTime.Now;
                                }
                                else if (newTM - lastTM == 2 && f)//for 1.65-1.70
                                {
                                    bossbattle = false;
                                    sendsplit();
                                    DebugLog("TM+2, split");
                                }
                                lastTM = newTM;
                            }
                            lastbosslist = bosses;
                            lastnoah3hp = Noah3HP;


                        }


                    }
                }


                #endregion SpecialBOSS

                if (debugArea.Checked)
                {
                    int ptr = MemoryHelper.GetMemoryValue<int>(process, StaticData.EnenyPtrAddr[veridx]);
                    List<int> bosses = new List<int>();
                    List<int> HPS = new List<int>();
                    ptr += StaticData.EnenyEntitySize[veridx]*3;
                    for (var i = 0; i < 5; i++)
                    {
                        ptr += StaticData.EnenyEntitySize[veridx];


                        bosses.Add(MemoryHelper.GetMemoryValue<int>(process,
                            ptr + StaticData.EnenyEnitiyIDOffset[veridx], false));
                        HPS.Add(MemoryHelper.GetMemoryValue<int>(process,
                            ptr + StaticData.EnenyEnitiyHPOffset[veridx], false));


                        this.Invoke(new Action(() =>
                        {
                            t1.Text = string.Join("\n", bosses);
                            t2.Text = string.Join("\n", HPS);
                        }));

                    }
                }
                this.Invoke(new Action(() => cbBoss.Checked = bossbattle));
            }
            else
            {

                rabiribititle = "";


                this.Invoke(new Action(() =>
                {
                    rbStatus.Text = "Not Found";
                    this.musicLabel.Text = "N/A";
                }));

            }
        }

        void sendsplit()
        {
            if (tcpclient != null && tcpclient.Connected)
            {
                try
                {
                    var b = Encoding.UTF8.GetBytes("split\r\n");
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
            connectBtn.Enabled = true;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (tcpclient != null && tcpclient.Connected) return;
            try
            {
                tcpclient = new TcpClient("127.0.0.1", Convert.ToInt32(portNum.Value));
                networkStream = tcpclient.GetStream();
                connectBtn.Enabled = false;
            }
            catch (Exception)
            {
                tcpclient = null;
                networkStream = null;
                MessageBox.Show(this, "Connect Failed");

            }
        }
    }
}
