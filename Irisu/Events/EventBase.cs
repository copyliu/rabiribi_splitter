using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irisu.EventHelper;
using Irisu.Memory;

namespace Irisu.Events
{

    public enum EventType
    {
        Test,
        BossStart,
        BossEnd,
        Music,
        Item,
        Map,
        Pos,
        Chapter,
        Special, // do we need this?
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
        public override string ToString()
        {
            return this.EventType + "";
        }
    }

    public class TestEvent : EventBase
    {
        public string Msg;

        public TestEvent(string msg)
        {
            EventType = EventType.Test;
            Msg = msg;
        }
    }

    public class MusicEvent : EventBase
    {
        public int OldMusicId;
        public int NewMusicId;

        public MusicEvent(int old, int newid)
        {
            OldMusicId = old;
            NewMusicId = newid;
            EventType = EventType.Music;
        }


    }

    public class MapEvent : EventBase
    {
        public int OldMapId;
        public int NewMapId;

        public MapEvent(int old, int newid)
        {
            OldMapId = old;
            NewMapId = newid;
            EventType = EventType.Map;
        }
    }
    /// <summary>
    /// useless usually
    /// </summary>
    public class PosEvent : EventBase 
    {
        public MapTileCoordinate OldCoordinate;
        public MapTileCoordinate NewCoordinate;

        public PosEvent(MapTileCoordinate old, MapTileCoordinate New)
        {
            OldCoordinate = old;
            NewCoordinate = New;
            EventType = EventType.Pos;
        }
    }

    public class BossStartEvent : EventBase
    {
        public BossFight Boss;

        public BossStartEvent(BossFight boss)
        {
            this.Boss = boss;
            EventType=EventType.BossStart;
        }
    }
    public class BossEndEvent : EventBase
    {
        public BossFight Boss;

        public BossEndEvent(BossFight boss)
        {
            this.Boss = boss;
            EventType = EventType.BossEnd;
        }
    }

    public class ItemGetEvent : EventBase
    {
        public float Percent;
        //todo items
        //public HashSet<Item> AddItems;
        public ItemGetEvent(float p)
        {
            EventType=EventType.Item;
            Percent = p;

        }
    }

    public class ChapterEvent : EventBase
    {
        public int OldCh;
        public int NewCh;

        public ChapterEvent(int old, int n)
        {
            OldCh = old;
            NewCh = n;
            EventType = EventType.Chapter;
        }
    }

    public class TownMemberEvent : EventBase
    {
        public int OldTownMember;
        public int NewTownMember;

        public TownMemberEvent(int old, int n)
        {
            OldTownMember = old;
            NewTownMember = n;
            EventType = EventType.TownMember;

        }


    }

    public class SpecialEvent : EventBase
    {
        public SpecialEvents Event;

        public SpecialEvent(SpecialEvents evt)
        {
            Event = evt;
            EventType = EventType.Special;
        }

        public override string ToString()
        {
            return $"Sp: {this.Event}";
        }
    }
}


