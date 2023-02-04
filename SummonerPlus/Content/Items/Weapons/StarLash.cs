using SummonerPlus.Content.Buffs;
using SummonerPlus.Content.Projectiles;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace SummonerPlus.Content.Items.Weapons
{
	public class StarLash : ModItem
	{
		public override void SetStaticDefaults() {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            Tooltip.SetDefault("25 summon tag damage\n15% summon tag critical strike chance\nRight clicking gives you the star frenzy buff for 10 seconds\n60 second cooldown\nYour summons will focus struck enemies\n[c/FF4500:Whip suggested by u/Mezminte]");
        }

		public override void SetDefaults() {
            // This method quickly sets the whip's properties.
            // Mouse over to see its parameters.
            Item.DamageType = DamageClass.SummonMeleeSpeed;
            Item.damage = 200;
            Item.knockBack = 8f;
            Item.rare = ItemRarityID.Red;

            Item.shootSpeed = 4f;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.UseSound = SoundID.Item152;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.value = 100000;
        }
        public override bool AltFunctionUse(Player player)//You use this to allow the item to be right clicked
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)//Sets what happens on right click(special ability)
            {
                if (!player.HasBuff<StarBreak>())
                {
                    Item.UseSound = SoundID.Item4;
                    player.AddBuff(ModContent.BuffType<StarBreak>(), 3600);
                    player.AddBuff(ModContent.BuffType<StarFrenzy>(), 600);
                }
                else
                { 
                    Item.UseSound = null;
                }

                Item.DamageType = DamageClass.SummonMeleeSpeed;
                Item.damage = 200;
                Item.knockBack = 8f;
                Item.rare = ItemRarityID.Red;

                Item.shoot = ProjectileID.None;
                Item.shootSpeed = 4f;

                Item.useStyle = ItemUseStyleID.HoldUp;
                Item.useTime = 30;
                Item.useAnimation = 30;
                Item.noMelee = true;
                Item.noUseGraphic = true;
            }
            else //Sets what happens on left click(normal use)
            { 
                Item.DamageType = DamageClass.SummonMeleeSpeed;
                Item.damage = 200;
                Item.knockBack = 8f;
                Item.rare = ItemRarityID.Red;

                Item.shoot = ModContent.ProjectileType<StarLashProjectile>();
                Item.shootSpeed = 4f;

                Item.useStyle = ItemUseStyleID.Swing;
                Item.useTime = 30;
                Item.useAnimation = 30;
                Item.UseSound = SoundID.Item152;
                Item.noMelee = true;
                Item.noUseGraphic = true;
            }

            return true;
        }
        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ItemID.FragmentStardust, 18);
            recipe.AddTile(TileID.LunarCraftingStation);

            recipe.Register(); 
        }

        // Makes the whip receive melee prefixes
        public override bool MeleePrefix() {
			return true;
		}
	}
}
