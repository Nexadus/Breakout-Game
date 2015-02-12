using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HW3
{
    class Ball {
        const float BALL_START_SPEED = 8f;
        float speed;
        bool collision;
        Vector2 motion, position;
        Texture2D ball;
        Rectangle currBound;
        Rectangle screenBounds;

        public Ball(Texture2D ballTexture, Rectangle boundary) {
            ball = ballTexture;
            screenBounds = boundary;
            currBound = new Rectangle(0, 0, ball.Width, ball.Height);
            
        }

        public void Update() {
            collision = false;
            position += motion * speed;
            speed += 0.001f;
            CheckWallBounds();
        }

        public Rectangle Bounded {
            get {
                currBound.X = (int)position.X;
                currBound.Y = (int)position.Y;
                return currBound;
            }
        }

        public void Start(Rectangle paddleLoc) {
            Random rand = new Random();

            motion = new Vector2(rand.Next(2, 6), -rand.Next(2, 6));
            motion.Normalize();

            speed = BALL_START_SPEED;
            position.Y = paddleLoc.Y - ball.Height;
            position.X = paddleLoc.X + ((paddleLoc.Width - ball.Width) / 2);
        }
        public bool LoseTurn() {
            if (position.Y > screenBounds.Height)
                return true;
            return false;
        }

        public void CheckPaddleCollision(Rectangle paddleLoc) {
            Rectangle ballLoc = new Rectangle( (int)position.X, (int)position.Y, ball.Width, ball.Height);

            if (paddleLoc.Intersects(ballLoc)) {
                position.Y = paddleLoc.Y - ball.Height;
                motion.Y *= -1;
            }
        }

        public void Deflect(Brick brick) {
            if (!collision) {
                motion.Y *= -1;
                collision = true;
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(ball, position, Color.White);
        }

        private void CheckWallBounds() {
            if (position.X < 0) {
                position.X = 0;
                motion.X *= -1;
            }
            if ((position.X + ball.Width) > screenBounds.Width) {
                position.X = screenBounds.Width - ball.Width;
                motion.X *= -1;
            }
            if (position.Y < 0) {
                position.Y = 0;
                motion.Y *= -1;
            }
        }
    }
}
