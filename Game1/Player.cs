﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TopDownShooter
{
    class Player
    {
        //public Texture2D PlayerTexture;
        public Animation PlayerAnimation;
        public Vector2   Position;
        public bool      Active;
        public int       Health;
        public int       Width { get { return PlayerAnimation.FrameWidth; } }
        public int       Height { get { return PlayerAnimation.FrameHeight; } }

        //Pixels per second
        private float    moveSpeed;
        private float    rotation;
        private Vector2  pointingVector;

        //Weapon for shooting baddies
        private Weapon   currentWeapon;

        public void Initialize(Animation animation, Vector2 position)
        {
            //Initialize variables
            PlayerAnimation = animation;
            Position = position;
            Active = true;
            Health = 100;
            moveSpeed = 200.0f;

            currentWeapon = new Blaster();
            currentWeapon.Initialize();
        }

        // Update player animation
        public void Update(GameTime gameTime)
        {
            PlayerAnimation.Position = Position;
            PlayerAnimation.Update(gameTime);

            Point mousePosition = InputManager.Instance.MousePosition();

            //Check keyboard inputs
            if (InputManager.Instance.KeyDown(Keys.A))
            {
                Position.X -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (InputManager.Instance.KeyDown(Keys.D))
            {
                Position.X += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (InputManager.Instance.KeyDown(Keys.W))
            {
                Position.Y -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            if (InputManager.Instance.KeyDown(Keys.S))
            {
                Position.Y += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }


            void CalculatePlayerRotation()
            {
                //Make a vector from player 
                pointingVector = new Vector2(mousePosition.X - (Position.X - Width / 2), mousePosition.Y - (Position.Y - Height / 2));
                //pointingVector.Normalize();
                rotation = (float)Math.Atan2(pointingVector.X, -pointingVector.Y);
            }

            //Rotation calculation
            CalculatePlayerRotation();

            currentWeapon.Update(Position);

            if (InputManager.Instance.MouseLeftDown())
            {
                currentWeapon.Shoot(gameTime, pointingVector, rotation);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            PlayerAnimation.Draw(spriteBatch, rotation);
            //TODO: draw weapon
        }

    }
}
