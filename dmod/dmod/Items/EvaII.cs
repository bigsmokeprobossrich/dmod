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
    public class EVAII : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eva Shotgun"); //The regular eva, but I accidentally made it stupidly powerful
            //I am intending on making this a drop from the Moon Lord
            //This item is another titanfall reference, but I stylized the sprite a ton
            Tooltip.SetDefault("'That's not right...'");

        }


        public override void SetDefaults()
        {




            item.rare = ItemRarityID.Red;
            item.value = Item.sellPrice(platinum: 37);
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.useTime = 10;
            item.UseSound = SoundID.Item36;
            item.useAnimation = 10;

            item.autoReuse = true;

            item.noMelee = true;
            item.ranged = true;
            item.damage = 1100;
            item.knockBack = 5;

            item.shoot = 10;

            item.shootSpeed = 200f;
            item.useAmmo = AmmoID.Bullet;



        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-8, 7);
        }



        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ModContent.ItemType<PK>());
            recipe.AddIngredient(ItemID.LunarBar, 15);
            recipe.AddIngredient(ItemID.ShroomiteBar, 15);
            recipe.AddTile(TileID.LunarCraftingStation);
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



            int numberProjectiles = 16 + Main.rand.Next(2); // 4 or 5 shots
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(8)); 
                                                                                                                // If you want to randomize the speed to stagger the projectiles
                                                                                                                // float scale = 1f - (Main.rand.NextFloat() * .3f);
                                                                                                                // perturbedSpeed = perturbedSpeed * scale; 
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }





            return true;

        }




    }

}
