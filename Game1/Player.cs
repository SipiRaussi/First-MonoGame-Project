using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TopDownShooter;

namespace Shooter
{
    class Player
    {
        //public Texture2D PlayerTexture;
        public Animation PlayerAnimation;
        public Vector2 Position;
        public bool Active;
        public int Health;
        public int Width { get { return PlayerAnimation.FrameWidth; } }
        public int Height { get { return PlayerAnimation.FrameHeight; } }

        public void Initialize(Animation animation, Vector2 position)
        {
            PlayerAnimation = animation;
            // Set the starting position of the player around the middle of the screen and to the back
            Position = position;
            // Set player to be active
            Active = true;
            // set player health
            Health = 100;
        }

        // Update player animation
        public void Update(GameTime gameTime)
        {
            PlayerAnimation.Position = Position;
            PlayerAnimation.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            PlayerAnimation.Draw(spriteBatch);
        }
    }
}
