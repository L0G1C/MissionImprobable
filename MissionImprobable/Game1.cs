using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MissionImprobable.Entities;
using MissionImprobable.Managers;
using MissionImprobable.States;

namespace MissionImprobable
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _player;
        private Texture2D _platform;
        private Texture2D _jump;
        private int alertMessage;

        private Sprite _background;
        private Parallax _background2;
        private Player _Player;
        private Platform _Platform;
        private Laser _laser;
        

        private bool _gameStart;
        private bool WarningMessage;
        private SpriteFont _SpriteFont;
        private SpriteFont _GameOverFont;
        private Timer _Timer;

        private KeyboardState currentKeyboardState;
        private KeyboardState previousKeyboardState;


        //Managers
        private LaserManager _LaserManager;
        private SoundManager _SoundManager;
        private CollisionManager _CollisionManager;
        private GameState gameState; 

        //Screens
        private Sprite titleScreen;


        public double Score { get; set; }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            //_graphics.IsFullScreen = true;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            gameState = new TitleScreenState(this);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            

            // TODO: use this.Content to load your game content here
            //player boundary
            var playerBounds = new Rectangle(0, 200, 100, 300);

            _player = Content.Load<Texture2D>(@"gfx/player");
            _platform = Content.Load<Texture2D>(@"gfx/platform");
            _jump = Content.Load<Texture2D>(@"gfx/jump");            

            
            _SpriteFont = Content.Load<SpriteFont>("SpriteFont1");
            _GameOverFont = Content.Load<SpriteFont>("SpriteFont2");
            _Timer = new Timer();            


            //screens
            titleScreen = new Sprite(Content.Load<Texture2D>(@"gfx/titleScreen"), Vector2.Zero, playerBounds);

            _background = new Sprite(Content.Load<Texture2D>(@"gfx/background"), Vector2.Zero, playerBounds);
            _background2 = new Parallax(Content.Load<Texture2D>(@"gfx/background2"), Vector2.Zero);
            _Platform = new Platform(_platform, new Vector2(0, 160 + _player.Height));
            _Player = new Player(_player, new Vector2(80, 300), playerBounds, Content);
            _LaserManager = new LaserManager(Content.Load<Texture2D>(@"gfx/laser"), _graphics.GraphicsDevice.Viewport.Bounds);
            _SoundManager = new SoundManager(Content);

            _CollisionManager = new CollisionManager(_Player, _LaserManager, _SoundManager);
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
            currentKeyboardState = Keyboard.GetState();
            gameState.Update(gameTime);
            previousKeyboardState = currentKeyboardState;



            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            gameState.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }


        /// <summary>
        /// Nested Class for TitleScreen State
        /// </summary>
        public class TitleScreenState : GameState
        {
            public TitleScreenState(Game1 game)
                : base(game)
            {
            }

            public override void Update(GameTime gameTime)
            {
                game._gameStart = false;
                game._SoundManager.StopMusic();
                game.Score = 0;
                game.titleScreen.Update(gameTime);


                //var keyboardState = Keyboard.GetState();

                if (game.currentKeyboardState.IsKeyDown(Keys.Enter) && !(game.previousKeyboardState.IsKeyDown(Keys.Enter)))
                {
                    game.gameState = new PlayingState(game);
                }
            }

            public override void Draw(SpriteBatch spriteBatch)
            {
                game.titleScreen.Draw(spriteBatch);
            }
        }

        public class PlayingState : GameState
        {
            public PlayingState(Game1 game)
                :base(game)
            {}

            public override void Update(GameTime gameTime)
            {
                game.Score += 2 * Math.Round(gameTime.TotalGameTime.TotalSeconds);

                // Allows the game to exit
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                    game.Exit();

                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    game.Exit();

                // TODO: Add your update logic here
                if (!game._gameStart)
                {
                    game._SoundManager.PlayBackgroundMusic();
                    game._gameStart = true;
                }

                game._Player.Update(gameTime);
                game._Platform.Update(gameTime);
                game._background2.Update(gameTime);
                game._LaserManager.Update(gameTime);


                game._CollisionManager.Update(gameTime);

                if (game._gameStart)
                {
                    game._Timer.Countdown(gameTime);
                }


                if (game._Timer._Timer == 0.00f)
                {
                    game._Platform.GameSpeed += (game._Platform.GameSpeed / 4);

                    game._Timer.Restart();
                    game._Timer.Difficulty += 1;

                    if (game._Timer.Difficulty > 2)
                    {
                        game._LaserManager._timeDelay -= .5f;
                        game._Timer.Difficulty = 0;
                    }

                    game._Player.SpeedUp();
                    game._SoundManager.Alarm();
                    game.WarningMessage = true;

                    var Rand = new Random();

                    game.alertMessage = Rand.Next(4);
                }

                if (game._Timer._Timer <= 8.00f)
                    game.WarningMessage = false;

                foreach (var laser in game._LaserManager.Lasers)
                {
                    laser.GameSpeed = game._Platform.GameSpeed;
                }

                if (game._Player.IsDead)
                {
                    game.gameState = new GameOverState(game);
                }
            }

            public override void Draw(SpriteBatch spriteBatch)
            {
                game._background.Draw(game._spriteBatch);
                game._background2.Draw(game._spriteBatch);
                game._Platform.Draw(game._spriteBatch);
                game._Player.Draw(game._spriteBatch);


                game._LaserManager.DrawLasers(game._spriteBatch);


                game._spriteBatch.DrawString(game._SpriteFont, game._Timer._Timer.ToString("F"), new Vector2(700, 20), Color.White);
                game._spriteBatch.DrawString(game._SpriteFont, game.Score.ToString(), new Vector2(20, 20), Color.White);

                if (game.WarningMessage)
                {
                    string[] alert = { "Catch that spy!", "Don't let him get away!", "Don't worry the lasers will get him!", "Stop Him!" };
                    game._spriteBatch.DrawString(game._SpriteFont, alert[game.alertMessage], new Vector2(75, 230), Color.Red);
                }

            }
        }

        public class GameOverState : PlayingState           
        {
            public GameOverState(Game1 game)
                : base(game)
            {
                
            }

            public override void Update(GameTime gameTime)
            {
                if (game.currentKeyboardState.IsKeyDown(Keys.Enter))
                {
                    if (game.currentKeyboardState.IsKeyDown(Keys.Enter) && !(game.previousKeyboardState.IsKeyDown(Keys.Enter)))
                    {
                        game.LoadContent();
                        game.gameState = new TitleScreenState(game);
                    }
                }
            }

            public override void Draw(SpriteBatch spriteBatch)
            {
                base.Draw(spriteBatch);
                game._spriteBatch.DrawString(game._GameOverFont, "Game over, press Enter to Restart!\n         Score: " + game.Score.ToString(), new Vector2(50, 250), Color.Black);

            }
        }
        
        
    }



}
