using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace SHOOTYMcSHOOTY
{
    public abstract class Projectile : ColisionBox
    {
        public Vector2 velocity;
        public Vector2 gravity;
        public float[] MaxSpeed;
        public int damage;

        public Projectile()
        {

        }

        public abstract void OnDeath();

        public abstract void UpdatePos();
    }

    public class Rocket : Projectile
    {
        public Rocket(Point Position, Point Size, /*Other colisionbox stuf,*/Vector2 velo, Vector2 grav, float[] MaxSpeed, int damage)
        {
            this.Position = Position;
            this.Size = Size;
            this.velocity = velo;
            this.gravity = grav;
            this.MaxSpeed = MaxSpeed;
            this.damage = damage;
        }
        public override void OnDeath()
        {
            //Add explosion effect here
        }
        public override void UpdatePos()
        {
            //Simple strait line mvoement
        }
    }
}
