using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using System;

namespace SHOOTYMcSHOOTY
{
    //ShootyMcShooty plannin stuff ig
    //Make making weapons efficient
    //Create animations
    //Make a head and body hitbox because y not
    //make both hitscan and projectile weapons
    //Platforms yay

    public class ColisionBox
    {

        //return new Rectangle(Position.X, Position.Y, (int)(Animation[whichAni][CurrentFrame].Width* Scale), (int) (Animation[whichAni][CurrentFrame].Height* Scale));
        public Point Position;
        public Point Size;
        //public double[] boundingBoxBody;
        //public double[] boundingBoxHead;

        public int CurrentFrame;
        public int[] FrameIntervol;
        public int Count;
        public Texture2D[][] Animation;
        public Vector2[][] Origins;
        public int whichAni;
        public float Scale;

        public bool IsCollisionOn;
        public Rectangle boundingBoxBody
        {
            get
            {
                if (CurrentFrame >= Animation[whichAni].Length)
                {
                    CurrentFrame = 0;
                }
                return new Rectangle(Position.X, Position.Y, Size.X, Size.Y);
            }
        }
        public ColisionBox()
        {

        }

        public virtual void DrawThing(SpriteBatch _spriteBatche)
        {
            //If frames.Length = 1, just draw, else do this
            if (Animation[whichAni].Length == 1)
            {
                _spriteBatche.Draw(Animation[whichAni][CurrentFrame], Position.ToVector2(), null, Color.White, 0f, Origins[whichAni][CurrentFrame], Scale, SpriteEffects.None, 0);
            }
            else
            {
                if (CurrentFrame >= Animation[whichAni].Length)
                {
                    CurrentFrame = 0;
                }
                _spriteBatche.Draw(Animation[whichAni][CurrentFrame], Position.ToVector2(), null, Color.White, 0f, Origins[whichAni][CurrentFrame], Scale, SpriteEffects.None, 0);
                Count++;
                if (FrameIntervol[whichAni] <= Count)
                {
                    CurrentFrame++;
                    Count = 0;
                }
            }
        }
        public void DrawThingDeug(SpriteBatch _spriteBatche)
        {
            if (Animation[whichAni].Length == 1)
            {
                _spriteBatche.DrawRectangle(boundingBoxBody, Color.Red, 2f, 0f);
            }
            else
            {
                if (CurrentFrame >= Animation[whichAni].Length)
                {
                    CurrentFrame = 0;
                }
                _spriteBatche.DrawRectangle(boundingBoxBody, Color.Red, 2f, 0f);
                Count++;
                if (FrameIntervol[whichAni] <= Count)
                {
                    CurrentFrame++;
                    Count = 0;
                }
            }
        }
        public bool Intercect(ColisionBox enemy)
        {
            if (enemy.boundingBoxBody.Intersects(boundingBoxBody) == true)
            {
                return true;
            }
            return false;
        }

        public bool IsIntercectSide(ColisionBox enemy, string SideToCheck /*Top, Bottom, Left, Right*/)
        {
            if(SideToCheck == "Top")
            {
                if(enemy.Position.Y <= boundingBoxBody.Bottom && enemy.Position.Y + enemy.boundingBoxBody.Height/6 >= Position.Y)
                {
                    if(boundingBoxBody.Right > enemy.Position.X && Position.X < enemy.boundingBoxBody.Right)
                    {
                        Console.WriteLine("Top");
                        return true;
                    }
                }
                return false;
            }
            if (SideToCheck == "Bottom")
            {
                if (Position.Y <= enemy.boundingBoxBody.Bottom && Position.Y + boundingBoxBody.Height / 6 >= enemy.Position.Y)
                {
                    if (boundingBoxBody.Right > enemy.Position.X && Position.X < enemy.boundingBoxBody.Right)
                    {
                        Console.WriteLine("Bottom");
                        return true;
                    }
                }
                return false;
            }
            if (SideToCheck == "Left")
            {
                if(enemy.Position.X <= boundingBoxBody.Right && Position.X < enemy.Position.X + enemy.boundingBoxBody.Width/6)
                {
                    if(boundingBoxBody.Bottom > enemy.Position.Y && Position.Y < enemy.boundingBoxBody.Bottom)
                    {
                        Console.WriteLine("Left");
                        return true;
                    }
                }
                return false;
            }
            if (SideToCheck == "Right")
            {
                if (Position.X <= enemy.boundingBoxBody.Right && Position.X + boundingBoxBody.Width/6 >= enemy.Position.X)
                {
                    if (boundingBoxBody.Bottom > enemy.Position.Y && Position.Y < enemy.boundingBoxBody.Bottom)
                    {
                        Console.WriteLine("Right");
                        return true;
                    }
                }
                return false;
            }
            return false;
        }

        public virtual void Displacement(ColisionBox enemy)
        {

        }
        public double distanceFunc(Point a)
        {

            double xterm = Math.Pow((double)(Position.X - a.X), 2);
            double yterm = Math.Pow((double)(Position.Y - a.Y), 2);
            return Math.Sqrt(xterm + yterm); ;
        }

        public virtual void DoDmg(int a)
        {

        }
    }
        //make a colision check here
        public class Platform : ColisionBox
        {
            public Platform(Point position, int[] frameIntervol, Texture2D[][] animation, Vector2[][] Origins, bool isCollisionOn, float Scale, Point Size)
            {
                this.Position = position;
                this.FrameIntervol = frameIntervol;
                this.Animation = animation;
                this.Origins = Origins;
                this.IsCollisionOn = isCollisionOn;
                this.Scale = Scale;
                this.Size = Size;
            }

        public override void DoDmg(int a)
        {
            Console.WriteLine("AAAAAAAAAAAAAAAAA");
        }
    }
}
