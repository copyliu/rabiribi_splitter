using rabi_splitter_WPF.Annotations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

using MapTileCoordinate = rabi_splitter_WPF.MainWindow.MapTileCoordinate;

namespace rabi_splitter_WPF
{
    public enum SplitTrigger
    {
        None,
        BossStart,
        BossEnd,
        Reload,
        MapChange,
        MusicChange,
        MapTileChange,
    }

    public enum ParameterOptions
    {
        HasValue,
        Any,
    }

    public struct ExtendedOptions<EnumType> where EnumType : new()
    {
        public readonly ParameterOptions option;
        public readonly EnumType value;

        #region Constructors
        private ExtendedOptions(ParameterOptions option)
        {
            this.option = option;
            value = new EnumType();
        }

        private ExtendedOptions(EnumType value)
        {
            option = ParameterOptions.HasValue;
            this.value = value;
        }
        #endregion

        public static ExtendedOptions<EnumType> Enum(EnumType value)
        {
            return new ExtendedOptions<EnumType>(value);
        }

        public static ExtendedOptions<EnumType> Option(ParameterOptions option)
        {
            return new ExtendedOptions<EnumType>(option);
        }

        #region Equals, GetHashCode
        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            var o = (ExtendedOptions<EnumType>)obj;
            return option.Equals(o.option) && value.Equals(o.value);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            const int prime = 31;
            int result = 1;
            result = prime * result + option.GetHashCode();
            result = prime * result + value.GetHashCode();
            return result;
        }
        #endregion
    }

    public class SplitCondition : INotifyPropertyChanged
    {
        private SplitTrigger _triggerType;
        private ExtendedOptions<Map> _mapTypeFrom;
        private ExtendedOptions<Map> _mapTypeTo;
        private ExtendedOptions<Music> _musicTypeFrom;
        private ExtendedOptions<Music> _musicTypeTo;
        private bool _mapTileFromAny;
        private int _mapTileFromX;
        private int _mapTileFromY;
        private bool _mapTileToAny;
        private int _mapTileToX;
        private int _mapTileToY;

        #region Dictionaries 

        private static Dictionary<ExtendedOptions<Map>, string> _mapCaptions;
        private static Dictionary<ExtendedOptions<Music>, string> _musicCaptions;

        // Captions for Split Trigger Options
        private static readonly Dictionary<SplitTrigger, string> _splitTriggerCaptions = new Dictionary<SplitTrigger, string>()
        {
            {SplitTrigger.None, "None"},
            {SplitTrigger.BossStart, "Boss Start"},
            {SplitTrigger.BossEnd, "Boss End"},
            {SplitTrigger.Reload, "Reload"},
            {SplitTrigger.MapChange, "Map Change"},
            {SplitTrigger.MusicChange, "Music Change"},
            {SplitTrigger.MapTileChange, "Map Tile Change"},
        };

        // Captions for Split Trigger Parameter Options
        private static readonly Dictionary<ParameterOptions, string> _parameterOptionCaptions = new Dictionary<ParameterOptions, string>()
        {
            {ParameterOptions.Any, "Any Value"},
        };
        
        public Dictionary<SplitTrigger, string> SplitTriggerCaptions
        {
            get { return _splitTriggerCaptions; }
        }

        public Dictionary<ExtendedOptions<Map>, string> MapCaptions
        {
            get
            {
                if (_mapCaptions == null)
                {
                    _mapCaptions = _parameterOptionCaptions.ToDictionary(t => ExtendedOptions<Map>.Option(t.Key), t => t.Value);
                    StaticData.MapList.ForEach(t => _mapCaptions.Add(ExtendedOptions<Map>.Enum(t.Item1), t.Item2));
                }
                return _mapCaptions;
            }
        }

        public Dictionary<ExtendedOptions<Music>, string> MusicCaptions
        {
            get
            {
                if (_musicCaptions == null)
                {
                    _musicCaptions = _parameterOptionCaptions.ToDictionary(t => ExtendedOptions<Music>.Option(t.Key), t => t.Value);
                    StaticData.MusicList.ForEach(t => _musicCaptions.Add(ExtendedOptions<Music>.Enum(t.Item1), t.Item2));
                }
                return _musicCaptions;
            }
        }

        #endregion

        #region Constructors to create Trigger Conditions
        public SplitCondition()
        {
            // Default values
            _mapTypeFrom = ExtendedOptions<Map>.Option(ParameterOptions.Any);
            _mapTypeTo = ExtendedOptions<Map>.Option(ParameterOptions.Any);
            _musicTypeFrom = ExtendedOptions<Music>.Option(ParameterOptions.Any);
            _musicTypeTo = ExtendedOptions<Music>.Option(ParameterOptions.Any);
        }

        public static SplitCondition Trigger(SplitTrigger triggerType)
        {
            return new SplitCondition() { TriggerType = triggerType };
        }

        public static SplitCondition MapChange(int oldMapId, int newMapId)
        {
            Map? oldMap = StaticData.GetMap(oldMapId);
            Map? newMap = StaticData.GetMap(newMapId);
            var oldMapExtended = oldMap.HasValue ? ExtendedOptions<Map>.Enum(oldMap.Value) : ExtendedOptions<Map>.Option(ParameterOptions.Any);
            var newMapExtended = newMap.HasValue ? ExtendedOptions<Map>.Enum(newMap.Value) : ExtendedOptions<Map>.Option(ParameterOptions.Any);

            return new SplitCondition() { TriggerType = SplitTrigger.MapChange, MapTypeFrom = oldMapExtended, MapTypeTo = newMapExtended };
        }

        public static SplitCondition MusicChange(int oldMusicId, int newMusicId)
        {
            Music? oldMusic = StaticData.GetMusic(oldMusicId);
            Music? newMusic = StaticData.GetMusic(newMusicId);
            var oldMusicExtended = oldMusic.HasValue ? ExtendedOptions<Music>.Enum(oldMusic.Value) : ExtendedOptions<Music>.Option(ParameterOptions.Any);
            var newMusicExtended = newMusic.HasValue ? ExtendedOptions<Music>.Enum(newMusic.Value) : ExtendedOptions<Music>.Option(ParameterOptions.Any);

            return new SplitCondition() { TriggerType = SplitTrigger.MusicChange, MusicTypeFrom = oldMusicExtended, MusicTypeTo = newMusicExtended };
        }

        public static SplitCondition MapTileChange(MapTileCoordinate oldMapTile, MapTileCoordinate newMapTile)
        {
            return new SplitCondition()
            {
                TriggerType = SplitTrigger.MapTileChange,
                MapTileFromX = oldMapTile.x,
                MapTileFromY = oldMapTile.y,
                MapTileToX = newMapTile.x,
                MapTileToY = newMapTile.y,
            };
        }

        public bool Matches(SplitCondition condition)
        {
            if (TriggerType != condition.TriggerType) return false;
            if (TriggerType == SplitTrigger.MapChange)
            {
                if (!(MapTypeFrom.option == ParameterOptions.Any || MapTypeFrom.Equals(condition.MapTypeFrom))) return false;
                if (!(MapTypeTo.option == ParameterOptions.Any || MapTypeTo.Equals(condition.MapTypeTo))) return false;
            }
            if (TriggerType == SplitTrigger.MusicChange)
            {
                if (!(MusicTypeFrom.option == ParameterOptions.Any || MusicTypeFrom.Equals(condition.MusicTypeFrom))) return false;
                if (!(MusicTypeTo.option == ParameterOptions.Any || MusicTypeTo.Equals(condition.MusicTypeTo))) return false;
            }
            if (TriggerType == SplitTrigger.MapTileChange)
            {
                if (!(MapTileFromAny || (MapTileFromX == condition.MapTileFromX && MapTileFromY == condition.MapTileFromY))) return false;
                if (!(MapTileToAny || (MapTileToX == condition.MapTileToX && MapTileToY == condition.MapTileToY))) return false;
            }
            return true;
        }

        #endregion

        #region Parameters

        public SplitTrigger TriggerType
        {
            get { return _triggerType; }
            set
            {
                if (value == _triggerType) return;
                _triggerType = value;
                OnPropertyChanged(nameof(TriggerType));
            }
        }

        public ExtendedOptions<Map> MapTypeFrom
        {
            get { return _mapTypeFrom; }
            set
            {
                if (value.Equals(_mapTypeFrom)) return;
                _mapTypeFrom = value;
                OnPropertyChanged(nameof(MapTypeFrom));
            }
        }

        public ExtendedOptions<Map> MapTypeTo
        {
            get { return _mapTypeTo; }
            set
            {
                if (value.Equals(_mapTypeTo)) return;
                _mapTypeTo = value;
                OnPropertyChanged(nameof(MapTypeTo));
            }
        }

        public ExtendedOptions<Music> MusicTypeFrom
        {
            get { return _musicTypeFrom; }
            set
            {
                if (value.Equals(_musicTypeFrom)) return;
                _musicTypeFrom = value;
                OnPropertyChanged(nameof(MusicTypeFrom));
            }
        }

        public ExtendedOptions<Music> MusicTypeTo
        {
            get { return _musicTypeTo; }
            set
            {
                if (value.Equals(_musicTypeTo)) return;
                _musicTypeTo = value;
                OnPropertyChanged(nameof(MusicTypeTo));
            }
        }

        public bool MapTileFromAny
        {
            get { return _mapTileFromAny; }
            set
            {
                if (value.Equals(_mapTileFromAny)) return;
                _mapTileFromAny = value;
                OnPropertyChanged(nameof(MapTileFromAny));
            }
        }

        public int MapTileFromX
        {
            get { return _mapTileFromX; }
            set
            {
                if (value.Equals(_mapTileFromX)) return;
                _mapTileFromX = value;
                OnPropertyChanged(nameof(MapTileFromX));
            }
        }

        public int MapTileFromY
        {
            get { return _mapTileFromY; }
            set
            {
                if (value.Equals(_mapTileFromY)) return;
                _mapTileFromY = value;
                OnPropertyChanged(nameof(MapTileFromY));
            }
        }

        public bool MapTileToAny
        {
            get { return _mapTileToAny; }
            set
            {
                if (value.Equals(_mapTileToAny)) return;
                _mapTileToAny = value;
                OnPropertyChanged(nameof(MapTileToAny));
            }
        }

        public int MapTileToX
        {
            get { return _mapTileToX; }
            set
            {
                if (value.Equals(_mapTileToX)) return;
                _mapTileToX = value;
                OnPropertyChanged(nameof(MapTileToX));
            }
        }

        public int MapTileToY
        {
            get { return _mapTileToY; }
            set
            {
                if (value.Equals(_mapTileToY)) return;
                _mapTileToY = value;
                OnPropertyChanged(nameof(MapTileToY));
            }
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    
    
    class PracticeModeContext : INotifyPropertyChanged
    {
        private SplitCondition _startTimerSetting = new SplitCondition() { TriggerType = SplitTrigger.BossStart };
        private SplitCondition _splitTimerSetting = new SplitCondition() { TriggerType = SplitTrigger.BossEnd };
        private SplitCondition _stopTimerSetting = new SplitCondition() { TriggerType = SplitTrigger.Reload };

        public SplitCondition StartTimerSetting
        {
            get { return _startTimerSetting; }
        }

        public SplitCondition SplitTimerSetting
        {
            get { return _splitTimerSetting; }
        }

        public SplitCondition ResetTimerSetting
        {
            get { return _stopTimerSetting; }
        }
        
        private bool _sendStart;
        private bool _sendSplit;
        private bool _sendReset;
        
        public void SendTrigger(SplitCondition splitCondition)
        {
            if (StartTimerSetting.Matches(splitCondition)) _sendStart = true;
            if (SplitTimerSetting.Matches(splitCondition)) _sendSplit = true;
            if (ResetTimerSetting.Matches(splitCondition)) _sendReset = true;
        }

        public void ResetSendTriggers()
        {
            _sendStart = false;
            _sendSplit= false;
            _sendReset = false;
        }

        // Send only one message at a time.
        // Currently prioritise Reset > Start > Split
        public bool SendStartTimerThisFrame()
        {
            return !_sendReset && _sendStart;
        }

        public bool SendSplitTimerThisFrame()
        {
            return !_sendReset && !_sendStart && _sendSplit;
        }

        public bool SendResetTimerThisFrame()
        {
            return _sendReset;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
