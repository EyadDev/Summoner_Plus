using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace SummonerPlus.Content.Buffs
{
	public class SlashFrenzy : ModBuff
	{
		public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Slash Frenzy");
            Description.SetDefault("Melee speed is increased");
            Main.debuff[Type] = false;

        }

		public override void Update(Player player, ref int buffIndex) 
		{
            player.GetAttackSpeed(DamageClass.Melee) += 0.5f;
        }
	}
}
