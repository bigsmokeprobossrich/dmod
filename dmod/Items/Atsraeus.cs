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
    public class AstraeusAntiMat : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Astraeus");
            Tooltip.SetDefault("Hades's twin brother and the second superweapon.  Yeet your enemies to the stars"
           );

        }


        public override void SetDefaults()
        {




            item.rare = ItemRarityID.Red;
            item.value = Item.sellPrice(platinum: 37);
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.useTime = 15;
            item.UseSound = SoundID.Item92;
            item.useAnimation = 15;

            item.autoReuse = true;

            item.noMelee = true;
            item.ranged = true;
            item.damage = 20000;
            item.knockBack = 10;

            item.shoot = 10;

            item.shootSpeed = 300f;




        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-27, 5);
        }





       


        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {

            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }



            type = ModContent.ProjectileType<AstralDivinity>();//Shotgun

            int numberProjectiles = 4 + Main.rand.Next(2);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(8));
                
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }

            return true;


        }



    }

}
