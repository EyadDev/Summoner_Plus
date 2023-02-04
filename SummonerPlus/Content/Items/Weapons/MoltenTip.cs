using SummonerPlus.Content.Projectiles;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace SummonerPlus.Content.Items.Weapons
{
	public class MoltenTip : ModItem
	{
		public override void SetStaticDefaults() {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            Tooltip.SetDefault("2 summon tag damage\nLights enemies on fire\nYour summons will focus struck enemies");
        }

		public override void SetDefaults() {
            // This method quickly sets the whip's properties.
            // Mouse over to see its parameters.
            Item.DamageType = DamageClass.SummonMeleeSpeed;
            Item.damage = 12;
            Item.knockBack = 0.5f;
            Item.rare = ItemRarityID.Blue;

            Item.shoot = ModContent.ProjectileType<MoltenTipProjectile>();
            Item.shootSpeed = 4f;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.UseSound = SoundID.Item152;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.value = 100000;
        }

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		public override void AddRecipes() {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<IronTip>());
            recipe.AddIngredient(ItemID.Torch, 99);
            recipe.AddIngredient(ItemID.PlatinumBar, 5);
            recipe.AddTile(TileID.Anvils);

            recipe.Register(); recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<IronTip>());
            recipe.AddIngredient(ItemID.Torch, 99);
            recipe.AddIngredient(ItemID.GoldBar, 5);
            recipe.AddTile(TileID.Anvils);

            recipe.Register();
        }
		
		// Makes the whip receive melee prefixes
		public override bool MeleePrefix() {
			return true;
		}
	}
}
