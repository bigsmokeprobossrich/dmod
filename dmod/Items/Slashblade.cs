using dmod.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace dmod.Items
{
    public class Slashblade : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Slash Blade");
            Tooltip.SetDefault("The sword is very lightweight and it emits a cozy warmth." +
                "  Effected By ranged and melee damage.");
        }

        public override void SetDefaults()//gives it the slash attack
        {
            
            item.damage = 17620;
            item.crit = 30;
            item.melee = true;
            item.ranged = true;
            item.width = 80;
            item.height = 70;
            item.useTime = 2;
            item.useAnimation = 60;
            item.channel = true;
            item.noMelee = true;
            item.useStyle = 5;
            item.knockBack = 6;
            item.value = Item.sellPrice(36, 0, 50, 0);
            item.rare = ItemRarityID.Red;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("Slash");
            item.shootSpeed = 40f;
            item.noUseGraphic = true;
            
            

        }

        public override bool AltFunctionUse(Player player)//gives it the fireball attack
        {
            return true;
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                
                
                item.damage = 8000;
                item.crit = 25;
                item.melee = true;
                item.ranged = true;
                item.width = 75;
                item.height = 75;
                item.useTime = 10;
                item.useAnimation = 10;
                item.channel = false;
                item.noUseGraphic = false;
                
                item.useStyle = 1;
                item.knockBack = 0;
                item.value = Item.buyPrice(36, 0, 50, 0);
                item.rare = ItemRarityID.Red;
                item.UseSound = SoundID.Item1;
                item.autoReuse = true;
                item.shoot = ModContent.ProjectileType<SlashBeam>();
                item.shootSpeed = 30f;
               

            }
            else//makes sure it can still use the slash attack
            {
                item.damage = 17620;
                item.crit = 30;
                item.melee = true;
                item.ranged = true;
                item.width = 80;
                item.height = 70;
                item.useTime = 2;
                item.useAnimation = 60;
                item.channel = true;
                item.noMelee = true;
                item.useStyle = 5;
                item.knockBack = 6;
                item.value = Item.sellPrice(36, 0, 50, 0);
                item.rare = ItemRarityID.Red;
                item.UseSound = SoundID.Item1;
                item.autoReuse = true;
                item.shoot = ModContent.ProjectileType<Slash>();
                item.shootSpeed = 40f;
                item.noUseGraphic = true;

            }
            return true;
        }


  
        public static void Channel(Item item)//I forgot what this does
        {
            item.channel = false;
        }
        
        

        
        

        public static void OnHitNPC(NPC target, int damage, float knockback, bool crit)//Sets the enemies on fire
        {

            target.AddBuff(BuffID.OnFire, 60);

        }
    }
}