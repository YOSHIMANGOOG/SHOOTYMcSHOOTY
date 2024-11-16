using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHOOTYMcSHOOTY
{
    public class ShootyHand
    {
        public Texture2D Texture;
        public Point Position;
        //public Point Size;
        public float Scale;
        public Weapon currentWeapon;

        public float CurrentRotation;
        public ShootyHand (Texture2D Texture, Point Position, float scale)
        {
            this.Texture = Texture;
            this.Position = Position;
            this.Scale = scale;
        }

        public void OrientHand(MouseState mousy)
        {
            float A = mousy.X - Position.X;
            float B = mousy.Y - Position.Y;
            CurrentRotation = MathF.Atan2(B, A);
        }

        public void DrawHand(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(Texture, new Vector2(Position.X, Position.Y), null, Color.White, CurrentRotation - MathF.PI/2, Texture.Bounds.Center.ToVector2(), Scale, SpriteEffects.None, 0f);
        }

        public void DrawHandDebug(SpriteBatch _spriteBatch)
        {
         //   _spriteBatch.DrawRectangle(BoundingBoxBody, Color.Blue, 2f, 0f);
        }
    }
}
