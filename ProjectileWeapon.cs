using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHOOTYMcSHOOTY
{
    abstract class ProjectileWeapon : Weapon
    {
        public override bool CheckIfHit(ColisionBox enemy)
        {
            throw new NotImplementedException();
        }

        public override void FireWeapon(ColisionBox[] enemy, MouseState mousoi)
        {
            if (Timer() == true && mousoi.LeftButton == ButtonState.Pressed)
            {
                //CreateProjectile
            }
        }

        public override void WeaponAnimation()
        {
            throw new NotImplementedException();
        }
        public abstract void ProjectileMove(); 
    }
}
