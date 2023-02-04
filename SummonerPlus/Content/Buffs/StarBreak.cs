using System.Diagnostics.Metrics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace SummonerPlus.Content.Buffs
{
	public class StarBreak : ModBuff
	{
		public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Star Break");
            Description.SetDefault("Can't activate star frenzy");
            Main.debuff[Type] = true;
            BuffID.Sets.NurseCannotRemoveDebuff[Type] = true;
        }

		public override void Update(Player player, ref int buffIndex) 
		{

        }
	}
}
