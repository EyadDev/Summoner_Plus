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
    public class RainbowHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Increases your max number of minions by 1\nIncreases minion damage by 10%");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = false;
            
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 28;
            Item.value = Item.sellPrice(gold: 4); // How many coins the item is worth
            Item.rare = ItemRarityID.Cyan; // The rarity of the item
            Item.defense = 9; // The amount of defense the item will give when equipped
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Summon) += 0.1f;
            player.maxMinions++; // Increase how many minions the player can have by one
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<RainbowBreastplate>() && legs.type == ModContent.ItemType<RainbowLeggings>();
        }

        // UpdateArmorSet allows you to give set bonuses to the armor.
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Increases your max number of minions by 2"; // This is the setbonus tooltip
            player.maxMinions += 2; // Increase how many minions the player can have by one
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<RainbowGem>(), 2);
            recipe.AddIngredient(ModContent.ItemType<SunBar>(), 8);
            recipe.AddTile(TileID.MythrilAnvil);

            recipe.Register();
        }
    }
}
