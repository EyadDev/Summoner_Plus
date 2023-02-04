using SummonerPlus.Content.Projectiles;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace SummonerPlus.Content.Items.Weapons
{
	public class DiamondWebLash : ModItem
	{
		public override void SetStaticDefaults() {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            Tooltip.SetDefault("3 summon tag damage\noccasionally confuse enemies\nWorks well against groups of enemies\nYour summons will focus struck enemies\n[c/FF4500:Whip suggested by u/Obama_prism_VHS]");
        }

		public override void SetDefaults() {
            // This method quickly sets the whip's properties.
            // Mouse over to see its parameters.
            Item.DamageType = DamageClass.SummonMeleeSpeed;
            Item.damage = 20;
            Item.knockBack = 1f;
            Item.rare = ItemRarityID.Green;

            Item.shoot = ModContent.ProjectileType<DiamondWebLashProjectile>();
            Item.shootSpeed = 4f;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.UseSound = SoundID.Item152;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.value = 10000;
        }

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		public override void AddRecipes() {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ItemID.Cobweb, 350);
            recipe.AddIngredient(ItemID.Diamond, 10);
            recipe.AddTile(TileID.Anvils);

            recipe.Register();
        }
		
		// Makes the whip receive melee prefixes
		public override bool MeleePrefix() {
			return true;
		}
	}
}
