using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Game1
{
    class Laser
    {
        // Animation the represents the laser animation
        public Animation LaserAnimation;
        // Position of the laser
        public Vector2 Position;

        // The Speed the laser travels
        private float laserMoveSpeed = 30f;
        // The damege the laser deals
        private int damage = 10;
        // Set damage the laser deals
        public bool Active;
        // Laser beams range
        private int range;
        // The width of the laser image
        public int Width { get { return LaserAnimation.FrameWidth; } }
        // The height of the laser image
        public int Height { get { return LaserAnimation.FrameHeight; } }

        public void Initialize(Animation animation, Vector2 position)
        {
            LaserAnimation = animation;
            Position = position;
            Active = true;
        }

        public void Update(GameTime gameTime)
        {
            Position.X += laserMoveSpeed;
            LaserAnimation.Position = Position;
            LaserAnimation.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            LaserAnimation.Draw(spriteBatch);
        }

    }
}
