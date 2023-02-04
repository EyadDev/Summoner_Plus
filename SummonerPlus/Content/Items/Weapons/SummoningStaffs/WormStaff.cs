using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using SummonerPlus.Content.Buffs.MinionBuffs;
using SummonerPlus.Content.Projectiles.Minions;

namespace SummonerPlus.Content.Items.Weapons.SummoningStaffs
{

    public class WormStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Worm Staff");
            Tooltip.SetDefault("Summons a Crimson Heart to protect you\nThe Crimson Heart occasionally drops hearts when hit\nThe Crimson Heart steals the life of nearby enemies\nConsumes 2 complimentary minion slots\nOnly one heart can be summoned\n[c/64ff00:Complimentary Minion Staff]");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
            ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true; // This lets the player target anywhere on the whole screen while using a controller
            ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.damage = 12;
            Item.knockBack = 2;
            Item.mana = 10;
            Item.useTime = 36;
            Item.useAnimation = 36;
            Item.useStyle = ItemUseStyleID.Swing; // how the player's arm moves when using the item
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item44; // What sound should play when using the item

            // These below are needed for a minion weapon
            Item.noMelee = true; // this item doesn't do any melee damage
            Item.DamageType = DamageClass.Summon; // Makes the damage register as summon. If your item does not have any damage type, it becomes true damage (which means that damage scalars will not affect it). Be sure to have a damage type
            Item.buffType = ModContent.BuffType<WormMinionBuff>();
            // No buffTime because otherwise the item tooltip would say something like "1 minute duration"
            Item.shoot = ModContent.ProjectileType<Worm_HeadMinion>(); // This item creates the minion projectile
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            // Here you can change where the minion is spawned. Most vanilla minions spawn at the cursor position
            position = Main.MouseWorld;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            // This is needed so the buff that keeps your minion alive and allows you to despawn it properly applies

            float usedMinionSlots = 0;

            for (int i = 0; i < Main.projectile.Length; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == player.whoAmI)
                {
                    usedMinionSlots += Main.projectile[i].minionSlots;
                }
            }

            if (usedMinionSlots < player.maxMinions - 0.5f)
            {
                Item.shoot = ModContent.ProjectileType<Worm_HeadMinion>(); // This item creates the minion projectile

                player.AddBuff(Item.buffType, 2);
                // Minions have to be spawned manually, then have originalDamage assigned to the damage of the summon item
                if (player.ownedProjectileCounts[ModContent.ProjectileType<Worm_BodyMinion>()] < 1)
                {
                    var projectile = Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, Main.myPlayer);
                    projectile.originalDamage = Item.damage;

                    projectile = Projectile.NewProjectileDirect(source, position, velocity, ModContent.ProjectileType<Worm_BodyMinion>(), damage, knockback, Main.myPlayer);
                    projectile.originalDamage = Item.damage / 2;

                    projectile = Projectile.NewProjectileDirect(source, position, velocity, ModContent.ProjectileType<Worm_TailMinion>(), damage, knockback, Main.myPlayer);
                    projectile.originalDamage = Item.damage / 2;
                }
                else
                {
                    var projectile = Projectile.NewProjectileDirect(source, position, velocity, ModContent.ProjectileType<Worm_BodyMinion>(), damage, knockback, Main.myPlayer);
                    projectile.originalDamage = Item.damage / 2;
                }

                for (int i = 0; i < Main.projectile.Length; i++)
                {
                    if (Main.projectile[i].active && Main.projectile[i].owner == player.whoAmI && Main.projectile[i].type == ModContent.ProjectileType<Worm_TailMinion>())
                    {
                        Worm_TailMinion worm_Tail = (Worm_TailMinion)Main.projectile[i].ModProjectile;

                        worm_Tail.ReorderWorm();
                    }
                }

                Main.NewText(usedMinionSlots, Color.White);
            }

            // Since we spawned the projectile manually already, we do not need the game to spawn it for ourselves anymore, so return false
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();

            recipe.AddIngredient(ItemID.LifeCrystal, 3);
            recipe.AddIngredient(ItemID.CrimtaneBar, 15);
            recipe.AddIngredient(ItemID.TissueSample, 15);
            recipe.AddTile(TileID.Anvils);

            recipe.Register(); 
        }
    }
}
