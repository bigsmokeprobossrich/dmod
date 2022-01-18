using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace dmod.Projectiles
{
	
	public class SpectreBullet : ModProjectile
	{
		
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Spectral Bullet"); 
		}

		public override void SetDefaults()
		{
			projectile.width = 6; 
			projectile.height = 6; 
			projectile.alpha = 255; 
			projectile.friendly = true; 
			projectile.hostile = false; 
			projectile.penetrate = 2; 
			projectile.timeLeft = 600; 
			projectile.ignoreWater = false;
			projectile.tileCollide = true;
			projectile.ranged = true;
			projectile.extraUpdates = 2;
		}

		public override void AI()
		{
			/*if (projectile.wet)
			{
				projectile.Kill(); //This kills the projectile when touching water. However, since our projectile is a cursed flame, we will comment this so that it won't run it. If you want to test this, feel free to uncomment this.
			}*/
			// Using a timer, we scale the earliest spawned dust smaller than the rest.
			float dustScale = 1f;
			if (projectile.ai[0] == 0f)
				dustScale = 0.25f;
			else if (projectile.ai[0] == 1f)
				dustScale = 0.5f;
			else if (projectile.ai[0] == 2f)
				dustScale = 0.75f;

			if (Main.rand.Next(2) == 0)
			{
				Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height, DustID.SpectreStaff, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100);

				// Some dust will be large, the others small and with gravity, to give visual variety.
				if (Main.rand.NextBool(3))
				{
					dust.noGravity = true;
					dust.scale *= 2f;
					dust.velocity.X *= 2f;
					dust.velocity.Y *= 2f;
				}

				dust.scale *= 1.5f;
				dust.velocity *= 1.2f;
				dust.scale *= dustScale;
			}
			projectile.ai[0] += 1f;
		}

		
		public override void ModifyDamageHitbox(ref Rectangle hitbox)
		{
			// By using ModifyDamageHitbox, we can allow the flames to damage enemies in a larger area than normal without colliding with tiles.
			// Here we adjust the damage hitbox. We adjust the normal 6x6 hitbox and make it 66x66 while moving it left and up to keep it centered.
			int size = 30;
			hitbox.X -= size;
			hitbox.Y -= size;
			hitbox.Width += size * 2;
			hitbox.Height += size * 2;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			//Makes the bullet bounce on collision with tiles
			projectile.penetrate--;
			if (projectile.penetrate <= 0)
			{
				projectile.Kill();
			}
			else
			{
				Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
				Main.PlaySound(SoundID.Item10, projectile.position);
				if (projectile.velocity.X != oldVelocity.X)
				{
					projectile.velocity.X = -oldVelocity.X;
				}
				if (projectile.velocity.Y != oldVelocity.Y)
				{
					projectile.velocity.Y = -oldVelocity.Y;
				}
			}
			return false;
		}
	}
}

