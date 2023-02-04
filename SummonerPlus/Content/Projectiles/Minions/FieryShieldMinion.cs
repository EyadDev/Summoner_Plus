//using TerrariaPlus.Content.Buffs;
using Humanizer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummonerPlus.Common.Players;
using SummonerPlus.Content.Buffs;
using SummonerPlus.Content.Buffs.MinionBuffs;
using System;
using System.Collections.Generic;
using System.Drawing;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace SummonerPlus.Content.Projectiles.Minions
{
    public class FieryShieldMinion : ModProjectile
    {
        public float AngleAroundPlayer;
        float CompSlots;
        bool CheckIfAllowed = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Thorny Shield");
            // Sets the amount of frames this minion has on its spritesheet
            Main.projFrames[Projectile.type] = 1;

            Main.projPet[Projectile.type] = true; // Denotes that this projectile is a pet or minion

            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true; // This is needed so your minion can properly spawn when summoned and replaced when other minions are summoned
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true; // Make the cultist resistant to this projectile, as it's resistant to all homing projectiles.
        }

        public sealed override void SetDefaults()
        {
            Projectile.tileCollide = false; // Makes the minion go through tiles freely
            Projectile.width = 18;
            Projectile.height = 22; 
            Projectile.scale = 1.4f;
            Projectile.friendly = true; // Only controls if it deals damage to enemies on contact (more on that later)
            Projectile.minion = true; // Declares this as a minion (has many effects)
            Projectile.DamageType = DamageClass.Summon; // Declares the damage type (needed for it to deal damage)
            Projectile.penetrate = -1;
            CompSlots = 1;
        }

        // Here you can decide if your minion breaks things like grass or pots
        public override bool? CanCutTiles()
        {
            return false;
        }

        // This is mandatory if your minion deals contact damage (further related stuff in AI() in the Movement region)
        public override bool MinionContactDamage()
        {
            return true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 300);
        }

        // The AI of this minion is split into multiple methods to avoid bloat. This method just passes values between calls actual parts of the AI.
        public override void AI()
        {
            if (!CheckIfAllowed)
            {
                if (Main.player[Projectile.owner].GetModPlayer<ComplimentaryMinionSlotsPlayer>().MaxCompSlots < (Main.player[Projectile.owner].GetModPlayer<ComplimentaryMinionSlotsPlayer>().FilledCompSlots + CompSlots))
                {
                    bool hasMinionOfType = false;
                    Projectile projOfType = new Projectile();

                    for (int i = 0; i < Main.projectile.Length; i++)
                    {
                        if (Main.projectile[i].type == Type && Main.projectile[i].active)
                        {
                            projOfType = Main.projectile[i];
                            hasMinionOfType = true;
                        }
                    }

                    if (hasMinionOfType)
                    {
                        projOfType.Kill();
                    }
                    else
                    {
                        Projectile.Kill();
                    }
                }

                Main.player[Projectile.owner].GetModPlayer<ComplimentaryMinionSlotsPlayer>().FilledCompSlots += CompSlots;
                CheckIfAllowed = true;

                float Anglebetween = 6.2f * 8;
                float NumOfMinions = 0;

                for (int i = 0; i < Main.projectile.Length; i++)
                {
                    if (Main.projectile[i].type == Type && Main.projectile[i].active)
                    {
                        NumOfMinions++;
                    }
                }

                Anglebetween = Anglebetween / NumOfMinions;

                int j = 0;

                for (int i = 0; i < Main.projectile.Length; i++)
                {
                    if (Main.projectile[i].type == Type)
                    {
                        FieryShieldMinion shield = (FieryShieldMinion)Main.projectile[i].ModProjectile;
                        shield.AngleAroundPlayer = AngleAroundPlayer + Anglebetween * j;
                        j++;
                    }
                }
            }

            Player owner = Main.player[Projectile.owner];

            if (!CheckActive(owner))
            {
                return;
            }

            Movement();

            if (Main.player[Projectile.owner].GetModPlayer<ComplimentaryMinionSlotsPlayer>().MaxCompSlots < (Main.player[Projectile.owner].GetModPlayer<ComplimentaryMinionSlotsPlayer>().FilledCompSlots))
            {
                Projectile.Kill();
            }

            Lighting.AddLight(Projectile.Center, 1, 0.364f, 0);
        }

        // This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
        private bool CheckActive(Player owner)
        {
            if (owner.dead || !owner.active)
            {
                owner.ClearBuff(ModContent.BuffType<FieryShieldMinionBuff>());

                return false;
            }

            if (owner.HasBuff(ModContent.BuffType<FieryShieldMinionBuff>()))
            {
                Projectile.timeLeft = 2;
            }

            return true;
        }

        private void Movement()
        {

            Projectile.Center = Main.player[Projectile.owner].Center + Vector2.One.RotatedBy(AngleAroundPlayer / 8f) * 85;
            //Main.NewText(AngleAroundPlayer, Microsoft.Xna.Framework.Color.White);
            AngleAroundPlayer++;
        }

        public override void Kill(int timeLeft)
        {
            Main.player[Projectile.owner].GetModPlayer<ComplimentaryMinionSlotsPlayer>().FilledCompSlots -= CompSlots;
        }
    }
}
