using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rabi_splitter_WPF
{
    public enum Boss
    {
        Cocoa,
        Rumi,
        Ashuri,
        Rita,
        Ribbon,
        Cocoa2,
        Cicini,
        Cicini2,
        Saya,
        Syaro,
        Pandora,
        Nieve,
        Nixie,
        Aruraune,
        Seana,
        Lilith,
        Vanilla,
        Chocolate,
        IllusionAlius,
        Kotri,
        Noah1,
        Irisu,
        Miriam,
        Miru,
        Noah3,
        KekeBunny,
        Cats,
        BigBox,
        RainbowMaid,
        TreasureCrystal,
    }

    public enum Map
    {
        SouthernWoodland,
        WesternCoast,
        IslandCore,
        NorthernTundra,
        EasternHighlands,
        RabiRabiTown,
        Plurkwood,
        SubterraneanArea,
        WarpDestination,
        SystemInterior,
    }

    public enum Music
    {
        NO_MUSIC,
        ADVENTURE_STARTS_HERE,
        SPECTRAL_CAVE,
        FORGOTTEN_CAVE,
        UNDERWATER_AMBIENT,
        LIBRARY_AMBIENT,
        FORGOTTEN_CAVE_II,
        STARTING_FOREST_NIGHT,
        BOUNCE_BOUNCE,
        RABI_RABI_BEACH,
        PANDORAS_PALACE,
        RABI_RABI_RAVINE,
        HOME_SWEET_HOME,
        RABI_RABI_PARK,
        INSIDE_UPRPRC,
        SKY_ISLAND_TOWN,
        WINTER_WONDERLAND,
        CYBERSPACE_EXE,
        EVERNIGHT_PEAK,
        EXOTIC_LABORATORY,
        GOLDEN_RIVERBANK,
        FLOATING_GRAVEYARD,
        SYSTEM_INTERIOR_II,
        AURORA_PALACE,
        SPEICHER_GALERIE,
        DEEP_UNDER_THE_SEA,
        SKY_HIGH_BRIDGE,
        WARP_DESTINATION,
        VOLCANIC_CANERNS,
        PLURKWOOD,
        ANOTHER_D,
        ICY_SUMMIT,
        PREPARE_EVENT,
        MIDBOSS_BATTLE,
        MIDSTREAM_JAM,
        MIRIAMS_SHOP,
        BUNNY_PANIC,
        THE_TRUTH_NEVER_SPOKEN,
        BRAWL_BREAKS_VER_2,
        BRAWL_BREAKS,
        SANDBAG_MINI_GAME,
        STAFF_ROLL,
        RFN_III,
        NO_REMORSE,
        GET_ON_WITH_IT,
        THEME_OF_RABI_RIBI_8BIT,
        THEME_OF_RABI_RIBI,
        FULL_ON_COMBAT,
        HI_TECH_DUEL,
        UNFAMILIAR_PLACE,
        UNFAMILIAR_PLACE_AGAIN,
        KITTY_ATTACK,
        M_R_,
        MAIN_MENU,
        SUDDEN_DEATH,
        RABI_RABI_RAVINE_VER_2,
        WASTE,
        ARTBOOK_INTRO,
        RABI_RIBI_PIANO_TITLE,
        MISCHIEVOUS_MASQUERADE,
        PLAY_ROUGH,
        FINALE,
        ESCAPE,
        HALL_OF_MEMORY,
        LAST_100SEC,

    }

    // A list of tuples (id, enum, string)
    public class IdEnumAssociation<EnumType> : List<Tuple<int, EnumType, string>>
    {
        public void Add(int id, EnumType value, string name)
        {
            Add(new Tuple<int, EnumType, string>(id, value, name));
        }
    }

    // A list of tuples (enum, string)
    public class IndexEnumAssociation<EnumType> : List<Tuple<EnumType, string>>
    {
        public void Add(EnumType value, string name)
        {
            Add(new Tuple<EnumType, string>(value, name));
        }
    }

    public static partial class StaticData {

        private static readonly Dictionary<int, Boss> _getBoss;
        private static readonly Dictionary<int, string> _getBossName;
        private static readonly Dictionary<Boss, string> _getBossFromType;

        private static readonly Map[] _getMap;
        private static readonly string[] _getMapName;
        private static readonly Dictionary<Map, string> _getMapFromType;

        private static readonly Music[] _getMusic;
        private static readonly string[] _getMusicName;
        private static readonly Dictionary<Music, string> _getMusicFromType;

        private static readonly bool[] _isBossMusic;

        static StaticData()
        {
            _getBoss = BossList.ToDictionary(t => t.Item1, t => t.Item2);
            _getBossName = BossList.ToDictionary(t => t.Item1, t => t.Item3);
            _getBossFromType = BossList.ToDictionary(t => t.Item2, t => t.Item3);

            _getMap = MapList.Select(t => t.Item1).ToArray();
            _getMapName = MapList.Select(t => t.Item2).ToArray();
            _getMapFromType = MapList.ToDictionary(t => t.Item1, t => t.Item2);

            _getMusic = MusicList.Select(t => t.Item1).ToArray();
            _getMusicName = MusicList.Select(t => t.Item2).ToArray();
            _getMusicFromType = MusicList.ToDictionary(t => t.Item1, t => t.Item2);

            _isBossMusic = Enumerable.Range(0, _getMusic.Length).Select(i => BossMusics.Contains(_getMusic[i])).ToArray();
        }

        public static Boss? GetBoss(int id) {
            Boss value;
            if (_getBoss.TryGetValue(id, out value)) return value;
            return null;
        }

        public static string GetBossName(int id) {
            string value;
            if (_getBossName.TryGetValue(id, out value)) return value;
            return "";
        }

        public static string GetBossName(Boss? boss) {
            string value;
            if (boss.HasValue && _getBossFromType.TryGetValue(boss.Value, out value)) return value;
            return "";
        }

        public static bool IsBoss(int id)
        {
            return GetBoss(id).HasValue;
        }

        public static Map? GetMap(int id) {
            if (0 <= id && id < _getMap.Length) return _getMap[id];
            return null;
        }

        public static string GetMapName(int id) {
            if (0 <= id && id < _getMapName.Length) return _getMapName[id];
            return "Unknown ID " + id;
        }

        public static string GetMapName(Map? map) {
            string value;
            if (map.HasValue && _getMapFromType.TryGetValue(map.Value, out value)) return value;
            return "";
        }

        public static bool IsValidMap(int id) {
            return 0 <= id && id < _getMap.Length;
        }

        public static Music? GetMusic(int id) {
            if (0 <= id && id < _getMusic.Length) return _getMusic[id];
            return null;
        }

        public static string GetMusicName(int id) {
            if (0 <= id && id < _getMusicName.Length) return _getMusicName[id];
            return "Unknown ID " + id;
        }

        public static string GetMusicName(Music? music) {
            string value;
            if (music.HasValue && _getMusicFromType.TryGetValue(music.Value, out value)) return value;
            return "";
        }

        public static bool IsBossMusic(int musicId)
        {
            return 0 <= musicId && musicId < _isBossMusic.Length && _isBossMusic[musicId];
        }
    }
}
