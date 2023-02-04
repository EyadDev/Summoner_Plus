using SummonerPlus.Content.Projectiles;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace SummonerPlus.Content.Items.Weapons
{
	public class KeyChain : ModItem
	{
		public override void SetStaticDefaults() {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            Tooltip.SetDefault("2 homing keys spawn on enemy strike\nOccasionally a shadow key spawns instead which deals more damage and lights enemies on fire\nYour summons will focus struck enemies\n[c/FF4500:Whip suggested by u/kulingames]");
        }

		public override void SetDefaults() {
            // This method quickly sets the whip's properties.
            // Mouse over to see its parameters.
            Item.DamageType = DamageClass.SummonMeleeSpeed;
            Item.damage = 29;
            Item.knockBack = 2f;
            Item.rare = ItemRarityID.Green;

            Item.shoot = ModContent.ProjectileType<KeyChainProjectile>();
            Item.shootSpeed = 4f;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.UseSound = SoundID.Item152;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.value = 5000;
        }

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		public override void AddRecipes() {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ItemID.ShadowKey);
            recipe.AddIngredient(ItemID.GoldenKey, 3);
            recipe.AddIngredient(ItemID.Chain, 20);
            recipe.AddIngredient(ItemID.HellstoneBar, 5);
            recipe.AddTile(TileID.Anvils);

            recipe.Register();
        }
		
		// Makes the whip receive melee prefixes
		public override bool MeleePrefix() {
			return true;
		}
	}
}
