using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Documents;
using Irisu.EventHelper;
using Irisu.Memory;

namespace Irisu.Events
{
    enum InGameActivity
    {
        STARTING,
        WALKING,
        BOSS_BATTLE,
    }

    class InGameState
    {
        public int nRestarts;
        public int nDeaths;

        public int nDeathsAlt;

        public InGameActivity currentActivity;
        public BossFight currentBossFight;
        public DateTime currentBossStartTime;

        public BossFight lastBossFight;
        public TimeSpan lastBossFightDuration;

        public int lastNonZeroPlayTime = -1;

        public InGameState()
        {
            currentActivity = InGameActivity.STARTING;
            currentBossFight = null;
            lastBossFight = null;
        }

        public bool CurrentActivityIs(InGameActivity gameActivity)
        {
            return currentActivity == gameActivity;
        }

        public bool IsGameStarted()
        {
            return !CurrentActivityIs(InGameActivity.STARTING);
        }

        public void StartBossFight(BossFight bossFight)
        {
            currentActivity = InGameActivity.BOSS_BATTLE;
            currentBossStartTime = DateTime.Now;
            currentBossFight = bossFight;
        }

        public void StopBossFight()
        {
            currentActivity = InGameActivity.WALKING;
            currentBossFight = null;
        }

        public void FinishBossFight()
        {
            lastBossFight = currentBossFight;
            lastBossFightDuration = (DateTime.Now - currentBossStartTime);
            currentActivity = InGameActivity.WALKING;
            currentBossFight = null;
        }
    }

    public static class SnapshotWorker
    {
        private  static InGameState _inGameState=new InGameState();
        public static List<EventBase> FindEvents(MemorySnapshot prevSnapshot, MemorySnapshot snapshot)
        {
            
            List<EventBase> events=new List<EventBase>();

            //TODO


            #region Detect Reload

            bool reloading = snapshot.playtime == 0 || ( (snapshot.playtime < prevSnapshot.playtime));
            if (_inGameState.IsGameStarted() && snapshot.playtime > 0)
            {
               
                _inGameState.lastNonZeroPlayTime = snapshot.playtime;
            }
            if (reloading)
            {
                events.Add(new SpecialEvent(SpecialEvents.GameReload));
            }

            #endregion

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


            #region minimap shift , bossevent


            if ( prevSnapshot.minimapPosition != snapshot.minimapPosition)
            {

                if (snapshot.minimapPosition == 1)
                {
                    var bossFight = BossFightIdentifier.IdentifyBossFight(snapshot);
                    events.Add(new BossStartEvent(bossFight));
                    _inGameState.StartBossFight(bossFight);
                }
                else // snapshot.minimapPosition == 0
                {
                    if (reloading)
                    {
                        _inGameState.StopBossFight();
                        events.Add(new SpecialEvent(SpecialEvents.BossReload));
                    }
                    else
                    {
                       
                        events.Add(new BossEndEvent(_inGameState.currentBossFight));
                        _inGameState.FinishBossFight();
                    }
                }
            }

            #endregion



            #region death


            //todo
            #endregion

            #region startgame
            if ((snapshot.CurrentMusicIs(Music.MAIN_MENU) || snapshot.CurrentMusicIs(Music.ARTBOOK_INTRO))
                && prevSnapshot.blackness == 0 && snapshot.blackness >= 100000)
            {
                // Sudden increase by 100000

                events.Add(new SpecialEvent(SpecialEvents.GameStart));
                _inGameState=new InGameState();
            }
            //todo

            #endregion

            #region item

          
            
            if (snapshot.itemPercent != prevSnapshot.itemPercent)
            {
                events.Add(new ItemPercentEvent(snapshot.itemPercent));
            }

            ItemEvent itemevt=null;
            foreach (var key in snapshot.badges.Keys)
            {
                if (snapshot.badges[key] > prevSnapshot.badges[key])
                {
                    if (itemevt == null)
                    {
                        itemevt = new ItemEvent();
                    }
                    itemevt.NewBadges.Add(key);
                }
            }

            foreach (var itemsKey in snapshot.items.Keys)
            {
                if (snapshot.items[itemsKey] > prevSnapshot.items[itemsKey])
                {
                    if (itemevt == null)
                    {
                        itemevt = new ItemEvent();
                    }
                    itemevt.NewItems.Add(itemsKey, snapshot.items[itemsKey]);
                }
            }
            if (itemevt != null)
            {
                events.Add(itemevt);
            }


            #endregion


            //            events.Add(new TestEvent("ping!"));

            return events;
           
           
            
        }
    }
}
