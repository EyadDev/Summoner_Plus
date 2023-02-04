using SummonerPlus.Content.Projectiles;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.DataStructures;
using static Humanizer.In;
using Terraria.GameContent.ItemDropRules;
using System;
using Terraria.GameContent;
using System.Drawing;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Microsoft.Xna.Framework;
using SummonerPlus.Content.Items.Weapons.SummoningStaffs;
using SummonerPlus.Common.Players;

namespace SummonerPlus.Content.Items
{
    internal class CustomGlobalItem : GlobalItem
    {
        public override void SetStaticDefaults()
        {

        }


        public override void SetDefaults(Item item)
        {
            if (item.type == ItemID.RainbowWhip)
            {
                item.damage = 150;

            }

            if (item.DamageType == DamageClass.SummonMeleeSpeed)
            {
                item.autoReuse = true;
            }

            if (item.type == ItemID.SpookyLeggings)
            {

            }

            if (item.type == ItemID.SpookyBreastplate)
            {

            }
        }

        public override void UpdateEquip(Item item, Player player)
        {
            if (item.type == ItemID.SpookyLeggings)
            {
                player.maxMinions--;
            }

            if (item.type == ItemID.SpookyBreastplate)
            {
                player.maxMinions--;
            }
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.type == ItemID.SpookyBreastplate)
            {
                foreach (TooltipLine line in tooltips)
                {
                    if (line.Mod == "Terraria" && line.Name == "Tooltip0")
                    {
                        line.Text = "Increases your max number of minions by 1\nincreases minion damage by 11%";
                    }
                    else if (line.Name == "Tooltip1")
                    {
                        line.Text = "";
                    }
                }
            }

            if (item.type == ItemID.SpookyLeggings)
            {
                foreach (TooltipLine line in tooltips)
                {
                    if (line.Mod == "Terraria" && line.Name == "Tooltip0")
                    {
                        line.Text = "increases minion damage by 11%\n20% increased movement speed";
                    }
                    else if(line.Name == "Tooltip1" || line.Name == "Tooltip2")
                    {
                        line.Text = "";
                    }
                }
            }

            if (item.type == ItemID.RainbowWhip)
            {
                foreach (TooltipLine line in tooltips)
                {
                    if (line.Mod == "Terraria" && line.Name == "Tooltip0")
                    {
                        line.Text = "15 summon tag damage\n5% summon tag critical strike chance\nYour summons will focus struck enemies";
                    }
                    else if (line.Name == "Tooltip1" || line.Name == "Tooltip2")
                    {
                        line.Text = "";
                    }
                }
            }

            if (item.type == ItemID.MaceWhip)
            {
                foreach (TooltipLine line in tooltips)
                {
                    if (line.Mod == "Terraria" && line.Name == "Tooltip0")
                    {
                        line.Text = "5 summon tag damage\n10% summon tag critical strike chance\nYour summons will focus struck enemies";
                    }
                    else if (line.Name == "Tooltip1" || line.Name == "Tooltip2")
                    {
                        line.Text = "";
                    }
                }
            }
        }



        public override string IsArmorSet(Item head, Item body, Item legs)
        {
            if (head.type == ItemID.ObsidianHelm && body.type == ItemID.ObsidianShirt && legs.type == ItemID.ObsidianPants)
            {
                return "Obsidian Set";
            }
            else if (head.type == ItemID.SpookyHelmet && body.type == ItemID.SpookyBreastplate && legs.type == ItemID.SpookyLeggings)
            {
                return "Spooky Set";
            }
            else if (head.type == ItemID.BeeHeadgear && body.type == ItemID.BeeBreastplate && legs.type == ItemID.BeeGreaves)
            {
                return "Bee Set";
            }
            return "";
        }

        public override void UpdateArmorSet(Player player, string set)
        {
            if (set == "Obsidian Set")
            {
                player.setBonus = "Increases whip range by 15% and speed by 20%\nIncreases minion damage by 10%";

                player.whipRangeMultiplier -= 0.35f;
                player.GetAttackSpeed(DamageClass.SummonMeleeSpeed) -= 0.15f;
                player.GetDamage(DamageClass.Summon) -= 0.05f;
            }

            if (set == "Spooky Set")
            {
                player.setBonus = "Increases whip range and speed by 50%\nIncreases minion damage by 7%";

                player.whipRangeMultiplier += 0.5f;
                player.GetAttackSpeed(DamageClass.SummonMeleeSpeed) += 0.5f;
                player.GetDamage(DamageClass.Summon) -= 0.18f;
            }

            if (set == "Bee Set")
            {
                player.setBonus = "Increases minion damage by 10%\nIncreased number of complimentary minion slots by 1";

                player.GetModPlayer<ComplimentaryMinionSlotsPlayer>().HasBeeSet = true;
            }
        }

        public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
        {
            if (item.type == ItemID.FairyQueenBossBag)
            {
                itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<RainbowGem>(), 1, 4, 8));
            }

            if (item.type == ItemID.EyeOfCthulhuBossBag)
            {
                itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<EyeStaff>(), 3));
            }

            if (item.type == ItemID.KingSlimeBossBag)
            {
                itemLoot.Add(ItemDropRule.Common(ItemID.SlimeStaff, 3));
            }

            if (item.type == ItemID.GolemBossBag)
            {
                itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<SunRockOre>(), 1, 60, 80));
            }
        }
    }
}
