using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SHOOTYMcSHOOTY
{
    public abstract class ProjectileWeapon : Weapon
    {
        public int fireSpeed;
        public Vector2 DirectionFire;
        public Texture2D projText;
        
        public ProjectileWeapon()
        {

        }
        public override bool CheckIfHit(ColisionBox enemy)
        {
            throw new NotImplementedException();
        }

        public override void FireWeapon(ColisionBox[] enemy, MouseState mousoi)
        {
            if (Timer() == true && mousoi.LeftButton == ButtonState.Pressed)
            {
                
            }
        }

        public virtual void fireProjectile(MouseState mousoi)
        {
        }

        public override void WeaponAnimation()
        {
            throw new NotImplementedException();
        }

        public void orientDirection(MouseState mousy)
        {
            float A = mousy.X - Position.X;
            float B = -(mousy.Y - Position.Y);
            float C = (float)Math.Cos(A);
            float D = (float)Math.Sin(A);
            DirectionFire = new Microsoft.Xna.Framework.Vector2(C * fireSpeed, D * fireSpeed);
        }
    }

    public class RocketLauncher : ProjectileWeapon
    {
        public RocketLauncher(int fireSpeed, Texture2D projText)
        {
            this.fireSpeed = fireSpeed;
            this.projText = projText;
        }
        public override bool CheckIfHit(ColisionBox enemy)
        {
            throw new NotImplementedException();
        }

        public override void FireWeapon(ColisionBox[] enemy, MouseState mousoi)
        {
            if (Timer() == true && mousoi.LeftButton == ButtonState.Pressed)
            {

            }
        }

       public virtual void fireProjectile(MouseState mousoi)
       {
            if (Timer() == true && mousoi.LeftButton == ButtonState.Pressed)
            {
                Rocket a = new Rocket(Position, new Point(100, 50), DirectionFire, new float[] { }, 15, 10, [[projText]]);
            }
        }

    }
}
