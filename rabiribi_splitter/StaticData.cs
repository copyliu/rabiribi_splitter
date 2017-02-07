using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rabiribi_splitter
{
    public static class StaticData
    {
        public static Dictionary<int, string> BossNames = new Dictionary<int, string>()
        {
            {1009, "Cocoa"},
            {1011, "Rumi"},
            {1012, "Ashuri"},
            {1013, "Rita"},
            {1014, "Ribbon"},
            {1015, "Cocoa"},
            {1018, "Cicini"},
            {1020, "Saya"},
            {1021, "Syaro"},
            {1022, "Pandora"},
            {1023, "Nieve"},
            {1024, "Nixie"},
            {1025, "Aruraune"},
            {1030, "Seana"},
            {1031, "Lilith"},
            {1032, "Vanilla"},
            {1033, "Chocolate"},
            {1035, "Illusion Alius"},
            {1036, "Pink Kotri"},
            {1037, "Noah 1"},
            {1038, "Irisu"},
            {1039, "Miriam"},
            {1043, "Miru"},
            {1053, "Noah 3"},
            {1054, "Keke Bunny"},


        };

        public static string[] MapNames = new string[]
        {
            "Southern Woodland",
            "Western Coast",
            "Island Core",
            "Northern Tundra",
            "Eastern Highlands",
            "Rabi Rabi Town",
            "Plurkwood",
            "Subterranean Area",
            "Warp Destination",
            "System Interior",
        };

        public static int[][] MapBoss = new int[][]
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

        public static string[] MusicNames = new[]
        {
            "-NO MUSIC-",
            "ADVENTURE STARTS HERE",
            "SPECTRAL CAVE",
            "FORGOTTEN CAVE",
            "UNDERWATER AMBIENT",
            "LIBRARY AMBIENT",
            "FORGOTTEN CAVE II",
            "STARTING FOREST NIGHT",
            "BOUNCE BOUNCE",
            "RABI RABI BEACH",
            "PANDORA'S PALACE",
            "RABI RABI RAVINE",
            "HOME SWEET HOME",
            "RABI RABI PARK",
            "INSIDE UPRPRC",
            "SKY ISLAND TOWN",
            "WINTER WONDERLAND",
            "CYBERSPACE.EXE",
            "EVERNIGHT PEAK",
            "EXOTIC LABORATORY",
            "GOLDEN RIVERBANK",
            "FLOATING GRAVEYARD",
            "SYSTEM INTERIOR II",
            "AURORA PALACE",
            "SPEICHER GALERIE",
            "DEEP UNDER THE SEA",
            "SKY-HIGH BRIDGE",
            "WARP DESTINATION",
            "VOLCANIC CANERNS",
            "PLURKWOOD",
            "ANOTHER D",
            "ICY SUMMIT",
            "PREPARE EVENT",
            "MIDBOSS BATTLE",
            "MIDSTREAM JAM",
            "MIRIAM'S SHOP",
            "BUNNY PANIC!!!",
            "THE TRUTH NEVER SPOKEN",
            "BRAWL BREAKS VER.2",
            "BRAWL BREAKS",
            "SANDBAG MINI GAME",
            "STAFF ROLL",
            "RFN - III",
            "NO REMORSE",
            "GET ON WITH IT",
            "THEME OF RABI-RIBI 8BIT",
            "THEME OF RABI-RIBI",
            "FULL ON COMBAT",
            "HI-TECH DUEL",
            "UNFAMILIAR PLACE",
            "UNFAMILIAR PLACE AGAIN",
            "KITTY ATTACK",
            "M.R.",
            "MAIN MENU",
            "SUDDEN DEATH",
            "RABI RABI RAVINE VER.2",
            "WASTE",
            "ARTBOOK INTRO",
            "RABI-RIBI PIANO TITLE",
            "MISCHIEVOUS MASQUERADE",
        };

        public static int[] BossMusics = new[]
        {
            44,
            38,
            47,
            34,
            51,
            43,
            52,
            37,
            39,
            42,
            48,
            8,
            54
        };


        public static int[] MapAddress =  { 0xA3353C, 0xA57020, 0xA5E0AC, 0xA600AC };
        public static int[] EnenyPtrAddr = { 0x00940EE0, 0x00964A1C, 0x0096BA3C, 0x0096DA3C };
        public static int[] EnenyEnitiyHPOffset = { 0x4c8, 0x4d0,0x4d8, 0x4d8 };
        public static int[] EnenyEnitiyIDOffset = { 0x4e4,0x4ec, 0x4F4, 0x4F4 };
        public static int[] EnenyEntitySize = {0x6F4, 0x6FC, 0x704, 0x704 };
        public static int[] MaxEntityEntry = { 50,50,50 ,50};
        public static int[] MoneyAddress = { 0xD3823C, 0xD5B9FC, 0xD63D2C, 0xD654CC };
        public static string[] VerNames = {"1.65", "1.70","1.71","1.75"};
        public static int[] MusicAddr = 
        {
            0xA46294,
           0xA69D98,
           0xA70E28,
           0x4FAB60
        };

        public static int[] TownMemberAddr = {0xD38934, 0xD5C0F4, 0xD63BC4, 0xD65BC4 };
        public static int[] IGTAddr = { 0xD388E0, 0xD5C0A0, 0xD63B70, 0xD65B70 };
    }
}
