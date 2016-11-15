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
using System.Windows.Forms;

namespace rabiribi_splitter
{
    public partial class Form1 : Form
    {
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(int hProcess,
            int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);

        const int PROCESS_WM_READ = 0x0010;

        private static TcpClient tcpclient;
        private static NetworkStream networkStream;
        private static System.Threading.Timer timer;
//        private int MapAddress = 0xA3353C;
//        private int PtrAddr = 0x00940EE0;
//        private int EnitiyOffset = 0x4e4;
//        private int EntitySize = 0x6F4;
//        private int MaxEntityEntry = 50;
        private bool bossbattle = false;
        private int bossmusicid;
        private Regex titleReg = new Regex(@"ver.*?(\d+\.?\d+.*)$");
        public Form1()
        {
            InitializeComponent();
            timer = new System.Threading.Timer(readmemory, null, 0, 10);


        }

        private void readmemory(object state)
        {
            string rabiver="";
            var processlist = Process.GetProcessesByName("rabiribi");
            if (processlist.Length > 0)
            {

                Process process = processlist[0];
                var result = titleReg.Match(process.MainWindowTitle);
                if (result.Success)
                {
                    rabiver = result.Groups[1].Value;
                    if (!StaticData.VerNames.Contains(rabiver))
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
                    this.Invoke(new Action(() =>
                    {
                        rbStatus.Text = rabiver + " Running (not support)";
                        this.musicLabel.Text = "N/A";
                    }));
                    return;
                }
                this.Invoke(new Action(() => rbStatus.Text = rabiver + " Running"));
                int addr = StaticData.MusicAddr[rabiver];
                byte[] buffer = new byte[4] {0, 0, 0, 0};
                int bytesRead = 0;
                IntPtr processHandle = OpenProcess(PROCESS_WM_READ, false, process.Id);
                ReadProcessMemory((int) processHandle, process.MainModule.BaseAddress.ToInt32() + addr, buffer,
                    4, ref bytesRead);
                if (buffer[0] < StaticData.MusicNames.Length)
                {
                   
                    int musicid = BitConverter.ToInt32(buffer,0);
                    this.Invoke(new Action(() => this.musicLabel.Text = StaticData.MusicNames[musicid]));
                    
                    var flag = StaticData.BossMusics.Contains(musicid);
                    if (flag)
                    {
                        if (bossmusicid > 0 && bossmusicid != musicid)
                        {
                            //直接换boss曲
                            if (cbBossStart.Checked || cbBossEnd.Checked)
                            {
                                sendsplit();
                            }
                            bossbattle = true;
                            this.Invoke(new Action(() => cbBoss.Checked = bossbattle));
                            bossmusicid = musicid;
                            return;
                        }
                    }
                    if (flag != bossbattle)
                    {
                        if (flag)
                        {
                            if (cbBossStart.Checked)
                            {
                                sendsplit();
                               
                            }
                            bossmusicid = musicid;
                        }
                        else
                        {
                            if (cbBossEnd.Checked)
                            {
                                sendsplit();
                            }
                        }
                    }
                    bossbattle = flag;
                    this.Invoke(new Action(() => cbBoss.Checked = bossbattle));
                }
                else
                {
                    this.Invoke(new Action(() => this.musicLabel.Text = "N/A"));
                }


            }
            else
            {
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
