using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Irisu.Events;
using Irisu.Memory;
using Irisu.Models;

namespace Irisu
{
    /// <summary>
    /// AddSplit.xaml 的交互逻辑
    /// </summary>
    public partial class AddSplit : Window
    {
        public SplitOption SplitOption;
        public AddSplit()
        {
            InitializeComponent();
            this.BossSelect.ItemsSource= Enum.GetValues(typeof(Boss));
            List<object> items=new List<object>();
            items.Add("--------Item--------");
            items.AddRange(Enum.GetValues(typeof(Item)).Cast<object>());
            items.Add("--------Badge--------");
            items.AddRange(Enum.GetValues(typeof(Badge)).Cast<object>());
            this.ItemSelect.ItemsSource = items;
            this.MusicSelect.ItemsSource = Enum.GetValues(typeof(Music));
            this.MapSelect.ItemsSource = Enum.GetValues(typeof(Map));
            var b = Map.SubterraneanArea;
            var c=b.ToString();
        }

        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            var rb = sender as RadioButton;
            if (rb == null) return;
            var content = rb.Content.ToString();
            foreach (var panelChild in this.panel.Children)
            {
               ((UIElement) panelChild).Visibility=Visibility.Collapsed;
                
            }
            switch (content)
            {
                case "BossStart":
                    this.BossPanel.Visibility=Visibility.Visible;
                    break;
                case "BossEnd":
                    this.BossPanel.Visibility = Visibility.Visible;
                    break;
                case "Music":
                    this.MusicPanel.Visibility = Visibility.Visible;
                    break;
                case "Item":
                    this.ItemPanel.Visibility = Visibility.Visible;
                    break;
                case "ItemPercent":
                    this.PercentPanel.Visibility = Visibility.Visible;
                    break;
                case "Map":
                    this.MapPanel.Visibility = Visibility.Visible;
                    break;
                case "Pos":
                    this.PosPanel.Visibility = Visibility.Visible;
                    break;
                case "Chapter":
                    this.ChapterPanel.Visibility = Visibility.Visible;
                    break;
                case "TownMember":
                    this.TMPanel.Visibility = Visibility.Visible;
                    break;

            }
        }

        private void Ok_Clicked(object sender, RoutedEventArgs e)
        {
            
            SplitOption opt=new SplitOption();
            foreach (var r in this.TypeSelect.Children)
            {
                var rb = r as RadioButton;
                if (rb==null)continue;
                if (rb.IsChecked == true)
                {
                    var content = rb.Content;
                    switch (content)
                    {
                        case "BossStart":
                            opt.EventType = EventType.BossStart;
                            if (! (BossSelect.SelectedValue is Boss))
                            {
                                MessageBox.Show(this, "Boss not selected!");
                                return;
                            }
                            opt.Value = BossSelect.SelectedValue;
                            break;
                        case "BossEnd":
                            opt.EventType = EventType.BossEnd;
                            if (!(BossSelect.SelectedValue is Boss))
                            {
                                MessageBox.Show(this, "Boss not selected!");
                                return;
                            }
                            opt.Value = BossSelect.SelectedValue;
                            break;
                        case "Music":
                            opt.EventType = EventType.Music;
                            if (!(MusicSelect.SelectedValue is Music))
                            {
                                MessageBox.Show(this, "Music not selected!");
                                return;
                            }
                            opt.Value = MusicSelect.SelectedValue;
                            break;
                        case "Item":
                            if (!((ItemSelect.SelectedValue is Item) || (ItemSelect.SelectedValue is Badge)))
                            {
                                MessageBox.Show(this, "Item not selected!");
                                return;
                            }
                            var value= new int[2];
                            value[0] = ItemSelect.SelectedValue is Item ? 0 : 1;
                            value[1] =(int) ItemSelect.SelectedValue;
                            opt.Value = value;
                            opt.EventType=EventType.Item;
                            break;
                        case "ItemPercent":
                            opt.EventType = EventType.ItemPercent;
                            float v = 0;
                            if (!float.TryParse(PercentTb.Text.Trim(),out v) || v <= 0)
                            {
                                MessageBox.Show(this, "Item Percent not vaild!");
                                return;
                            }
                           
                            opt.Value = v;
                            break;
                        case "Map":
                            opt.EventType = EventType.Map;
                            if (!(MapSelect.SelectedValue is Map))
                            {
                                MessageBox.Show(this, "Map not selected!");
                                return;
                            }
                            opt.Value = MapSelect.SelectedValue;
                            break;
                        case "Pos":
                            opt.EventType = EventType.Pos;
                            int[] arr=new int[2];
                            if (!int.TryParse(this.PosX.Text.Trim(),out arr[0]) ||
                                !int.TryParse(this.PosY.Text.Trim(), out arr[1])
                                || arr[0]<=0 || arr[1]<=0)
                            {
                                MessageBox.Show(this, "Pos not vaild!");
                                return;
                            }
                            opt.Value = arr;
                            break;
                        case "Chapter":
                            opt.EventType = EventType.Chapter;
                            break;
                        case "TownMember":
                            opt.EventType = EventType.TownMember;
                            break;

                    }
                    this.SplitOption = opt;
                    break;
                }
               
            }
            this.DialogResult = true;
            this.Close();

        }

        private void Cancel_Clicked(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
