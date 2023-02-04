using SummonerPlus.Content.Projectiles.Minions;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace SummonerPlus.Content.Buffs.MinionBuffs
{
    public class CrimsonHeartMinionBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crimson Heart");
            Description.SetDefault("The Crimson Heart will enpower you");

            Main.buffNoSave[Type] = true; // This buff won't save when you exit the world
            Main.buffNoTimeDisplay[Type] = true; // The time remaining won't display on this buff
        }

        public override void Update(Player player, ref int buffIndex)
        {
            // If the minions exist reset the buff time, otherwise remove the buff from the player
            if (player.ownedProjectileCounts[ModContent.ProjectileType<CrimsonHeartMinion>()] > 0)
            {
                player.buffTime[buffIndex] = 18000;
            }
            else
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }

            int NumOfLeechedNPCs = 0;

            for (int i = 0; i < Main.npc.Length - 1; i++)
            {
                if (Main.npc[i].HasBuff<CrimsonHeartLeech>())
                {
                    NumOfLeechedNPCs++;
                }
            }

            for (int j = 0; j < Main.npc.Length - 1; j++)
            {
                if (Vector2.Distance(player.position, Main.npc[j].position) < 700 && !Main.npc[j].friendly && Main.npc[j].type != NPCID.TargetDummy && !Main.npc[j].SpawnedFromStatue && NumOfLeechedNPCs < 10)
                {
                    if (!Main.npc[j].HasBuff<CrimsonHeartLeech>())
                    {

                        NumOfLeechedNPCs++;
                    }

                    Main.npc[j].AddBuff(ModContent.BuffType<CrimsonHeartLeech>(), 120);
                }
            }
        }
    }

}
