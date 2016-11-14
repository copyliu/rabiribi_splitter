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
        private int MapAddress = 0xA3353C;
        private int PtrAddr = 0x00940EE0;
        private int EnitiyOffset = 0x4e4;
        private int EntitySize = 0x6F4;
        private int MaxEntityEntry = 50;
        private bool bossbattle = false;

        private static Dictionary<int, string> BossNames = new Dictionary<int, string>()
        {
            {1009, "Cocoa"},
            {1011, "Rumi"},
            {1012, "Ashuri"},
            {1013, "Rita"},
            {1014, "Ribbon"},
            {1015, "Cocoa"},
            {1018, "Cicini"},
            {1020, "Saya"},
            {1021, "Syaro"},
            {1022, "Pandora"},
            {1023, "Nieve"},
            {1024, "Nixie"},
            {1025, "Aruraune"},
            {1030, "Seana"},
            {1031, "Lilith"},
            {1032, "Vanilla"},
            {1033, "Chocolate"},
            {1035, "Illusion Alius"},
            {1036, "Pink Kotri"},
            {1037, "Noah 1"},
            {1038, "Irisu"},
            {1039, "Miriam"},
            {1043, "Miru"},
            {1053, "Noah 3"},
            {1054, "Keke Bunny"},


        };

        static string[] MapNames = new string[]
        {
            "Southern Woodland",
            "Western Coast",
            "Island Core",
            "Northern Tundra",
            "Eastern Highlands",
            "Rabi Rabi Town",
            "Plurkwood",
            "Subterranean Area",
            "Warp Destination",
            "System Interior",
        };

        static int[][] MapBoss = new int[][]
        {
            new[] {1011, 1009, 1025, 1014},
            new[] {1036, 1038, 1031, 1022, 1012},
            new[] {1032, 1036, 1030, 1033},
            new[] {1024, 1023, 1013, 1030},
            new[] {1012, 1020,},
            new int[0],
            new[] {1054},
            new[] {1036, 1039},
            new[] {1037, 1053, 1035, 1043},
            new[] {1021},

        };

        public Form1()
        {
            InitializeComponent();
            timer = new System.Threading.Timer(readmemory, null, 0, 10);


        }

        private void readmemory(object state)
        {
            var processlist = Process.GetProcessesByName("rabiribi");
            if (processlist.Length > 0)
            {
                rbStatus.Text = "Running";
                Process process = processlist[0];
                byte[] buffer = new byte[4] {0, 0, 0, 0};
                int bytesRead = 0;
                IntPtr processHandle = OpenProcess(PROCESS_WM_READ, false, process.Id);
                ReadProcessMemory((int) processHandle, process.MainModule.BaseAddress.ToInt32() + MapAddress, buffer,
                    1, ref bytesRead);
                int mapid;
                if (buffer[0] < MapNames.Length)
                {
                    this.mapLabel.Text = MapNames[buffer[0]];
                    mapid = buffer[0];
                    ReadProcessMemory((int) processHandle, process.MainModule.BaseAddress.ToInt32() + PtrAddr, buffer, 4,
                        ref bytesRead);
                    var ptr = BitConverter.ToInt32(buffer, 0) + EnitiyOffset;
                    List<int> bosses = new List<int>();
                    for (var i = 0; i < 50; i++)
                    {
                        ptr += 0x6f4;
                        ReadProcessMemory((int) processHandle, ptr, buffer, buffer.Length, ref bytesRead);
                        var emyid = BitConverter.ToInt32(buffer, 0);
                        if (BossNames.ContainsKey(emyid))
                        {
                            bosses.Add(emyid);
                        }

                    }
                    //Now checking map
                    bool flag = false;
                    this.bossLabel.Text = "";
                    foreach (var i in MapBoss[mapid])
                    {
                        if (bosses.Contains(i))
                        {
                            flag = true;
                            this.bossLabel.Text += BossNames[i] + " ";
                        }
                    }
                    if (flag != bossbattle)
                    {
                        sendsplit();
                    }
                    bossbattle = flag;
                }
                else
                {
                    this.mapLabel.Text = "N/A";
                    this.bossLabel.Text = "";
                }


            }
            else
            {
                rbStatus.Text = "Not Found";
                this.mapLabel.Text = "N/A";
                this.bossLabel.Text = "";
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
