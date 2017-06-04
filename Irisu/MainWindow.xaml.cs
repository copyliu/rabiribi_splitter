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
using Irisu.RabiHelper;

namespace Irisu
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();

            var gamereader = new RabiReader();
            var obs = Observable.FromEventPattern<RabiEventHandler, EventBase>(h => gamereader.GameEvent += h, h => gamereader.GameEvent -= h);
            
            obs//.Where(p=>p.EventArgs.EventType==EventType.Test).Select(p=>(TestEvent)p.EventArgs)
                .ObserveOnDispatcher() //UI thread
                .Subscribe (b =>
                {
                    this.Box.AppendText(b.EventArgs.ToString());
                    this.Box.ScrollToEnd();
                });
           
        }

      
    }
}
