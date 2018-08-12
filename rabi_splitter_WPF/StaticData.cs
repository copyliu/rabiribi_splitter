using System.Collections.Generic;

namespace rabi_splitter_WPF
{
    public static partial class StaticData
    {
        public static readonly IdEnumAssociation<Boss> BossList = new IdEnumAssociation<Boss>
        {
            {1009, Boss.Cocoa, "Cocoa"}, // Prologue Cocoa
            {1011, Boss.Rumi, "Rumi"},
            {1012, Boss.Ashuri, "Ashuri"},
            {1013, Boss.Rita, "Rita"},
            {1014, Boss.Ribbon, "Ribbon"},
            {1015, Boss.Cocoa2, "Cocoa2"}, // Cocoa2 boss fight / in town/cutscenes
            {1016, Boss.Cicini, "Cicini"}, // Cicini boss fight
            {1018, Boss.Cicini2, "Cicini2"}, // Cicini in town/cutscenes
            {1020, Boss.Saya, "Saya"},
            {1021, Boss.Syaro, "Syaro"},
            {1022, Boss.Pandora, "Pandora"},
            {1023, Boss.Nieve, "Nieve"},
            {1024, Boss.Nixie, "Nixie"},
            {1025, Boss.Aruraune, "Aruraune"},
            {1030, Boss.Seana, "Seana"},
            {1031, Boss.Lilith, "Lilith"},
            {1032, Boss.Vanilla, "Vanilla"},
            {1033, Boss.Chocolate, "Chocolate"},
            {1035, Boss.IllusionAlius, "Illusion Alius"},
            {1036, Boss.Kotri, "Kotri"}, // All three kotris
            {1037, Boss.Noah1, "Noah 1"},
            {1038, Boss.Irisu, "Irisu"},
            {1039, Boss.Miriam, "Miriam"},
            {1043, Boss.Miru, "Miru"},
            {1053, Boss.Noah3, "Noah 3"},
            {1054, Boss.KekeBunny, "Keke Bunny"},

            //{1056, Boss.Cats, "Plurkwood Cats"},
            //{1133, Boss.BigBox, "Big Box"},
            //{1136, Boss.RainbowMaid, "Robot Maid"},
            //{1143, Boss.TreasureCrystal, "Treasure Crystal"},
        };

        public static readonly IndexEnumAssociation<Map> MapList = new IndexEnumAssociation<Map>
        {
            {Map.SouthernWoodland, "Southern Woodland"},
            {Map.WesternCoast, "Western Coast"},
            {Map.IslandCore, "Island Core"},
            {Map.NorthernTundra, "Northern Tundra"},
            {Map.EasternHighlands, "Eastern Highlands"},
            {Map.RabiRabiTown, "Rabi Rabi Town"},
            {Map.Plurkwood, "Plurkwood"},
            {Map.SubterraneanArea, "Subterranean Area"},
            {Map.WarpDestination, "Warp Destination"},
            {Map.SystemInterior, "System Interior"},
        };

        public static readonly int[][] MapBoss = new int[][]
        {
            new[] {1011, 1009, 1025, 1014, 1018},
            new[] {1036, 1038, 1031, 1022, 1012},
            new[] {1032, 1036, 1030, 1033},
            new[] {1024, 1023, 1013, 1030},
            new[] {1012, 1020,},
            new int[0],
            new[] {1054},
            new[] {1036, 1039},
            new[] {1037, 1053, 1035, 1043},
            new[] {1021},

        };

