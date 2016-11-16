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
        public Form1()
        {
            InitializeComponent();
          memoryThread=new Thread(() =>
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

                    }
                    lastmoney = newmoney;
                }

                #endregion

                #region Music

                int musicaddr = StaticData.MusicAddr[veridx];
                int musicid = MemoryHelper.GetMemoryValue<int>(process, musicaddr);
                if (musicid < StaticData.MusicNames.Length)
                {
                    if (lastmusicid != musicid)
                    {
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
                                }

                                this.Invoke(new Action(() => cbBoss.Checked = bossbattle));
                                lastmusicid = musicid;
                                return;
                            }
                        }
                        if (!bossbattle)
                        {
                            if (bossmusicflag) //boss music start!
                            {
                                bossbattle = true;
                                if (cbBossStart.Checked)
                                {
                                    sendsplit();
                                }
                            }
                        }
                        if (bossbattle)
                        {
                            if (!bossmusicflag) //boss music end!
                            {
                                bossbattle = false;
                                if (cbBossEnd.Checked)
                                {
                                    sendsplit();
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

                #endregion SpecialBOSS

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
                    networkStream.Write(b,0,b.Length);
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
