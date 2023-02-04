using SummonerPlus.Content.Projectiles;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace SummonerPlus.Content.Items.Weapons
{
	public class ChlorophyteSlash : ModItem
	{
		public override void SetStaticDefaults() {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            Tooltip.SetDefault("Spore clouds are released when enemies are struck by summons\nYour summons will focus struck enemies");
        }

		public override void SetDefaults() {
            // This method quickly sets the whip's properties.
            // Mouse over to see its parameters.
            Item.DamageType = DamageClass.SummonMeleeSpeed;
            Item.damage = 70;
            Item.knockBack = 4f;
            Item.rare = ItemRarityID.Lime;

            Item.shoot = ModContent.ProjectileType<ChlorophyteSlashProjectile>();
            Item.shootSpeed = 4f;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.UseSound = SoundID.Item152;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.value = 40000;
        }

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		public override void AddRecipes() {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ItemID.ChlorophyteBar, 12);
            recipe.AddTile(TileID.MythrilAnvil);

            recipe.Register();
        }
		
		// Makes the whip receive melee prefixes
		public override bool MeleePrefix() {
			return true;
		}
	}
}
