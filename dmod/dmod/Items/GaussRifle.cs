using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using dmod.Projectiles;
using dmod.Items;

namespace dmod.Items
{
    public class GaussRifle : ModItem
    {

        public override void SetStaticDefaults()//this gun is reeaaally ugly
            //A shotgun that fires invisible ghost bullets that bounce off walls
            //this item is intended to be obtained after defeating plantera
            //I hate this gun
        {
            DisplayName.SetDefault("Soul Storm"); 
            Tooltip.SetDefault("Torments enemies with spectral energy.");

        }


        public override void SetDefaults()
        {




            item.rare = ItemRarityID.Lime;
            item.value = Item.sellPrice(gold: 50);
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.useTime = 4;
            item.UseSound = SoundID.Item40;
            item.useAnimation = 12;
            item.autoReuse = true;


            item.noMelee = true;
            item.ranged = true;
            item.damage = 89;
            item.knockBack = 5;

            item.shoot = 10;

            item.shootSpeed = 70f;
            item.useAmmo = AmmoID.Bullet;



        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-9, 0);
        }



        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SpectreBar, 15);
            recipe.AddIngredient(ItemID.SpectreStaff,1);
            recipe.AddIngredient(ItemID.TacticalShotgun);
            recipe.AddIngredient(ItemID.MartianConduitPlating, 50);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();



        }


        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {

            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }

            type = ModContent.ProjectileType<SpectreBullet>();

            int numberProjectiles = 6 + Main.rand.Next(2); // 4 or 5 shots
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(15)); // 30 degree spread.
                                                                                                                // If you want to randomize the speed to stagger the projectiles
                
                // perturbedSpeed = perturbedSpeed * scale; 
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }





            return true;

        }




    }

}
