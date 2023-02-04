using SummonerPlus.Content.Items.Weapons;
using SummonerPlus.Content.Projectiles;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace SummonerPlus.Content.Items.Accessories
{
	public class HotCord : ModItem
	{
		public override void SetStaticDefaults() {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            Tooltip.SetDefault("30% increased whip range\nwhips and melee weapons inflict fire damage");
        }

		public override void SetDefaults() {
            // This method quickly sets the whip's properties.
            // Mouse over to see its parameters.
            Item.rare = ItemRarityID.Orange;
            Item.accessory = true;
            Item.value = 30000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.magmaStone = true;
            player.whipRangeMultiplier += 0.3f;
        }

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            if (incomingItem.type == ModContent.ItemType<ExtensionCord>() || incomingItem.type == ModContent.ItemType<ColdMechanicalCord>() || incomingItem.type == ModContent.ItemType<RainbowCord>())
            {
                return false;
            }

            return true;
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes() {

            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<ExtensionCord>());
            recipe.AddIngredient(ItemID.MagmaStone);
            recipe.AddIngredient(ItemID.DemoniteBar, 20);
            recipe.AddIngredient(ItemID.ShadowScale, 10);
            recipe.AddTile(TileID.TinkerersWorkbench);

            recipe.Register();

            recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<ExtensionCord>());
            recipe.AddIngredient(ItemID.MagmaStone);
            recipe.AddIngredient(ItemID.CrimtaneBar, 20);
            recipe.AddIngredient(ItemID.TissueSample, 10);
            recipe.AddTile(TileID.TinkerersWorkbench);

            recipe.Register();
        }
	}
}
