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
        public static Texture2D blasterTexture;

        public Animation Animation;
        public Vector2 Position;

        public bool Active;
        private float MoveSpeed = 30f;
        private int damage = 10;
        private int range;

        //The width and height of the image
        public int Width { get { return Animation.FrameWidth; } }
        public int Height { get { return Animation.FrameHeight; } }

        public static List<Projectile> Projectiles
        {
            get
            {
                return projectiles;
            }
            protected set
            {
                projectiles = value;
            }
        }

        public Projectile()
        {
            if (Projectiles == null)
            {
                Projectiles = new List<Projectile>();
            }
        }

        public virtual void Initialize(Vector2 position)
        {
            if (!Projectiles.Contains(this))
            {
                Projectiles.Add(this);
            }

            Animation = new Animation();
            Animation.Initialize(blasterTexture,
                Position,
                32,
                40,
                1,
                30,
                Color.White,
                1f,
                true);
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

        /*public virtual void AddProjectile()
        {
            Animation animation = new Animation();
            animation.Initialize(blasterTexture,
                Position,
                32,
                40,
                1,
                30,
                Color.White,
                1f,
                true);

            //Projectile projectile = new Projectile();

            this.Initialize(animation, Position);
        }*/
    }
}
