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

        public Option()
        {
            this.Autoreset = true;
            this.Autostart = true;
            this.SendIgt = true;
        }

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

        public bool SendIgt
        {
            get => _sendIgt;
            set
            {
                if (value == _sendIgt) return;
                _sendIgt = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region backing fields

        




        private bool _autostart;
        private bool _autoreset;
        private bool _sendIgt;
        #endregion
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
