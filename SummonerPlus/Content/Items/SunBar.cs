using SummonerPlus.Common.Players;
using SummonerPlus.Content.Buffs;
using SummonerPlus.Content.Items.Weapons;
using SummonerPlus.Content.Projectiles;
using SummonerPlus.Content.Tiles;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace SummonerPlus.Content.Items
{
    public class SunBar : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 25;
            ItemID.Sets.SortingPriorityMaterials[Type] = 59;
        }

        public override void SetDefaults()
        {
            // This method quickly sets the whip's properties.
            // Mouse over to see its parameters.
            Item.rare = ItemRarityID.Yellow;
            Item.value = 5000;
            Item.maxStack = 999;
            Item.consumable = true;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useTurn = true;
            Item.autoReuse = true;

            Item.createTile = ModContent.TileType<SunBarTile>();
            Item.placeStyle = 0;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<SunRockOre>(), 4);
            recipe.AddTile(TileID.AdamantiteForge);

            recipe.Register();
        }
    }
}

