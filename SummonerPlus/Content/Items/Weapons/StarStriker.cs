using SummonerPlus.Content.Projectiles;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace SummonerPlus.Content.Items.Weapons
{
	public class StarStriker : ModItem
	{
		public override void SetStaticDefaults() {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            Tooltip.SetDefault("Stars fall on enemies struck by summons\nYour summons will focus struck enemies");
        }

		public override void SetDefaults() {
            // This method quickly sets the whip's properties.
            // Mouse over to see its parameters.
            Item.DamageType = DamageClass.SummonMeleeSpeed;
            Item.damage = 25;
            Item.knockBack = 2f;
            Item.rare = ItemRarityID.Green;

            Item.shoot = ModContent.ProjectileType<StarStrikerProjectile>();
            Item.shootSpeed = 4f;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.UseSound = SoundID.Item152;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.value = 900000;
        }

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		public override void AddRecipes() {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ItemID.FallenStar, 20);
            recipe.AddIngredient(ItemID.HellstoneBar, 10);
            recipe.AddTile(TileID.Anvils);

            recipe.Register();
        }
		
		// Makes the whip receive melee prefixes
		public override bool MeleePrefix() {
			return true;
		}
	}
}
