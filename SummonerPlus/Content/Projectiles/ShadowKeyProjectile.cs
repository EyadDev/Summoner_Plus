using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using System;
using IL.Terraria.Utilities;

namespace SummonerPlus.Content.Projectiles
{
    public class ShadowKeyProjectile : ModProjectile
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
            Projectile.damage = 25;
            //Projectile.maxPenetrate = 3;
            Projectile.friendly = true;
            Projectile.ArmorPenetration = 15;
            Projectile.DamageType = DamageClass.Default;
        }
        public float Timer
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }

        public override bool PreAI()
        {
            Lighting.AddLight(Projectile.position, 0.4f, 0, 1);

            Timer++;
            if (Timer > 40)
            {
                return true;
            }
            else
            {
                return false;
            }
            // Other code...
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 180);
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

                Projectile.rotation = angle / 60;
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
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.PurpleCrystalShard, 0, 0, 0, default, 1f);
            }
        }
    }
}
