using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
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
using Irisu.EventHelper;
using Irisu.Events;
using Irisu.Memory;

namespace Irisu
{
    [Obsolete("remove or refactor needed")]
    class MainContext : INotifyPropertyChanged
    {
        public string oldtitle;
        public int veridx;

        private int _serverPort;
        private string _gameVer;
        private string _gameMusic;
        private bool _igt;

        private string _text1;
        private string _text2;
        private string _text3;
        private string _text4;
        private string _text5;
        private string _text6;
        private string _text7;
        private string _text8;
        private string _text9;
        private string _text10;
        private string _text11;
        private string _text12;
        private string _text13;
        private string _text14;
        private string _text15;
        private string _text16;
        private string _text17;
        private string _text18;
        private string _text19;
        private string _text20;

        public string Text1
        {
            get { return _text1; }
            set
            {
                if (value == _text1) return;
                _text1 = value;
                OnPropertyChanged(nameof(Text1));
            }
        }

        public string Text2
        {
            get { return _text2; }
            set
            {
                if (value == _text2) return;
                _text2 = value;
                OnPropertyChanged(nameof(Text2));
            }
        }

        public string Text3
        {
            get { return _text3; }
            set
            {
                if (value == _text3) return;
                _text3 = value;
                OnPropertyChanged(nameof(Text3));
            }
        }

        public string Text4
        {
            get { return _text4; }
            set
            {
                if (value == _text4) return;
                _text4 = value;
                OnPropertyChanged(nameof(Text4));
            }
        }

        public string Text5
        {
            get { return _text5; }
            set
            {
                if (value == _text5) return;
                _text5 = value;
                OnPropertyChanged(nameof(Text5));
            }
        }

        public string Text6
        {
            get { return _text6; }
            set
            {
                if (value == _text6) return;
                _text6 = value;
                OnPropertyChanged(nameof(Text6));
            }
        }

        public string Text7
        {
            get { return _text7; }
            set
            {
                if (value == _text7) return;
                _text7 = value;
                OnPropertyChanged(nameof(Text7));
            }
        }

        public string Text8
        {
            get { return _text8; }
            set
            {
                if (value == _text8) return;
                _text8 = value;
                OnPropertyChanged(nameof(Text8));
            }
        }

        public string Text9
        {
            get { return _text9; }
            set
            {
                if (value == _text9) return;
                _text9 = value;
                OnPropertyChanged(nameof(Text9));
            }
        }

        public string Text10
        {
            get { return _text10; }
            set
            {
                if (value == _text10) return;
                _text10 = value;
                OnPropertyChanged(nameof(Text10));
            }
        }

        public string Text11
        {
            get { return _text11; }
            set
            {
                if (value == _text11) return;
                _text11 = value;
                OnPropertyChanged(nameof(Text11));
            }
        }

        public string Text12
        {
            get { return _text12; }
            set
            {
                if (value == _text12) return;
                _text12 = value;
                OnPropertyChanged(nameof(Text12));
            }
        }

        public string Text13
        {
            get { return _text13; }
            set
            {
                if (value == _text13) return;
                _text13 = value;
                OnPropertyChanged(nameof(Text13));
            }
        }

        public string Text14
        {
            get { return _text14; }
            set
            {
                if (value == _text14) return;
                _text14 = value;
                OnPropertyChanged(nameof(Text14));
            }
        }

        public string Text15
        {
            get { return _text15; }
            set
            {
                if (value == _text15) return;
                _text15 = value;
                OnPropertyChanged(nameof(Text15));
            }
        }

        public string Text16
        {
            get { return _text16; }
            set
            {
                if (value == _text16) return;
                _text16 = value;
                OnPropertyChanged(nameof(Text16));
            }
        }

        public string Text17
        {
            get { return _text17; }
            set
            {
                if (value == _text17) return;
                _text17 = value;
                OnPropertyChanged(nameof(Text17));
            }
        }

        public string Text18
        {
            get { return _text18; }
            set
            {
                if (value == _text18) return;
                _text18 = value;
                OnPropertyChanged(nameof(Text18));
            }
        }

