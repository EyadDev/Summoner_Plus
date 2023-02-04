using SummonerPlus.Content.Projectiles;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace SummonerPlus.Content.Items.Weapons
{
	public class LuminiteChain : ModItem
	{
		public override void SetStaticDefaults() {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            Tooltip.SetDefault("50% summon tag critical strike chance\nOn strike, causes enemy to implode dealing the whips base dmg and making minions deal 3x on the marked enemy\nMinions hitting a marked enemy spreads the debuff\nYour summons will focus struck enemies\n[c/FF4500:Whip suggested by u/MeThatsnotTaken]");
        }

		public override void SetDefaults() {
            // This method quickly sets the whip's properties.
            // Mouse over to see its parameters.
            Item.DamageType = DamageClass.SummonMeleeSpeed;
            Item.damage = 400;
            Item.knockBack = 10f;
            Item.rare = ItemRarityID.Purple;

            Item.shoot = ModContent.ProjectileType<LuminiteChainProjectile>();
            Item.shootSpeed = 4f;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.UseSound = SoundID.Item152;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.value = 2000000;
        }
        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<MoltenTip>());
            recipe.AddIngredient(ModContent.ItemType<SunSlash>());
            recipe.AddIngredient(ModContent.ItemType<StarStriker>());
            recipe.AddIngredient(ModContent.ItemType<SpectralHarvest>());
            recipe.AddIngredient(ItemID.FireWhip);
            recipe.AddIngredient(ItemID.MaceWhip);
            recipe.AddIngredient(ItemID.RainbowWhip);
            recipe.AddIngredient(ItemID.LunarBar, 20);
            recipe.AddTile(TileID.LunarCraftingStation);

            recipe.Register(); 
        }

        // Makes the whip receive melee prefixes
        public override bool MeleePrefix() {
			return true;
		}
	}
}
