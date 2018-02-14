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

        private bool active;
        private Vector2 direction;
        private float rotation;
        private float MoveSpeed = 640f;
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

        public bool Active
        {
            get
            {
                return active;
            }
            set
            {
                active = value;

                if (active == false)
                {
                    projectiles.Remove(this);
                }
            }
        }

        public int Damage { get => damage; private set => damage = value; }

        public Projectile()
        {
            if (Projectiles == null)
            {
                Projectiles = new List<Projectile>();
            }
        }

        public virtual void Initialize(Vector2 position, Vector2 direction, float rotation)
        {
            //Check if the projectile exists already, if not
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
            this.direction = direction;
            this.direction.Normalize();
            this.rotation = rotation;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (Active)
            {
                Position += direction * MoveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                Animation.Position = Position;
                Animation.Update(gameTime); 
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (Active)
            {
                Animation.Draw(spriteBatch, rotation);
            }
        }
    }
}
