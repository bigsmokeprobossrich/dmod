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
	public class SlashBeam : ModProjectile
	{

		public override void SetStaticDefaults()//The slash blade's fireball
		{
			DisplayName.SetDefault("Slash Beam");
		}
		

		public override void SetDefaults()
		{
			projectile.width = 16;               
			projectile.height = 16;             
			projectile.aiStyle = 16;             
			projectile.friendly = true;        
			projectile.hostile = false;         
			projectile.ranged = true;
			projectile.melee = true;
			projectile.penetrate = 1;           
			projectile.timeLeft = 600;          
			projectile.alpha = 50;             			          
			projectile.ignoreWater = true;          
			projectile.tileCollide = false;          
			projectile.extraUpdates = 1;            
			aiType = ProjectileID.RocketSnowmanIII;//Makes the projectile home into enemies
			
		}

		public override void AI()
		{

			Lighting.AddLight(projectile.position, 1f, 0.85f, 0.85f);
			Vector2 vector14 = projectile.Center + projectile.velocity * 3f;
			if (Main.rand.Next(3) == 0)
			{ 
				int num30 = Dust.NewDust(vector14 - projectile.Size / 2f, projectile.width, projectile.height, 222, projectile.velocity.X, projectile.velocity.Y, 100, new Color(255, 255, 255), 1f);
				Main.dust[num30].noGravity = false;
				Main.dust[num30].position -= projectile.velocity;
			}

			
			

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





