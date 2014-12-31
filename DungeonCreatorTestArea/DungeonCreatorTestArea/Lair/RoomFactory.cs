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
        private static Model cornertopleftSection = Game1.CORNER_TOP_LEFT_BRICK;
        private static Model cornertoprightSection = Game1.CORNER_TOP_RIGHT_BRICK;
        private static Model cornerbottomleftSection = Game1.CORNER_BOTTOM_LEFT_BRICK;
        private static Model cornerbottomrightSection = Game1.CORNER_BOTTOM_RIGHT_BRICK;
        private static Model sidetopSection = Game1.SIDE_TOP_BRICK;
        private static Model siderightSection = Game1.SIDE_RIGHT_BRICK;
        private static Model sideleftSection = Game1.SIDE_LEFT_BRICK;
        private static Model sidebottomSection = Game1.SIDE_BOTTOM_BRICK;
        private static Model middleSection = Game1.MIDDLE_BRICK;

         public static Room buildRoom(Matrix projection, int width, int length)
        {
            Vector3 currentVector = new Vector3(0, 0, 0);
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
                            tempSection = new RoomSection(cornertopleftSection, world, projection);
                            currentVector.X += VECTOR_CHANGE;
                        }
                        //top right corner roomSection
                        else if (j == length - 1)
                        {
                            world = Matrix.CreateTranslation(currentVector);
                            tempSection = new RoomSection(cornertoprightSection, world, projection);
                            currentVector.X = 0;
                            currentVector.Y -= VECTOR_CHANGE;
                        }
                        // middle of top row roomSection
                        else
                        {
                            world = Matrix.CreateTranslation(currentVector);
                            tempSection = new RoomSection(sidetopSection, world, projection);
                            currentVector.X += VECTOR_CHANGE;
                        }
                    }
                    //bottom roomSection
                    else if(i == width - 1)
                    {
                        //bottom left corner roomSection
                        if (j == 0)
                        {
                            
                            world = Matrix.CreateTranslation(currentVector);
                            tempSection = new RoomSection(cornerbottomleftSection, world, projection);
                            currentVector.X += VECTOR_CHANGE;
                        }
                        //bottom right corner roomSection
                        else if (j == length - 1)
                        {
                            world = Matrix.CreateTranslation(currentVector);
                            tempSection = new RoomSection(cornerbottomrightSection, world, projection);
                        }
                        // middle of bottom row roomSection
                        else
                        {
                            world = Matrix.CreateTranslation(currentVector);
                            tempSection = new RoomSection(sidebottomSection, world, projection);
                            currentVector.X += VECTOR_CHANGE;
                        }
                    }
                    //middle roomSection
                    else
                    {
                        //middle left roomSection
                        if (j == 0)
                        {
                            world = Matrix.CreateTranslation(currentVector);
                            tempSection = new RoomSection(sideleftSection, world, projection);
                            currentVector.X += VECTOR_CHANGE;
                        }
                        //middle right roomSection
                        else if (j == length - 1)
                        {
                            world = Matrix.CreateTranslation(currentVector);
                            tempSection = new RoomSection(siderightSection, world, projection);
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
