using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DungeonCreatorTestArea.Lair
{
    class Level
    {
        private RoomFactory factory;

        private List<Room> rooms;

        public Level(List<Room> rooms)
        {
            this.rooms = rooms;
        }

        private void createLevel()
        {

        }
        public void drawLevel()
        {
            foreach  (Room room in rooms)
            {
                //room.draw();
            }
        }
    }
}
