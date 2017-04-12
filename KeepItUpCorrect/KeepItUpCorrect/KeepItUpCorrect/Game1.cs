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

namespace KeepItUpCorrect
{
    /// This is the main type for your game
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        enum GameStates { TitleScreen, Playing, GameOver, Pause };
        GameStates gameState = GameStates.TitleScreen;
        Texture2D titleScreen;
        Texture2D spriteSheet;
        Texture2D Background;
        Texture2D gameOver;

        bool isAlGay = true;
        bool clicked = false;

        Sprite ball;

        int clicks = 0;  //Score
        

        public Vector2 scoreLocation = new Vector2(20, 10);
        SpriteFont pericles14;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 768;
            Content.RootDirectory = "Content";
        }

        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            this.IsMouseVisible = true;
            base.Initialize();
        }
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            titleScreen = Content.Load<Texture2D>(@"Textures\TitleScreen");
            spriteSheet = Content.Load<Texture2D>(@"Textures\SpriteSheet");
            Background = Content.Load<Texture2D>(@"Textures\Background");
            gameOver = Content.Load<Texture2D>(@"Textures\GameOver");

            ball = new Sprite(
                new Vector2(this.Window.ClientBounds.Width/2, this.Window.ClientBounds.Height/2),
                spriteSheet,
                new Rectangle(0,0,200,200),
                Vector2.Zero);



            pericles14 = Content.Load<SpriteFont>(@"Fonts\Pericles14");
            // TODO: use this.Content to load your game content here
        }

        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            MouseState ms = Mouse.GetState();
            Vector2 mousePoint = new Vector2(ms.X, ms.Y);



            //NOTE: KeyboardState MUST be in the update method in order to work.  Not sure why.  Ask Tanczos later.
            KeyboardState kb = Keyboard.GetState();
            // TODO: Add your update logic here
            switch (gameState)
            {
                case GameStates.TitleScreen:
                    if (kb.IsKeyDown(Keys.Space))
                    {
                        gameState = GameStates.Playing;
                       
                    }
                    break;

                case GameStates.Playing:
                    if(kb.IsKeyDown(Keys.P))
                    {
                        gameState = GameStates.Pause;
                    }

                    if (ms.LeftButton == ButtonState.Pressed && !clicked)
                    {
                        clicked = true;
                        Vector2 offset = (new Vector2(ms.X, ms.Y) - ball.Center);

                        offset.Y = Math.Abs(offset.Y);
                        offset.Normalize();
                        offset = new Vector2(-1500) + offset*-100;

                        ball.Velocity = offset;

                       

                        Vector2 vc = ball.Velocity;
                        float speed = vc.Length();
                        vc.Normalize();
                        vc *= Math.Min(speed, 750f);
                        ball.Velocity = vc;

                        clicks ++;
                    }
                    else if (ms.LeftButton == ButtonState.Released)
                        clicked = false;


                    if (ball.Location.Y >= 650  || ball.Location.X <=0)
                    {
                        clicks = 0;
                        gameState = GameStates.GameOver;
                    }

                    /*
                    if ((ms.LeftButton == ButtonState.Pressed) &&
             (currentSquare.Contains(ms.X, ms.Y)))
                    {
                        playerScore++;
                        timeRemaining = 0.0f;
                        TimePerSquare = TimePerSquare - 0.05f;
                    }*/
                    ball.Velocity += new Vector2(0, 15f);

                    ball.Update(gameTime);
                        break;

                case GameStates.Pause:

                    if (kb.IsKeyDown(Keys.P))
                        gameState = GameStates.Playing;
                    break;

                case GameStates.GameOver:
                    if(ms.LeftButton == ButtonState.Pressed && !clicked)
                    {
                        gameState = GameStates.Playing;

                        ball = new Sprite(
                            new Vector2(this.Window.ClientBounds.Width / 2, this.Window.ClientBounds.Height / 2),
                            spriteSheet,
                            new Rectangle(0, 0, 200, 200),
                            Vector2.Zero);
                    }
                    break;
            }

            base.Update(gameTime);
        }

        /// This is called when the game should draw itself.
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            if (gameState == GameStates.TitleScreen)
            {
                spriteBatch.Draw(titleScreen,
                    new Rectangle(0, 0, this.Window.ClientBounds.Width,
                        this.Window.ClientBounds.Height),
                        Color.White);
            }

            if (gameState == GameStates.Playing)
            {
                spriteBatch.Draw(Background,
                    new Rectangle(0, 0, this.Window.ClientBounds.Width,
                        this.Window.ClientBounds.Height),
                        Color.White);

                ball.Draw(spriteBatch);
                
                    spriteBatch.DrawString(
                        pericles14,
                        "Score: " + clicks.ToString(),
                        scoreLocation,
                        Color.Black);
                
            }

            if(gameState == GameStates.Pause)
            {
                //code here loser
            }

            if(gameState == GameStates.GameOver)
            {
                spriteBatch.Draw(gameOver,
                    new Rectangle(0, 0, this.Window.ClientBounds.Width,
                        this.Window.ClientBounds.Height),
                        Color.White);
            }


            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