        public static readonly IndexEnumAssociation<Music> MusicList = new IndexEnumAssociation<Music>
        {
            {Music.NO_MUSIC, "-NO MUSIC-"},
            {Music.ADVENTURE_STARTS_HERE, "ADVENTURE STARTS HERE"},
            {Music.SPECTRAL_CAVE, "SPECTRAL CAVE"},
            {Music.FORGOTTEN_CAVE, "FORGOTTEN CAVE"},
            {Music.UNDERWATER_AMBIENT, "UNDERWATER AMBIENT"},
            {Music.LIBRARY_AMBIENT, "LIBRARY AMBIENT"},
            {Music.FORGOTTEN_CAVE_II, "FORGOTTEN CAVE II"},
            {Music.STARTING_FOREST_NIGHT, "STARTING FOREST NIGHT"},
            {Music.BOUNCE_BOUNCE, "BOUNCE BOUNCE"},
            {Music.RABI_RABI_BEACH, "RABI RABI BEACH"},
            {Music.PANDORAS_PALACE, "PANDORA'S PALACE"},
            {Music.RABI_RABI_RAVINE, "RABI RABI RAVINE"},
            {Music.HOME_SWEET_HOME, "HOME SWEET HOME"},
            {Music.RABI_RABI_PARK, "RABI RABI PARK"},
            {Music.INSIDE_UPRPRC, "INSIDE UPRPRC"},
            {Music.SKY_ISLAND_TOWN, "SKY ISLAND TOWN"},
            {Music.WINTER_WONDERLAND, "WINTER WONDERLAND"},
            {Music.CYBERSPACE_EXE, "CYBERSPACE.EXE"},
            {Music.EVERNIGHT_PEAK, "EVERNIGHT PEAK"},
            {Music.EXOTIC_LABORATORY, "EXOTIC LABORATORY"},
            {Music.GOLDEN_RIVERBANK, "GOLDEN RIVERBANK"},
            {Music.FLOATING_GRAVEYARD, "FLOATING GRAVEYARD"},
            {Music.SYSTEM_INTERIOR_II, "SYSTEM INTERIOR II"},
            {Music.AURORA_PALACE, "AURORA PALACE"},
            {Music.SPEICHER_GALERIE, "SPEICHER GALERIE"},
            {Music.DEEP_UNDER_THE_SEA, "DEEP UNDER THE SEA"},
            {Music.SKY_HIGH_BRIDGE, "SKY-HIGH BRIDGE"},
            {Music.WARP_DESTINATION, "WARP DESTINATION"},
            {Music.VOLCANIC_CANERNS, "VOLCANIC CANERNS"},
            {Music.PLURKWOOD, "PLURKWOOD"},
            {Music.ANOTHER_D, "ANOTHER D"},
            {Music.ICY_SUMMIT, "ICY SUMMIT"},
            {Music.PREPARE_EVENT, "PREPARE EVENT"},
            {Music.MIDBOSS_BATTLE, "MIDBOSS BATTLE"},
            {Music.MIDSTREAM_JAM, "MIDSTREAM JAM"},
            {Music.MIRIAMS_SHOP, "MIRIAM'S SHOP"},
            {Music.BUNNY_PANIC, "BUNNY PANIC!!!"},
            {Music.THE_TRUTH_NEVER_SPOKEN, "THE TRUTH NEVER SPOKEN"},
            {Music.BRAWL_BREAKS_VER_2, "BRAWL BREAKS VER.2"},
            {Music.BRAWL_BREAKS, "BRAWL BREAKS"},
            {Music.SANDBAG_MINI_GAME, "SANDBAG MINI GAME"},
            {Music.STAFF_ROLL, "STAFF ROLL"},
            {Music.RFN_III, "RFN - III"},
            {Music.NO_REMORSE, "NO REMORSE"},
            {Music.GET_ON_WITH_IT, "GET ON WITH IT"},
            {Music.THEME_OF_RABI_RIBI_8BIT, "THEME OF RABI-RIBI 8BIT"},
            {Music.THEME_OF_RABI_RIBI, "THEME OF RABI-RIBI"},
            {Music.FULL_ON_COMBAT, "FULL ON COMBAT"},
            {Music.HI_TECH_DUEL, "HI-TECH DUEL"},
            {Music.UNFAMILIAR_PLACE, "UNFAMILIAR PLACE"},
            {Music.UNFAMILIAR_PLACE_AGAIN, "UNFAMILIAR PLACE AGAIN"},
            {Music.KITTY_ATTACK, "KITTY ATTACK"},
            {Music.M_R_, "M.R."},
            {Music.MAIN_MENU, "MAIN MENU"},
            {Music.SUDDEN_DEATH, "SUDDEN DEATH"},
            {Music.RABI_RABI_RAVINE_VER_2, "RABI RABI RAVINE VER.2"},
            {Music.WASTE, "WASTE"},
            {Music.ARTBOOK_INTRO, "ARTBOOK INTRO"},
            {Music.RABI_RIBI_PIANO_TITLE, "RABI-RIBI PIANO TITLE"},
            {Music.MISCHIEVOUS_MASQUERADE, "MISCHIEVOUS MASQUERADE"},
        };

