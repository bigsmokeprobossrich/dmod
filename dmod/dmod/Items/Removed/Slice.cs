using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace dmod.Items.Removed
{
    public class Slice : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blazing Slash");
            Main.projFrames[projectile.type] = 7;
        }

        public override void SetDefaults()
        {
            projectile.width = 175;
            projectile.height = 100;
            projectile.aiStyle = 20;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.hide = true;
            projectile.ownerHitCheck = true;
            projectile.melee = true;

        }



        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            //3a: target.immune[projectile.owner] = 20;
            //3b: target.immune[projectile.owner] = 5;
        }

        

        public override void AI()
        {
            Lighting.AddLight(projectile.position, 0.1f, 0.85f, 0.85f);
            if (++projectile.frameCounter >= 3)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 7)
                {
                    projectile.frame = 0;
                }
            }

            projectile.direction = (projectile.spriteDirection = ((projectile.velocity.X > 0f) ? 1 : -1));
            projectile.rotation = projectile.velocity.ToRotation();
            if (projectile.velocity.Y > 16f)
            {
                projectile.velocity.Y = 16f;
            }
            if (projectile.spriteDirection == -1)
                projectile.rotation += MathHelper.Pi;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (projectile.spriteDirection == -1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            Texture2D texture = Main.projectileTexture[projectile.type];
            int frameHeight = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            int startY = frameHeight * projectile.frame;
            Rectangle sourceRectangle = new Rectangle(0, startY, texture.Width, frameHeight);
            Vector2 origin = sourceRectangle.Size() / 2f;
            origin.X = (float)((projectile.spriteDirection == 1) ? (sourceRectangle.Width + 5) : -5);

            Color drawColor = projectile.GetAlpha(lightColor);
            Main.spriteBatch.Draw(texture,
            projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY),
            sourceRectangle, drawColor, projectile.rotation, origin, projectile.scale = 2f, spriteEffects, 0f);

            return false;
        }
    }
}