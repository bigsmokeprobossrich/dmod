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
    public class HadesRifle : ModItem
        //Hades is the greek god of the underworld
        //The first superweapon I designed
        //I have planned a right click ability that allows you to suck the life out if enemies and heal yourself
        //Im really proud of this gun
        //I am also trying to figure out how to make hadesbolt1 home into enemies, but for now, it will just act like a regular bullet
        //I will elaborate on the lore of the superweapons later
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hades"); 
            Tooltip.SetDefault("An ancient superweapon and Astraeus's twin brother.  Do you like to play with fire?"
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
        //Any time you see holdout offset mentioned, its usually what makes your character hold guns correctly
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-27, 5);
        }



        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            
            recipe.AddIngredient(ModContent.ItemType<TheStepBelowHell>());
            
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


            //Makes the weapon fire its three bullets in a random order
            type = Main.rand.Next(new int[] { ProjectileType<Projectiles.HadesBolt1>(), ProjectileType<Projectiles.HadesBolt2>(), ProjectileType<Projectiles.HadesBolt3>() });
            return true;


           
        }

     


    }

}
