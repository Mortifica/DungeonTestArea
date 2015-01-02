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
            for (int i = 0; i < numberOfRooms; i++)
            {
                //makes a long room
                if (random.Next(2) == 1)
                {
                    width = random.Next(4) + 2;
                    length = random.Next(4) + 4;
                }
                //makes a wide room
                else
                {
                    width = random.Next(4) + 4;
                    length = random.Next(4) + 2;
                }
                //need to find the rom of the exit of the room
                exitRow = random.Next(length) + 1;
                // if the row is on the top or bottom does it exit going up or down
                if(exitRow == 0 && random.Next(2) == 1) //if the row is top and random is == 1 then exit is going up
                {
                    roomExit.W = 1; //use the 4th part of this vetor to say the direction.  1 is going up
                }
                else if(exitRow == length && random.Next(2) == 1)//bottom row and random is == 1 the exit is going down
                {
                    roomExit.W = -1; //use the 4th part of this vetor to say the direction.  -1 is going down
                }
                else//exit is going right
                {
                    roomExit.W = 0; //use the 4th part of this vetor to say the direction.  0 is going right
                }
                //we will find the column of the exit if the room is wider than 2
                if(width > 2)
                {
                    roomExit.W *= random.Next(width) + 1; //if the W was 0 to start with it will remain 0 
                }
                roomExit.X = topLeftVector.X + (width - roomExit.W - 1) * RoomFactory.VECTOR_CHANGE;

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
                    nextRoomEntrance.X += RoomFactory.VECTOR_CHANGE;
                }


                currentRoom = RoomFactory.buildRoom(projection, width, length, topLeftVector, roomEntrance, roomExit);
                rooms.Add(currentRoom);

                // need to adjust topLeftcorner


            }

            return new Level(rooms);
        }

    }
}
