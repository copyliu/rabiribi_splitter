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

    public class SplitCondition : INotifyPropertyChanged
    {
        private SplitTrigger _triggerType;
    
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
