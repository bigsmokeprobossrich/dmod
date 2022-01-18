using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace dmod.Items.Removed
{
	public class ArkhalisMurasama : ModItem//This folder contains things that will never see the light of day, such as this broken 2nd attempt at making the slash blade
		//I had to delete the first one instead of making it unobtainable because it broke the entire game
	{

		

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Draconic Flurry but broken"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("The sword is very lightweight and it emits a cozy warmth." +
				"  Effected By ranged and melee damage." +
				"");
		}

		public override void SetDefaults()
		{
			
			item.damage = 16320;
			item.melee = true;
			item.ranged = true;
			item.width = 100;
			item.height = 75;
			item.useTime = 10;
			item.useAnimation = 10;
			item.useStyle = 5;
			item.noUseGraphic = true;
			item.maxStack = 1;
			
		
			item.knockBack = 4;
			item.value = Item.sellPrice(platinum: 37);
			item.rare = ItemRarityID.Red;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;

			
			item.shootSpeed = 34;

		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LunarBar, 69);
			recipe.AddIngredient(ItemID.FragmentSolar, 210);
			recipe.AddIngredient(ItemID.FragmentVortex, 210);
			recipe.AddIngredient(ItemID.Arkhalis, 1);
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







			return true;
		}

		



	}
}