using SummonerPlus.Common.Players;
using SummonerPlus.Content.Buffs;
using SummonerPlus.Content.Items.Weapons;
using SummonerPlus.Content.Projectiles;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace SummonerPlus.Content.Items
{
	public class RainbowGem : ModItem
	{
		public override void SetStaticDefaults() {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
        }

		public override void SetDefaults() {
            // This method quickly sets the whip's properties.
            // Mouse over to see its parameters.
            Item.rare = ItemRarityID.Lime;
            Item.value = 10000;
            Item.maxStack = 999;
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes() {

            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ItemID.Sapphire);
            recipe.AddIngredient(ItemID.Emerald);
            recipe.AddIngredient(ItemID.Amethyst);
            recipe.AddIngredient(ItemID.Topaz);
            recipe.AddIngredient(ItemID.Ruby);
            recipe.AddIngredient(ItemID.Diamond);
            recipe.AddIngredient(ItemID.Amber);
            recipe.AddTile(TileID.AdamantiteForge);

            recipe.Register();
        }
	}
}
