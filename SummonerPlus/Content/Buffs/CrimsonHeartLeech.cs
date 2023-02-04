using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using SummonerPlus.Content.Projectiles;
using System.Net.Http.Headers;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.Chat.Commands;
using SummonerPlus.Content.Projectiles.Minions;

namespace SummonerPlus.Content.Buffs
{
    public class CrimsonHeartLeech : ModBuff
    {
        int Timer = 0;

        public override void SetStaticDefaults()
        {
            // This allows the debuff to be inflicted on NPCs that would otherwise be immune to all debuffs.
            BuffID.Sets.IsAnNPCWhipDebuff[Type] = true;
            Main.debuff[Type] = true;  // Is it a debuff?
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<CrimsonHeartLeechNPC>().CrimsonHeartLeech = true;

            Dust.NewDust(npc.position, npc.width, npc.height, DustID.Blood, 0, 0, 0, default, 1.2f);

            if (Timer == 120)
            {
                for (int i = 0; i < Main.projectile.Length; i++)
                {
                    if (Main.projectile[i].type == ModContent.ProjectileType<CrimsonHeartMinion>() && Main.projectile[i].active)
                    {
                        Main.player[Main.projectile[i].owner].Heal(1);
                    }
                }

                Timer = 0;
            }

            Timer++;
        }
    }

    public class CrimsonHeartLeechNPC : GlobalNPC
    {
        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (CrimsonHeartLeech)
            {
                npc.lifeRegen -= 10;
                damage = 5;
            }
        }

        // This is required to store information on entities that isn't shared between them.
        public override bool InstancePerEntity => true;


        public bool CrimsonHeartLeech;

        public override void ResetEffects(NPC npc)
        {
            CrimsonHeartLeech = false;
        }
    }
}
