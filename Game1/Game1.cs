using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Shooter;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private Player player;

        private GamePadState currentGamePadState;
        private GamePadState previousGamePadState;
        private KeyboardState currentKeyboardState;
        private KeyboardState previousKeyboardState;
        private MouseState currentMouseState;
        private MouseState previosMouseState;

        private float playerMoveSpeed;

        // Image used to display the static background
        private Texture2D mainBackground;
        private Rectangle rectBackground;
        private float scale = 1f;
        private ParallaxingBackground bgLayer1, bgLayer2;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            player = new Player();
            // Background
            bgLayer1 = new ParallaxingBackground();
            bgLayer2 = new ParallaxingBackground();

            playerMoveSpeed = 8.0f;
            TouchPanel.EnabledGestures = GestureType.FreeDrag;
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

            // Load the player resources
            Animation playerAnimation = new Animation();
            Texture2D playerTexture = Content.Load<Texture2D>("Graphics/SonicFrames");
            playerAnimation.Initialize(playerTexture, Vector2.Zero, 32, 40, 8, 75, Color.White, 1f, true);

            Vector2 playerPosition = new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, 
                                                 GraphicsDevice.Viewport.TitleSafeArea.Y
                                                 + GraphicsDevice.Viewport.TitleSafeArea.Height / 2);
            player.Initialize(playerAnimation, playerPosition);
            // Load the parallaxing background
            bgLayer1.Initialize(Content, "Graphics/bgLayer1", GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, -1);
            bgLayer2.Initialize(Content, "Graphics/bgLayer2", GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, -2);

            mainBackground = Content.Load<Texture2D>("Graphics/mainbackground");

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Save the previous state of the keyboard and game pad so we can detrmine single key/button presses
            previousGamePadState = currentGamePadState;
            previousKeyboardState = currentKeyboardState;
            previosMouseState = currentMouseState;
            // Read the current state of the keboard and gamepad and store it
            currentKeyboardState = Keyboard.GetState();
            currentGamePadState = GamePad.GetState(PlayerIndex.One);
            currentMouseState = Mouse.GetState();

            // Update the player
            UpdatePlayer(gameTime);
            // Update the parallaxing background
            bgLayer1.Update(gameTime);
            bgLayer2.Update(gameTime);

            base.Update(gameTime);
        }

        private void UpdatePlayer(GameTime gameTime)
        {
            player.Update(gameTime);

            // Get thumbstick controls
            player.Position.X += currentGamePadState.ThumbSticks.Left.X * playerMoveSpeed;
            player.Position.Y -= currentGamePadState.ThumbSticks.Left.Y * playerMoveSpeed;

            // Use the Keyboard / Dpad
            if (currentKeyboardState.IsKeyDown(Keys.Left))
            {
                player.Position.X -= playerMoveSpeed;
            }

            if (currentKeyboardState.IsKeyDown(Keys.Right))
            {
                player.Position.X += playerMoveSpeed;
            }

            if (currentKeyboardState.IsKeyDown(Keys.Up))
            {
                player.Position.Y -= playerMoveSpeed;
            }

            if (currentKeyboardState.IsKeyDown(Keys.Down))
            {
                player.Position.Y += playerMoveSpeed;
            }

            // Make sure that the player does not go out of bounds
            player.Position.X = MathHelper.Clamp(player.Position.X, 0, GraphicsDevice.Viewport.Width - player.Width);
            player.Position.Y = MathHelper.Clamp(player.Position.Y, 0, GraphicsDevice.Viewport.Height - player.Height);

            // Windows Touch Gesture for MonoGame
            while (TouchPanel.IsGestureAvailable)
            {
                GestureSample gesture = TouchPanel.ReadGesture();

                if(gesture.GestureType == GestureType.FreeDrag)
                {
                    player.Position += gesture.Delta;
                }
            }

            // Get Mouse State then Capture the button type and Respond Button Press
            Vector2 mousePosition = new Vector2(currentMouseState.X, currentMouseState.Y);

            if(currentMouseState.LeftButton == ButtonState.Pressed)
            {
                Vector2 posDelta = mousePosition - player.Position;
                posDelta.Normalize();
                posDelta *= playerMoveSpeed;
                player.Position += posDelta;
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            // Draw the Main Background Texture
            spriteBatch.Draw(mainBackground, rectBackground, Color.White);
            // Draw the moving background
            bgLayer1.Draw(spriteBatch);
            bgLayer2.Draw(spriteBatch);

            player.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
