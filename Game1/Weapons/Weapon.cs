using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TopDownShooter
{
    abstract class Weapon
    {
        public Animation animation;
        public Vector2   Position;
        public bool      Active;

        private float rateOfFire = 800;

        TimeSpan spawnTime;
        TimeSpan previousSpawnTime;

        public virtual void Initialize()
        {
            spawnTime = TimeSpan.FromSeconds(60 / rateOfFire);
            previousSpawnTime = TimeSpan.Zero;
        }

        public virtual void Shoot(GameTime gameTime, Vector2 dir, float rotation)
        {
            if (gameTime.TotalGameTime - previousSpawnTime > spawnTime)
            {
                previousSpawnTime = gameTime.TotalGameTime;
                Projectile proj = new Laser();
                proj.Initialize(Position, dir, rotation); 
            }
        }

        public virtual void Update(Vector2 position)
        {
            Position = position;
        }

        /*public void Draw(SpriteBatch spriteBatch)
        {
            animation.Draw(spriteBatch, 0f);
        }*/
    }
}
