using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace SHOOTYMcSHOOTY
{
    public class Pistol : Weapon
    {
        public Pistol(Texture2D Texture, Point Position, float Scale, int ReloadTimeS)
        {
            this.Texture = Texture;
            this.Position = Position;
            this.Scale = Scale;
            this.ReloadTimeS = ReloadTimeS;
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
        public override void FireWeapon(ColisionBox[] enemy, MouseState mousoi)
        {
            if(Timer() == true && mousoi.LeftButton == ButtonState.Pressed)
            {
                ReloadTimeC = 0;
                List<ColisionBox> a = new List<ColisionBox>();
                for (int i = 0; i < enemy.Length; i++)
                {
                    if (CheckIfHit(enemy[i]) == true)
                    {
                        a.Add(enemy[i]);
                    }
                }
                if (a.Count >= 2)
                {
                    for (int i = 0; i < stupidSquaring2Thing(a.Count); i++)
                    {
                        for (int i2 = 0; i2 < a.Count; i2++)
                        {
                            if (enemy[i2].distanceFunc(Position) >= enemy[i2 + 1].distanceFunc(Position))
                            {
                                a.RemoveAt(i2);
                            }
                            else
                            {
                                a.RemoveAt(i2 + 1);
                            }
                        }
                    }
                    a[0].DoDmg(10);
                }
                else if(a.Count > 0)
                {
                    a[0].DoDmg(10);
                }
               
            }
        }

        public override void WeaponAnimation()
        {
            throw new NotImplementedException();
        }
    }
}
