using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

        //Pixels per second
        private float moveSpeed;
        private float rotation;

        public void Initialize(Animation animation, Vector2 position)
        {
            PlayerAnimation = animation;
            // Set the starting position of the player around the middle of the screen and to the back
            Position = position;
            // Set player to be active
            Active = true;
            // set player health
            Health = 100;
            // Set player movement speed
            moveSpeed = 200.0f;
        }

        // Update player animation
        public void Update(GameTime gameTime, KeyboardState currentKeyboardState, KeyboardState previousKeyboardState, MouseState currentMouseState)
        {
            PlayerAnimation.Position = Position;
            PlayerAnimation.Update(gameTime);

            // Use the Keyboard
            if (currentKeyboardState.IsKeyDown(Keys.A))
            {
                Position.X -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (currentKeyboardState.IsKeyDown(Keys.D))
            {
                Position.X += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (currentKeyboardState.IsKeyDown(Keys.W))
            {
                Position.Y -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (currentKeyboardState.IsKeyDown(Keys.S))
            {
                Position.Y += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            void CalculatePlayerRotation()
            {
                Vector2 pointingVector = new Vector2(currentMouseState.Position.X +32- (Position.X + Width / 2), currentMouseState.Position.Y - (Position.Y - Height / 2));
                rotation = (float)Math.Atan2(pointingVector.X, -pointingVector.Y);
            }

            CalculatePlayerRotation();
            if (currentMouseState.LeftButton == ButtonState.Pressed)
            {
                //Call shoot or something
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            PlayerAnimation.Draw(spriteBatch, rotation);
        }

    }
}
