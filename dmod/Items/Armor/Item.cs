using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria.ModLoader;
using Terraria.ID;

namespace dmod.Items.Armor//Braindead simple vanity mask
{
	[AutoloadEquip(EquipType.Head)]
	public class Item : ModItem
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("V"); 
			Tooltip.SetDefault("Please ignore this.");
		}
		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 20;
			item.rare = ItemRarityID.LightRed;
			item.vanity = true;
			item.scale = 2;
		}

		public override bool DrawHead()
		{
			return false;
		}
	}
}