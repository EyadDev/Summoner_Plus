using System.Diagnostics.Metrics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace SummonerPlus.Content.Buffs
{
	public class StarFrenzy : ModBuff
	{
		public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Star Frenzy");
            Description.SetDefault("-Melee speed is increased\n-Whip range is increased\n-Move speed and jump speed are increased\n-Melee damage is reduced\n-Stardust Guardian damage is increased");
            Main.debuff[Type] = false;

        }

		public override void Update(Player player, ref int buffIndex) 
		{
            player.GetAttackSpeed(DamageClass.Melee) += 0.8f;
            player.whipRangeMultiplier += 0.5f;
            player.moveSpeed += 2f;
            player.jumpSpeedBoost += 2f;
            player.GetDamage(DamageClass.Melee) -= 0.9f;
        }
	}
}
