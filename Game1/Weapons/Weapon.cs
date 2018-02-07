using System;
using Microsoft.Xna.Framework;

namespace TopDownShooter
{
    abstract class Weapon
    {
        public Animation animation;
        public Vector2 Position;
        public bool Active;


        public virtual void Shoot()
        {

        }
    }
}
