//using TerrariaPlus.Content.Buffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummonerPlus.Content.Buffs;
using SummonerPlus.Content.Buffs.MinionBuffs;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using static Humanizer.In;

namespace SummonerPlus.Content.Projectiles.Minions
{
    public class Worm_BodyMinion : ModProjectile
    {
        Projectile AttachedProjectile = new Projectile();
        bool ranOnce;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Worm body");
            // Sets the amount of frames this minion has on its spritesheet
            Main.projFrames[Projectile.type] = 1;

            Main.projPet[Projectile.type] = true; // Denotes that this projectile is a pet or minion

            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true; // This is needed so your minion can properly spawn when summoned and replaced when other minions are summoned
            ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true; // Make the cultist resistant to this projectile, as it's resistant to all homing projectiles.
        }

        public sealed override void SetDefaults()
        {
            Projectile.tileCollide = false; // Makes the minion go through tiles freely

            // These below are needed for a minion weapon
            Projectile.friendly = true; // Only controls if it deals damage to enemies on contact (more on that later)
            Projectile.minion = true; // Declares this as a minion (has many effects)
            Projectile.DamageType = DamageClass.Summon; // Declares the damage type (needed for it to deal damage)
            Projectile.minionSlots = 0f; // Amount of slots this minion occupies from the total minion slots available to the player (more on that later)
            Projectile.penetrate = -1; // Needed so the minion doesn't despawn on collision with enemies or tiles // Needed so the minion doesn't despawn on collision with enemies or tiles
            Projectile.height = 32;
            Projectile.width = 26;
            Projectile.scale = 1;
        }

        public override void AI()
        {
            base.AI();

            if (!ranOnce)
            {
                for (int i = 0; i < Main.projectile.Length; i++)
                {
                    if (Main.projectile[i].owner == Projectile.owner && Main.projectile[i].whoAmI != Projectile.whoAmI && Main.projectile[i].active && (Main.projectile[i].type == Type || Main.projectile[i].type == ModContent.ProjectileType<Worm_HeadMinion>()))
                    {
                        AttachedProjectile = Main.projectile[i];
                    }
                }

                ranOnce = true;
            }



            float dirX = AttachedProjectile.position.X - Projectile.position.X;
            float dirY = AttachedProjectile.position.Y - Projectile.position.Y;
            // We then use Atan2 to get a correct rotation towards that parent NPC.
            // Assumes the sprite for the NPC points upward.  You might have to modify this line to properly account for your NPC's orientation
            Projectile.rotation = (float)Math.Atan2(dirY, dirX) + MathHelper.PiOver2;

            Vector2 newPos = AttachedProjectile.position;
            Vector2 RotVec = new Vector2((float)Math.Cos(AttachedProjectile.rotation + 1.5708f), (float)Math.Sin(AttachedProjectile.rotation + 1.5708f));

            if (AttachedProjectile.type == ModContent.ProjectileType<Worm_HeadMinion>())
            {
                RotVec *= ((AttachedProjectile.height / 2) + (Projectile.height / 2)) - 7f;
                //Main.NewText(RotVec + ", Angle: " + (int)((AttachedProjectile.rotation + 1.5708f) * (180 / Math.PI)), Color.White);
            }
            else
            {
                RotVec *= (AttachedProjectile.height - 14f);
            }

            newPos += RotVec;

            Projectile.position = newPos;

            CheckActive(Main.player[Projectile.owner]);
        }

        // This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
        private bool CheckActive(Player owner)
        {
            if (owner.dead || !owner.active)
            {
                owner.ClearBuff(ModContent.BuffType<WormMinionBuff>());

                return false;
            }

            if (owner.HasBuff(ModContent.BuffType<WormMinionBuff>()))
            {
                Projectile.timeLeft = 2;
            }

            return true;
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
    }
}
