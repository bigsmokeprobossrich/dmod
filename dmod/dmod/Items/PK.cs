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

namespace dmod.Items
{
    public class PK : ModItem//Another titanfall reference
        //This weapon is a slow firing laser shotgun that does high damage
        //It is intended to be obtained after plantera is defeated
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Peacekeeper"); 
            Tooltip.SetDefault("Precision choke not included.");

        }


        public override void SetDefaults()
        {




            item.rare = ItemRarityID.Lime;
            item.value = Item.sellPrice(platinum: 1);
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.useTime = 50;
            item.UseSound = SoundID.Item33;
            item.useAnimation = 50;
            item.scale = 1;


            item.noMelee = true;
            item.ranged = true;
            item.damage = 133;
            item.knockBack = 6;

            item.shoot = 10;

            item.shootSpeed = 200f;
            item.useAmmo = AmmoID.Bullet;
            


        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-27, 0);
        }



        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.OnyxBlaster);
            recipe.AddIngredient(ItemID.Boomstick);
            recipe.AddIngredient(ItemID.ShroomiteBar, 15);
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

            type = ModContent.ProjectileType<Devobolt>();

            int numberProjectiles = 11 + Main.rand.Next(2); // Shotgun effect
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(6)); 
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            

            


            return true;

        }




    }

}
