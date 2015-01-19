using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DungeonCreatorTestArea.TestingMenu
{
    public class TMenu
    {
        private bool isEnabled = false;
        public bool IsEnabled
        {
            get { return isEnabled; }
            set { isEnabled = value; }
        }
        private bool isHovered = false;
        private KeyboardState oldKeyboardState;
        private MouseState oldMouseState;
        private VertexPositionColor[] background = new VertexPositionColor[6];
        private VertexPositionColor[] backgroundBorder = new VertexPositionColor[8];
        private const int height = 200;
        private const int width = 50;
        private const int inset = 10;
        private Vector3 origin = new Vector3(20, 20, 0);
        private Color backgroundColor = Color.Black;
        private Color borderColor = Color.HotPink;
        private Color hoverColor = Color.White;
        private BasicEffect basicEffect;
        private GraphicsDevice graphicsDevice;
        public TMenu(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
            createMenu();
        }

        private void createMenu()
        {
            /*
            * setting up the graphics here
            */
            basicEffect = new BasicEffect(graphicsDevice);
            basicEffect.VertexColorEnabled = true;
            basicEffect.Projection = Matrix.CreateOrthographicOffCenter
               (0, graphicsDevice.Viewport.Width,       // left, right
                graphicsDevice.Viewport.Height, 0,      // bottom, top
                0, 1);                                  // near, far plane
            
            createBackground();
        }
        private void createBackground()
        {
            
            background[0] = new VertexPositionColor(new Vector3(origin.X - inset, origin.Y - inset, 0), backgroundColor);
            background[1] = new VertexPositionColor(new Vector3(origin.X + width + inset, origin.Y - inset, 0), backgroundColor);
            background[2] = new VertexPositionColor(new Vector3(origin.X + width + inset, origin.Y + height + inset, 0), backgroundColor);
            background[3] = new VertexPositionColor(new Vector3(origin.X + width + inset, origin.Y + height + inset, 0), backgroundColor);
            background[4] = new VertexPositionColor(new Vector3(origin.X - inset, origin.Y + height + inset, 0), backgroundColor);
            background[5] = new VertexPositionColor(new Vector3(origin.X - inset, origin.Y - inset, 0), backgroundColor);

            backgroundBorder[0] = new VertexPositionColor(new Vector3(origin.X - inset, origin.Y - inset, 0), borderColor);
            backgroundBorder[1] = new VertexPositionColor(new Vector3(origin.X + width + inset, origin.Y - inset, 0), borderColor);
            backgroundBorder[2] = new VertexPositionColor(new Vector3(origin.X + width + inset, origin.Y - inset, 0), borderColor);
            backgroundBorder[3] = new VertexPositionColor(new Vector3(origin.X + width + inset, origin.Y + height + inset, 0), borderColor);
            backgroundBorder[4] = new VertexPositionColor(new Vector3(origin.X + width + inset, origin.Y + height + inset, 0), borderColor);
            backgroundBorder[5] = new VertexPositionColor(new Vector3(origin.X - inset, origin.Y + height + inset, 0), borderColor);
            backgroundBorder[6] = new VertexPositionColor(new Vector3(origin.X - inset, origin.Y + height + inset, 0), borderColor);
            backgroundBorder[7] = new VertexPositionColor(new Vector3(origin.X - inset, origin.Y - inset, 0), borderColor);
        }
        public void handleInput(KeyboardState currentKeyboardState, MouseState currentMouseState) 
        {
            //initializes the states if they are null
            if (oldKeyboardState == null)
            {
                oldKeyboardState = currentKeyboardState;
            }
            if (oldMouseState == null)
            {
                oldMouseState = currentMouseState;
            }
            //turn on or off the menu here
            if (currentKeyboardState.IsKeyDown(Keys.M) && oldKeyboardState.IsKeyUp(Keys.M))
            {
                if (IsEnabled)
                {
                    IsEnabled = false;
                }
                else
                {
                    IsEnabled = true;
                }

            }
            //checks to see is the mouse is in the menu
            if (IsPointInPolygon(currentMouseState.X, currentMouseState.Y))
            {
                isHovered = true;
                setBackgroundColor(hoverColor);
            }
            else if (isHovered)
            {
                isHovered = false;
                setBackgroundColor(backgroundColor);
            }

            oldKeyboardState = currentKeyboardState;
            oldMouseState = currentMouseState;

        }
        private void setBackgroundColor(Color color)
        {
            for (int i = 0; i < background.Length; i++)
            {
                background[i].Color = color;
            }
        }
        private bool IsPointInPolygon(int pointX, int pointY)
        {

            bool isInside = false;
            VertexPositionColor temp1;
            VertexPositionColor temp2;
            for (int i = 0, j = backgroundBorder.Length - 1; i < backgroundBorder.Length; j = i++)
            {
                temp1 = backgroundBorder[i];
                temp2 = backgroundBorder[j];
                if (((temp1.Position.Y > pointY) != (temp2.Position.Y > pointY)) &&
                    (pointX < (temp2.Position.X - temp1.Position.X) * (pointY - temp1.Position.Y) / (temp2.Position.Y - temp1.Position.Y) + temp1.Position.X))
                {
                    isInside = !isInside;
                }
            }
            return isInside;
        }
        public void update()
        {

        }
        public void draw(SpriteBatch spriteBatch)
        {
            if (IsEnabled)
            {
                basicEffect.CurrentTechnique.Passes[0].Apply();
                spriteBatch.GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleList, background, 0, 2);
                spriteBatch.GraphicsDevice.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.LineList, backgroundBorder, 0, 4);
            }
        }

    }
}
