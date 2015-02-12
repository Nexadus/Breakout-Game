/*
 * CS3113 - Game Programming
 */

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace HW3
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    /// 
    enum Life {
        Alive, Dead
    }
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public const int INDENT = 10;
        const int BRICK_WIDTH = 10;
        const int BRICK_HEIGHT = 6;
        const int SCREEN_Y = 600;
        const int SCREEN_X = 750;
        const int SCORE_SIZE_Y = 75;
        bool[] rows;

        Paddle paddle;
        Ball ball;
        Rectangle screen;
        Score score;

        Texture2D brickImg;
        Brick[,] bricks;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = SCREEN_Y;
            graphics.PreferredBackBufferWidth = SCREEN_X;

            screen = new Rectangle(0, 0, SCREEN_X, SCREEN_Y);


        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic heredy

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            score = new Score(screen);
            score.Font = Content.Load<SpriteFont>("Arial");
            score.lives += 3;
            rows = new bool[6] { false, false, false, false, false, false};
            paddle = new Paddle(Content.Load<Texture2D>(@"Sprites\paddle"), screen);
            ball = new Ball(Content.Load<Texture2D>(@"Sprites\ball"), screen);
            brickImg = Content.Load<Texture2D>(@"Sprites\brick");
            // TODO: use this.Content to load your game content here
            Start();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            paddle.Update();
            ball.Update();

            foreach (Brick brick in bricks) {
                score.score += brick.CheckCollision(ball);
            }
            CheckRows();
            ball.CheckPaddleCollision(paddle.PaddlePos());
            //if (ball.LoseTurn()) {
            //    Start();
            //}
            if (ball.LoseTurn() ) {
                score.lives -= 1;
                if (score.lives < 0) {
                    Start();
                    score.lives += 4;
                }
                else {
                    paddle.Start();
                    ball.Start(paddle.PaddlePos());
                }
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Silver);

            spriteBatch.Begin();
            score.Draw(spriteBatch);
            foreach (Brick brick in bricks)
                brick.Draw(spriteBatch);
            paddle.Draw(spriteBatch);
            ball.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }

        protected void Start() {
            paddle.Start();
            ball.Start(paddle.PaddlePos());
            int worth = 0;
            bricks = new Brick[BRICK_WIDTH, BRICK_HEIGHT];

            for(int y = 0; y < BRICK_HEIGHT; y++) {
                Color color = Color.White;

                switch (y) {
                    case 0:
                        color = Color.Red;
                        worth = 10;
                        break;
                    case 1:
                        worth = 7;
                        color = Color.Orange;
                        break;
                    case 2:
                        worth = 6;
                        color = Color.Brown;
                        break;
                    case 3:
                        worth = 5;
                        color = Color.Yellow;
                        break;
                    case 4:
                        worth = 3;
                        color = Color.Green;
                        break;
                    case 5:
                        worth = 1;
                        color = Color.Blue;
                        break;

                }

                for(int x = 0; x < BRICK_WIDTH; x++) {
                    bricks[x, y] = new Brick(brickImg, new Rectangle((x * brickImg.Width+2),
                                    (y * brickImg.Height+SCORE_SIZE_Y), brickImg.Width-5, brickImg.Height-5), color, worth);
                }
            }
        }
        protected void CheckRows() {
            int count = 0;
            for(int y = 0; y < BRICK_HEIGHT; y++) {
                if(!rows[y]) {
                    for(int x = 0; x < BRICK_WIDTH; x++) {
                        if (bricks[x, y].alive == Life.Alive) {
                            break;
                        }
                        else {
                            ++count;
                        }                        
                    }
                    if (count == 10)
                    {
                        rows[y] = true;
                        score.score += 416;
                    }
                    else
                        count = 0;
                }
            }
        }
    }
}
