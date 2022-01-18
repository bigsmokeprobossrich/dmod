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
    public class AetherVoidAR : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aether");
            Tooltip.SetDefault("The third superweapon, forged by your own hands.  The fabric of space and time is yours"
           );

        }


        public override void SetDefaults()
        {




            item.rare = ItemRarityID.Red;
            item.value = Item.sellPrice(platinum: 37);
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.useTime = 20;
            item.UseSound = SoundID.Item122;
            item.useAnimation = 20;
            item.autoReuse = true;
            item.channel = true;
            item.mana = 0;
            item.noMelee = true;
            item.ranged = true;
            item.damage = 23000;
            item.knockBack = 2;

            item.shoot = 10;

            item.shootSpeed = 14f;
            item.shoot = ModContent.ProjectileType<VoidCutter>();



        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-27, 5);
        }



        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LunarBar, 69);
            recipe.AddIngredient(ModContent.ItemType<GaussRifle>());
            recipe.AddIngredient(ModContent.ItemType<DevotionII>());
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



            type = ModContent.ProjectileType<VoidCutter>();
            return true;



        }



    }

}
