using SummonerPlus.Content.Projectiles;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace SummonerPlus.Content.Items.Accessories
{
	public class ExtensionCord : ModItem
	{
		public override void SetStaticDefaults() {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            Tooltip.SetDefault("15% increased whip range");
        }

		public override void SetDefaults() {
            // This method quickly sets the whip's properties.
            // Mouse over to see its parameters.
            Item.rare = ItemRarityID.Blue;
            Item.accessory = true;
            Item.value = 10000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.whipRangeMultiplier += 0.15f;
        }
        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            if (incomingItem.type == ModContent.ItemType<HotCord>() || incomingItem.type == ModContent.ItemType<ColdMechanicalCord>() || incomingItem.type == ModContent.ItemType<RainbowCord>())
            {
                return false;
            }

            return true;
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes() {

            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ItemID.Silk, 20);
            recipe.AddIngredient(ItemID.PlatinumBar, 20);
            recipe.AddTile(TileID.TinkerersWorkbench);

            recipe.Register();

            recipe = CreateRecipe();

            recipe.AddIngredient(ItemID.Silk, 20);
            recipe.AddIngredient(ItemID.GoldBar, 20);
            recipe.AddTile(TileID.TinkerersWorkbench);

            recipe.Register();
        }
	}
}
