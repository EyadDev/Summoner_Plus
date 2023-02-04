using Humanizer;
using SummonerPlus.Content.Items;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace SummonerPlus.Content.Items.Armor
{
	// The AutoloadEquip attribute automatically attaches an equip texture to this item.
	// Providing the EquipType.Legs value here will result in TML expecting a X_Legs.png file to be placed next to the item's main texture.
	[AutoloadEquip(EquipType.Legs)]
	public class FearLeggings : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Increases minion damage and whip speed by 8% ");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.value = Item.sellPrice(gold: 3); // How many coins the item is worth
			Item.rare = ItemRarityID.Pink; // The rarity of the item
			Item.defense = 10; // The amount of defense the item will give when equipped
		}

		public override void UpdateEquip(Player player) 
        {
            player.GetDamage(DamageClass.Summon) += 0.08f;
            player.GetAttackSpeed(DamageClass.Melee) += 0.08f;
        }

        // Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ItemID.TitaniumBar, 16);
            recipe.AddIngredient(ItemID.SoulofFright, 6);
            recipe.AddTile(TileID.MythrilAnvil);

            recipe.Register(); 
            
            recipe = CreateRecipe();

            recipe.AddIngredient(ItemID.AdamantiteBar, 16);
            recipe.AddIngredient(ItemID.SoulofFright, 6);
            recipe.AddTile(TileID.MythrilAnvil);

            recipe.Register();
        }
    }
}
