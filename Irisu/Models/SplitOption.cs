using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Irisu.Annotations;
using Irisu.Events;
using Irisu.Memory;

namespace Irisu.Models
{
    public class SplitOption : INotifyPropertyChanged
    {
        public static bool operator ==(EventBase evt, SplitOption opt)
        {
            if (opt == null || evt == null) return false;
            if (opt.EventType != evt.EventType) return false;
            switch (opt.EventType)
            {
                    case EventType.BossStart:
                        var bossstartevt = (BossStartEvent) evt;
                        return bossstartevt.Boss.startingBosses.Contains((Boss) opt.Value);
                case EventType.BossEnd:
                    var bossendevt = (BossEndEvent) evt;
                    return bossendevt.Boss.startingBosses.Contains((Boss) opt.Value);
                    case EventType.Chapter:
                        return true;
                    case EventType.TownMember:
                        return true;
                    case EventType.Item:
                        var itemevt = (ItemEvent) evt;
                        if (opt.Value is Item)
                        {
                            return itemevt.NewItems.ContainsKey((Item) opt.Value);
                        }
                        else if (opt.Value is Badge)
                        {
                            return itemevt.NewBadges.Contains((Badge) opt.Value);
                        }
                        else
                        {
                            return false;

                        }
                        case EventType.ItemPercent:
                            var itempevt = (ItemPercentEvent) evt;
                            return itempevt.Percent > (opt.Value as float?);
                    case EventType.Map:
                        var mapevt = (MapEvent) evt;
                        return mapevt.NewMapId == (int)(Map)opt.Value;
                    case EventType.Music:
                        var musicevt = (MusicEvent) evt;
                        return musicevt.NewMusicId == (int) (Music) opt.Value;
                    case EventType.Pos:
                        var posevt = (PosEvent) evt;
                        if (opt.Value is int[] && ((int[]) opt.Value).Length == 2)
                        {
                            return posevt.NewCoordinate.x == ((int[]) opt.Value)[0] &&
                                   posevt.NewCoordinate.y == ((int[]) opt.Value)[1];
                        }
                        else
                        {
                            return false;
                        }
                default:
                    return false;
            }
                    

        }

        public static bool operator !=(EventBase evt, SplitOption opt)
        {
            return !(evt == opt);
        }


        public EventType EventType;
        public object Value;
        public bool _disabled;

        public bool Disabled
        {
            get { return _disabled; }
            set
            {
                if (value == _disabled) return;
                _disabled = value;
                OnPropertyChanged(nameof(Disabled));
                OnPropertyChanged(nameof(Name));
            }
        }
        public string Name => this.ToString();

        public override string ToString()
        {
            string s = "";
            if (this.Disabled)
            {
                s += "✔ ";
            }
            if (this.Value is int[])
            {
                var pos = (int[]) Value;
                if (pos.Length == 2) s += $"{this.EventType} ({pos[0]},{pos[1]})";
                else s += $"{this.EventType} , Unknow";
            }
            else
            {
                s += $"{EventType},{Value}";
            }
            return s;
        }

        public SplitOption()
        {
            this.Disabled = false;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
