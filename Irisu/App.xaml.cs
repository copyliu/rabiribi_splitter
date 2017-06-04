using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Irisu
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public static void Logging(string text)
        {
            var window = Application.Current.MainWindow as MainWindow;
            window?.Dispatcher.Invoke(new Action(() =>
            {
                window.Box.AppendText(text);
            }));
        }
    }
}
