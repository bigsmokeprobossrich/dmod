using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
namespace dmod.Items
{
	public class Devotion : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("BuriedSword"); // Braindead simple sword that I accidentally gave the wrong namespace
			Tooltip.SetDefault("A sword you found in some of the dirt you dug up." +
				"It seems to be made of a lightweight plastic-like material.");
		}

		public override void SetDefaults() 
		{
			item.damage = 5;
			item.melee = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 5;
			item.useAnimation = 6;
			item.useStyle = 1;
			item.knockBack = 3;
			item.value = 5;
			item.rare = 2;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}

		public override void AddRecipes() 
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DirtBlock, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}