using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HW3
{
    class Score
    {
        Vector2 scorePos, livesPos;

        public Score(Rectangle boundary) {
            scorePos = new Vector2(1, 1);
            livesPos = new Vector2(boundary.Width-(Game1.INDENT*9), 1);
        }
        public SpriteFont Font {
            get;
            set;
        }
        public int score {
            get;
            set;
        }

        public int lives
        {
            get;
            set;
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.DrawString(Font, "Score: " + score.ToString(), scorePos, Color.Black);
            spriteBatch.DrawString(Font, "Lives: " + lives.ToString(), livesPos, Color.Black);
        }
    }
}
