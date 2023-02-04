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
    public class ShieldMinion : ModProjectile
    {
        public float AngleAroundPlayer;
        float CompSlots;
        bool CheckIfAllowed = false;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shield");
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

                float Anglebetween = 6.2f * 20;
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
                        ShieldMinion shield = (ShieldMinion)Main.projectile[i].ModProjectile;
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

            GeneralBehavior(owner);
            SearchForTargets(owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter);
            Movement(foundTarget, distanceFromTarget, targetCenter);

            if (Main.player[Projectile.owner].GetModPlayer<ComplimentaryMinionSlotsPlayer>().MaxCompSlots < (Main.player[Projectile.owner].GetModPlayer<ComplimentaryMinionSlotsPlayer>().FilledCompSlots))
            {
                Projectile.Kill();
            }
        }

        // This is the "active check", makes sure the minion is alive while the player is alive, and despawns if not
        private bool CheckActive(Player owner)
        {
            if (owner.dead || !owner.active)
            {
                owner.ClearBuff(ModContent.BuffType<ShieldMinionBuff>());

                return false;
            }

            if (owner.HasBuff(ModContent.BuffType<ShieldMinionBuff>()))
            {
                Projectile.timeLeft = 2;
            }

            return true;
        }

        private void GeneralBehavior(Player owner)
        {

        }

        private void SearchForTargets(Player owner, out bool foundTarget, out float distanceFromTarget, out Vector2 targetCenter)
        {
            // Starting search distance
            distanceFromTarget = 700f;
            targetCenter = Projectile.position;
            foundTarget = false;

            if (!foundTarget)
            {
                // This code is required either way, used for finding a target
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];

                    if (npc.CanBeChasedBy())
                    {
                        float between = Vector2.Distance(npc.Center, Projectile.Center);
                        bool closest = Vector2.Distance(Projectile.Center, targetCenter) > between;
                        bool inRange = between < distanceFromTarget;
                        bool lineOfSight = Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height);
                        // Additional check for this specific minion behavior, otherwise it will stop attacking once it dashed through an enemy while flying though tiles afterwards
                        // The number depends on various parameters seen in the movement code below. Test different ones out until it works alright
                        bool closeThroughWall = between < 100f;

                        if (((closest && inRange) || !foundTarget) && (lineOfSight || closeThroughWall))
                        {
                            distanceFromTarget = between;
                            targetCenter = npc.Center;
                            foundTarget = true;
                        }
                    }
                }
            }

            Projectile.friendly = true;
        }

        private void Movement(bool foundTarget, float distanceFromTarget, Vector2 targetCenter)
        {
            Projectile.Center = Main.player[Projectile.owner].Center + Vector2.One.RotatedBy(AngleAroundPlayer / 20f) * 35;
            //Main.NewText(AngleAroundPlayer, Microsoft.Xna.Framework.Color.White);
            AngleAroundPlayer++;
        }

        public override void Kill(int timeLeft)
        {
            Main.player[Projectile.owner].GetModPlayer<ComplimentaryMinionSlotsPlayer>().FilledCompSlots -= CompSlots;
        }
    }
}