        public static readonly Music[] BossMusics = new[]
        {
            Music.BOUNCE_BOUNCE,
            Music.MIDSTREAM_JAM,
            Music.THE_TRUTH_NEVER_SPOKEN,
            Music.BRAWL_BREAKS_VER_2,
            Music.BRAWL_BREAKS,
            Music.RFN_III,
            Music.NO_REMORSE,
            Music.GET_ON_WITH_IT,
            Music.FULL_ON_COMBAT,
            Music.HI_TECH_DUEL,
            Music.KITTY_ATTACK,
            Music.M_R_,
            Music.SUDDEN_DEATH,
            Music.MISCHIEVOUS_MASQUERADE,
        };

        public static int[] MapAddress = {0xA3353C, 0xA57020, 0xA5E0AC, 0xA600AC, 0x9FD3BC, 0x9FE5CC, 0x9FF3BC, 0xA233BC, 0xA233BC, 0xA233BC, 0xA243BC };
        public static int[] EnemyPtrAddr = {0x00940EE0, 0x00964A1C, 0x0096BA3C, 0x0096DA3C, 0x012E1AF8, 0x012E2B08, 0x012E3B08, 0x01307B1C, 0x01307B1C, 0x01307B1C, 0x01308B20 };
        public static int[] EnemyEntityHPOffset = {0x4c8, 0x4d0, 0x4d8, 0x4d8, 0x4d8, 0x4d8, 0x4d8, 0x4d8, 0x4d8, 0x4d8, 0x4d8 };
        public static int[] EnemyEntityIDOffset = {0x4e4, 0x4ec, 0x4F4, 0x4F4, 0x4F4, 0x4F4, 0x4F4, 0x4F4, 0x4F4, 0x4F4, 0x4F4 };
        public static int[] EnemyEntityXPositionOffset = {0, 0, 0, 0xC, 0xC, 0xC, 0xC, 0xC, 0xC, 0xC, 0xC };
        public static int[] EnemyEntityYPositionOffset = {0, 0, 0, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10 };
        public static int[] EnemyEntitySize = {0x6F4, 0x6FC, 0x704, 0x704,0x704,0x704, 0x704, 0x704, 0x704, 0x704, 0x704 };
        public static int[] MoneyAddress = {0xD3823C, 0xD5B9FC, 0xD63D2C, 0xD654CC, 0x12D898C, 0x12D999C, 0x12DA99C, 0x12FE9B4, 0x12FE9B4, 0x12FE9B4, 0x12FF9B4 };
        public static string[] VerNames = { "1.65", "1.70", "1.71", "1.75", "1.8","1.85","1.88","1.90","1.91", "1.92", "1.92f" };
        
        public static int[] MusicAddr = {0xA46294, 0xA69D98, 0xA70E28, 0x4FAB60, 0x580C94, 0x581C94, 0x582C94, 0x5A6C94, 0x5A6C94, 0x5A6C94, 0x5A7C94 };

        public static int[] TownMemberAddr = {0xD38934, 0xD5C0F4, 0xD63BC4, 0xD65BC4, 0x12D9084, 0x12DA094, 0x12DB094, 0x12FF0AC, 0x12FF0AC, 0x12FF0AC, 0x13000AC };
        public static int[] IGTAddr = {0xD388E0, 0xD5C0A0, 0xD63B70, 0xD65B70, 0x12D9030, 0x12DA040, 0x12DB040, 0x12FF058, 0x12FF058, 0x12FF058, 0x1300058 };

        public static int[] BlacknessAddr = {0, 0, 0, 0xA723B0, 0x133F68C, 0x13406A4, 0x13416B4, 0x13656C4, 0x13656C4, 0x13656C4, 0x13666CC };
        public static int[] PlaytimeAddr = {0, 0, 0, 0xD642D8, 0x12D7798, 0x12D87A8, 0x12D97A8, 0x12FD7C0, 0x12FD7C0, 0x12FD7C0, 0x12FE7C0 };
    }
}
