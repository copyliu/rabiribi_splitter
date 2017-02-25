using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using rabi_splitter_WPF.Annotations;

namespace rabi_splitter_WPF
{
    public class BossData:INotifyPropertyChanged
    {

        public int BossIdx { get; set; }
        public int BossID { get; set; }
        public int BossHP { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    class DebugContext : INotifyPropertyChanged
    {
        private bool _bossEvent;
        private string _debugLog;

        public ObservableCollection<BossData> BossList = new ObservableCollection<BossData>();

        public bool BossEvent
        {
            get { return _bossEvent; }
            set
            {
                if (value == _bossEvent) return;
                _bossEvent = value;
                OnPropertyChanged(nameof(BossEvent));
            }
        }

        public string DebugLog
        {
            get { return _debugLog; }
            set
            {
                if (value == _debugLog) return;
                _debugLog = value;
                OnPropertyChanged(nameof(DebugLog));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    class MainContext : INotifyPropertyChanged

    {
        private bool _musicStart;
        private bool _musicEnd;
        private bool _computer;
        private bool _miruDe;
        private bool _sideCh;
        private bool _aliusI;
        private bool _tm2;
        private bool _irisu1;
        private bool _debugArea;
        private int _serverPort;
        private string _gameVer;
        private string _gameMusic;
        private bool _igt;

        public bool MusicStart
        {
            get { return _musicStart; }
            set
            {
                if (value == _musicStart) return;
                _musicStart = value;
                OnPropertyChanged(nameof(MusicStart));
            }
        }

        public bool MusicEnd
        {
            get { return _musicEnd; }
            set
            {
                if (value == _musicEnd) return;
                _musicEnd = value;
                OnPropertyChanged(nameof(MusicEnd));
            }
        }

        public bool Computer
        {
            get { return _computer; }
            set
            {
                if (value == _computer) return;
                _computer = value;
                OnPropertyChanged(nameof(Computer));
            }
        }

        public bool MiruDe
        {
            get { return _miruDe; }
            set
            {
                if (value == _miruDe) return;
                _miruDe = value;
                OnPropertyChanged(nameof(MiruDe));
            }
        }

        public bool SideCh
        {
            get { return _sideCh; }
            set
            {
                if (value == _sideCh) return;
                _sideCh = value;
                OnPropertyChanged(nameof(SideCh));
            }
        }

        public bool AliusI
        {
            get { return _aliusI; }
            set
            {
                if (value == _aliusI) return;
                _aliusI = value;
                OnPropertyChanged(nameof(AliusI));
            }
        }

        public bool Tm2
        {
            get { return _tm2; }
            set
            {
                if (value == _tm2) return;
                _tm2 = value;
                OnPropertyChanged(nameof(Tm2));
            }
        }

        public bool Irisu1
        {
            get { return _irisu1; }
            set
            {
                if (value == _irisu1) return;
                _irisu1 = value;
                OnPropertyChanged(nameof(Irisu1));
            }
        }

        public bool DebugArea
        {
            get { return _debugArea; }
            set
            {
                if (value == _debugArea) return;
                _debugArea = value;
                OnPropertyChanged(nameof(DebugArea));
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


        public string oldtitle;
        public int veridx;
        public int lastmoney;
        public int lastmapid;
        public int lastmusicid;
        public bool bossbattle;
        public List<int> lastbosslist;
        public int lastnoah3hp;
        public int lastTM;
        public DateTime LastTMAddTime;

        public MainContext()
        {
            this.MusicEnd = true;
            this.MusicStart = false;
            this.Computer = true;
            this.MiruDe = true;
            this.SideCh = true;
            this.AliusI = true;
            this.Tm2 = true;
            this.Irisu1 = true;
            this.DebugArea = false;
            this.ServerPort = 16834;
            this.Igt = true;


        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



    }
}

