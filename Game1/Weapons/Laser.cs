using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopDownShooter
{
    class Laser : Projectile
    {
        //Inherit the constructor
        public Laser() : base()
        {
            if (Projectiles == null)
            {
                Projectiles = new List<Projectile>();
            }
        }
    }
}
