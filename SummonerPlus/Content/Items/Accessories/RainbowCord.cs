using SummonerPlus.Common.Players;
using SummonerPlus.Content.Items.Weapons;
using SummonerPlus.Content.Projectiles;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace SummonerPlus.Content.Items.Accessories
{
	public class RainbowCord : ModItem
	{
		public override void SetStaticDefaults() {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            Tooltip.SetDefault("20% increased summon damage\n75% increased whip range\nWhips inflict weak Spectral flames on hit");
        }

		public override void SetDefaults() {
            // This method quickly sets the whip's properties.
            // Mouse over to see its parameters.
            Item.rare = ItemRarityID.Cyan;
            Item.accessory = true;
            Item.value = 100000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Summon) += 0.2f;
            player.whipRangeMultiplier += 0.75f;
            player.GetModPlayer<SpectralFlameInflictionPlayer>().HasSpectralFlameInflictionItem = true;
        }

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            if (incomingItem.type == ModContent.ItemType<ExtensionCord>() || incomingItem.type == ModContent.ItemType<HotCord>() || incomingItem.type == ModContent.ItemType<ColdMechanicalCord>())
            {
                return false;
            }

            return true;
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes() {

            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<ColdMechanicalCord>());
            recipe.AddIngredient(ModContent.ItemType<RainbowGem>(), 3);
            recipe.AddIngredient(ModContent.ItemType<SunBar>(), 10);
            recipe.AddIngredient(ItemID.SpectreBar, 10);
            recipe.AddTile(TileID.TinkerersWorkbench);

            recipe.Register();
        }
	}
}
