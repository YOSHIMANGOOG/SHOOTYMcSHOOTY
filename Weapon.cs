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
        public abstract void FireWeapon(MouseState mousy);
        public abstract bool CheckIfHit(ColisionBox[] enemy);
    }
    
    public class Pistol : Weapon
    {
        public Pistol(Texture2D Texture, Point Position, float Scale, int ReloadTime)
        {
            this.Texture = Texture;
            this.Position = Position;
            this.Scale = Scale;
            this.ReloadTime = ReloadTime;
        }
        // x = 2
        // y = 2 -3 < x < 3 
        // y = 2x + b
        // 2 = 2x + b
        // (2-b)/2 = x
        // raytracing is HARD xd
        //Fix the stupid Monogame unit circle
        //Its 0, Pi/2, Pi, 3Pi/2, then back to 0 clockwise (holy shi-)
        public override bool CheckIfHit(ColisionBox[] enemy)
        {
            for (int i = 0; i < enemy.Length; i++)
            {
                Console.WriteLine($"{Rotation * 180/Math.PI}");
                if (Rotation > 0 && Rotation < Math.PI)
                {
                    var thingy = (enemy[i].boundingBoxBody.Bottom - Position.Y) / Math.Tan((double)CurrentRotation) + Position.X;
                    if (thingy >= enemy[i].Position.X && thingy <= enemy[i].boundingBoxBody.Right && enemy[i].boundingBoxBody.Bottom < Position.Y)
                    {
                        return true;
                    }
                }
                if((Rotation > Math.PI /2 && Rotation < Math.PI) || (Rotation > -Math.PI && Rotation < -Math.PI/2))
                {
                    var thingy2 = Math.Tan((double)CurrentRotation) * (enemy[i].boundingBoxBody.Right - Position.X) + Position.Y;
                    if (thingy2 >= enemy[i].Position.Y && thingy2 <= enemy[i].boundingBoxBody.Bottom && enemy[i].boundingBoxBody.Right < Position.X)
                    {
                        return true;
                    }
                }
                if (Rotation > -Math.PI && Rotation < 0)
                {
                    var thingy = (enemy[i].boundingBoxBody.Bottom - Position.Y) / Math.Tan((double)CurrentRotation) + Position.X;
                    if (thingy >= enemy[i].Position.X && thingy <= enemy[i].boundingBoxBody.Right && enemy[i].boundingBoxBody.Bottom > Position.Y)
                    {
                        return true;
                    }
                }
                if ((Rotation > -Math.PI /2 && Rotation <= 0) || (Rotation >= 0 && Rotation < Math.PI/2))
                {
                    var thingy2 = Math.Tan((double)CurrentRotation) * (enemy[i].boundingBoxBody.Right - Position.X) + Position.Y;
                    if (thingy2 >= enemy[i].Position.Y && thingy2 <= enemy[i].boundingBoxBody.Bottom && enemy[i].boundingBoxBody.Right > Position.X)
                    {
                        return stupidVar;
                    }
                }
            }
            return false;
            //Draw a line and then check if it hit anything
        }
        public override void FireWeapon(MouseState mousy)
        {
            //
        }
    }
}
