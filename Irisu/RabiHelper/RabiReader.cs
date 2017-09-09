﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;
using Irisu.Annotations;
using Irisu.Events;
using Irisu.Memory;

namespace Irisu.RabiHelper
{
    public delegate void RabiEventHandler(object sender,EventBase e);
    public class RabiReader : INotifyPropertyChanged
    {
        public event RabiEventHandler GameEvent;
        private System.Timers.Timer _timer;
        private static Process _rabiProcess;
        private static MemoryHelper _rabiMemoryHelper;
        private MemorySnapshot? _currentsnapshot;
        private MemorySnapshot? _oldsnapshot;
        private readonly Regex _titleReg = new Regex(@"ver.*?(\d+\.?\d+.*)$");
        private string _gameVer;
        private string _oldtitle;
        private int _veridx;
        private bool _busy;
        private readonly object _lock=new object();
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

        public RabiReader()
        {
            _timer=new Timer(1000.0/60);
            _timer.AutoReset = true;
            _timer.Elapsed+=TimerOnElapsed;
            _timer.Start();
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            lock (_lock)
            {
                if (_busy) return;
                _busy = true;
            }
            try
            {
                _currentsnapshot = ReadMemory();
                if (_currentsnapshot == null)
                {
                    _oldsnapshot = null;
                }
                else if (_oldsnapshot == null)
                {
                    _oldsnapshot = _currentsnapshot;
                }
                else
                {
                    var result = SnapshotWorker.FindEvents(_oldsnapshot.Value, _currentsnapshot.Value);
                    foreach (var eventBase in result)
                    {
                        GameEvent?.Invoke(this,eventBase);
                    }
                    _oldsnapshot = _currentsnapshot;
                }

            }
            catch (Exception ex)
            {
                App.Logging(ex.ToString());
            }

            _busy = false;
        }

        private MemorySnapshot? ReadMemory()
        {
            if (_rabiProcess == null || _rabiProcess.HasExited)
            {
                if (_rabiMemoryHelper != null)
                {
                    _rabiMemoryHelper.Dispose();
                    _rabiMemoryHelper = null;
                }
                var processlist = Process.GetProcessesByName("rabiribi");
                if (processlist.Length > 0)
                {
                    Process process = processlist[0];
                    if (process.MainWindowTitle != _oldtitle)
                    {
                        var result = _titleReg.Match(process.MainWindowTitle);
                        if (result.Success)
                        {
                            var rabiver = result.Groups[1].Value;
                            _veridx = Array.IndexOf(StaticData.VerNames, rabiver);
                            if (_veridx < 0)
                            {
                                GameVer = rabiver + " Running (not supported)";
                                GameVer = rabiver + " Running";
                                _oldtitle = process.MainWindowTitle;

                            }
                        }
                        else
                        {
                            _veridx = -1;
                            GameVer = "Running (Unknown version)";

                        }

                    }
                    if (_veridx < 0)
                    {
                        _rabiProcess = null;
                        _oldtitle = "";
                        GameVer = "Not Found";

                    }
                    else
                    {
                        _rabiProcess = process;
                        _rabiMemoryHelper = new MemoryHelper(_rabiProcess);
                    }

                }



            }
            if (_rabiProcess != null && !_rabiProcess.HasExited)
            {
                return new MemorySnapshot(_rabiMemoryHelper, _veridx);

            }
            return null;

        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}