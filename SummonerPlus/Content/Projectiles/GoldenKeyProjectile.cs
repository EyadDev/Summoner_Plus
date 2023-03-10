//using TerrariaPlus.Content.Buffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SummonerPlus.Content.Buffs;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Physics;
using System;
using IL.Terraria.Utilities;
using log4net.Util;

namespace SummonerPlus.Content.Projectiles
{
	public class GoldenKeyProjectile : ModProjectile
    {
        public override void SetStaticDefaults() 
		{

		}

		public override void SetDefaults()
		{
            Projectile.alpha = 0;
            Projectile.timeLeft = 6000;
            Projectile.scale = 1.25f;
            Projectile.tileCollide = false;
            Projectile.damage = 15;
            Projectile.ArmorPenetration = 10;
            Projectile.maxPenetrate = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Default;
        }
        public float Timer
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        public override bool PreAI()
        {
            Lighting.AddLight(Projectile.position, 1, 1, 0);

            Timer++;
            if (Timer > 40)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            crit = false;
        }

        public override void AI()
        {
            float distanceFromTarget = 700f;
            Vector2 targetCenter = Projectile.position;
            bool foundTarget = false;

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.CanBeChasedBy())
                {
                    float between = Vector2.Distance(npc.Center, Projectile.Center);
                    bool closest = Vector2.Distance(Projectile.Center, targetCenter) > between;
                    bool inRange = between < distanceFromTarget;
                    //bool lineOfSight = Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height);
                    bool abovePlayer = Main.player[Projectile.owner].Center.Y > npc.Center.Y;

                    if (((closest && inRange) || !foundTarget))
                    {
                        distanceFromTarget = between;
                        targetCenter = npc.Center;
                        foundTarget = true;
                    }
                }
            }
            float speed = 10f;
            float inertia = 15f;

            // The immediate range around the target (so it doesn't latch onto it when close)
            Vector2 direction = targetCenter - Projectile.Center;
            direction.Normalize();
            direction *= speed;


            if (foundTarget)
            {

                Projectile.velocity = (Projectile.velocity * (inertia - 1) + direction) / inertia; 

                float angle = ((float)Math.Atan2(targetCenter.Y - Projectile.position.Y, targetCenter.X - Projectile.position.X) * 180 / (float)Math.PI) + 90;

                Projectile.rotation = angle/60;
            }
            else
            {
                Projectile.velocity = Projectile.velocity * (inertia - 1) / inertia;
            }

            if (Math.Abs(Projectile.velocity.X) < 1f && Math.Abs(Projectile.velocity.Y) < 1f)
            {
                Projectile.alpha += 8;
            }

            if (Projectile.alpha >= 255)
            {
                Projectile.Kill();
            }
        }
        public override void Kill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item4, Projectile.position);

            for (int i = 0; i < 5; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Gold, 0, 0, 0, default, 1f);
            }
        }
    }
}