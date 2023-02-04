using SummonerPlus.Common.Players;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace SummonerPlus.Content.Buffs
{
	public class AttackSpeedBuff : ModBuff
	{
		public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Attack speed");
            Description.SetDefault("Attack speed is increased by 10%");
            Main.debuff[Type] = false;

        }

		public override void Update(Player player, ref int buffIndex) 
		{
            player.GetAttackSpeed(DamageClass.Generic) += 0.1f;
        }
	}
}
