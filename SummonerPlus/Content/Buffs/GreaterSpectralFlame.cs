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
	public class GreaterSpectralFlame : ModBuff
	{
		public override void SetStaticDefaults() {
			// This allows the debuff to be inflicted on NPCs that would otherwise be immune to all debuffs.
			BuffID.Sets.IsAnNPCWhipDebuff[Type] = true;
            DisplayName.SetDefault("Greater Spectral flame"); // Buff display name
            Description.SetDefault("the heat of lost souls"); // Buff description
            Main.debuff[Type] = true;  // Is it a debuff?
            Main.pvpBuff[Type] = true; // Players can give other players buffs, which are listed as pvpBuff
        }

		public override void Update(NPC npc, ref int buffIndex)
        {

            npc.GetGlobalNPC<GreaterSpectralFlameNPC>().Greaterspectralburn = true;

            // These lines zero out any positive lifeRegen. This is expected for all bad life regeneration effects

            Dust.NewDust(npc.position, npc.width, npc.height, DustID.DungeonSpirit, 0, 0, 0, default, 1.6f);    
        }
	}

	public class GreaterSpectralFlameNPC : GlobalNPC
	{
        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (Greaterspectralburn)
            {
                npc.lifeRegen -= 160;
                damage = 80;
            }
        }
        // This is required to store information on entities that isn't shared between them.
        public override bool InstancePerEntity => true;

        public bool Greaterspectralburn;

        public override void ResetEffects(NPC npc)
        {
            Greaterspectralburn = false;
        }

        public override void OnHitByProjectile(NPC npc, Projectile projectile, int damage, float knockback, bool crit)
        {

            if (npc.HasBuff(ModContent.BuffType<GreaterSpectralFlame>()) && npc.type != NPCID.TargetDummy && (projectile.minion || ProjectileID.Sets.MinionShot[projectile.type]))
            {
                for (int j = 0; j < Main.npc.Length - 1; j++)
                {
                    if (Vector2.Distance(npc.position, Main.npc[j].position) < 400 && !Main.npc[j].friendly && Main.npc[j].type != NPCID.TargetDummy && !Main.npc[j].HasBuff(ModContent.BuffType<SupremeSpectralFlame>()) && !Main.npc[j].HasBuff(ModContent.BuffType<SpectralFlame>()) && !Main.npc[j].HasBuff(ModContent.BuffType<GreaterSpectralFlame>()) && !Main.npc[j].HasBuff(ModContent.BuffType<LesserSpectralFlame>()))
                    {
                        SoundEngine.PlaySound(SoundID.NPCHit36, Main.npc[j].position);
                        Main.npc[j].AddBuff(ModContent.BuffType<SpectralFlame>(), 120);
                        break;                        
                    }
                }
            }
        }
    }
}
