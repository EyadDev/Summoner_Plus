using SummonerPlus.Common.Players;
using SummonerPlus.Content.Buffs;
using SummonerPlus.Content.Items.Weapons;
using SummonerPlus.Content.Projectiles;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace SummonerPlus.Content.Items.Consumables
{
	public class AttackSpeedPotion : ModItem
	{
		public override void SetStaticDefaults() {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 30;
            Tooltip.SetDefault("Increases attack speed of all weapons by 10%");
        }

		public override void SetDefaults() {
            // This method quickly sets the whip's properties.
            // Mouse over to see its parameters.
            Item.rare = ItemRarityID.Blue;
            Item.consumable = true;
            Item.value = 1000;
            Item.buffTime = 28800;
            Item.buffType = ModContent.BuffType<AttackSpeedBuff>();
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item3;
            Item.maxStack = 30;
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes() {

            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ItemID.BottledWater);
            recipe.AddIngredient(ItemID.Cactus);
            recipe.AddIngredient(ItemID.Blinkroot);
            recipe.AddIngredient(ItemID.Deathweed);
            recipe.AddTile(TileID.Bottles);

            recipe.Register();
        }
	}
}
