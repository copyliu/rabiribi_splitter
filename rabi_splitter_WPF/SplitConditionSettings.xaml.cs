using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace rabi_splitter_WPF
{
    /// <summary>
    /// Interaction logic for SplitConditionSettings.xaml
    /// </summary>
    public partial class SplitConditionSettings : UserControl
    {
        public String Label
        {
            get { return (String)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }
        
        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string),
              typeof(SplitConditionSettings), new PropertyMetadata(""));
        
        public object SplitConditionObject
        {
            get { return (object)GetValue(SplitConditionObjectProperty); }
            set { SetValue(SplitConditionObjectProperty, value); }

        }
        
        public static readonly DependencyProperty SplitConditionObjectProperty =
            DependencyProperty.Register("SplitConditionObject", typeof(object),
              typeof(SplitConditionSettings), new PropertyMetadata(null));
        
        public SplitConditionSettings()
        {
            InitializeComponent();
            this.MainPanel.DataContext = this;
        }
    }
}
