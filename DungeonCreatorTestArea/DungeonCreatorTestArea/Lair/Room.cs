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
        private Vector3 entrance;
        public Vector3 Entrance
        {
            get { return entrance; }
            set { entrance = value; }
        }
        private Vector3 exit;
        public Vector3 Exit
        {
            get { return exit; }
            set { exit = value; }
        }
        private Vector3 nextRoom;
        public Vector3 NextRoom
        {
            get { return nextRoom; }
            set { nextRoom = value; }
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
