using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DungeonCreatorTestArea.Lair
{
    class RoomFactory
    {
        private static Random random = new Random();
        private static int VECTOR_CHANGE = -60;
        private static Model cornerSection = Game1.CORNER_BRICK;
        private static Model sideSection = Game1.SIDE_BRICK;
        private static Model middleSection = Game1.MIDDLE_BRICK;


         public static Room buildRoom(Matrix projection, int width, int length)
        {
            Vector3 currentVector = new Vector3(0, 0, 0);
            Console.WriteLine("Width/Length " + width + "/" + length);
            Matrix world;
            Room result = new Room(width, length);
            RoomSection[,] room = new RoomSection[width, length];
            RoomSection tempSection;
            for (int i = 0; i < width; i++)
			{
			    for (int j = 0; j < length; j++)
			    {
                    //top roomSection
			        if(i == 0)
                    {
                        //top left corner roomSection
                        if (j == 0)
                        {
                            world = Matrix.CreateTranslation(currentVector);
                            tempSection = new RoomSection(cornerSection, world, projection);
                            currentVector.X += VECTOR_CHANGE;
                        }
                        //top right corner roomSection
                        else if (j == length - 1)
                        {
                            currentVector.X -= VECTOR_CHANGE;
                            world = Matrix.CreateRotationZ(-MathHelper.Pi / 2) * Matrix.CreateTranslation(currentVector);
                            tempSection = new RoomSection(cornerSection, world, projection);
                            currentVector.X = 0;
                            currentVector.Y -= VECTOR_CHANGE;
                        }
                        // middle of top row roomSection
                        else
                        {
                            world = Matrix.CreateTranslation(currentVector);
                           tempSection = new RoomSection(sideSection, world, projection);
                           currentVector.X += VECTOR_CHANGE;
                        }
                    }
                    //bottom roomSection
                    else if(i == width - 1)
                    {
                        //bottom left corner roomSection
                        if (j == 0)
                        {
                            currentVector.Y += VECTOR_CHANGE;
                            world = Matrix.CreateRotationZ(MathHelper.Pi / 2) * Matrix.CreateTranslation(currentVector);
                            tempSection = new RoomSection(cornerSection, world, projection);
                            currentVector.Y -= VECTOR_CHANGE;
                        }
                        //bottom right corner roomSection
                        else if (j == length - 1)
                        {
                            currentVector.Y += VECTOR_CHANGE;
                            world = Matrix.CreateRotationZ(-MathHelper.Pi) * Matrix.CreateTranslation(currentVector);
                            tempSection = new RoomSection(cornerSection, world, projection);
                        }
                        // middle of bottom row roomSection
                        else
                        {
                            currentVector.Y += VECTOR_CHANGE;
                            world = Matrix.CreateRotationZ(-MathHelper.Pi) * Matrix.CreateTranslation(currentVector);
                            tempSection = new RoomSection(sideSection, world, projection);
                            currentVector.Y -= VECTOR_CHANGE;
                            currentVector.X += VECTOR_CHANGE;
                        }
                    }
                    //middle roomSection
                    else
                    {
                        //middle left roomSection
                        if (j == 0)
                        {
                            currentVector.Y += VECTOR_CHANGE;
                            world = Matrix.CreateRotationZ(MathHelper.Pi / 2) * Matrix.CreateTranslation(currentVector);
                            tempSection = new RoomSection(sideSection, world, projection);
                            currentVector.Y -= VECTOR_CHANGE;
                            currentVector.X += VECTOR_CHANGE;
                        }
                        //middle right roomSection
                        else if (j == length - 1)
                        {
                            currentVector.X -= VECTOR_CHANGE;
                            world = Matrix.CreateRotationZ(-MathHelper.Pi / 2) * Matrix.CreateTranslation(currentVector);
                            tempSection = new RoomSection(sideSection, world, projection);
                            currentVector.X = 0;
                            currentVector.Y -= VECTOR_CHANGE;
                        }
                        // middle of middle row roomSection
                        else
                        {
                            world = Matrix.CreateTranslation(currentVector);
                            tempSection = new RoomSection(middleSection, world, projection);
                            currentVector.X += VECTOR_CHANGE;
                        }
                    }
                   room[i, j] = tempSection; 
			    }
                
			}
            result.Sections = room;
            
            return result;
        }
    }
}
