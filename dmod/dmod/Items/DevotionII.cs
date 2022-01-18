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
    public class DevotionII : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Devotion Lmg"); //The Devotion is an LMG that originated from the Titanfall series of games.  The most recent installment in this series is Apex Legends, which is my favorite game besides Terraria
            Tooltip.SetDefault("Still not as overpowered as the real thing.");//This weapon is unfinished, and is intended to be similar in power to the shadowspec weapons from Calamity
            
        }


        public override void SetDefaults()
        {




            item.rare = ItemRarityID.Red;
            item.value = Item.sellPrice(platinum: 37);
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.useTime = 1;
            item.UseSound = SoundID.DD2_ExplosiveTrapExplode;
            item.useAnimation = 5;
            
            

            item.noMelee = true;
            item.ranged = true;
            item.damage = 2500;
            item.knockBack = 2;
            
            item.shoot = 10;
           
            item.shootSpeed = 300f;
            item.useAmmo = AmmoID.Bullet;

            item.autoReuse = true;

        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-13, 5);
        }



        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LunarBar, 69);
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

            
            type = ModContent.ProjectileType<Devobolt>();


            return true;
            }


    
    }

}
