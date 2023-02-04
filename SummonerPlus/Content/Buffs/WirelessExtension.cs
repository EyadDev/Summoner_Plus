using SummonerPlus.Common.Players;
using SummonerPlus.Content.Items.Weapons;
using SummonerPlus.Content.Projectiles;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace SummonerPlus.Content.Buffs
{
	public class WirelessExtension : ModBuff
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wireless Extension");
            Description.SetDefault("Whip range of wireless strike is increased");
            BuffID.Sets.IsAnNPCWhipDebuff[Type] = true;
            Main.debuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.whipRangeMultiplier += 0.5f + player.GetModPlayer<WirelessExtensionPlayer>().WhipRangeAmount;

            if (player.HeldItem.type != ModContent.ItemType<WirelessStrike>())
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }

        public override bool ReApply(Player player, int time, int buffIndex)
        {
            if (player.GetModPlayer<WirelessExtensionPlayer>().WhipRangeAmount < 15)
            {
                player.GetModPlayer<WirelessExtensionPlayer>().WhipRangeAmount += 0.2f;
            }

            return false;
        }

        public override void ModifyBuffTip(ref string tip, ref int rare)
        {
            //tip = "ExtensionAmount: " + player.GetModPlayer<WirelessExtensionPlayer>().WhipRangeAmount;
        }
    }
}
