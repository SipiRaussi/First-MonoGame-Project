﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TopDownShooter
{
    class Enemy
    {
        //Animation representing the enemy
        public Animation Animation;

        //The position of the enemy relative to the top left corner of the screen
        public Vector2   Position;
        public bool      Active;
        public int       Health;
        public int       Damage;
        public int       Value;

        //Height and width of the enemy
        public int       Width { get { return Animation.FrameWidth; } }
        public int       Height { get { return Animation.FrameHeight; } }

        //The speed at witch the enemy moves
        private float    moveSpeed;
        private float    rotation;

        public void Initialize(Animation animation, Vector2 position)
        {
            //Load the enemy texture
            Animation = animation;

            //Set position of the enemy
            Position = position;

            //We initialize the enemy to be active so it will be updated in the game
            Active = true;

            //Set health for the enemy
            Health = 10;

            //Set amount of damage the enemy can do
            Damage = 10;

            //Set how fast the enemy moves
            moveSpeed = 200;
            rotation = 0f;

            //Set the score value of the enemy
            Value = 100;
        }

        public void Update(GameTime gameTime, Vector2 playerPos)
        {
            //The enemy always moves to the left so decrement its x position
            //Position.X -= enemyMoveSpeed;

            Vector2 pointingVector = new Vector2(playerPos.X - Position.X, playerPos.Y - Position.Y);
            pointingVector.Normalize();

            Position += pointingVector * moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            rotation = (float)Math.Atan2(pointingVector.X, -pointingVector.Y);


            //Update the position of the animation
            Animation.Position = Position;

            //Update Anmation
            Animation.Update(gameTime);

            //If the enemy is past the screen or its health reaches 0 then deactivate it
            if(Position.X < -Width || Health <= 0)
            {
                //By setting the Active flag to false, the game will remove this object from the active game list
                Active = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Draw the animation
            Animation.Draw(spriteBatch, rotation);
        }
    }
}
