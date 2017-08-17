using System;
using System.Collections.Generic;
using System.Linq;

namespace Irisu.Memory
{
    public enum Badge
    {
        Health_Plus,
        Health_Surge,
        Mana_Plus,
        Mana_Surge,
        Crisis_Boost,
        Atk_Grow,
        Def_Grow,
        Atk_Trade,
        Def_Trade,
        Arm_Strength,
        Carrot_Boost,
        Weaken,
        Self_Defense,
        Armored,
        Lucky_Seven,
        Hex_Cancel,
        Pure_Love,
        Toxic_Strike,
        Frame_Cancel,
        Health_Wager,
        Mana_Wager,
        Stamina_Plus,
        Blessed,
        Hitbox_Down,
        Cashback,
        Survival,
        Top_Form,
        Tough_Skin,
        Erina_Badge,
        Ribbon_Badge,
        Auto_Trigger,
        Liliths_Gift,
    }
    public enum Item
    {
        Piko_Hammer,
        Air_Jump,
        Sliding_Powder,
        Carrot_Bomb,
        Hour_Glass,
        Speed_Boost,
        Auto_Earrings,
        Ribbon,
        Soul_Heart,
        Rabi_Slippers,
        Bunny_Whirl,
        Quick_Barrette,
        Book_of_Carrot,
        Chaos_Rod,
        Hammer_Wave,
        Hammer_Roll,
        Light_Orb,
        Water_Orb,
        Fire_Orb,
        Nature_Orb,
        P_Hairpin,
        Sunny_Beam,
        Plus_Necklace,
        Cyber_Flower,
        Healing_Staff,
        Max_Bracelet,
        Explode_Shot,
        Air_Dash,
        Bunny_Strike,
        Strange_Box,
        Wall_Jump,
        Spike_Barrier,
        Bunny_Amulet,
        Charge_Ring,
        Carrot_Shooter,
        Super_Carrot,
        Rumi_Donut,
        Rumi_Cake,
        Golden_Carrot,
        Cocoa_Bomb,
    }
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
    }

    // A set of (id, enum, string)
    public class IdEnumAssociation<EnumType> : List<Tuple<int, EnumType, string>>
    {
        public void Add(int id, EnumType value, string name)
        {
            Add(new Tuple<int, EnumType, string>(id, value, name));
        }
    }

    // A set of (enum, string)
    public class IndexEnumAssociation<EnumType> : List<Tuple<EnumType, string>>
    {
        public void Add(EnumType value, string name)
        {
            Add(new Tuple<EnumType, string>(value, name));
        }
    }

    // A set of (exp, string)
    public class ExpDescriptions : List<Tuple<int, string, string>>
    {
        public void Add(int exp, string shortDescript, string description)
        {
            Add(new Tuple<int, string, string>(exp, shortDescript, description));
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

        public static Tuple<int, string, string> GetNextHammerLevel(int exp) {
            int index = 0;
            while (index < HammerLevels.Count && exp >= HammerLevels[index].Item1) ++index;
            return index < HammerLevels.Count ? HammerLevels[index] : null;
        }

        public static Tuple<int, string, string> GetNextRibbonLevel(int exp) {
            int index = 0;
            while (index < RibbonLevels.Count && exp >= RibbonLevels[index].Item1) ++index;
            return index < RibbonLevels.Count ? RibbonLevels[index] : null;
        }

        public static Tuple<int, string, string> GetNextCarrotLevel(int exp) {
            int index = 0;
            while (index < CarrotLevels.Count && exp >= CarrotLevels[index].Item1) ++index;
            return index < CarrotLevels.Count ? CarrotLevels[index] : null;
        }
    }
}
