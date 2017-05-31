using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Irisu.Events
{
  
    public enum EventType
    {
        Test,
        BossStart,
        BossEnd,
        Music,
        Item,
        Pos,
        Chapter,
        Special, // we need this?
        TownMember,
        
    }

    public enum SpecialEvents
    {
        GameStart,
        GameReload,
        BossReload,
    }

    public class EventBase : EventArgs
    {
        public EventType EventType;
    }

    public class TestEvent:EventBase
    {
        public string Msg;
        public TestEvent(string msg)
        {
            EventType=EventType.Test;
            Msg = msg;
        }
    }
}
