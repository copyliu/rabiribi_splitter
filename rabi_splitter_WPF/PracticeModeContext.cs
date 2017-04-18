using rabi_splitter_WPF.Annotations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace rabi_splitter_WPF
{
    enum SplitTrigger
    {
        None,
        BossStart,
        BossEnd,
        Reload,
        MapChange,
        MusicChange,
    }
    
    
    class PracticeModeContext : INotifyPropertyChanged
    {
        private SplitTrigger _startTimerSetting = SplitTrigger.BossStart;
        private SplitTrigger _splitTimerSetting = SplitTrigger.BossEnd;
        private SplitTrigger _stopTimerSetting = SplitTrigger.Reload;
        private static readonly Dictionary<SplitTrigger, string> _splitTriggerCaptions = new Dictionary<SplitTrigger, string>()
        {
            {SplitTrigger.None, "None"},
            {SplitTrigger.BossStart, "Boss Start"},
            {SplitTrigger.BossEnd, "Boss End"},
            {SplitTrigger.Reload, "Reload"},
            {SplitTrigger.MapChange, "Map Change (Experimental)"},
            {SplitTrigger.MusicChange, "Music Change (Experimental)"},
        };

        public Dictionary<SplitTrigger, string> SplitTriggerCaptions
        {
            get { return _splitTriggerCaptions; }
        }

        public SplitTrigger StartTimerSetting
        {
            get { return _startTimerSetting; }
            set
            {
                if (value == _startTimerSetting) return;
                _startTimerSetting = value;
                OnPropertyChanged(nameof(StartTimerSetting));
            }
        }

        public SplitTrigger SplitTimerSetting
        {
            get { return _splitTimerSetting; }
            set
            {
                if (value == _splitTimerSetting) return;
                _splitTimerSetting = value;
                OnPropertyChanged(nameof(SplitTimerSetting));
            }
        }

        public SplitTrigger ResetTimerSetting
        {
            get { return _stopTimerSetting; }
            set
            {
                if (value == _stopTimerSetting) return;
                _stopTimerSetting = value;
                OnPropertyChanged(nameof(ResetTimerSetting));
            }
        }
        
        private bool _sendStart;
        private bool _sendSplit;
        private bool _sendReset;

        public void SendTrigger(SplitTrigger trigger)
        {
            if (StartTimerSetting == trigger) _sendStart = true;
            if (SplitTimerSetting == trigger) _sendSplit = true;
            if (ResetTimerSetting == trigger) _sendReset = true;
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
