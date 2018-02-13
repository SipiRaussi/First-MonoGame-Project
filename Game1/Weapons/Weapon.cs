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


        public virtual void Shoot(Vector2 dir, float rotation)
        {
            Projectile proj = new Laser();
            proj.Initialize(Position);
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
