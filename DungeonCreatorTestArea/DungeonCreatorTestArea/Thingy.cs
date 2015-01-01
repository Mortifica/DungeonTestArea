using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DungeonCreatorTestArea
{
    class Thingy
    {
        #region Constants


        // This constant controls how quickly the thingy can move forward and backward, left and right
        const float ThingyVelocity = 2;

        // controls how quickly the thingy can turn from side to side.
        const float ThingyTurnSpeed = .025f;


        #endregion


        #region Properties

        public Vector3 Position
        {
            get { return position; }
        }
        private Vector3 position;

        public float FacingDirection
        {
            get { return facingDirection; }
        }
        private float facingDirection;


        #endregion


        #region Fields

        // The thingy's model
        Model model = Game1.THINGY;

        Matrix orientation = Matrix.Identity;
        Matrix world;
        #endregion


        #region Initialization

        /// <summary>
        /// Called when the Game is loading its content. Pass in a ContentManager so the
        /// thingy can load its model.
        /// </summary>
        public void LoadContent(ContentManager content)
        {

        }

        #endregion


        #region Update and Draw

        /// <summary>
        /// This function is called when the game is Updating in response to user input.
        /// It'll move the thingy around the heightmap, and update all of the thingy's 
        /// necessary state.
        /// </summary>
        public void HandleInput(KeyboardState currentKeyboardState, MouseState currentMouseState)
        {
            // First, we want to check to see if the thingy should turn. turnAmount will 
            // be an accumulation of all the different possible inputs.
            float turnAmount = 0;
            if (currentKeyboardState.IsKeyDown(Keys.A) || currentKeyboardState.IsKeyDown(Keys.Left))
            {
                turnAmount += 1;
            }

            if (currentKeyboardState.IsKeyDown(Keys.D) || currentKeyboardState.IsKeyDown(Keys.Right))
            {
                turnAmount -= 1;
            }

            // clamp the turn amount between -1 and 1, and then use the finished
            // value to turn the thingy.
            turnAmount = MathHelper.Clamp(turnAmount, -1, 1);
            facingDirection += turnAmount * ThingyTurnSpeed;


            // Next, we want to move the thingy forward or back. to do this, 
            // we'll create a Vector3 and modify use the user's input to modify the X
            // component, which corresponds to the forward direction.
            Vector3 movement = Vector3.Zero;

            if (currentKeyboardState.IsKeyDown(Keys.W) || currentKeyboardState.IsKeyDown(Keys.Up))
            {
                movement.X = 1;
            }
            if (currentKeyboardState.IsKeyDown(Keys.S) || currentKeyboardState.IsKeyDown(Keys.Down))
            {
                movement.X = -1;
            }

            // next, we'll create a rotation matrix from the direction the thingy is 
            // facing, and use it to transform the vector.
            orientation = Matrix.CreateRotationZ(FacingDirection - MathHelper.Pi);
            Vector3 velocity = Vector3.Transform(movement, orientation);
            velocity *= ThingyVelocity;

            Vector3 newPosition = Position + velocity;

            position = newPosition;
        }

        public void Draw(Matrix viewMatrix, Matrix projectionMatrix)
        {

            Matrix[] boneTransforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(boneTransforms);

            //multiply the orientation matrix by the translated(position) matrix to get the world matrix
            world = orientation * Matrix.CreateTranslation(Position);

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = boneTransforms[mesh.ParentBone.Index] * world;
                    effect.View = viewMatrix;
                    effect.Projection = projectionMatrix;

                    effect.EnableDefaultLighting();
                    effect.PreferPerPixelLighting = true;

                    // Set the fog to match the black background color
                    //effect.FogEnabled = true;
                    //effect.FogColor = Vector3.Zero;
                    //effect.FogStart = 100;
                    //effect.FogEnd = 320;
                }
                mesh.Draw();
            }
        }

        #endregion
    }
}
