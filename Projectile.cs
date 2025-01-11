using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SHOOTYMcSHOOTY
{
    public class Projectile : ColisionBox
    {
        public Vector2 velocity;
        public Vector2 gravity;
        public float[] MaxSpeed;
        public int damage;
        public int splashDamage;
        
        public Projectile(Point Position, Point Size, /*Other colisionbox stuf,*/Vector2 velo, float[] MaxSpeed, int damage, int splashDamage, Texture2D[][] texture)
        {
            this.Position = Position;
            this.Size = Size;
            this.Animation = texture;
            this.velocity = velo;
            this.gravity = new Vector2(0f, 0f);
            this.MaxSpeed = MaxSpeed;
            this.damage = damage;
            this.splashDamage = splashDamage;
        }

        public virtual void OnDeath(ColisionBox[] enemies)
        {
        }


        public virtual void UpdatePos()
        {
            Position.X += (int)velocity.X;
            velocity.Y -= gravity.Y;
            Position.Y += (int)velocity.Y;
        }

        public virtual void UpdateMaxVal()
        {
            if (MaxSpeed[1] < velocity.Y)
            {
                velocity.Y = MaxSpeed[1];
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
        }
    }

    public class Rocket : Projectile
    {
        public Rocket(Point Position, Point Size, Vector2 velo, float[] MaxSpeed, int damage, int splashDamage, Texture2D[][] texture) : base(Position, Size, velo, MaxSpeed, damage, splashDamage, texture)
        {
        }

        public override void OnDeath(ColisionBox[] enemies)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                if (Intercect(enemies[i]) == true)
                {
                    enemies[i].DoDmg(damage);
                    for(int j = 0; j < enemies.Length; j++)
                    {
                        if (distanceFunc(enemies[i].Position) <= 50)
                        {
                            enemies[j].DoDmg(splashDamage);
                        }
                    }
                    return;
                }
            }
        }
    }
}
