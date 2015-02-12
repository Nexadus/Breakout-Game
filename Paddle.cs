using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HW3
{
    class Paddle {

        float speed;
        Vector2 position, motion;
        Texture2D paddle;
        Rectangle screenBounds;
        KeyboardState keyboardState;

   

        public Paddle(Texture2D paddleTexture, Rectangle boundary) {
            paddle = paddleTexture;
            screenBounds = boundary;
            speed = 8f;
            Start();
        }

        public void Start() {
            position.X = ((screenBounds.Width - paddle.Width) / 2);
            position.Y = ((screenBounds.Height - paddle.Height) - Game1.INDENT);
        }

        public void Update() {
            motion = Vector2.Zero;

            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Left))
                motion.X = -1;
            if (keyboardState.IsKeyDown(Keys.Right))
                motion.X = 1;

            motion.X *= speed;
            position += motion;
            LockPaddle();
        }

        
        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(paddle, position, Color.White);
        }

        public Rectangle PaddlePos() {
            return new Rectangle((int)position.X, (int)position.Y, paddle.Width, paddle.Height);
        }

        private void LockPaddle()
        {
            if (position.X < 0)
                position.X = 0;
            if ((position.X + paddle.Width) > screenBounds.Width)
                position.X = (screenBounds.Width - paddle.Width);
        }

    }
}
