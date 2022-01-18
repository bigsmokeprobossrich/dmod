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
	public class HadesBolt3 : ModProjectile
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hades Bolt");
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
			projectile.timeLeft = 250;
			projectile.alpha = 255;
			projectile.ignoreWater = true;
			projectile.tileCollide = true;
			projectile.extraUpdates = 3;
			aiType = ProjectileID.GoldenBullet;
			
		}

		public override void AI()// Gives it some fancy particle effects
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

		public override void Kill(int timeLeft)
		{

			Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);

			
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)//Adds various debuffs to make the enemy's life hell
		{
			target.AddBuff(BuffID.WitheredArmor, 240);
			target.AddBuff(BuffID.WitheredWeapon, 240);
			target.AddBuff(BuffID.Electrified, 240);
			target.AddBuff(BuffID.Suffocation, 240);
			target.AddBuff(BuffID.Midas, 240);
			target.AddBuff(BuffID.Burning, 240);
			target.AddBuff(BuffID.BetsysCurse, 240);
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{
			target.AddBuff(BuffID.WitheredArmor, 240, false);//makes sure you cant hurt yourself
			target.AddBuff(BuffID.WitheredWeapon, 240, false);
			target.AddBuff(BuffID.Electrified, 240, false);
			target.AddBuff(BuffID.Suffocation, 240, false);
			target.AddBuff(BuffID.Midas, 240, false);
			target.AddBuff(BuffID.Burning, 240, false);
			target.AddBuff(BuffID.BetsysCurse, 240, false);
		}

	}
	
}





