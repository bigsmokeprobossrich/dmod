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
	public class HadesBolt2 : ModProjectile
	{

		public override void SetStaticDefaults()//The second bullet hades fires
			//This one flies in a straight line and creates a big explosion
		{
			DisplayName.SetDefault("Inferna Blast");
			Main.projFrames[projectile.type] = 5;
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
			projectile.extraUpdates = 3;
			aiType = ProjectileID.Bullet;

		}

		public override void AI()//Fancy effects
		{

			Lighting.AddLight(projectile.position, 1f, 0.85f, 0.85f);
			Vector2 vector14 = projectile.Center + projectile.velocity * 3f;
			if (Main.rand.Next(3) == 0)
			{
				int num30 = Dust.NewDust(vector14 - projectile.Size / 2f, projectile.width, projectile.height, 244, projectile.velocity.X, projectile.velocity.Y, 100, new Color(255, 111, 0), 1f);
				Main.dust[num30].noGravity = false;
				Main.dust[num30].position -= projectile.velocity;
			}



			if (++projectile.frameCounter >= 3)
			{
				projectile.frameCounter = 0;
				if (++projectile.frame >= 5)
				{
					projectile.frame = 0;
				}
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

		public override void Kill(int timeLeft)//Cool big explosion
		{

			Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);

			// Play explosion sound
			Main.PlaySound(SoundID.Item15, projectile.position);

			// Fire Dust spawn
			for (int i = 0; i < 80; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 3f);
				Main.dust[dustIndex].noGravity = true;
				Main.dust[dustIndex].velocity *= 5f;
				dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 3f;
			}
			for (int i = 0; i < 50; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 1.4f;

			}
		}
	}
}





