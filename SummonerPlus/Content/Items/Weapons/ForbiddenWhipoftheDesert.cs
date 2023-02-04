using SummonerPlus.Content.Items.Accessories;
using SummonerPlus.Content.Projectiles;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace SummonerPlus.Content.Items.Weapons
{
	public class ForbiddenWhipoftheDesert : ModItem
	{
		public override void SetStaticDefaults() {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            Tooltip.SetDefault("10 summon tag damage at day time\n8 summon tag damage at night time\nIncreased Whip stats during daytime\nYour summons will focus struck enemies");
            DisplayName.SetDefault("Forbidden Whip of the Desert");
        }

		public override void SetDefaults() {
            // This method quickly sets the whip's properties.
            // Mouse over to see its parameters.
            Item.DamageType = DamageClass.SummonMeleeSpeed;
            Item.damage = 35;
            Item.knockBack = 1.5f;
            Item.rare = ItemRarityID.Green;

            Item.shoot = ModContent.ProjectileType<ForbiddenWhipoftheDesertProjectile>();
            Item.shootSpeed = 4f;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.UseSound = SoundID.Item152;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.value = 30000;

            
        }

        public override void UpdateInventory(Player player)
        {
            if (Main.dayTime)
            {
                Item.damage = 55;
                Item.useTime = 25;
                Item.useAnimation = 25;
            }
            else
            {
                Item.damage = 45;
                Item.useTime = 30;
                Item.useAnimation = 30;
            }
        }



        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {

            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ItemID.AncientBattleArmorMaterial);
            recipe.AddIngredient(ItemID.SoulofNight, 8);
            recipe.AddIngredient(ItemID.SoulofLight, 8);
            recipe.AddTile(TileID.MythrilAnvil);

            recipe.Register();
        }

        // Makes the whip receive melee prefixes
        public override bool MeleePrefix() {
			return true;
		}
	}
}
