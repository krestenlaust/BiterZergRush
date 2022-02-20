using System.Drawing;

namespace BiterZergRushForms
{
    class BiterRunSpritesheet
    {
        private SpritesheetImage[] biterRun;

        public BiterRunSpritesheet(Bitmap north, Bitmap east, Bitmap south, Bitmap west)
        {
            biterRun = new SpritesheetImage[4];
            biterRun[0] = new SpritesheetImage(north, 8, 8);
            biterRun[1] = new SpritesheetImage(east, 8, 8);
            biterRun[2] = new SpritesheetImage(south, 8, 8);
            biterRun[3] = new SpritesheetImage(west, 8, 8);
        }

        public Bitmap GetBiter(int frame, int rotation)
        {
            return biterRun[rotation][frame];
        }
    }
}
