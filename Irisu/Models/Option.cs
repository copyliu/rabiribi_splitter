using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Irisu.Annotations;
using Irisu.EventHelper;
using Irisu.Memory;

namespace Irisu.Models
{
    public class Option : INotifyPropertyChanged
    {
        public HashSet<BossFight> EnabledBosses => _enabledBosses;

        private void UpdateEnabledBossSettings()
        {
            _enabledBosses.Clear();
            if (_cocoa1)
            {
                _enabledBosses.Add(BossFight.Cocoa1);
            }
            if (_ribbon)
            {
                _enabledBosses.Add(BossFight.Ribbon);
            }
            if (_ashuri1)
            {
                _enabledBosses.Add(BossFight.Ashuri1);
            }
            if (_ashuri2)
            {
                _enabledBosses.Add(BossFight.Ashuri2);
            }
            if (_cicini)
            {
                _enabledBosses.Add(BossFight.Cicini);
            }
//            if (_syaro)
//            {
//                _enabledBosses.Add(BossFight.Syaro);
//            }
//            if (_vanilla)
//            {
//                _enabledBosses.Add(BossFight.Vanilla);
//            }
//            if (_chocolate)
//            {
//                _enabledBosses.Add(BossFight.Chocolate);
//            }
            if (_cocoa2)
            {
                _enabledBosses.Add(BossFight.Cocoa2);
            }
            if (_rita)
            {
                _enabledBosses.Add(BossFight.Rita);
            }
//            if (_saya)
//            {
//                _enabledBosses.Add(BossFight.Saya);
//            }
//            if (_aruraune)
//            {
//                _enabledBosses.Add(BossFight.Aruraune);
//            }
//            if (_nieve)
//            {
//                _enabledBosses.Add(BossFight.Nieve);
//            }
//            if (_nixie)
//            {
//                _enabledBosses.Add(BossFight.Nixie);
//            }
//            if (_keke)
//            {
//                _enabledBosses.Add(BossFight.Keke);
//            }
            if (_seana1)
            {
                _enabledBosses.Add(BossFight.Seana1);
            }
            if (_seana2)
            {
                _enabledBosses.Add(BossFight.Seana2);
            }
//            if (_pandora)
//            {
//                _enabledBosses.Add(BossFight.Pandora);
//            }
            if (_kotri1)
            {
                _enabledBosses.Add(BossFight.Kotri1);
            }
            if (_kotri2)
            {
                _enabledBosses.Add(BossFight.Kotri2);
            }
            if (_kotri3)
            {
                _enabledBosses.Add(BossFight.Kotri3);
            }
            if (_alius1)
            {
                _enabledBosses.Add(BossFight.Alius1);
            }
            if (_alius2)
            {
                _enabledBosses.Add(BossFight.Alius2);
            }
            if (_alius3)
            {
                _enabledBosses.Add(BossFight.Alius3);
            }
            if (_miru)
            {
                _enabledBosses.Add(BossFight.Miru);
            }
            if (_noah1)
            {
                _enabledBosses.Add(BossFight.Noah1);
            }
            if (_noah3)
            {
                _enabledBosses.Add(BossFight.Noah3);
            }
//            if (_www)
//            {
//                _enabledBosses.Add(BossFight.WWW);
//            }
//            if (_rumi)
//            {
//                _enabledBosses.Add(BossFight.Rumi);
//            }
//            if (_miriam)
//            {
//                _enabledBosses.Add(BossFight.Miriam);
//            }
//            if (_irisu)
//            {
//                _enabledBosses.Add(BossFight.Irisu);
//            }
            OnPropertyChanged(nameof(EnabledBosses));

        }

        public bool Bossstart
        {
            get => _bossstart;
            set
            {
                if (value == _bossstart) return;
                _bossstart = value;
                OnPropertyChanged();
            }
        }

        public bool Bossend
        {
            get => _bossend;
            set
            {
                if (value == _bossend) return;
                _bossend = value;
                OnPropertyChanged();
            }
        }
        #region Boss
        public bool Cocoa1
        {
            get => _cocoa1;
            set
            {
                if (value == _cocoa1) return;
                _cocoa1 = value;
                UpdateEnabledBossSettings();
                OnPropertyChanged();
            }
        }

