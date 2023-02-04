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

namespace SummonerPlus.Content.Buffs
{
	public class LuminiteCurse : ModBuff
	{
		public override void SetStaticDefaults() {
			// This allows the debuff to be inflicted on NPCs that would otherwise be immune to all debuffs.
			BuffID.Sets.IsAnNPCWhipDebuff[Type] = true;
            DisplayName.SetDefault("Luminite Curse"); // Buff display name
            Description.SetDefault("The power of the moon"); // Buff description
            Main.debuff[Type] = true;  // Is it a debuff?
        }

		public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<LuminiteCurseNPC>().markedByLuminiteChain = true;
            Dust.NewDust(npc.position, npc.width, npc.height, DustID.Vortex, 0, 0, 0, default, 0.75f);
        }
	}

	public class LuminiteCurseNPC : GlobalNPC
	{
        // This is required to store information on entities that isn't shared between them.
        public override bool InstancePerEntity => true;

        public bool markedByLuminiteChain;

        public override void ResetEffects(NPC npc)
        {
            markedByLuminiteChain = false;
        }

        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (markedByLuminiteChain && !projectile.npcProj && !projectile.trap && (projectile.minion || ProjectileID.Sets.MinionShot[projectile.type]))
            {
                damage *= 3;

                Random rand = new Random();

                if (rand.NextDouble() > 0.5f)
                {
                    crit = true;
                }
            }

        }

        public override void OnHitByProjectile(NPC npc, Projectile projectile, int damage, float knockback, bool crit)
        {
            if (npc.HasBuff(ModContent.BuffType<LuminiteCurse>()) && npc.type != NPCID.TargetDummy && (projectile.minion || ProjectileID.Sets.MinionShot[projectile.type]))
            {
                for (int j = 0; j < Main.npc.Length - 1; j++)
                {
                    if (Vector2.Distance(npc.position, Main.npc[j].position) < 400 && !Main.npc[j].friendly && Main.npc[j].type != NPCID.TargetDummy && !Main.npc[j].HasBuff(ModContent.BuffType<LuminiteCurse>()))
                    {
                        Main.npc[j].AddBuff(ModContent.BuffType<LuminiteCurse>(), 600);
                        break;                        
                    }
                }
            }
        }
    }
}
