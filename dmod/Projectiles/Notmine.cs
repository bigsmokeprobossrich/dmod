using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace dmod.Projectiles
{
    class Slash : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 7;
        }

        public override void SetDefaults()
        {
            projectile.scale = 2f;
            projectile.width = 130;
            projectile.height = 128;
            projectile.aiStyle = -1;
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

            Player player = Main.player[projectile.owner]; // Cache the player that is the owner of this projectile.
            float num = 1.57079637f; // Hardcoded floating number (we'll get to this later).
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true); // Basically a position that we can use to properly place our projectile.
            if (player.direction > 0)
            {
                drawOffsetX = +0;
                drawOriginOffsetX = -0;
                drawOriginOffsetY = -0;
            }
            else if (player.direction < 0)
            {
                drawOffsetX = -0;
                drawOriginOffsetX = +0;
                drawOriginOffsetY = -0;
            }

            num = 0f; // We set the 'num' value to 0 (you can obviously also do this when you set the 'num' for the first time).
            if (projectile.spriteDirection == -1) // If the projectile is facing left.
            {
                num = 3.14159274f; // Hardcode the 'num' value.
            }
            if (++projectile.frameCounter >= 3)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 7)
                {
                    projectile.frame = 0;
                }
            }
            projectile.soundDelay--; // Something to do with sound (I'm not exactly sure WHAT this does).
            if (projectile.soundDelay <= 0)
            {
                Main.PlaySound(2, (int)projectile.Center.X, (int)projectile.Center.Y, 1);
                projectile.soundDelay = 12; // But it basically plays a sound and sets the sound delay to another value. As you may (or may not) know, every second in Terraria
            }                                      // consists of 60 ticks, so setting this value to '12' makes sure that this sound is played 5x every second (if my math skillz have not left me).
            if (Main.myPlayer == projectile.owner) // Check if the local player is the owner of this projectile, if it is we update the rest.
            {
                if (player.channel && !player.noItems && !player.CCed) // So if the player is still using this weapon, since this weapon channels, we update it.
                {                                                                                          // Otherwise we KILL it (mwuahahaha). So basically destroy this projectile if the item is not being used.
                    float scaleFactor6 = 1f; //
                    if (player.inventory[player.selectedItem].shoot == projectile.type) // Check if the item that is currently selected in the players' inventory is the one that is
                    {                                                                                                  // shooting this projectile.
                        scaleFactor6 = player.inventory[player.selectedItem].shootSpeed * projectile.scale; // Set the 'scaleFactor6' value
                    }
                    Vector2 vector13 = Main.MouseWorld - vector; // Get the direction vector between the mouse position and the vector (vector was declared earlier, remember?)
                    vector13.Normalize(); // Normalize this vector since we're not in need of any large values, we just need to get the direction out of this.
                    if (vector13.HasNaNs()) // This check is basically used to check if the X value of the direction is 'Not a Number' (or a negative value in this case).
                    {
                        vector13 = Vector2.UnitX * (float)player.direction; // If it is, we
                    }
                    vector13 *= scaleFactor6;
                    if (vector13.X != projectile.velocity.X || vector13.Y != projectile.velocity.Y) // If the vector13 value is actually changing something
                    {                                                                                                        // (so if the mouse or the player have been moved).
                        projectile.netUpdate = true; // Make sure it gets updated for everyone if you're in multiplayer.
                    }
                    projectile.velocity = vector13; // At last, set the velocity of this projectile to the 'vector13'. This is later used to set the rotation of the projectile correctly.
                }
                else
                {
                    projectile.Kill(); // Yeahh, so we destroy the projectile here if the item is not being used.
                }
            }
            Vector2 vector14 = projectile.Center + projectile.velocity * 3f; // The following few lines are just to make this projectile 'gorgeous'.

            if (Main.rand.Next(3) == 0) // And this function spawns some dusts at the given position without a gravity pull and at the set position (this does not affect the actual
            {                                           // use of this projectile... Like stated earlier: this is just to make things pretty).     
                int num30 = Dust.NewDust(vector14 - projectile.Size / 2f, projectile.width, projectile.height, 259, projectile.velocity.X, projectile.velocity.Y, 100, default(Color), 2f);
                Main.dust[num30].noGravity = true;
                Main.dust[num30].position -= projectile.velocity;
            }

            // These lines are actually the lines that set the position, rotation AND animation, sooo... very important!
            projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - projectile.Size / 2f; // Set the position of the Arkhalis projectile, based on the 'Scale' vector.
            projectile.rotation = projectile.velocity.ToRotation() + num; // As you can see, we apply the rotation of this projectile based on the velocity AND the 'num' variable.
                                                                          // Now, I'm sure that the 'num' variable is needed here, but since it's hardcoded, I'm not exactly sure WHAT is does.
                                                                          // You'll just have to fiddle around a bit with setting the 'num' variable untill you achieve the correct rotation.
            projectile.spriteDirection = projectile.direction; // Make sure that the visual direction of the projectile is set correctly.
            projectile.timeLeft = 2; // Makes sure that if this update does not get called a second time, this projectile is destroyed since it's only able to live for 2 more ticks.
            player.ChangeDir(projectile.direction); // Makes sure that the owner of this projectile isfacing the same way that the projectile is (so that you don't get a situation in which
                                                    // the projectile is on the left side of the player while the player is still facing the right.
            player.heldProj = projectile.whoAmI; // Again, not exactly sure what this is good for (you can test by commenting this line out of course, although my lucky guess is
                                                 // that it'll break something for sure.
            player.itemTime = 20; 
            player.itemAnimation = 20; 
        }
    }
}