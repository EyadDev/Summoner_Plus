using SummonerPlus.Common.Players;
using SummonerPlus.Content.Items.Weapons;
using SummonerPlus.Content.Projectiles;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace SummonerPlus.Content.Items.Accessories
{
	public class ColdMechanicalCord : ModItem
	{
		public override void SetStaticDefaults() {
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            Tooltip.SetDefault("15% increased summon damage\n50% increased whip range\nWhips inflict Frostburn on hit");
        }

		public override void SetDefaults() {
            // This method quickly sets the whip's properties.
            // Mouse over to see its parameters.
            Item.rare = ItemRarityID.LightRed;
            Item.accessory = true;
            Item.value = 60000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetDamage(DamageClass.Summon) += 0.15f;
            player.whipRangeMultiplier += 0.5f;
            player.GetModPlayer<FrostBurnInflictionPlayer>().HasFrostburnInflictionItem = true;
        }

        public override bool CanAccessoryBeEquippedWith(Item equippedItem, Item incomingItem, Player player)
        {
            if (incomingItem.type == ModContent.ItemType<ExtensionCord>() || incomingItem.type == ModContent.ItemType<HotCord>())
            {
                return false;
            }

            return true;
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes() {

            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ModContent.ItemType<HotCord>());
            recipe.AddIngredient(ItemID.SummonerEmblem);
            recipe.AddIngredient(ItemID.FrostCore);
            recipe.AddIngredient(ItemID.HallowedBar, 10);
            recipe.AddIngredient(ItemID.SoulofFright);
            recipe.AddIngredient(ItemID.SoulofMight);
            recipe.AddIngredient(ItemID.SoulofSight);
            recipe.AddTile(TileID.TinkerersWorkbench);

            recipe.Register();
        }
	}
}
