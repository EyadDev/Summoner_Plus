using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using SummonerPlus.Content.Items.Weapons;
using SummonerPlus.Content.Items;
using Terraria.GameContent.ItemDropRules;
using Terraria.WorldBuilding;
using IL.Terraria.DataStructures;
using SummonerPlus.Content.Tiles;
using SummonerPlus.Content.Items.Weapons.SummoningStaffs;

namespace SummonerPlus.Content.NPCs
{
    internal class CustomGlobalNPC : GlobalNPC
    {
        static bool GolemKilled = false;

        public override void SetupShop(int type, Chest shop, ref int nextSlot)
        {
            if (type == NPCID.Dryad)
            {
                shop.item[nextSlot].SetDefaults(ModContent.ItemType<NaturesCall>());
                shop.item[nextSlot].shopCustomPrice = 100000;
                nextSlot++;
            }
        }

        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.HallowBoss)
            {
                npcLoot.Add(ItemDropRule.ByCondition(new Terraria.GameContent.ItemDropRules.Conditions.NotExpert(), ModContent.ItemType<RainbowGem>(), 1, 3, 7));
            }

            if (npc.type == NPCID.EyeofCthulhu)
            {
                npcLoot.Add(ItemDropRule.ByCondition(new Terraria.GameContent.ItemDropRules.Conditions.NotExpert(), ModContent.ItemType<EyeStaff>(), 4));
            }

            if (npc.type == NPCID.KingSlime)
            {
                npcLoot.Add(ItemDropRule.ByCondition(new Terraria.GameContent.ItemDropRules.Conditions.NotExpert(), ItemID.SlimeStaff, 4));
            }

            if (npc.type == NPCID.MartianSaucerCore)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WirelessStrike>(), 6));
            }

            if (npc.type == NPCID.Golem)
            {
                npcLoot.Add(ItemDropRule.ByCondition(new Terraria.GameContent.ItemDropRules.Conditions.NotExpert(), ModContent.ItemType<SunRockOre>(), 1, 40, 60));
            }
        }

        public override bool CheckDead(NPC npc)
        {
            if (npc.type == NPCID.Golem &&!NPC.downedGolemBoss && !GolemKilled)
            {
                Main.NewText("Rocks from the sun burn through the surface...", 100, 42, 0);

                int maxtospawn = 10000;

                if (Main.maxTilesX == 4200)
                {
                    maxtospawn = WorldGen.genRand.Next(250, 300);
                }
                else if (Main.maxTilesX == 6400)
                {

                    maxtospawn = WorldGen.genRand.Next(375, 450);
                }
                else if (Main.maxTilesX == 8400)
                {
                    maxtospawn = WorldGen.genRand.Next(500, 600);
                }

                int numSpawned = 0;
                int attempts = 0;
                while (numSpawned < maxtospawn)
                {
                    int x = WorldGen.genRand.Next(0, Main.maxTilesX);
                    int y = WorldGen.genRand.Next((int)Main.worldSurface - 200, (int)Main.worldSurface + 100);

                    Tile tile = Framing.GetTileSafely(x, y);

                    if (tile.TileType == TileID.Dirt && tile.TileType != TileID.Trees)
                    {
                        WorldGen.TileRunner(x, y, WorldGen.genRand.Next(4, 8), WorldGen.genRand.Next(2, 5), ModContent.TileType<SunRockTile>());
                        numSpawned++;
                    }

                    attempts++;

                    if (attempts > 100000)
                    {
                        break;
                    }
                }

                GolemKilled = true;
            }

            return true;
        }
    }
}
