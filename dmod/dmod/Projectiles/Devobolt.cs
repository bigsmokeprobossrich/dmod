using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace dmod.Projectiles
{
	public class Devobolt : ModProjectile//A simple laser
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Energy Projectile");
		}


		public override void SetDefaults()
		{
			projectile.width = 8;               
			projectile.height = 8;             
			projectile.aiStyle = 1;             
			projectile.friendly = true;        
			projectile.hostile = false;         
			projectile.ranged = true;          
			projectile.penetrate = 1;           
			projectile.timeLeft = 600;          
			projectile.alpha = 255;             			          
			projectile.ignoreWater = true;          
			projectile.tileCollide = true;          
			projectile.extraUpdates = 4;            
			aiType = ProjectileID.Bullet;
			
		}

		public override void AI()
		{

			Lighting.AddLight(projectile.position, 0.85f, 0.85f, 1f);
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			//Redraw the projectile with the color not influenced by light
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
				spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}

		public override void Kill(int timeLeft)
		{
			 
			Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
			
		}
	}
}





