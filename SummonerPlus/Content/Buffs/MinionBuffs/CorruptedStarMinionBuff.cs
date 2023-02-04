using SummonerPlus.Content.Projectiles.Minions;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace SummonerPlus.Content.Buffs.MinionBuffs
{
    public class CorruptedStarMinionBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Corrupted Star");
            Description.SetDefault("The Corrupted Star will enpower you");

            Main.buffNoSave[Type] = true; // This buff won't save when you exit the world
            Main.buffNoTimeDisplay[Type] = true; // The time remaining won't display on this buff
        }

        public override void Update(Player player, ref int buffIndex)
        {
            // If the minions exist reset the buff time, otherwise remove the buff from the player
            if (player.ownedProjectileCounts[ModContent.ProjectileType<CorruptedStarMinion>()] > 0)
            {
                player.buffTime[buffIndex] = 18000;
            }
            else
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }

            player.GetDamage(DamageClass.Summon) += 0.2f;
            player.GetAttackSpeed(DamageClass.SummonMeleeSpeed) += 0.3f;
            player.whipRangeMultiplier += 0.2f;
        }
    }
}
