using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HW3
{
    class Brick {
        Texture2D brick;
        Rectangle loc;
        Color color;
        Life life;
        int points;

        public Rectangle Loc {
            get { return loc; }
        }

        public Life alive {
            get { return life;}
        }
        public Brick(Texture2D brickTexture, Rectangle brickLoc, Color brickColor, int amount) {
            brick = brickTexture;
            loc = brickLoc;
            color = brickColor;
            life = Life.Alive;
            points = amount;
        }

        public int CheckCollision(Ball ball) {
            if ( life == Life.Alive && ball.Bounded.Intersects(loc)) {
                life = Life.Dead;
                ball.Deflect(this);
                return points;
            }
            return 0;
        }

        public void Draw(SpriteBatch spriteBatch) {
            if (life == Life.Alive)
                spriteBatch.Draw(brick, loc, color);
        }
        
    }
}
