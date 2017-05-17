using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Irisu.Memory;

namespace Irisu.EventHelper
{
    public static partial class BossFightIdentifier
    {
        private static ILookup<MapTileCoordinate, BossFight> _matchMapTile;
        private static ILookup<Tuple<Music, Map>, BossFight> _matchMusicAndMap;
        private static ILookup<Music, BossFight> _matchMusic;
        private static ILookup<Map, BossFight> _matchMap;
        private static BossFight[] _remainingDescriptions;

        static BossFightIdentifier()
        {
            // Build dictionaries here.
            var specifiedMapTile = new List<BossFight>();
            var specifiedMusicAndMap = new List<BossFight>();
            var specifiedMusicOnly = new List<BossFight>();
            var specifiedMapOnly = new List<BossFight>();
            var specifiedNeither = new List<BossFight>();
            
            foreach (var bossFight in BossFight.GetBossFights())
            {
                if (bossFight == null || bossFight == BossFight.UNKNOWN) continue;

                if (bossFight.mapTile != null)
                {
                    specifiedMapTile.Add(bossFight);
                    continue;
                }

                if (bossFight.music != null)
                {
                    if (bossFight.map != null) specifiedMusicAndMap.Add(bossFight);
                    else specifiedMusicOnly.Add(bossFight);
                }
                else
                {
                    if (bossFight.map != null) specifiedMapOnly.Add(bossFight);
                    else specifiedNeither.Add(bossFight);
                }
            }

            _matchMapTile = specifiedMapTile.ToLookup(bossFight => bossFight.mapTile.Value, bossFight => bossFight);
            _matchMusicAndMap = specifiedMusicAndMap.ToLookup(bossFight => new Tuple<Music, Map>(bossFight.music.Value, bossFight.map.Value), bossFight => bossFight);
            _matchMusic = specifiedMusicOnly.ToLookup(bossFight => bossFight.music.Value, bossFight => bossFight);
            _matchMap = specifiedMapOnly.ToLookup(bossFight => bossFight.map.Value, bossFight => bossFight);
            _remainingDescriptions = specifiedNeither.ToArray();
        }

        private static bool Matches(BossFight bossFight, HashSet<Boss> startingBosses, Music music, Map map, MapTileCoordinate mapTile)
        {
            if (bossFight.startingBosses != null && !bossFight.startingBosses.SetEquals(startingBosses)) return false;
            if (bossFight.extraCondition == null) return true;
            return bossFight.extraCondition(startingBosses, music, map, mapTile);
        }

        public static BossFight IdentifyBossFight(MemorySnapshot snapshot)
        {
            Music? music_ = StaticData.GetMusic(snapshot.musicid);
            Map? map_ = StaticData.GetMap(snapshot.mapid);
            if (music_ == null || map_ == null) return BossFight.UNKNOWN;

            var music = music_.Value;
            var map = map_.Value;
            var mapTile = snapshot.mapTile;
            var startingBosses = new HashSet<Boss>(snapshot.bossList
                .Select(bossStats => StaticData.GetBoss(bossStats.id))
                .Where(boss => boss != null)
                .Select(boss => boss.Value)
            );

            foreach (var bossFight in _matchMapTile[mapTile])
            {
                if ((bossFight.music == null || music == bossFight.music) &&
                    (bossFight.map == null || map == bossFight.map) &&
                    Matches(bossFight, startingBosses, music, map, mapTile)) return bossFight;
            }

            foreach (var bossFight in _matchMusicAndMap[new Tuple<Music, Map>(music, map)])
            {
                if (Matches(bossFight, startingBosses, music, map, mapTile)) return bossFight;
            }

            foreach (var bossFight in _matchMusic[music])
            {
                if (Matches(bossFight, startingBosses, music, map, mapTile)) return bossFight;
            }

            foreach (var bossFight in _matchMap[map])
            {
                if (Matches(bossFight, startingBosses, music, map, mapTile)) return bossFight;
            }

            return _remainingDescriptions.FirstOrDefault(bossFight => Matches(bossFight, startingBosses, music, map, mapTile)) ?? BossFight.UNKNOWN;
        }

    }
}
