using SummonerPlus.Common.Players;
using SummonerPlus.Content.Projectiles;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace SummonerPlus.Content.Items.Accessories
{
	public class BandOfFriendship : ModItem
	{
		public override void SetStaticDefaults() {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            Tooltip.SetDefault("Increased number of complimentary minion slots by 1");
        }

		public override void SetDefaults() {
            // This method quickly sets the whip's properties.
            // Mouse over to see its parameters.
            Item.rare = ItemRarityID.Green;
            Item.accessory = true;
            Item.value = Item.sellPrice(gold: 2);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<ComplimentaryMinionSlotsPlayer>().HasBandOfFriendship = true;
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes() {

            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ItemID.LifeCrystal);
            recipe.AddIngredient(ItemID.DemoniteBar, 10);
            recipe.AddIngredient(ItemID.ShadowScale, 10);
            recipe.AddTile(TileID.TinkerersWorkbench);

            recipe.Register();

            recipe = CreateRecipe();

            recipe.AddIngredient(ItemID.LifeCrystal);
            recipe.AddIngredient(ItemID.CrimtaneBar, 10);
            recipe.AddIngredient(ItemID.TissueSample, 10);
            recipe.AddTile(TileID.TinkerersWorkbench);

            recipe.Register();
        }
	}
}
