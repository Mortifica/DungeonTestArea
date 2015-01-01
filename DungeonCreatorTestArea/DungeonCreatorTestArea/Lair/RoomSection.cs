using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DungeonCreatorTestArea.Lair
{
    class RoomSection
    {
        private Model section;

        private Matrix world;
        private Matrix projection;
        public RoomSection(Model section, Matrix world, Matrix projection) 
        {
            this.section = section;
            this.world = world;
            this.projection = projection;
        }

        public void drawSection(Matrix view)
        {
            Matrix[] transforms = new Matrix[section.Bones.Count];
            section.CopyAbsoluteBoneTransformsTo(transforms);

            foreach (ModelMesh mesh in section.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {

                    effect.EnableDefaultLighting();
                    effect.World = transforms[mesh.ParentBone.Index] * world;

                    effect.View = view;
                    effect.Projection = projection;
                    effect.PreferPerPixelLighting = true;

                    // Set the fog to match the black background color
                    //effect.FogEnabled = true;
                    //effect.FogColor = Vector3.Zero;
                    //effect.FogStart = 1000;
                    //effect.FogEnd = 3200;
                }

                mesh.Draw();
            }
        }
    }
}
