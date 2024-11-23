using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Particles;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public float CurrentRotation;

        public Weapon()
        {

        }

        public void OrientWeapon(MouseState mousy)
        {
            float A = mousy.X - Position.X;
            float B = mousy.Y - Position.Y;
            CurrentRotation = MathF.Atan2(B, A);
        }
        public void DrawWeapon(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(Texture, new Vector2(Position.X, Position.Y), null, Color.White, CurrentRotation - MathF.PI / 2, Texture.Bounds.Center.ToVector2(), Scale, SpriteEffects.None, 0f);
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
        public override bool CheckIfHit(ColisionBox[] enemy)
        {
            for (int i = 0; i < enemy.Length; i++)
            {
                if ((enemy[i].boundingBoxBody.Bottom - Position.Y)/Math.Tan((double)CurrentRotation - (double)Math.PI/2) + Position.X >= enemy[i].Position.X && (enemy[i].boundingBoxBody.Bottom - Position.Y) / Math.Tan((double)CurrentRotation - (double)Math.PI / 2) + Position.X <= enemy[i].boundingBoxBody.Right)
                {
                    return true;
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
