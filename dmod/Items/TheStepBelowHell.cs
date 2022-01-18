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
    public class TheStepBelowHell : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Step Below Hell");
            Tooltip.SetDefault("The Hades but edgier.  Do you like to play with fire?"
           );

        }


        public override void SetDefaults()
        {




            item.rare = ItemRarityID.Red;
            item.value = Item.sellPrice(platinum: 37);
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.useTime = 1;
            item.UseSound = SoundID.Item40;
            item.useAnimation = 5;
            item.autoReuse = true;


            item.noMelee = true;
            item.ranged = true;
            item.damage = 16520;
            item.knockBack = 2;

            item.shoot = 10;

            item.shootSpeed = 300f;




        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-27, 5);
        }



        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            
            recipe.AddIngredient(ModContent.ItemType<HadesRifle>());
            
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



            type = Main.rand.Next(new int[] { ProjectileType<Projectiles.HadesBolt1>(), ProjectileType<Projectiles.HadesBolt2>(), ProjectileType<Projectiles.HadesBolt3>() });
            return true;



        }



    }

}