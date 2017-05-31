using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Windows.Documents;
using Irisu.Memory;

namespace Irisu.Events
{
    public enum InGameActivity
    {
        STARTING,
        WALKING,
        BOSS_BATTLE,
    }
    public static class SnapshotWorker
    {
        public static InGameActivity CurrentActivity;

        public static List<EventBase> ComparerSnapShotAndFireEvent(MemorySnapshot prevSnapshot, MemorySnapshot snapshot)
        {
            
            List<EventBase> events=new List<EventBase>();

            //TODO

            #region music


            if (prevSnapshot.musicid != snapshot.musicid)
            {
                events.Add(new MusicEvent(prevSnapshot.musicid,snapshot.musicid));
            }

            #endregion


            #region map

            if (prevSnapshot.mapid != snapshot.mapid)
            {
                events.Add(new MapEvent(prevSnapshot.musicid, snapshot.musicid));
            }


            #endregion


            #region minimap shift

            //todo

            #endregion

            #region Boss

            //todo boss start

            //todo boss end

            #endregion

            #region death


            //todo
            #endregion

            #region startgame

            //todo

            #endregion

            #region item

            //todo

            #endregion


            events.Add(new TestEvent("ping!"));

            return events;
           
           
            
        }
    }
}
