using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using SummonerPlus.Content.Items.Weapons.SummoningStaffs;

namespace SummonerPlus.Content
{
    internal class CustomRecipes : ModSystem
    {
        public override void AddRecipes()
        {
            Recipe recipe = Recipe.Create(ItemID.BabyBirdStaff);

            recipe.AddIngredient(ItemID.Bird);
            recipe.AddIngredient(ItemID.Hay, 50);
            recipe.AddIngredient(ItemID.Wood, 100);
            recipe.AddTile(TileID.WorkBenches);

            recipe.Register();

            List<Recipe> rec = Main.recipe.ToList();

            rec.Where(x => x.createItem.type == ItemID.ThornWhip).ToList().ForEach(s =>
            {
                s.AddIngredient(ItemID.BeeWax, 10);

                //alternatively, we could use our ResetRecipe() function
            }); 
            
            rec = Main.recipe.ToList();

            rec.Where(x => x.createItem.type == ItemID.OpticStaff).ToList().ForEach(s =>
            {
                s.AddIngredient(ModContent.ItemType<EyeStaff>());

            });
        }
    }
}
