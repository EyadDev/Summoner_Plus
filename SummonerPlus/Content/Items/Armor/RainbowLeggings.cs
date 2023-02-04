using Humanizer;
using SummonerPlus.Content.Items;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace SummonerPlus.Content.Items.Armor
{
	// The AutoloadEquip attribute automatically attaches an equip texture to this item.
	// Providing the EquipType.Legs value here will result in TML expecting a X_Legs.png file to be placed next to the item's main texture.
	[AutoloadEquip(EquipType.Legs)]
	public class RainbowLeggings : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Increases your max number of minions by 1\nIncreases minion damage by 18% ");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 5); // How many coins the item is worth
			Item.rare = ItemRarityID.Cyan; // The rarity of the item
			Item.defense = 15; // The amount of defense the item will give when equipped
		}

		public override void UpdateEquip(Player player) 
        {
            player.maxMinions++;
            player.GetDamage(DamageClass.Summon) += 0.18f;
		}

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<RainbowGem>(), 3);
            recipe.AddIngredient(ModContent.ItemType<SunBar>(), 12);
            recipe.AddTile(TileID.MythrilAnvil);

            recipe.Register();
        }
    }
}
