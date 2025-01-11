using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Particles;
using System;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SHOOTYMcSHOOTY
{
    public abstract class Weapon
    {
        public Texture2D Texture;
        public Point Position;
        public float Scale;
        //In milliseconds
        public int ReloadTimeS;
        public int ReloadTimeC;

       

        public float Rotation;
        public float CurrentRotation
        {
            get
            {
                return -(Rotation);
            }
        }

        public Weapon()
        {

        }

        public void OrientWeapon(MouseState mousy)
        {
            float A = mousy.X - Position.X;
            float B = -(mousy.Y - Position.Y);
            Rotation = MathF.Atan2(B, A);
            //Rotation = -MathF.PI / 4;
        }
        public void DrawWeapon(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(Texture, new Vector2(Position.X, Position.Y), null, Color.White, CurrentRotation, Texture.Bounds.Center.ToVector2(), Scale, SpriteEffects.None, 0f);
        }
        public abstract void FireWeapon(ColisionBox[] a, MouseState mousy);
        public abstract bool CheckIfHit(ColisionBox enemy);
        public bool Timer()
        {
            if (ReloadTimeC >= ReloadTimeS)
            {
                return true;
            }
            else
            {
                ReloadTimeC++;
                return false;
            }
        }
       
        public abstract void WeaponAnimation();

        public int stupidSquaring2Thing(int a)
        {
            if (a >= 2)
            {
                int stupidVar = 1;
                while (true)
                {
                    if (a >= Math.Pow(2, stupidVar))
                    {
                        stupidVar++;
                    }
                    else
                    {
                        return stupidVar;
                    }
                }
            }
            return -1;
        }

        public void Update()
        {
            Timer();
        }
    }
    
    
}
