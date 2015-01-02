using DungeonCreatorTestArea.Lair;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DungeonCreatorTestArea
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public static Model CORNER_TOP_RIGHT_BRICK;
        public static Model CORNER_TOP_LEFT_BRICK;
        public static Model CORNER_BOTTOM_RIGHT_BRICK;
        public static Model CORNER_BOTTOM_LEFT_BRICK;
        public static Model SIDE_TOP_BRICK;
        public static Model SIDE_LEFT_BRICK;
        public static Model SIDE_RIGHT_BRICK;
        public static Model SIDE_BOTTOM_BRICK;
        public static Model MIDDLE_BRICK;
        public static Model THINGY;

        // This vector controls how much the camera's position is offset from the
        // thingy. This value can be changed to move the camera further away from or
        // closer to the thingy.
        private readonly Vector3 CameraPositionOffset = new Vector3(200, 75, 0);
        private readonly Vector3 CameraTargetOffset = new Vector3(0, 0, 0);

        private KeyboardState currentKeyboardState = new KeyboardState();
        private MouseState currentMouseState = new MouseState();
        private Thingy thing;
        private Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), 800f / 480f, 1, 1000f);

        private Level level;
        private float cameraRotation = 90;
        private Matrix view;
        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
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
            MIDDLE_BRICK = Content.Load<Model>("basicModels/middle_brick");
            CORNER_TOP_RIGHT_BRICK = Content.Load<Model>("basicModels/corner_top_right_brick");
            CORNER_TOP_LEFT_BRICK = Content.Load<Model>("basicModels/corner_top_left_brick");
            CORNER_BOTTOM_RIGHT_BRICK = Content.Load<Model>("basicModels/corner_bottom_right_brick");
            CORNER_BOTTOM_LEFT_BRICK = Content.Load<Model>("basicModels/corner_bottom_left_brick");
            SIDE_TOP_BRICK = Content.Load<Model>("basicModels/side_top_brick");
            SIDE_LEFT_BRICK = Content.Load<Model>("basicModels/side_left_brick");
            SIDE_RIGHT_BRICK = Content.Load<Model>("basicModels/side_right_brick");
            SIDE_BOTTOM_BRICK = Content.Load<Model>("basicModels/side_bottom_brick");
            THINGY = Content.Load<Model>("basicModels/thingy");
            //adjust the size of the level here
            level = LevelFactory.buildLevel(projection, 4);
            thing = new Thingy();
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            HandleInput();

            UpdateCamera();
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGreen);
            level.drawLevel(view);
            thing.Draw(view, projection);

            base.Draw(gameTime);
        }
        /// <summary>
        /// Handles input for quitting the game.
        /// </summary>
        private void HandleInput()
        {
            currentKeyboardState = Keyboard.GetState();
            currentMouseState = Mouse.GetState();
            // Check for exit.
            if (currentKeyboardState.IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            thing.HandleInput(currentKeyboardState, currentMouseState);
        }

        /// <summary>
        /// this function will calculate the camera's position and the position of 
        /// its target. From those, we'll update the view matrix
        /// </summary>
        private void UpdateCamera()
        {
            //calculate the rotation of the camera so that the rooms are looking the right way up
            Matrix cameraFacingMatrix = Matrix.CreateRotationY(thing.FacingDirection) * Matrix.CreateRotationX(MathHelper.ToRadians(cameraRotation));
            Vector3 positionOffset = Vector3.Transform(CameraPositionOffset, cameraFacingMatrix);
            Vector3 targetOffset = Vector3.Transform(CameraTargetOffset, cameraFacingMatrix);

            // figuring out the new camera position
            Vector3 cameraPosition = thing.Position + positionOffset;

            //figure out the targets position + offset
            Vector3 cameraTarget = thing.Position + targetOffset;
            //calculate the view matrix with the cameraPosition and cameraTarget witht the z being up
            view = Matrix.CreateLookAt(cameraPosition, cameraTarget, Vector3.UnitZ);
        }
    }
}
