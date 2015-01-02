using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DungeonCreatorTestArea.Lair
{
    class Level
    {
        private List<Room> rooms;

        public Level(List<Room> rooms)
        {
            this.rooms = rooms;
        }

        public void drawLevel(Matrix view)
        {
            foreach  (Room room in rooms)
            {
                room.DrawRoom(view);
            }
        }
    }
}
