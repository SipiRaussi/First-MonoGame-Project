using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TopDownShooter
{
    class Enemy
    {
        // Animation representing the enemy
        public Animation EnemyAnimation;
        // The position of the enemy relative to the top left corner of the screen
        public Vector2 Position;
        // The state of the enemy
        public bool Active;
        // The hit points of the enemy, if this goes to zero the enemy dies
        public int Health;
        // The amount of damage the enemy inflicts on the player
        public int Damage;
        // The amount of score the enemy will give to the player
        public int Value;
        // Get the width of the enemy
        public int Width { get { return EnemyAnimation.FrameWidth; } }
        // Get the height of the enemy
        public int Height { get { return EnemyAnimation.FrameHeight; } }

        // The speed at witch the enemy moves
        private float enemyMoveSpeed;

        public void Initialize(Animation animation, Vector2 position)
        {
            // Load the enemy texture
            EnemyAnimation = animation;
            // Set position of the enemy
            Position = position;
            // We initialize the enemy to be active so it will be updated in the game
            Active = true;
            // Set health for the enemy
            Health = 10;
            // Set amount of damage the enemy can do
            Damage = 10;
            // Set how fast the enemy moves
            enemyMoveSpeed = 6f;
            // Set the score value of the enemy
            Value = 100;
        }

        public void Update(GameTime gameTime)
        {
            // The enemy always moves to the left so decrement its x position
            Position.X -= enemyMoveSpeed;
            // Update the position of the animation
            EnemyAnimation.Position = Position;
            // Update Anmation
            EnemyAnimation.Update(gameTime);
            // If the enemy is past the screen or its health reaches 0 then deactivate it
            if(Position.X < -Width || Health <= 0)
            {
                // By setting the Active flag to false, the game will remove this object from the active game list
                Active = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Draw the animation
            EnemyAnimation.Draw(spriteBatch);
        }
    }
}
