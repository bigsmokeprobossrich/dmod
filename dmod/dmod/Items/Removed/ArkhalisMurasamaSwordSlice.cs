using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dmod.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace dmod.Items.Removed
{
	public class ArkhalisMurasamaSwordSlice : ModProjectile
	{

		private const float AimResponsiveness = 1f;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Burning Slash");
			Main.projFrames[projectile.type] = 7;
		}


		public override void SetDefaults()
		{

			
			projectile.aiStyle = 595;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.melee = true;
			projectile.maxPenetrate = 100;
			projectile.extraUpdates = 1;
			projectile.alpha = 50;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
			aiType = ProjectileID.Arkhalis;
			projectile.CloneDefaults(ProjectileID.Arkhalis);

			projectile.width = 135;
			projectile.height = 100;
			projectile.scale = 2;
			
		}




		public override void AI()
		{

			

			Lighting.AddLight(projectile.position, 0.1f, 0.85f, 0.85f);

			//This will cycle through all of the frames in the sprite sheet
			int frameSpeed = 10;
			projectile.frameCounter++;
			if (projectile.frameCounter >= frameSpeed)
			{
				projectile.frameCounter = 0;
				projectile.frame++;
				if (projectile.frame >= Main.projFrames[projectile.type])
				{
					projectile.frame = 0;
				}
			}

			Player player = Main.player[projectile.owner];
			Vector2 rrp = player.RotatedRelativePoint(player.MountedCenter, true);

			UpdatePlayerVisuals(player, rrp);

			UpdateAim(rrp, player.HeldItem.shootSpeed);

			projectile.timeLeft = 3;

			

		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{



			SpriteEffects effects = projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
			Texture2D texture = Main.projectileTexture[projectile.type];
			int frameHeight = texture.Height / Main.projFrames[projectile.type];
			int spriteSheetOffset = frameHeight * projectile.frame;
			Vector2 sheetInsertPosition = (projectile.Center + Vector2.UnitY * projectile.gfxOffY - Main.screenPosition).Floor();

			
			
			
			return false;



		}

		private void UpdatePlayerVisuals(Player player, Vector2 playerHandPos)
		{
			// Place the Prism directly into the player's hand at all times.
			projectile.Center = playerHandPos;
			// The beams emit from the tip of the Prism, not the side. As such, rotate the sprite by pi/2 (90 degrees).
			
			projectile.spriteDirection = projectile.direction;

			// The Prism is a holdout projectile, so change the player's variables to reflect that.
			// Constantly resetting player.itemTime and player.itemAnimation prevents the player from switching items or doing anything else.
			player.ChangeDir(projectile.direction);
			player.heldProj = projectile.whoAmI;
			player.itemTime = 2;
			player.itemAnimation = 2;

			// If you do not multiply by projectile.direction, the player's hand will point the wrong direction while facing left.
			player.itemRotation = (projectile.velocity * projectile.direction).ToRotation();
		}

		private void UpdateAim(Vector2 source, float speed)
		{
			// Get the player's current aiming direction as a normalized vector.
			Vector2 aim = Vector2.Normalize(Main.MouseWorld - source);
			if (aim.HasNaNs())
			{
				aim = -Vector2.UnitY;
			}

			// Change a portion of the Prism's current velocity so that it points to the mouse. This gives smooth movement over time.
			aim = Vector2.Normalize(Vector2.Lerp(Vector2.Normalize(projectile.velocity), aim, AimResponsiveness));
			aim *= speed;

			if (aim != projectile.velocity)
			{
				projectile.netUpdate = true;
			}
			projectile.velocity = aim;
		}



	}

}
