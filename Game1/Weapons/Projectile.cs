using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TopDownShooter
{
    abstract class Projectile
    {
        private static List<Projectile> projectiles;

        public Animation Animation;
        public Vector2 Position;

        public bool Active;
        private float MoveSpeed = 30f;
        private int damage = 10;
        private int range;

        //The width and height of the image
        public int Width { get { return Animation.FrameWidth; } }        
        public int Height { get { return Animation.FrameHeight; } }

        public static List<Projectile> Projectiles { get; private set; }

        public Projectile()
        {
            if (!projectiles.Contains(this))
            {
                projectiles.Add(this);
            }
        }

        public virtual void Initialize(Animation animation, Vector2 position)
        {
            Animation = animation;
            Position = position;
            Active = true;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (Active)
            {
                Position.X += MoveSpeed;
                Animation.Position = Position;
                Animation.Update(gameTime); 
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (Active)
            {
                Animation.Draw(spriteBatch, 0f); 
            }
        }
    }
}
