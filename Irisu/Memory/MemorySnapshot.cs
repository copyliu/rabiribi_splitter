using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Irisu.Memory
{
    public static partial class StaticData
    {
        public static IdEnumAssociation<Boss> BossList = new IdEnumAssociation<Boss>
        {
            {1009, Boss.Cocoa, "Cocoa"},
            {1011, Boss.Rumi, "Rumi"},
            {1012, Boss.Ashuri, "Ashuri"},
            {1013, Boss.Rita, "Rita"},
            {1014, Boss.Ribbon, "Ribbon"},
            {1015, Boss.Cocoa2, "Cocoa2"},
            {1016, Boss.Cicini, "Cicini"},
            {1018, Boss.Cicini2, "Cicini2"},
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
            {1036, Boss.Kotri, "Kotri"},
            {1037, Boss.Noah1, "Noah 1"},
            {1038, Boss.Irisu, "Irisu"},
            {1039, Boss.Miriam, "Miriam"},
            {1043, Boss.Miru, "Miru"},
            {1053, Boss.Noah3, "Noah 3"},
            {1054, Boss.KekeBunny, "Keke Bunny"},

            {1056, Boss.Cats, "Plurkwood Cats"},
            {1133, Boss.BigBox, "Big Box"},
            {1136, Boss.RainbowMaid, "Robot Maid"},
            {1143, Boss.TreasureCrystal, "Treasure Crystal"},
        };

        public static IndexEnumAssociation<Map> MapList = new IndexEnumAssociation<Map>
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

        public static IndexEnumAssociation<Music> MusicList = new IndexEnumAssociation<Music>
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

        public static Music[] BossMusics = new[]
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

        public static ExpDescriptions HammerLevels = new ExpDescriptions
        {
            {600, "+Rang", "Attack Range Increase"},
            {1000, "UpDr", "Up-drill"},
            {1650, "Quad", "Quad-combo"},
            {2050, "Spd1", "Combo Speed Increase (Lv.1)"},
            {2500, "DwnDr", "Down-drill"},
            {2750, "Dmg1", "Hammer Drill Damage Increase (Lv.1)"},// / Up-drill Exp Increase"},
            {3100, "Penta", "Penta-combo (Ground-drill)"},
            {3750, "Spd2", "Combo Speed Increase (Lv.2)"},
            {5500, "-SP", "SP Usage Reduced"},
            {6450, "Spd3", "Combo Speed Increase (Lv.3)"},
            {7500, "Dmg2", "Hammer Drill Damage Increase (Lv.2)"},// / Up-drill/Down-drill Exp Increase"},
        };

        public static ExpDescriptions RibbonLevels = new ExpDescriptions
        {
            {100, "CSpd1", "Charge Speed Increase (Lv.1)"},
            {600, "CSpd2", "Charge Speed Increase (Lv.2)"},
            {1100, "BSpd1", "Boost Attack Speed Increase (Lv.1)"},
            {1650, "CSpd3", "Charge Speed Increase (Lv.3)"},
            {2800, "BSpd2", "Boost Attack Speed Increase (Lv.2)"},
            {3300, "CSpd4", "Charge Speed Increase (Lv.4)"},
            {3800, "BGag1", "Boost Gauge Charges Faster (Lv.1)"},
            {4500, "BSpd3", "Boost Attack Speed Increase (Lv.3)"},
            {5800, "BGag2", "Boost Gauge Charges Faster (Lv.2)"},
            {7000, "BGag3", "Boost Gauge Charges Faster (Lv.3)"},
        };

        public static ExpDescriptions CarrotLevels = new ExpDescriptions
        {
            {7, "Del1", "Shorter Delay (Lv.1)"},
            {25, "Dmg1", "Damage Increase (Lv.1)"},
            {50, "Del2", "Shorter Delay (Lv.2)"},
            {100, "Rad", "Effect Radius Increase"},
            {175, "Dmg2", "Damage Increase (Lv.2)"},// / Exp Increase"},
            {275, "Del3", "Shorter Delay (Lv.3)"},
            {500, "Dmg3", "Damage Increase (Lv.3)"},// / Exp Increase"},
        };

        public static int[] MapAddress = { 0xA3353C, 0xA57020, 0xA5E0AC, 0xA600AC };
        public static int[] EnenyPtrAddr = { 0x00940EE0, 0x00964A1C, 0x0096BA3C, 0x0096DA3C };
        public static int[] EnenyEnitiyHPOffset = { 0x4c8, 0x4d0, 0x4d8, 0x4d8 };
        public static int[] EnenyEnitiyMaxHPOffset = { 0, 0, 0, 0x4e8 };
        public static int[] EnenyEnitiyIDOffset = { 0x4e4, 0x4ec, 0x4F4, 0x4F4 };
        public static int[] EnenyEntitySize = { 0x6F4, 0x6FC, 0x704, 0x704 };
        public static int[] EnenyEnitiyIsActiveOffset = { 0, 0, 0, 0x674 };
        public static int[] EnenyEnitiyAnimationOffset = { 0, 0, 0, 0x678 };
        public static int[] MaxEntityEntry = { 50, 50, 50, 50 };
        public static int[] MoneyAddress = { 0xD3823C, 0xD5B9FC, 0xD63D2C, 0xD654CC };
        public static string[] VerNames = { "1.65", "1.70", "1.71", "1.75" };
        public static int[] MusicAddr =
        {
            0xA46294,
            0xA69D98,
            0xA70E28,
            0x4FAB60
        };

        public static int[] TownMemberAddr = { 0xD38934, 0xD5C0F4, 0xD63BC4, 0xD65BC4 };
        public static int[] IGTAddr = { 0xD388E0, 0xD5C0A0, 0xD63B70, 0xD65B70 };

        public static int[] BlacknessAddr = { 0, 0, 0, 0xA723B0 };
        public static int[] PlaytimeAddr = { 0, 0, 0, 0xD642D8 };
    }
    public struct MapTileCoordinate
    {
        public readonly int x;
        public readonly int y;

        public MapTileCoordinate(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static MapTileCoordinate FromWorldPosition(int mapid, float px, float py)
        {
            // Note: a game-tile is 64x64
            // A map-tile is 1280x720. (20 x 11.25 game tiles)
            int x = (int)(px / 1280) + mapid * 25;
            int y = (int)(py / 720);

            return new MapTileCoordinate(x, y);
        }

        public override string ToString()
        {
            return $"({x}, {y})";
        }

        #region Equals, Hashcode
        // override object.Equals
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (MapTileCoordinate)obj;
            return x == other.x && y == other.y;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return x.GetHashCode() * 31 + y.GetHashCode();
        }
        #endregion
    }

    public struct BossStats
    {
        public int entityArrayIndex;
        public int id;
        public Boss type;
        public int hp;
        public int maxHp;
    }

    public struct MemorySnapshot
    {
        public readonly int t_playtime;
        public readonly int playtime;
        public readonly int blackness;
        public readonly int mapid;
        public readonly int musicid;
        public readonly int money;
        public readonly int hp;
        public readonly int maxhp;

        public readonly int currentSprite;
        public readonly int actionFrame;

        public readonly float amulet;
        public readonly int boost;
        public readonly float mana;
        public readonly int stamina;

        public readonly float px;
        public readonly float py;
        public readonly MapTileCoordinate mapTile;

        public readonly int entityArrayPtr;
        public readonly int entityArraySize;
        public readonly int nActiveEntities;
        public readonly List<BossStats> bossList;

        public readonly int carrotXp;
        public readonly int hammerXp;
        public readonly int ribbonXp;
        public readonly float itemPercent;

        public readonly Tuple<int, string, string> nextHammer;
        public readonly Tuple<int, string, string> nextRibbon;
        public readonly Tuple<int, string, string> nextCarrot;

        public readonly int nAttackUps;
        public readonly int nHpUps;
        public readonly int nManaUps;
        public readonly int nPackUps;
        public readonly int nRegenUps;

        public readonly int minimapPosition;

        

        public MemorySnapshot(MemoryHelper memoryHelper, int veridx) 
        {
            t_playtime = memoryHelper.GetMemoryValue<int>(StaticData.IGTAddr[veridx]);
            playtime = memoryHelper.GetMemoryValue<int>(StaticData.PlaytimeAddr[veridx]);
            blackness = memoryHelper.GetMemoryValue<int>(StaticData.BlacknessAddr[veridx]);

            mapid = memoryHelper.GetMemoryValue<int>(StaticData.MapAddress[veridx]);
            musicid = memoryHelper.GetMemoryValue<int>(StaticData.MusicAddr[veridx]);
            money = memoryHelper.GetMemoryValue<int>(StaticData.MoneyAddress[veridx]);

            carrotXp = memoryHelper.GetMemoryValue<int>(0xD654BC);
            hammerXp = memoryHelper.GetMemoryValue<int>(0xD654B4);
            ribbonXp = memoryHelper.GetMemoryValue<int>(0xD654B8);
            itemPercent = memoryHelper.GetMemoryValue<float>(0xA730E8);
            nextHammer = StaticData.GetNextHammerLevel(hammerXp);
            nextRibbon = StaticData.GetNextRibbonLevel(ribbonXp);
            nextCarrot = StaticData.GetNextCarrotLevel(carrotXp);

            minimapPosition = memoryHelper.GetMemoryValue<int>(0xA72E08);

            nAttackUps = CountItems(memoryHelper, 0xD6352C, 0xD63628);
            nHpUps = CountItems(memoryHelper, 0xD6342C, 0xD63528);
            nManaUps = CountItems(memoryHelper, 0xD6362C, 0xD63728);
            nPackUps = CountItems(memoryHelper, 0xD6382C, 0xD63928);
            nRegenUps = CountItems(memoryHelper, 0xD6372C, 0xD63828);

            entityArrayPtr = memoryHelper.GetMemoryValue<int>(StaticData.EnenyPtrAddr[veridx]);

            hp = memoryHelper.GetMemoryValue<int>(entityArrayPtr + 0x4D8, false);
            maxhp = memoryHelper.GetMemoryValue<int>(entityArrayPtr + 0x4E8, false);

            currentSprite = memoryHelper.GetMemoryValue<int>(entityArrayPtr + 0x654, false);
            actionFrame = memoryHelper.GetMemoryValue<int>(entityArrayPtr + 0x660, false);

            amulet = memoryHelper.GetMemoryValue<float>(entityArrayPtr + 0x52C, false);
            boost = memoryHelper.GetMemoryValue<int>(entityArrayPtr + 0x5DC, false);
            mana = memoryHelper.GetMemoryValue<float>(entityArrayPtr + 0x6B8, false);
            stamina = memoryHelper.GetMemoryValue<int>(entityArrayPtr + 0x5B4, false);

            px = memoryHelper.GetMemoryValue<float>(entityArrayPtr + 0xC, false);
            py = memoryHelper.GetMemoryValue<float>(entityArrayPtr + 0x10, false);
            mapTile = MapTileCoordinate.FromWorldPosition(mapid, px, py);

            // Read Entity Array and Search for boss data
            bossList = new List<BossStats>();
            nActiveEntities = 0;
            entityArraySize = 4;
            int entitySize = StaticData.EnenyEntitySize[veridx];
            int currArrayPtr = entityArrayPtr + entitySize * 4;
            for (int i = 0; i < 500; ++i)
            {
                // (Hard limit of reading 500 entries)
                int entityId = memoryHelper.GetMemoryValue<int>(
                    currArrayPtr + StaticData.EnenyEnitiyIDOffset[veridx], false);
                int entityMaxHp = memoryHelper.GetMemoryValue<int>(
                    currArrayPtr + StaticData.EnenyEnitiyMaxHPOffset[veridx], false);

                if (entityId == 0 && entityMaxHp == 0) break;

                int activeFlag = memoryHelper.GetMemoryValue<int>(
                    currArrayPtr + StaticData.EnenyEnitiyIsActiveOffset[veridx], false);
                int animationState = memoryHelper.GetMemoryValue<int>(
                    currArrayPtr + StaticData.EnenyEnitiyAnimationOffset[veridx], false);

                bool isAlive = activeFlag == 1 && animationState >= 0;

                if (isAlive && StaticData.IsBoss(entityId))
                {
                    BossStats boss;
                    boss.entityArrayIndex = entityArraySize;
                    boss.id = entityId;
                    boss.hp = memoryHelper.GetMemoryValue<int>(currArrayPtr + StaticData.EnenyEnitiyHPOffset[veridx], false);
                    boss.type = StaticData.GetBoss(entityId).Value;
                    boss.maxHp = entityMaxHp;

                    bossList.Add(boss);
                }

                currArrayPtr += entitySize;

                if (isAlive) ++nActiveEntities;
                ++entityArraySize;
            }
        }

        private static int CountItems(MemoryHelper memoryHelper, int addrFirst, int addrLast)
        {
            int count = 0;
            for (int addr = addrFirst; addr <= addrLast; ++addr)
            {
                count += memoryHelper.GetMemoryValue<int>(addr) == 1 ? 1 : 0;
            }
            return count;
        }

        public bool CurrentMapIs(Map? map)
        {
            return StaticData.GetMap(mapid) == map;
        }

        public bool CurrentMusicIs(Music? music)
        {
            return StaticData.GetMusic(musicid) == music;
        }

        public string GetCurrentAction()
        {
            if (actionFrame == 0) return "None";
            int actionType = actionFrame / 100;
            switch (actionType)
            {
                case 1: return "Carrot Bomb";
                case 2: return "Bunny Whirl";
                case 4: return "Hammer Roll";
                case 7: return "Hammer Combo 1";
                case 8: return "Hammer Combo 2";
                case 0: return "Hammer Combo 3";
                case 9: return "Hammer Combo 4";
                case 10: return "Hammer Drill";
                case 13: return "Air Dash";
                case 14: return "Down Drill";
                case 100: return "Up Drill";
                case 110: return "Walking Hammer";
                case 120: return "Bunny Strike";
                case 150: return "Amulet";
                default: return "Unknown";
            }
        }

        public bool IsDeathSprite()
        {
            return currentSprite == 31;
        }

        public string GetCurrentSprite()
        {
            switch (currentSprite)
            {
                case 0: return "Idle";
                case 1: return "Walking";
                case 2: return "Jump";
                case 3: return "Air Jump";
                case 4: return "Falling";
                case 5: return "Slide";
                case 6: return "Carrot Bomb";
                case 7: return "Hurt";
                case 9: return "Bunny Whirl";
                case 15: return "Hammer Roll";
                case 20: return "Hammer Combo 1/4";
                case 21: return "Hammer Combo 2";
                case 10: return "Hammer Combo 3";
                case 22: return "Hammer Drill";
                case 23: return "Air Dash";
                case 24: return "Down Drill";
                case 25: return "Up Drill";
                case 26: return "Walking Hammer";
                case 27: return "Amulet";
                case 31: return "Death";
                case 32: return "Bunny Strike";
                default: return "Unknown";
            }
        }
    }
}
