using SummonerPlus.Content.Items;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace SummonerPlus.Content.Items.Armor
{
	// The AutoloadEquip attribute automatically attaches an equip texture to this item.
	// Providing the EquipType.Body value here will result in TML expecting X_Arms.png, X_Body.png and X_FemaleBody.png sprite-sheet files to be placed next to the item's main texture.
	[AutoloadEquip(EquipType.Body)]
	public class FearBreastplate : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Increases your max number of minions by 1\nIncreases minion damage and whip speed by 12%");

			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override void SetDefaults()
        {
            Item.value = Item.sellPrice(gold: 4); // How many coins the item is worth
			Item.rare = ItemRarityID.Pink; // The rarity of the item
			Item.defense = 15; // The amount of defense the item will give when equipped
		}

		public override void UpdateEquip(Player player) 
		{
			player.GetDamage(DamageClass.Summon) += 0.12f;
            player.GetAttackSpeed(DamageClass.Melee) += 0.12f;
            player.maxMinions++; // Increase how many minions the player can have by one
		}

		// Please see Content/ExampleRecipes.cs for a detailed explanation of recipe creation.
		public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ItemID.TitaniumBar, 20);
            recipe.AddIngredient(ItemID.SoulofFright, 8);
            recipe.AddTile(TileID.MythrilAnvil);

            recipe.Register();

            recipe = CreateRecipe();

            recipe.AddIngredient(ItemID.AdamantiteBar, 20);
            recipe.AddIngredient(ItemID.SoulofFright, 8);
            recipe.AddTile(TileID.MythrilAnvil);

            recipe.Register();
        }
	}
}
