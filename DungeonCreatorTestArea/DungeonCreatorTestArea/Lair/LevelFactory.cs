using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DungeonCreatorTestArea.Lair
{
    class LevelFactory
    {


        public static Level buildLevel(int numberOfRooms)
        {

            return new Level(new List<Room>());
        }

    }
}