        public bool Ribbon
        {
            get => _ribbon;
            set
            {
                if (value == _ribbon) return;
                _ribbon = value;
                UpdateEnabledBossSettings();
                OnPropertyChanged();
            }
        }

        public bool Ashuri1
        {
            get => _ashuri1;
            set
            {
                if (value == _ashuri1) return;
                _ashuri1 = value;
                UpdateEnabledBossSettings();
                OnPropertyChanged();
            }
        }

        public bool Ashuri2
        {
            get => _ashuri2;
            set
            {
                if (value == _ashuri2) return;
                _ashuri2 = value;
                UpdateEnabledBossSettings();
                OnPropertyChanged();
            }
        }

        public bool Cicini
        {
            get => _cicini;
            set
            {
                if (value == _cicini) return;
                _cicini = value;
                UpdateEnabledBossSettings();
                OnPropertyChanged();
            }
        }

        public bool Syaro
        {
            get => _syaro;
            set
            {
                if (value == _syaro) return;
                _syaro = value;
                UpdateEnabledBossSettings();
                OnPropertyChanged();
            }
        }

        public bool Vanilla
        {
            get => _vanilla;
            set
            {
                if (value == _vanilla) return;
                _vanilla = value;
                UpdateEnabledBossSettings();
                OnPropertyChanged();
            }
        }

        public bool Chocolate
        {
            get => _chocolate;
            set
            {
                if (value == _chocolate) return;
                _chocolate = value;
                UpdateEnabledBossSettings();
                OnPropertyChanged();
            }
        }

        public bool Cocoa2
        {
            get => _cocoa2;
            set
            {
                if (value == _cocoa2) return;
                _cocoa2 = value;
                UpdateEnabledBossSettings();
                OnPropertyChanged();
            }
        }

        public bool Rita
        {
            get => _rita;
            set
            {
                if (value == _rita) return;
                _rita = value;
                UpdateEnabledBossSettings();
                OnPropertyChanged();
            }
        }

        public bool Saya
        {
            get => _saya;
            set
            {
                if (value == _saya) return;
                _saya = value;
                UpdateEnabledBossSettings();
                OnPropertyChanged();
            }
        }

        public bool Aruraune
        {
            get => _aruraune;
            set
            {
                if (value == _aruraune) return;
                _aruraune = value;
                UpdateEnabledBossSettings();
                OnPropertyChanged();
            }
        }

        public bool Nieve
        {
            get => _nieve;
            set
            {
                if (value == _nieve) return;
                _nieve = value;
                UpdateEnabledBossSettings();
                OnPropertyChanged();
            }
        }

        public bool Nixie
        {
            get => _nixie;
            set
            {
                if (value == _nixie) return;
                _nixie = value;
                UpdateEnabledBossSettings();
                OnPropertyChanged();
            }
        }

        public bool Keke
        {
            get => _keke;
            set
            {
                if (value == _keke) return;
                _keke = value;
                UpdateEnabledBossSettings();
                OnPropertyChanged();
            }
        }

        public bool Seana1
        {
            get => _seana1;
            set
            {
                if (value == _seana1) return;
                _seana1 = value;
                UpdateEnabledBossSettings();
                OnPropertyChanged();
            }
        }

        public bool Seana2
        {
            get => _seana2;
            set
            {
                if (value == _seana2) return;
                _seana2 = value;
                UpdateEnabledBossSettings();
                OnPropertyChanged();
            }
        }

        public bool Pandora
        {
            get => _pandora;
            set
            {
                if (value == _pandora) return;
                _pandora = value;
                UpdateEnabledBossSettings();
                OnPropertyChanged();
            }
        }

        public bool Kotri1
        {
            get => _kotri1;
            set
            {
                if (value == _kotri1) return;
                _kotri1 = value;
                UpdateEnabledBossSettings();
                OnPropertyChanged();
            }
        }

        public bool Kotri2
        {
            get => _kotri2;
            set
            {
                if (value == _kotri2) return;
                _kotri2 = value;
                UpdateEnabledBossSettings();
                OnPropertyChanged();
            }
        }

        public bool Kotri3
        {
            get => _kotri3;
            set
            {
                if (value == _kotri3) return;
                _kotri3 = value;
                UpdateEnabledBossSettings();
                OnPropertyChanged();
            }
        }

