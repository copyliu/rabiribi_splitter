using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reactive.Concurrency;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
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
using Microsoft.Win32;
using Newtonsoft.Json;
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
        ObservableCollection<SplitOption> splitoptions=new ObservableCollection<SplitOption>();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = option;
            this.SplitList.ItemsSource = splitoptions;
            var gamereader = new RabiReader();
            var obs = Observable.FromEventPattern<RabiEventHandler, EventBase>(h => gamereader.GameEvent += h,
                h => gamereader.GameEvent -= h);


            obs.Where(p => splitoptions.Any(o => o.Disabled == false))
                .Where(p => p.EventArgs == splitoptions.First(o => o.Disabled == false))
                .Subscribe(b =>
                {
                    DebugLog("Split:  " + splitoptions.First(o => o.Disabled == false));
                    SpeedrunSendSplit();
                    splitoptions.First(o => o.Disabled == false).Disabled = true;

                });
            obs.Where(p => option.Autostart && p.EventArgs.EventType == EventType.Special &&
                           ((SpecialEvent) p.EventArgs).Event == SpecialEvents.GameStart).Subscribe(
                b =>
                {
                    DebugLog("GameStart!");
                    SpeedrunSendStart();
                });
            obs.Where(p => option.Autoreset && p.EventArgs.EventType == EventType.Music &&
                           (((MusicEvent)p.EventArgs).NewMusicId == (int)Music.THEME_OF_RABI_RIBI ||
                            ((MusicEvent)p.EventArgs).NewMusicId == (int)Music.THEME_OF_RABI_RIBI_8BIT ||
                            ((MusicEvent)p.EventArgs).NewMusicId == (int)Music.MAIN_MENU)
                           ).Subscribe(
                b =>
                {
                    DebugLog("GameReset!");
                    foreach (var splitOption in splitoptions)
                    {
                        splitOption.Disabled = false;
                    }
                    SpeedrunSendReset();
                });
            obs.Where(p => option.SendIgt && p.EventArgs.EventType == EventType.InGameTimer).Subscribe(
                b =>
                {
                    SpeedRunSendIgt(((TimerEvent) b.EventArgs).playtime_T);

                });
            //
            //
            //            obs.Where(p => p.EventArgs.EventType == EventType.BossStart && option.Bossstart)
            //                .Select(p => (BossStartEvent) p.EventArgs)
            //                .Where(p => option.EnabledBosses.Contains(p.Boss)).Subscribe(b =>
            //                {
            //                    DebugLog("Split: Bossstart");
            //                    SpeedrunSendSplit();
            //                });
            //            obs.Where(p => p.EventArgs.EventType == EventType.BossEnd && option.Bossend)
            //                .Select(p => (BossEndEvent) p.EventArgs)
            //                .Where(p => option.EnabledBosses.Contains(p.Boss)).Subscribe(b =>
            //                {
            //                    DebugLog("Split: Bossend");
            //                    SpeedrunSendSplit();
            //                });
            //            obs.Where(p => p.EventArgs.EventType == EventType.Item ||
            //                           p.EventArgs.EventType == EventType.ItemPercent) //.Select(p=>(TestEvent)p.EventArgs)
            //                .ObserveOnDispatcher() //UI thread
            //                .Subscribe(b =>
            //                {
            //                    DebugLog(b.EventArgs.ToString());
            //                });


        }

        private void SpeedRunSendIgt(float time)
        {
            SendMessage($"setgametime {time}\r\n");
        }
        void SpeedrunSendStart()
        {
            SendMessage("starttimer\r\n");
        }

        void SpeedrunSendReset()
        {
            SendMessage("reset\r\n");
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


        private void Add_OnClick(object sender, RoutedEventArgs e)
        {
            AddSplit w=new AddSplit();
            w.Owner = this;
            w.Closed+=WOnClosed;
            w.ShowDialog();
        }

        private void WOnClosed(object sender, EventArgs eventArgs)
        {
            var w = sender as AddSplit;
            if (w == null) return;
            if (w.DialogResult != true)
            {
                return;
            }
            if (w.SplitOption != null)
            {
                splitoptions.Add(w.SplitOption);
                
            }
            
        }

        private void Remove_OnClick(object sender, RoutedEventArgs e)
        {
            if (SplitList.SelectedItem != null && splitoptions.Contains(((SplitOption) SplitList.SelectedItem)))
            {
                splitoptions.Remove((SplitOption)SplitList.SelectedItem);
            }
        }

        private void Save_Clicked(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.DefaultExt = ".json"; // Default file extension
            dlg.Filter = "Splitter Settings (.json)|*.json"; // Filter files by extension

            var result = dlg.ShowDialog();

            if (result == true)
            {
                var filename = dlg.FileName;
                foreach (var s in splitoptions)
                {
                    s.Disabled = false;
                }
               
                System.IO.File.WriteAllText(filename, JsonConvert.SerializeObject(splitoptions.ToList(),new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.All
                }));

                MessageBox.Show(this, "perset saved!");
            }
        }

        private void Load_Clicked(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg=new OpenFileDialog();
            dlg.DefaultExt = ".json";
            dlg.Filter = "Splitter Settings (.json)|*.json"; // Filter files by extension
            var result = dlg.ShowDialog();
            if (result == true)
            {
                var filename = dlg.FileName;
                try
                {
                    var newoption=JsonConvert.DeserializeObject < List<SplitOption>>(System.IO.File.ReadAllText(filename), new JsonSerializerSettings()
                    {
                        TypeNameHandling = TypeNameHandling.All
                    });
                    splitoptions.Clear();
                    foreach (var item in newoption)
                    {
                        item.Disabled = false;
                        //typefix
                        switch (item.EventType)
                        {

                            case EventType.BossEnd:
                            case EventType.BossStart:
                                item.Value = (Boss) item.Value;
                                break;
                            case EventType.Music:
                                item.Value = (Music) item.Value;
                                break;
                            case EventType.Map:
                                item.Value = (Map) item.Value;
                                break;
                            case EventType.ItemPercent:
                                item.Value = (float)(double)item.Value;
                                break;
                            

                        }

                        splitoptions.Add(item);
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(this, "cannot load perset");
                    DebugLog(exception.ToString());
                }

            }
        }

        private void SaveSplit_Clicked(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
