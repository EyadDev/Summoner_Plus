using Terraria;
using Terraria.GameContent.Creative;
using SummonerPlus.Content.Tiles;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.WorldBuilding;
using Terraria.IO;

namespace SummonerPlus.Content
{
    internal class SunRockGenPass
    {
        //public SunRockGenPass(string name, float weight) : base(name, weight)
        //{
        //
        //}

        //protected override void ApplyPass(GenerationProgress progress, GameConfiguration configuration)
        //{
            //progress.Message = "Generating Sun rock";

            //int maxtospawn = (int)(Main.maxTilesX * Main.maxTilesY * 6E-05);
            //for (int i = 0; i < maxtospawn; i++)
            //{
            //    int x = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
            //    int y = WorldGen.genRand.Next((int)Main.worldSurface, (int)Main.worldSurface + 200);

            //    WorldGen.TileRunner(x, y, WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(2, 5), ModContent.TileType<SunRockTile>());
            //}
            //int maxtospawn = WorldGen.genRand.Next(200, 250);
            //int numSpawned = 0;
           // int attempts = 0;
            //while (numSpawned < maxtospawn)
            //{
            //    int x = WorldGen.genRand.Next(0, Main.maxTilesX);
            //   int y = WorldGen.genRand.Next((int)Main.worldSurface - 200, (int)Main.worldSurface);

            //    Tile tile = Framing.GetTileSafely(x, y);

            //    if (tile.TileType == TileID.Dirt)
            //    {
            //        WorldGen.TileRunner(x, y, WorldGen.genRand.Next(4, 8), WorldGen.genRand.Next(2, 5), ModContent.TileType<SunRockTile>());
            //        numSpawned++;
            //    }

            //    attempts++;

            //    if (attempts > 100000)
            //    {
            //        break;
            //    }
            //}
        //}
    }
}