        public bool Alius1
        {
            get => _alius1;
            set
            {
                if (value == _alius1) return;
                _alius1 = value;
                UpdateEnabledBossSettings();
                OnPropertyChanged();
            }
        }

        public bool Alius2
        {
            get => _alius2;
            set
            {
                if (value == _alius2) return;
                _alius2 = value;
                UpdateEnabledBossSettings();
                OnPropertyChanged();
            }
        }

        public bool Alius3
        {
            get => _alius3;
            set
            {
                if (value == _alius3) return;
                _alius3 = value;
                UpdateEnabledBossSettings();
                OnPropertyChanged();
            }
        }

        public bool Miru
        {
            get => _miru;
            set
            {
                if (value == _miru) return;
                _miru = value;
                UpdateEnabledBossSettings();
                OnPropertyChanged();
            }
        }

        public bool Noah1
        {
            get => _noah1;
            set
            {
                if (value == _noah1) return;
                _noah1 = value;
                UpdateEnabledBossSettings();
                OnPropertyChanged();
            }
        }

        public bool Noah3
        {
            get => _noah3;
            set
            {
                if (value == _noah3) return;
                _noah3 = value;
                UpdateEnabledBossSettings();
                OnPropertyChanged();
            }
        }

        public bool Www
        {
            get => _www;
            set
            {
                if (value == _www) return;
                _www = value;
                UpdateEnabledBossSettings();
                OnPropertyChanged();
            }
        }

        public bool Rumi
        {
            get => _rumi;
            set
            {
                if (value == _rumi) return;
                _rumi = value;
                UpdateEnabledBossSettings();
                OnPropertyChanged();
            }
        }

        public bool Miriam
        {
            get => _miriam;
            set
            {
                if (value == _miriam) return;
                _miriam = value;
                UpdateEnabledBossSettings();
                OnPropertyChanged();
            }
        }

        public bool Irisu
        {
            get => _irisu;
            set
            {
                if (value == _irisu) return;
                _irisu = value;
                UpdateEnabledBossSettings();
                OnPropertyChanged();
            }
        }
        #endregion

        #region Items

        //TODO

        #endregion

        #region Options

        

        
        public bool Autostart
        {
            get => _autostart;
            set
            {
                if (value == _autostart) return;
                _autostart = value;
                OnPropertyChanged();
            }
        }

        public bool Autoreset
        {
            get => _autoreset;
            set
            {
                if (value == _autoreset) return;
                _autoreset = value;
                OnPropertyChanged();
            }
        }

        public bool Computer
        {
            get => _computer;
            set
            {
                if (value == _computer) return;
                _computer = value;
                OnPropertyChanged();
            }
        }

        public bool Mre
        {
            get => _mre;
            set
            {
                if (value == _mre) return;
                _mre = value;
                OnPropertyChanged();
            }
        }

        public bool Sidechapter
        {
            get => _sidechapter;
            set
            {
                if (value == _sidechapter) return;
                _sidechapter = value;
                OnPropertyChanged();
            }
        }

        public bool Igt
        {
            get => _igt;
            set
            {
                if (value == _igt) return;
                _igt = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region backing fields

        

        
        private bool _bossstart;
        private bool _bossend;

        

        private bool _cocoa1;
        private bool _ribbon;
        private bool _ashuri1;
        private bool _ashuri2;
        private bool _cicini;
        private bool _syaro;
        private bool _vanilla;
        private bool _chocolate;
        private bool _cocoa2;
        private bool _rita;
        private bool _saya;
        private bool _aruraune;
        private bool _nieve;
        private bool _nixie;
        private bool _keke;
        private bool _seana1;
        private bool _seana2;
        private bool _pandora;
        private bool _kotri1;
        private bool _kotri2;
        private bool _kotri3;
        private bool _alius1;
        private bool _alius2;
        private bool _alius3;
        private bool _miru;
        private bool _noah1;
        private bool _noah3;
        private bool _www;
        private bool _rumi;
        private bool _miriam;
        private bool _irisu;




        private bool _autostart;
        private bool _autoreset;
        private bool _computer;
        private bool _mre;
        private bool _sidechapter;
        private bool _igt;
        private HashSet<BossFight> _enabledBosses=new HashSet<BossFight>();

        #endregion
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
