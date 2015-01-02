using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DungeonCreatorTestArea.Lair
{
    class LevelFactory
    {


        public static Level buildLevel(Matrix projection, int numberOfRooms)
        {
            Random random = new Random();
            List<Room> rooms = new List<Room>();
            Room currentRoom;
            Vector3 topLeftVector = Vector3.Zero; // sets vector to (0,0,0)
            Vector4 roomEntrance = Vector4.Zero; // sets vector to (0,0,0,0)
            Vector4 roomExit = Vector4.Zero; // sets vector to (0,0,0,0)
            Vector4 nextRoomEntrance = Vector4.Zero; // sets vector to (0,0,0,0)
            int width;
            int length;
            int exitRow;
            int entranceRow;
            int entranceColumn;
            bool isUp = true;
            //makes a long room initallizes to first room if the level
            if (random.Next(2) == 1)
            {
                length = random.Next(4) + 4;
                width = random.Next(4) + 2;
            }
            //makes a wide room
            else
            {
                length = random.Next(4) + 2;
                width = random.Next(4) + 4;
            }
            for (int i = 0; i < numberOfRooms; i++)
            {
                roomEntrance = nextRoomEntrance;
                //need to find the row of the exit of the room
                exitRow = random.Next(width);
                // need to find if next room joins on top or bottom or right side
                int upOrDown = random.Next(2);

               if(upOrDown == 1)
               {
                   if (isUp)
                   {
                        exitRow = 0;
                        roomExit.W = 1;// 1; //use the 4th part of this vetor to say the direction.  1 is going up
                        
                   }
                   else
                   {
                        exitRow = width - 1;
                        roomExit.W = -1; //use the 4th part of this vetor to say the direction.  -1 is going down
                   }
                   isUp = !isUp;
                  
               }
                else//exit is going right
                {
                    roomExit.W = 0;// 0; //use the 4th part of this vetor to say the direction.  0 is going right
                }
                roomExit.X = topLeftVector.X - (length - 1) * RoomFactory.VECTOR_CHANGE;
                //we will find the column of the exit if the room is wider than 2
                if(length > 2)
                {
                    //roomExit.W *= random.Next(2, length); //if the W was 0 to start with it will remain 0
                    //adjusts the roomExit.X 
                    if (roomExit.W > 0)
                    {
                        roomExit.X = topLeftVector.X - (length - roomExit.W) * RoomFactory.VECTOR_CHANGE;
                    }
                    else if (roomExit.W < 0)
                    {
                        roomExit.X = topLeftVector.X - (length + roomExit.W) * RoomFactory.VECTOR_CHANGE;
                    }

                }

                roomExit.Y = topLeftVector.Y + (exitRow * RoomFactory.VECTOR_CHANGE);

                nextRoomEntrance.X = roomExit.X;
                nextRoomEntrance.Y = roomExit.Y;
                nextRoomEntrance.W = roomExit.W;
                if(roomExit.W > 0)
                {
                    nextRoomEntrance.Y -= RoomFactory.VECTOR_CHANGE;
                }
                else if(roomExit.W < 0)
                {
                    nextRoomEntrance.Y += RoomFactory.VECTOR_CHANGE;
                }
                else
                {
                    nextRoomEntrance.X -= RoomFactory.VECTOR_CHANGE;
                }


                currentRoom = RoomFactory.buildRoom(projection, width, length, topLeftVector, roomEntrance, roomExit);
                rooms.Add(currentRoom);
                //makes a long room initallizes to first room if the level
                if (random.Next(2) == 1)
                {
                    width = random.Next(3) + 7;
                    length = random.Next(3) + 10;
                }
                //makes a wide room
                else
                {
                    width = random.Next(3) + 10;
                    length = random.Next(3) + 7;
                }
                //need to find the column the entrance of next room is in, but only if the room's entrance is 
                //going up or down not to the right
                entranceColumn = random.Next(6 , length);

                entranceRow = random.Next(width);
                // need to adjust topLeftcorner
                if (nextRoomEntrance.W > 0)//is W is > the entrance needs to be on the bottom row of next room
                {
                    topLeftVector.Y = nextRoomEntrance.Y - ((width -1) * RoomFactory.VECTOR_CHANGE);
                    topLeftVector.X = nextRoomEntrance.X + ((length - entranceColumn) * RoomFactory.VECTOR_CHANGE);
                }
                else if(nextRoomEntrance.W < 0)
                {
                    topLeftVector.Y = nextRoomEntrance.Y;
                    topLeftVector.X = nextRoomEntrance.X + ((length - entranceColumn) * RoomFactory.VECTOR_CHANGE);
                }
                else
                {
                    topLeftVector.Y = nextRoomEntrance.Y - ((entranceRow) * RoomFactory.VECTOR_CHANGE);
                    topLeftVector.X = nextRoomEntrance.X;
                }




            }

            return new Level(rooms);
        }

    }
}
