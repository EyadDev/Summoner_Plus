using SummonerPlus.Content.Projectiles;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace SummonerPlus.Content.Items.Weapons
{
	public class IronTip : ModItem
	{
		public override void SetStaticDefaults() {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            Tooltip.SetDefault("2 summon tag damage\nYour summons will focus struck enemies");
        }

		public override void SetDefaults() {
            // This method quickly sets the whip's properties.
            // Mouse over to see its parameters.
            Item.DamageType = DamageClass.SummonMeleeSpeed;
            Item.damage = 8;
            Item.knockBack = 0.5f;
            Item.rare = ItemRarityID.Blue;

            Item.shoot = ModContent.ProjectileType<IronTipProjectile>();
            Item.shootSpeed = 4f;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.UseSound = SoundID.Item152;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.value = 50000;
        }

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		public override void AddRecipes() {

            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ItemID.Silk, 15);
            recipe.AddIngredient(ItemID.IronBar, 10);
            recipe.AddTile(TileID.Anvils);

            recipe.Register();

            recipe = CreateRecipe();

            recipe.AddIngredient(ItemID.Silk, 15);
            recipe.AddIngredient(ItemID.LeadBar, 10);
            recipe.AddTile(TileID.Anvils);

            recipe.Register();
        }
		
		// Makes the whip receive melee prefixes
		public override bool MeleePrefix() {
			return true;
		}
	}
}
