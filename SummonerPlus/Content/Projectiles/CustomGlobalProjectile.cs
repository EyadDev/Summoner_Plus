using Steamworks;
using SummonerPlus.Common.Players;
using SummonerPlus.Content.Buffs;
using SummonerPlus.Content.Buffs.MinionBuffs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SummonerPlus.Content.Projectiles
{
    internal class CustomGlobalProjectile : GlobalProjectile
    {
        public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.type == Terraria.ID.ProjectileID.RainbowWhip)
            {
                target.AddBuff(ModContent.BuffType<ModifiedKaleidescopeDebuff>(), 180);
            }

            if (projectile.type == Terraria.ID.ProjectileID.MaceWhip)
            {
                target.AddBuff(ModContent.BuffType<ModifiedMourningStarDebuff>(), 180);
            }


            if (projectile.DamageType == DamageClass.SummonMeleeSpeed)
            {
                if (Main.player[projectile.owner].magmaStone == true)
                {
                    Random rand = new Random();

                    if (rand.Next(0, 8) <= 1)
                    {
                        target.AddBuff(323, 360);
                    }
                    else
                    {
                        if (rand.Next(0, 2) == 0)
                        {
                            target.AddBuff(323, 240);
                        }
                        else
                        {
                            target.AddBuff(323, 120);
                        }
                    }
                }

                if (Main.player[projectile.owner].GetModPlayer<FrostBurnInflictionPlayer>().HasFrostburnInflictionItem == true)
                {
                    target.AddBuff(44, 240);
                }

                if (Main.player[projectile.owner].GetModPlayer<SpectralFlameInflictionPlayer>().HasSpectralFlameInflictionItem == true)
                {
                    target.AddBuff(ModContent.BuffType<LesserSpectralFlame>(), 240);
                }
            }
        }

        public override void ModifyHitNPC(Projectile projectile, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (projectile.minion && Main.player[projectile.owner].HasBuff<CorruptedStarMinionBuff>())
            {
                Random rand = new Random();

                if (rand.NextDouble() > 0.9f)
                {
                    crit = true;
                }
            }
        }

        public override void AI(Projectile projectile)
        {
            if (projectile.DamageType == DamageClass.SummonMeleeSpeed)
            {
                if (Main.player[projectile.owner].magmaStone == true)
                {
                    Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, DustID.Torch, projectile.velocity.X, projectile.velocity.Y);
                }

                if (Main.player[projectile.owner].GetModPlayer<FrostBurnInflictionPlayer>().HasFrostburnInflictionItem == true)
                {
                    Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, DustID.Frost, projectile.velocity.X, projectile.velocity.Y);
                }
            }

            if (projectile.type == ProjectileID.StardustGuardian && Main.player[projectile.owner].HasBuff<StarFrenzy>())
            {
                projectile.damage = (int)((double)projectile.damage * 1.5f);
            }
        }
    }
}
