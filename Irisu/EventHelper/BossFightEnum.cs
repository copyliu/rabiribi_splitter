using Irisu.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Irisu.EventHelper
{
    /// <summary>
    /// enum
    /// </summary>
    public partial class BossFight
    {
        // Do not make these properties public.
        private static int nextAvailableValue = 0;
        private readonly int _value;
        private static BossFight[] _bossFights;

        // Do not make these properties static.
        public readonly string displayName;
        public readonly Music? music;
        public readonly Map? map;
        public readonly MapTileCoordinate? mapTile;
        public readonly HashSet<Boss> startingBosses;
        public readonly Func<HashSet<Boss>, Music, Map, MapTileCoordinate, bool> extraCondition;

        private BossFight(string displayName = null,
                            Music? music = null,
                            Map? map = null,
                            MapTileCoordinate? mapTile = null,
                            Boss[] startingBosses = null,
                            Func<HashSet<Boss>, Music, Map, MapTileCoordinate, bool> extraCondition = null)
        {
            _value = nextAvailableValue++;
            this.displayName = displayName;
            this.music = music;
            this.map = map;
            this.mapTile = mapTile;
            this.startingBosses = (startingBosses != null) ? new HashSet<Boss>(startingBosses) : null;
            this.extraCondition = extraCondition;
        }
        
        public int Value
        {
            get { return _value; }
        }
        
        public override string ToString()
        {
            return displayName;
        }

        private static BossFight[] GetAllBossFights()
        {
            var type = typeof(BossFight);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

            var bossFights = new List<BossFight>();
            foreach (var info in fields)
            {
                var instance = new BossFight();
                var locatedValue = info.GetValue(instance) as BossFight;

                if (locatedValue != null && locatedValue != BossFight.UNKNOWN)
                {
                    bossFights.Add(locatedValue);
                }
            }

            return bossFights.ToArray();
        }

        public static IEnumerable<BossFight> GetBossFights()
        {
            if (_bossFights == null) _bossFights = GetAllBossFights();

            foreach (var bossFight in _bossFights)
            {
                yield return bossFight;
            }
        }

        // Convenience function for BossFightConfig.cs
        private static MapTileCoordinate Tile(int x, int y)
        {
            return new MapTileCoordinate(x, y);
        }

        public override bool Equals(object obj)
        {
            var otherValue = obj as BossFight;
            if (otherValue == null) return false;
            return _value.Equals(otherValue.Value);
        }

        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }
    }
}
