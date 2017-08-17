using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
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
using Irisu.EventHelper;
using Irisu.Events;
using Irisu.Memory;
using Irisu.Models;
using Irisu.RabiHelper;

namespace Irisu
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        private static TcpClient tcpclient;
        private static NetworkStream networkStream;
        private static Option option=new Option();

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = option;
            var gamereader = new RabiReader();
            var obs = Observable.FromEventPattern<RabiEventHandler, EventBase>(h => gamereader.GameEvent += h,
                h => gamereader.GameEvent -= h);
            obs.Where(p => p.EventArgs.EventType == EventType.BossStart && option.Bossstart)
                .Select(p => (BossStartEvent) p.EventArgs)
                .Where(p => option.EnabledBosses.Contains(p.Boss)).Subscribe(b =>
                {
                    DebugLog("Split: Bossstart");
                    SpeedrunSendSplit();
                });
            obs.Where(p => p.EventArgs.EventType == EventType.BossEnd && option.Bossend)
                .Select(p => (BossEndEvent) p.EventArgs)
                .Where(p => option.EnabledBosses.Contains(p.Boss)).Subscribe(b =>
                {
                    DebugLog("Split: Bossend");
                    SpeedrunSendSplit();
                });
            obs.Where(p => p.EventArgs.EventType == EventType.Item ||
                           p.EventArgs.EventType == EventType.ItemPercent) //.Select(p=>(TestEvent)p.EventArgs)
                .ObserveOnDispatcher() //UI thread
                .Subscribe(b =>
                {
                    DebugLog(b.EventArgs.ToString());
                });


        }

        private void SpeedrunSendSplit()
        {
            SendMessage("split\r\n");
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
        private void DebugLog(string log)
        {
            if (this.Dispatcher.CheckAccess())
            {
                this.DebugLogTb.AppendText(log+"\n");
                this.DebugLogTb.ScrollToEnd();

            }
            else
            {
                this.Dispatcher.Invoke(() => DebugLog(log));
            }
            
        }
        private void TextBlock_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://github.com/copyliu/rabiribi_splitter");
        }


        private void BtnConnect_Click(object sender, RoutedEventArgs e)
        {
            if (tcpclient != null && tcpclient.Connected) return;
            try
            {
                tcpclient = new TcpClient("127.0.0.1", Convert.ToInt32(this.Port.Text.Trim()));
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



    }
}
