using SummonerPlus.Content.Projectiles;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace SummonerPlus.Content.Items.Weapons
{
	public class SpectralHarvest : ModItem
	{
		public override void SetStaticDefaults() {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            Tooltip.SetDefault("Inflicts spectral flames on hit\nSpectral flames are spread by minion attacks\nSpectral flames get weaker as they spread\nYour summons will focus struck enemies");
        }

		public override void SetDefaults() {
            // This method quickly sets the whip's properties.
            // Mouse over to see its parameters.
            Item.DamageType = DamageClass.SummonMeleeSpeed;
            Item.damage = 100;
            Item.knockBack = 0.5f;
            Item.rare = ItemRarityID.Orange;

            Item.shoot = ModContent.ProjectileType<SpectralHarvestProjectile>();
            Item.shootSpeed = 4f;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.UseSound = SoundID.Item152;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.value = 500000;
        }
        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ItemID.ScytheWhip);
            recipe.AddIngredient(ItemID.SpectreBar, 20);
            recipe.AddTile(TileID.MythrilAnvil);

            recipe.Register(); 
        }

        // Makes the whip receive melee prefixes
        public override bool MeleePrefix() {
			return true;
		}
	}
}
