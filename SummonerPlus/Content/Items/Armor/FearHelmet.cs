using Humanizer;
using SummonerPlus.Content.Items;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace SummonerPlus.Content.Items.Armor
{
    // The AutoloadEquip attribute automatically attaches an equip texture to this item.
    // Providing the EquipType.Body value here will result in TML expecting X_Arms.png, X_Body.png and X_FemaleBody.png sprite-sheet files to be placed next to the item's main texture.
    [AutoloadEquip(EquipType.Head)]
    public class FearHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Increases minion damage and whip speed by 5%");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = false;
            
        }

        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(gold: 2); // How many coins the item is worth
            Item.rare = ItemRarityID.Pink; // The rarity of the item
            Item.defense = 5; // The amount of defense the item will give when equipped
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Summon) += 0.05f;
            player.GetAttackSpeed(DamageClass.Melee) += 0.05f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<FearBreastplate>() && legs.type == ModContent.ItemType<FearLeggings>();
        }

        // UpdateArmorSet allows you to give set bonuses to the armor.
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Increases minion damage and whip speed by 10%\nIncreases whip range by 35%"; // This is the setbonus tooltip
            player.GetDamage(DamageClass.Summon) += 0.1f;
            player.GetAttackSpeed(DamageClass.Melee) += 0.1f;
            player.whipRangeMultiplier += 0.35f;
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ItemID.TitaniumBar, 10);
            recipe.AddIngredient(ItemID.SoulofFright, 4);
            recipe.AddTile(TileID.MythrilAnvil);

            recipe.Register();

            recipe = CreateRecipe();

            recipe.AddIngredient(ItemID.AdamantiteBar, 10);
            recipe.AddIngredient(ItemID.SoulofFright, 4);
            recipe.AddTile(TileID.MythrilAnvil);

            recipe.Register();
        }
    }
}
