using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace SummonerPlus.Content.Buffs
{
	public class NaturesRegen : ModBuff
	{

		public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nature's Regeneration");
            Description.SetDefault("increased life regen");
            Main.debuff[Type] = false;

        }

		public override void Update(Player player, ref int buffIndex)
        {
            player.lifeRegen += 5;
        }
	}
}
