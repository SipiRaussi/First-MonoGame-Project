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
        public static Texture2D         Texture;

        public Animation                Animation;
        public Vector2                  Position;

        protected bool                  active;
        protected Vector2               direction;
        protected float                 rotation;
        protected float                 moveSpeed = 640f;
        protected float                 traveledDistance = 0f;
        protected float                 range = 500f;
        protected int                   damage = 10;

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
            Animation.Initialize(Texture,
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

        /// <summary>
        /// Update positions.
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void Update(GameTime gameTime)
        {
            if (Active)
            {
                //Calculate the distance to move
                float deltaAmt = moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                //Keep track of distance
                traveledDistance += deltaAmt;

                //Translate position
                Position += direction * deltaAmt;

                //Translate and update animation
                Animation.Position = Position;
                Animation.Update(gameTime); 
            }
        }

        /// <summary>
        /// Tests if the projectile is at the end of its range.
        /// </summary>
        public virtual bool RangeCheck()
        {
            //Check for end of range
            if (traveledDistance >= range)
            {
                return true;
            }
            else
            {
                return false;
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
