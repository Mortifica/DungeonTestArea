using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DungeonCreatorTestArea.Lair
{
    class Room
    {
        private int width;

        private int length;

        private RoomSection[,] sections;

        public RoomSection[,] Sections
        {
            set { sections = value; }
        }

        public Room(int width, int length)
        {
            this.width = width;
            this.length = length;
            sections = new RoomSection[width, length];
        }

        public void DrawRoom(Matrix view)
        {
            foreach (RoomSection section in sections)
            {
                section.drawSection(view);
            }
        }
    }
}
