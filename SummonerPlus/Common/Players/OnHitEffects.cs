using Steamworks;
using SummonerPlus.Content.Buffs.MinionBuffs;
using SummonerPlus.Content.Projectiles.Minions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace SummonerPlus.Common.Players
{
    internal class OnHitEffects : ModPlayer
    {
        public override void OnHitByNPC(NPC npc, int damage, bool crit)
        {
            if (Player.HasBuff<ThornyShieldMinionBuff>())
            {
                double damageDone = damage * (1d + 0.2d * (Player.ownedProjectileCounts[ModContent.ProjectileType<ThornyShieldMinion>()] - 1));

                npc.StrikeNPC((int)damageDone, 6, 0);
            }

            if (Player.HasBuff<FieryShieldMinionBuff>())
            {
                double damageDone = 40 + 10 * (Player.ownedProjectileCounts[ModContent.ProjectileType<FieryShieldMinion>()] - 1);

                npc.StrikeNPC((int)damageDone, 10, 0);
                npc.AddBuff(BuffID.OnFire, 300);

                SoundEngine.PlaySound(SoundID.Item14, npc.position);

                Random rand = new Random();

                for (int i = 0; i < 80; i++)
                {
                    Dust.NewDust(npc.position, npc.width, npc.height, DustID.Torch, (float)rand.NextDouble() * rand.Next(-1 , 2) * 3, (float)rand.NextDouble() * rand.Next(-1, 2) * 3, 0, default, 1.2f);
                }
            }


            if (Player.HasBuff<CrimsonHeartMinionBuff>())
            {
                Random rand = new Random();

                if (rand.NextDouble() > 0.6667f && damage > 20)
                {
                    Item.NewItem(Player.GetSource_None(), new Vector2(Player.Center.X + 5, Player.Center.Y - 115), Vector2.Zero, ItemID.Heart);
                }
            }
        }
    }
}
