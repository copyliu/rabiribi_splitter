using rabi_splitter_WPF.Annotations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

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
    }

    public enum ParameterOptions
    {
        HasValue,
        Any,
    }

    public sealed class ExtendedOptions<EnumType>
    {
        public readonly ParameterOptions option;
        public readonly EnumType value;

        #region Constructors
        private ExtendedOptions(ParameterOptions option)
        {
            this.option = option;
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
        
        private static Dictionary<ExtendedOptions<Map>, string> _mapCaptions;
        private static Dictionary<ExtendedOptions<Music>, string> _musicCaptions;

        // Captions for Split Trigger Options
        private static readonly Dictionary<SplitTrigger, string> _splitTriggerCaptions = new Dictionary<SplitTrigger, string>()
        {
            {SplitTrigger.None, "None"},
            {SplitTrigger.BossStart, "Boss Start"},
            {SplitTrigger.BossEnd, "Boss End"},
            {SplitTrigger.Reload, "Reload"},
            {SplitTrigger.MapChange, "Map Change (Experimental)"},
            {SplitTrigger.MusicChange, "Music Change (Experimental)"},
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
                if (value == _mapTypeFrom) return;
                _mapTypeFrom = value;
                OnPropertyChanged(nameof(MapTypeFrom));
            }
        }

        public ExtendedOptions<Map> MapTypeTo
        {
            get { return _mapTypeTo; }
            set
            {
                if (value == _mapTypeTo) return;
                _mapTypeTo = value;
                OnPropertyChanged(nameof(MapTypeTo));
            }
        }

        public ExtendedOptions<Music> MusicTypeFrom
        {
            get { return _musicTypeFrom; }
            set
            {
                if (value == _musicTypeFrom) return;
                _musicTypeFrom = value;
                OnPropertyChanged(nameof(MusicTypeFrom));
            }
        }

        public ExtendedOptions<Music> MusicTypeTo
        {
            get { return _musicTypeTo; }
            set
            {
                if (value == _musicTypeTo) return;
                _musicTypeTo = value;
                OnPropertyChanged(nameof(MusicTypeTo));
            }
        }

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

        public void SendTrigger(SplitTrigger trigger)
        {
            if (StartTimerSetting.TriggerType == trigger) _sendStart = true;
            if (SplitTimerSetting.TriggerType == trigger) _sendSplit = true;
            if (ResetTimerSetting.TriggerType == trigger) _sendReset = true;
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
