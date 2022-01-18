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
using dmod.Dusts;
namespace dmod.Projectiles
{
	public class AstralDivinity : ModProjectile
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Astral Divinity");
			
		}


		public override void SetDefaults()
		{
			projectile.width = 16;
			projectile.height = 16;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.ranged = true;

			projectile.penetrate = 1;
			projectile.timeLeft = 250;
			projectile.alpha = 255;
			projectile.ignoreWater = true;
			projectile.tileCollide = true;
			projectile.extraUpdates = 1;
			aiType = ProjectileID.GoldenBullet;

		}

		public override void AI()// Gives it some fancy particle effects
		{

			Lighting.AddLight(projectile.position, .6f, 0.6f, 1f);
			Vector2 vector14 = projectile.Center + projectile.velocity * 3f;
			if (Main.rand.Next(3) == 0)
			{
				int num30 = Dust.NewDust(vector14 - projectile.Size / 2f, projectile.width, projectile.height, DustID.Clentaminator_Cyan, projectile.velocity.X, projectile.velocity.Y, 100, default(Color), 1f);
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

			Main.PlaySound(SoundID.Item15, projectile.position);

			// Fire Dust spawn
			for (int i = 0; i < 80; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.Clentaminator_Cyan, 0f, 0f, 100, default(Color), 3f); ;
				Main.dust[dustIndex].noGravity = true;
				Main.dust[dustIndex].velocity *= 15f;
				
			}
			for (int i = 0; i < 50; i++)
			{
				int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.Clentaminator_Blue, 0f, 0f, 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 10f;

			}

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