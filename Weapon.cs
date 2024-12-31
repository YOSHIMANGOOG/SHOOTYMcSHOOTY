using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Particles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
        public int ReloadTime;

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
        }
        public void DrawWeapon(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(Texture, new Vector2(Position.X, Position.Y), null, Color.White, CurrentRotation, Texture.Bounds.Center.ToVector2(), Scale, SpriteEffects.None, 0f);
        }
        public bool ReloadTimer()
        {
            //Timer is in frames
            if (ReloadTimeC >= ReloadTime)
            {
                return true;
            }
            ReloadTimeC++;
            return false;
        }
        public virtual void Update()
        {
            ReloadTimer();
        }
        public abstract void FireWeapon(MouseState mousy, ColisionBox[] LeBoxes);
        public abstract bool CheckIfHit(ColisionBox enemy);
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
        public override bool CheckIfHit(ColisionBox enemy)
        {
                Console.WriteLine($"{Rotation * 180/Math.PI}");
                if (Rotation > 0 && Rotation < Math.PI)
                {
                    var thingy = (enemy.boundingBoxBody.Bottom - Position.Y) / Math.Tan((double)CurrentRotation) + Position.X;
                    if (thingy >= enemy.Position.X && thingy <= enemy.boundingBoxBody.Right && enemy.boundingBoxBody.Bottom < Position.Y)
                    {
                        return true;
                    }
                }
                if((Rotation > Math.PI /2 && Rotation < Math.PI) || (Rotation > -Math.PI && Rotation < -Math.PI/2))
                {
                    var thingy2 = Math.Tan((double)CurrentRotation) * (enemy.boundingBoxBody.Right - Position.X) + Position.Y;
                    if (thingy2 >= enemy.Position.Y && thingy2 <= enemy.boundingBoxBody.Bottom && enemy.boundingBoxBody.Right < Position.X)
                    {
                        return true;
                    }
                }
                if (Rotation > -Math.PI && Rotation < 0)
                {
                    var thingy = (enemy.Position.Y - Position.Y) / Math.Tan((double)CurrentRotation) + Position.X;
                    if (thingy >= enemy.Position.X && thingy <= enemy.boundingBoxBody.Right && enemy.Position.Y > Position.Y)
                    {
                        return true;
                    }
                }
                if ((Rotation > -Math.PI /2 && Rotation <= 0) || (Rotation >= 0 && Rotation < Math.PI/2))
                {
                    var thingy2 = Math.Tan((double)CurrentRotation) * (enemy.Position.X - Position.X) + Position.Y;
                    if (thingy2 >= enemy.Position.Y && thingy2 <= enemy.boundingBoxBody.Bottom && enemy.Position.X > Position.X)
                    {
                        return true;
                    }
                }
            return false;
            //Draw a line and then check if it hit anything
        }
        public override void FireWeapon(MouseState mousy, ColisionBox[] LeBoxes)
        {
            // Do split check to see which collision box is hit first
            // ex: LeBoxes.Length = 5
            // LeBoxes[all].X = {12, 6, 5, 306, 1)
            // CheckIfHit(LeBoxes[all]) == (true, false, true, true, false)
            // LeBoxes[0] > LeBoxes[1] (remove leBoxes[0] from list)
            // LeBoxes[1] < LeBoxes[2] (remove LeBoxes[2] from list)
            // LeBoxes[1].HitWithABigStick()
            if(ReloadTimer() == true)
            {
                List<ColisionBox> a = new List<ColisionBox>();
                for(int i = 0;i < LeBoxes.Length; i++)
                {
                    if (CheckIfHit(LeBoxes[i]) == true)
                    {
                        a.Add(LeBoxes[i]);
                    }
                }
            }
        }
    }
}