        public string Text19
        {
            get { return _text19; }
            set
            {
                if (value == _text19) return;
                _text19 = value;
                OnPropertyChanged(nameof(Text19));
            }
        }

        public string Text20
        {
            get { return _text20; }
            set
            {
                if (value == _text20) return;
                _text20 = value;
                OnPropertyChanged(nameof(Text20));
            }
        }

        public int ServerPort
        {
            get { return _serverPort; }
            set
            {
                if (value == _serverPort) return;
                _serverPort = value;
                OnPropertyChanged(nameof(ServerPort));
            }
        }

        public string GameVer
        {
            get { return _gameVer; }
            set
            {
                if (value == _gameVer) return;
                _gameVer = value;
                OnPropertyChanged(nameof(GameVer));
            }
        }

        public string GameMusic
        {
            get { return _gameMusic; }
            set
            {
                if (value == _gameMusic) return;
                _gameMusic = value;
                OnPropertyChanged(nameof(GameMusic));
            }
        }

        public bool Igt
        {
            get { return _igt; }
            set
            {
                if (value == _igt) return;
                _igt = value;
                OnPropertyChanged(nameof(Igt));
            }
        }

        public MainContext()
        {
            this.ServerPort = 16834;
            this.Igt = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

    
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Thread memoryThread;
        MainContext mainContext=new MainContext();
        private readonly Regex titleReg = new Regex(@"ver.*?(\d+\.?\d+.*)$");
        private static Process rabiProcess;
        private static MemoryHelper rabiMemoryHelper;
        private MemorySnapshot? currentsnapshot;
        private MemorySnapshot? oldsnapshot;
       
        private IObservable<EventBase> obs;
        private MemorySnapshot? ReadMemory()
        {
            if (rabiProcess == null || rabiProcess.HasExited)
            {
                if (rabiMemoryHelper != null)
                {
                    rabiMemoryHelper.Dispose();
                    rabiMemoryHelper = null;
                }
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
                                mainContext.GameVer = rabiver + " Running";
                                mainContext.oldtitle = process.MainWindowTitle;

                            }
                        }
                        else
                        {
                            mainContext.veridx = -1;
                            mainContext.GameVer = "Running (Unknown version)";
                            
                        }
                       
                    }
                    if (mainContext.veridx < 0)
                    {
                        rabiProcess = null;
                        mainContext.oldtitle = "";
                        mainContext.GameVer = "Not Found";
                        mainContext.GameMusic = "N/A";

                    }
                    else
                    {
                        rabiProcess = process;
                        rabiMemoryHelper = new MemoryHelper(rabiProcess);
                    }

                }

              

            }
            if (rabiProcess != null && !rabiProcess.HasExited)
            {
                return  new MemorySnapshot(rabiMemoryHelper, mainContext.veridx);
               
            }
            return null;

        }
        public MainWindow()
        {
            InitializeComponent();
            Console.WriteLine(2);
            obs = Observable.Create<EventBase>(o =>
            {
                return ThreadPoolScheduler.Instance.ScheduleAsync(async (ctrl, ct) =>
                {
                    while (!ct.IsCancellationRequested) { 
                    currentsnapshot = ReadMemory();
                    if (currentsnapshot == null)
                    {
                        oldsnapshot = null;
                    }
                    else if (oldsnapshot == null)
                    {
                        oldsnapshot = currentsnapshot;
                    }
                    else
                    {

                        var result = EventFirer.ComparerSnapShotAndFireEvent(oldsnapshot, currentsnapshot);
                        foreach (var eventBase in result)
                        {
                            o.OnNext(eventBase);
                        }
                    }
                   
                    await ctrl.Sleep(TimeSpan.FromSeconds(1.0/60), ct);
                    }
                });



            });
            var obs2 = Observable.Interval(TimeSpan.FromSeconds(1));
            obs.Where(p=>p.EventType==EventType.Test).Select(p=>(TestEvent)p)
                .ObserveOnDispatcher() //UI thread
                .Subscribe (b =>
                {
                    this.Box.AppendText(b.Msg);
                });
           
        }

      
    }
}
