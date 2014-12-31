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
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
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
        // tank. This value can be changed to move the camera further away from or
        // closer to the tank.
        readonly Vector3 CameraPositionOffset = new Vector3(200, 75, 0);

        // This value controls the point the camera will aim at. This value is an offset
        // from the tank's position.
        readonly Vector3 CameraTargetOffset = new Vector3(0, 0, 0);

        KeyboardState currentKeyboardState = new KeyboardState();
        private Thingy thing;
        private Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), 800f / 480f, 1, 1000f);
        private Room room;
        float cameraArc = 90;
        float cameraRotation = 0;
        float cameraDistance = 100;
        Matrix view;
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
            //adjust the size of the room here
            room = RoomFactory.buildRoom(projection,6, 5);
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
            GraphicsDevice.Clear(Color.DarkMagenta);

            // Compute camera matrices.
            //Matrix view = Matrix.CreateTranslation(0, 0, 0) *
            //              Matrix.CreateRotationZ(MathHelper.ToRadians(cameraRotation)) *
            //              Matrix.CreateRotationX(MathHelper.ToRadians(cameraArc)) *
            //              Matrix.CreateLookAt(new Vector3(0, 50, -cameraDistance),
            //                                  new Vector3(0, 0, 0), Vector3.UnitZ);
            room.DrawRoom(view);
            thing.Draw(view, projection);

            base.Draw(gameTime);
        }
        /// <summary>
        /// Handles input for quitting the game.
        /// </summary>
        private void HandleInput()
        {
            currentKeyboardState = Keyboard.GetState();

            // Check for exit.
            if (currentKeyboardState.IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            thing.HandleInput(currentKeyboardState);
        }


        ///// <summary>
        ///// Handles camera input.
        ///// </summary>
        //private void UpdateCamera(GameTime gameTime)
        //{
        //    float time = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

        //    // Check for input to rotate the camera up and down around the model.
        //    if (currentKeyboardState.IsKeyDown(Keys.Up) ||
        //        currentKeyboardState.IsKeyDown(Keys.W))
        //    {
        //        cameraArc += time * 0.1f;
        //    }

        //    if (currentKeyboardState.IsKeyDown(Keys.Down) ||
        //        currentKeyboardState.IsKeyDown(Keys.S))
        //    {
        //        cameraArc -= time * 0.1f;
        //    }

        //    //cameraArc += currentGamePadState.ThumbSticks.Right.Y * time * 0.25f;

        //    // Limit the arc movement.
        //    if (cameraArc > 180.0f)
        //        cameraArc = 180.0f;
        //    else if (cameraArc < -180.0f)
        //        cameraArc = -180.0f;

        //    // Check for input to rotate the camera around the model.
        //    if (currentKeyboardState.IsKeyDown(Keys.Right) ||
        //        currentKeyboardState.IsKeyDown(Keys.D))
        //    {
        //        cameraRotation += time * 0.1f;
        //    }

        //    if (currentKeyboardState.IsKeyDown(Keys.Left) ||
        //        currentKeyboardState.IsKeyDown(Keys.A))
        //    {
        //        cameraRotation -= time * 0.1f;
        //    }

        //    //cameraRotation += currentGamePadState.ThumbSticks.Right.X * time * 0.25f;

        //    // Check for input to zoom camera in and out.
        //    if (currentKeyboardState.IsKeyDown(Keys.Z))
        //        cameraDistance += time * 0.25f;

        //    if (currentKeyboardState.IsKeyDown(Keys.X))
        //        cameraDistance -= time * 0.25f;

        //    //cameraDistance += currentGamePadState.Triggers.Left * time * 0.5f;
        //    //cameraDistance -= currentGamePadState.Triggers.Right * time * 0.5f;

        //    // Limit the camera distance.
        //    if (cameraDistance > 500.0f)
        //        cameraDistance = 500.0f;
        //    else if (cameraDistance < 10.0f)
        //        cameraDistance = 10.0f;

        //    if (currentKeyboardState.IsKeyDown(Keys.R))
        //    {
        //        cameraArc = 0;
        //        cameraRotation = 0;
        //        cameraDistance = 100;
        //    }
        //}
        /// <summary>
        /// this function will calculate the camera's position and the position of 
        /// its target. From those, we'll update the viewMatrix.
        /// </summary>
        private void UpdateCamera()
        {
            // The camera's position depends on the tank's facing direction: when the
            // tank turns, the camera needs to stay behind it. So, we'll calculate a
            // rotation matrix using the tank's facing direction, and use it to
            // transform the two offset values that control the camera.
            Matrix cameraFacingMatrix = Matrix.CreateRotationY(thing.FacingDirection) * Matrix.CreateRotationX(MathHelper.ToRadians(cameraArc));
            Vector3 positionOffset = Vector3.Transform(CameraPositionOffset, cameraFacingMatrix);
            Vector3 targetOffset = Vector3.Transform(CameraTargetOffset, cameraFacingMatrix);

            // once we've transformed the camera's position offset vector, it's easy to
            // figure out where we think the camera should be.
            Vector3 cameraPosition = thing.Position + positionOffset;

                // we don't want the camera to go beneath the terrain's height +
                // a small offset.
                float minimumHeight = 0;
               // Vector3 normal = Vector3.Zero;

               // minimumHeight += CameraPositionOffset.X;

               // if (cameraPosition.X < minimumHeight)
              //  {
               //     cameraPosition.X = minimumHeight;
              //  }
            

            // next, we need to calculate the point that the camera is aiming it. That's
            // simple enough - the camera is aiming at the tank, and has to take the 
            // targetOffset into account.
            Vector3 cameraTarget = thing.Position + targetOffset;

            //view = Matrix.CreateRotationZ(MathHelper.ToRadians(cameraRotation)) * Matrix.CreateRotationX(MathHelper.ToRadians(cameraArc));
            // with those values, we'll calculate the viewMatrix.
            view = Matrix.CreateLookAt(cameraPosition, cameraTarget, Vector3.UnitZ);
        }
    }
}
