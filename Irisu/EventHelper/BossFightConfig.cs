using Irisu.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Irisu.EventHelper
{
    // The descriptions of all the boss fights are listed here.
    // Fields can be omitted to keep them unspecified.
    public partial class BossFight
    {
        public static BossFight UNKNOWN =
            new BossFight (
                displayName: "UNKNOWN"
            );

        public static BossFight Cocoa1 =
            new BossFight (
                displayName: "Cocoa1",
                music: Music.GET_ON_WITH_IT,
                map: Map.SouthernWoodland,
                startingBosses: new[] {Boss.Cocoa}
            );

        public static BossFight Ribbon =
            new BossFight (
                displayName: "Ribbon",
                music: Music.GET_ON_WITH_IT,
                map: Map.SouthernWoodland,
                startingBosses: new[] {Boss.Ribbon}
            );

        public static BossFight Ashuri1 =
            new BossFight (
                displayName: "Ashuri1",
                music: Music.GET_ON_WITH_IT,
                map: Map.WesternCoast,
                startingBosses: new[] {Boss.Ashuri}
            );

        public static BossFight Ashuri2 =
            new BossFight (
                displayName: "Ashuri2",
                music: Music.BRAWL_BREAKS_VER_2,
                map: Map.EasternHighlands,
                startingBosses: new[] {Boss.Ashuri}
            );

        public static BossFight Rita =
            new BossFight (
                displayName: "Rita",
                music: Music.MIDSTREAM_JAM,
                mapTile: Tile(86, 10),
                startingBosses: new[] { Boss.Rita }
            );

        public static BossFight Cicini =
            new BossFight (
                displayName: "Cicini",
                music: Music.HI_TECH_DUEL,
                mapTile: Tile(22, 14),
                startingBosses: new[] { Boss.Cicini }
            );

        public static BossFight Cocoa2 =
            new BossFight (
                displayName: "Cocoa2",
                music: Music.GET_ON_WITH_IT,
                mapTile: Tile(14, 10),
                startingBosses: new[] { Boss.Cocoa2 }
            );

        public static BossFight BigBox =
            new BossFight (
                displayName: "Big Box",
                music: Music.MIDBOSS_BATTLE,
                map: Map.EasternHighlands,
                startingBosses: new[] {Boss.BigBox}
            );

        public static BossFight RainbowMaid =
            new BossFight (
                displayName: "Rainbow Maid",
                music: Music.MIDBOSS_BATTLE,
                map: Map.EasternHighlands,
                startingBosses: new[] {Boss.RainbowMaid}
            );

        public static BossFight Seana1 =
            new BossFight (
                displayName: "Seana1",
                music: Music.KITTY_ATTACK,
                map: Map.NorthernTundra,
                startingBosses: new[] {Boss.Seana}
            );

        public static BossFight Seana2 =
            new BossFight (
                displayName: "Seana2",
                music: Music.BOUNCE_BOUNCE,
                map: Map.IslandCore,
                startingBosses: new[] {Boss.Seana}
            );

        public static BossFight Kotri1 =
            new BossFight (
                displayName: "Kotri1",
                music: Music.BRAWL_BREAKS,
                map: Map.IslandCore,
                startingBosses: new[] {Boss.Kotri}
            );

        public static BossFight Kotri2 =
            new BossFight (
                displayName: "Kotri2",
                music: Music.BRAWL_BREAKS,
                map: Map.WesternCoast,
                startingBosses: new[] {Boss.Kotri}
            );

        public static BossFight Kotri3 =
            new BossFight (
                displayName: "Kotri3",
                music: Music.BRAWL_BREAKS_VER_2,
                map: Map.SubterraneanArea,
                startingBosses: new[] {Boss.Kotri}
            );

        public static BossFight Alius1 =
            new BossFight (
                displayName: "Alius1",
                music: Music.SUDDEN_DEATH,
                map: Map.WarpDestination,
                startingBosses: new[] {Boss.IllusionAlius}
            );

        public static BossFight Alius2 =
            new BossFight (
                displayName: "Alius2",
                music: Music.SUDDEN_DEATH,
                map: Map.WarpDestination,
                startingBosses: new[] {Boss.Noah1, Boss.IllusionAlius}
            );

        public static BossFight Alius3 =
            new BossFight (
                displayName: "Alius3",
                music: Music.SUDDEN_DEATH,
                map: Map.WarpDestination,
                startingBosses: new[] {Boss.Noah1}
            );

        public static BossFight Miru =
            new BossFight (
                displayName: "Miru",
                music: Music.M_R_,
                map: Map.WarpDestination,
                startingBosses: new[] {Boss.Miru}
            );

        public static BossFight Noah1 =
            new BossFight (
                displayName: "Noah1",
                music: Music.THE_TRUTH_NEVER_SPOKEN,
                map: Map.WarpDestination,
                startingBosses: new[] {Boss.Noah1}
            );

        public static BossFight Noah3 =
            new BossFight (
                displayName: "Noah3",
                music: Music.RFN_III,
                map: Map.WarpDestination,
                startingBosses: new[] {Boss.Noah3}
            );
        
        public static BossFight SideChapter =
            new BossFight (
                displayName: "Side Chapter",
                music: Music.GET_ON_WITH_IT,
                map: Map.RabiRabiTown,
                mapTile: Tile(133, 12),
                extraCondition: (startingBosses, music, map, mapTile) => {
                    return startingBosses.Count == 3;
                }
            );
    }
}
