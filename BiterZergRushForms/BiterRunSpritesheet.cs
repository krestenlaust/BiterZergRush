using System;
using System.Drawing;

namespace BiterZergRushForms
{
    class BiterRunSpritesheet
    {
        private SpritesheetImage[] biterRun;
        private const int BiterAnimationFrameCount = 16;

        public BiterRunSpritesheet(Bitmap north, Bitmap east, Bitmap south, Bitmap west)
        {
            biterRun = new SpritesheetImage[4];
            biterRun[0] = new SpritesheetImage(north, 8, 8);
            biterRun[1] = new SpritesheetImage(east, 8, 8);
            biterRun[2] = new SpritesheetImage(south, 8, 8);
            biterRun[3] = new SpritesheetImage(west, 8, 8);
        }

        public Bitmap GetBiter(int animationFrameIndex, int rotation)
        {
            // Divides the rotation between 0 and 15 into the 4 cardinal directions (integer division).
            // and stores the remaining to determine the particular rotation.
            int cardinalDirection = Math.DivRem(rotation, 4, out int quarterDivision);

            int biterAnimationOffset = quarterDivision * BiterAnimationFrameCount;
            
            return biterRun[cardinalDirection][biterAnimationOffset + animationFrameIndex];
        }
    }
}
