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
        public static Model CORNER_BRICK;
        public static Model SIDE_BRICK;
        public static Model MIDDLE_BRICK;

        KeyboardState currentKeyboardState = new KeyboardState();

        private Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), 800f / 480f, 1, 10000f);
        private Room room;
        float cameraArc = -90;
        float cameraRotation = 0;
        float cameraDistance = 100;

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
            CORNER_BRICK = Content.Load<Model>("corner_brick");
            SIDE_BRICK = Content.Load<Model>("side_brick");
            MIDDLE_BRICK = Content.Load<Model>("middle_brick");
            //adjust the size of the room here
            room = RoomFactory.buildRoom(projection,4, 4);
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

            UpdateCamera(gameTime);
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
            Matrix view = Matrix.CreateTranslation(0, 0, 0) *
                          Matrix.CreateRotationZ(MathHelper.ToRadians(cameraRotation)) *
                          Matrix.CreateRotationX(MathHelper.ToRadians(cameraArc)) *
                          Matrix.CreateLookAt(new Vector3(0, 50, -cameraDistance),
                                              new Vector3(0, 0, 0), Vector3.UnitZ);
            room.DrawRoom(view);


            base.Draw(gameTime);
        }
        private void DrawModel(Model model, Matrix world, Matrix view, Matrix projection)
        {

            Matrix[] transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);  
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {

                    effect.EnableDefaultLighting();
                    effect.World = transforms[mesh.ParentBone.Index] * world;

                    effect.View = view;
                    effect.Projection = projection;
                }

                mesh.Draw();
            }
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
        }


        /// <summary>
        /// Handles camera input.
        /// </summary>
        private void UpdateCamera(GameTime gameTime)
        {
            float time = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            // Check for input to rotate the camera up and down around the model.
            if (currentKeyboardState.IsKeyDown(Keys.Up) ||
                currentKeyboardState.IsKeyDown(Keys.W))
            {
                cameraArc += time * 0.1f;
            }

            if (currentKeyboardState.IsKeyDown(Keys.Down) ||
                currentKeyboardState.IsKeyDown(Keys.S))
            {
                cameraArc -= time * 0.1f;
            }

            //cameraArc += currentGamePadState.ThumbSticks.Right.Y * time * 0.25f;

            // Limit the arc movement.
            if (cameraArc > 180.0f)
                cameraArc = 180.0f;
            else if (cameraArc < -180.0f)
                cameraArc = -180.0f;

            // Check for input to rotate the camera around the model.
            if (currentKeyboardState.IsKeyDown(Keys.Right) ||
                currentKeyboardState.IsKeyDown(Keys.D))
            {
                cameraRotation += time * 0.1f;
            }

            if (currentKeyboardState.IsKeyDown(Keys.Left) ||
                currentKeyboardState.IsKeyDown(Keys.A))
            {
                cameraRotation -= time * 0.1f;
            }

            //cameraRotation += currentGamePadState.ThumbSticks.Right.X * time * 0.25f;

            // Check for input to zoom camera in and out.
            if (currentKeyboardState.IsKeyDown(Keys.Z))
                cameraDistance += time * 0.25f;

            if (currentKeyboardState.IsKeyDown(Keys.X))
                cameraDistance -= time * 0.25f;

            //cameraDistance += currentGamePadState.Triggers.Left * time * 0.5f;
            //cameraDistance -= currentGamePadState.Triggers.Right * time * 0.5f;

            // Limit the camera distance.
            if (cameraDistance > 500.0f)
                cameraDistance = 500.0f;
            else if (cameraDistance < 10.0f)
                cameraDistance = 10.0f;

            if (currentKeyboardState.IsKeyDown(Keys.R))
            {
                cameraArc = 0;
                cameraRotation = 0;
                cameraDistance = 100;
            }
        }
    }
}
