using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Security.Cryptography.X509Certificates;

namespace SHOOTYMcSHOOTY
{
    public class Player : ColisionBox
    {
        public Vector2 velocity;
        public Vector2 gravity = new Vector2(0,-0.5f);
        public bool IsJumping;
        public float[] MaxSpeed = new float[2] { 5f, 4f };

        public Texture2D Hand;
        public float ScaleH;
        public ShootyHand shooty
        {
            get
            {
                return new ShootyHand(Hand, new Point(Position.X + Size.X / 2, Position.Y + Size.Y / 2), ScaleH);
            }
        }
        public Player(Point position, Point Size, Texture2D[][] animation, bool IsCollisionOn, int[] frameIntervol, Vector2[][] Origins, float Scale, Texture2D Hand, float ScaleH)
        {
            this.Position = position;
            this.Size = Size;
            this.Animation = animation;
            this.IsCollisionOn = IsCollisionOn;
            this.FrameIntervol = frameIntervol;
            this.Origins = Origins;
            this.Scale = Scale;
            this.Hand = Hand;
            this.ScaleH = ScaleH;
        }
        public void MovePlayerGravity(ColisionBox enemy)
        {
            velocity.Y -= gravity.Y;
            Position.Y += (int)velocity.Y;
            if (IsIntercectSide(enemy, "Top") == true)
            {
                velocity.Y = 0;
            }
            if(IsIntercectSide(enemy, "Bottom") == true)
            {
                velocity.Y = Math.Abs(velocity.Y / 2);
            }
            if (MaxSpeed[1] < velocity.Y)
            {
                velocity.Y = MaxSpeed[1];
            }
        }
        public void ChangesToVelocityY()
        {
            //If PlayerHeadTop(or ig PlayerBodyTop) touches bottom of bloc, set velocity.Y = veocity.y * (-1/2)
            //IF PlayerBodyBottom touches top of bloc, velocity.y = 0 and don't activate gravity
            //If PlayerHeadBottom touches top of bloc, idk either kill them or let them hang there
        }

        public override void DrawThing(SpriteBatch _spriteBatche)
        {
            //If frames.Length = 1, just draw, else do this
            SpriteEffects sport = SpriteEffects.None;
            if(velocity.X < 0)
            {
                sport = SpriteEffects.FlipHorizontally;
            }
            if (Animation[whichAni].Length == 1)
            {
                _spriteBatche.Draw(Animation[whichAni][CurrentFrame], Position.ToVector2(), null, Color.White, 0f, Origins[whichAni][CurrentFrame], 0.4657f, sport, 0);
            }
            else
            {
                if (CurrentFrame >= Animation[whichAni].Length)
                {
                    CurrentFrame = 0;
                }
                _spriteBatche.Draw(Animation[whichAni][CurrentFrame], Position.ToVector2(), null, Color.White, 0f, Origins[whichAni][CurrentFrame], 0.4657f, sport, 0);
                Count++;
                if (FrameIntervol[whichAni] <= Count)
                {
                    CurrentFrame++;
                    Count = 0;
                }
            }
        }

        public void UpdateAnimations()
        {
            if(velocity.X <= 1f && velocity.X >= -1f)
            {
                if (whichAni == 1)
                {
                    CurrentFrame = 0;
                }
                whichAni = 0;
            }
            else
            {
                if(whichAni == 0)
                {
                    CurrentFrame = 0;
                }
                whichAni = 1;
            }
        }

        public void MovePlayer(KeyboardState keyState, MouseState mousy, ColisionBox enemy)
        {
            keyState = Keyboard.GetState();
            if(keyState.IsKeyDown(Keys.D))
            {
                if(IsIntercectSide(enemy,"Left") == false)
                {
                    velocity.X += 0.5f;
                }
                else
                {
                    velocity.X = 0f;
                }
            }
            if (keyState.IsKeyDown(Keys.S))
            {
                //Make hitbox half size
                velocity.Y -= 4f;
            }
            if (keyState.IsKeyDown(Keys.A))
            {
                if (IsIntercectSide(enemy, "Right") == false)
                {
                    velocity.X -= 0.5f;
                }
                else
                {
                    velocity.X = 0f;
                }
            }

            if (keyState.IsKeyDown(Keys.W) && IsJumping == false)
            {
                //if player is touchin a floor
                velocity.Y -= 12f;
                IsJumping = true;
            }
            if(keyState.IsKeyDown(Keys.W) == false/*Replace this with when you touch the ground*/)
            {
                IsJumping = false;
            }
            if (keyState.GetPressedKeyCount() == 0 /* && on the ground*/)
            {
                velocity.X = (velocity.X * 10)/11f;
            }
            if (velocity.X >= MaxSpeed[0] || velocity.X < -MaxSpeed[0])
            {
                if (velocity.X > 0)
                {
                    velocity.X = MaxSpeed[0];
                }
                else
                {
                    velocity.X = -MaxSpeed[0];
                }
            }
            Position.X += (int)velocity.X;
        }

        public override void Displacement(ColisionBox enemy)
        {
            Point CentEn = new Point(enemy.Position.X + (enemy.boundingBoxBody.Width / 2), enemy.Position.Y + (enemy.boundingBoxBody.Height / 2));
            Point CentUs = new Point(Position.X + (boundingBoxBody.Width / 2), Position.Y + (boundingBoxBody.Height / 2));
            if (Intercect(enemy) == true)
            {
                int XDis;
                int YDis;
                if(CentEn.X > CentUs.X && CentEn.Y > CentUs.Y)
                {
                    XDis = enemy.Position.X - (Position.X + boundingBoxBody.Width);
                    YDis = enemy.Position.Y - (Position.Y + boundingBoxBody.Height);
                    if(XDis > YDis)
                    {
                        Position.X = enemy.Position.X - boundingBoxBody.Width;
                    }
                    else
                    {
                        Position.Y = enemy.Position.Y - boundingBoxBody.Height;
                    }
                }
                else if (CentEn.X > CentUs.X && CentEn.Y <= CentUs.Y)
                {
                    XDis = enemy.Position.X - (Position.X + boundingBoxBody.Width);
                    YDis = Position.Y - (enemy.Position.Y + enemy.boundingBoxBody.Height);
                    if (XDis > YDis)
                    {
                        Position.X = enemy.Position.X - boundingBoxBody.Width;
                    }
                    else
                    {
                        Position.Y = enemy.Position.Y + enemy.boundingBoxBody.Height;
                    }
                }
                else if(CentEn.X <= CentUs.X && CentEn.Y > CentUs.Y)
                {
                    XDis = Position.X - (enemy.Position.X + enemy.boundingBoxBody.Width);
                    YDis = enemy.Position.Y - (Position.Y + boundingBoxBody.Height);
                    if (XDis > YDis)
                    {
                        Position.X = enemy.Position.X + enemy.boundingBoxBody.Width;
                    }
                    else
                    {
                        Position.Y = enemy.Position.Y - boundingBoxBody.Height;
                    }
                }
                else if(CentEn.X <= CentUs.X && CentEn.Y <= CentUs.Y)
                {
                    XDis = Position.X - (enemy.Position.X + enemy.boundingBoxBody.Width);
                    YDis = Position.Y - (enemy.Position.Y + enemy.boundingBoxBody.Height);
                    if (XDis > YDis)
                    {
                        Position.X = enemy.Position.X + enemy.boundingBoxBody.Width;
                    }
                    else
                    {
                        Position.Y = enemy.Position.Y + enemy.boundingBoxBody.Height;
                    }
                }
            }
        }
    }
}
