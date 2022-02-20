using System;
using System.Drawing;

namespace BiterZergRushForms
{
    public class BiterEntity : GameEntity
    {
        /// <summary>
        /// Amount of pixels to move before animation frame swap.
        /// Unit: Pixels / 1
        /// </summary>
        const float PixelsPerFrame = 1f; // 4.4f
        /// <summary>
        /// Movement speed,
        /// Unit: Pixels / Second
        /// </summary>
        const float PixelsPerSecond = 220; //50;

        /// <summary>
        /// Interval of sprite swap.
        /// Unit: 1 / second.
        /// </summary>
        float AnimationIntervalSecond => PixelsPerFrame / PixelsPerSecond;

        int animationIndex = 0;
        float timeSinceFrameSwap = 0;
        GameVector movementStart;
        GameVector movementEnd;

        float timeSinceTargetSet = 0;
        float totalTravelTime = 0;

        public BiterEntity()
        {
            Scale = 0.5f;
        }

        GameVector internalLocation;

        public override GameVector Location {
            get => internalLocation;
            set
            {
                internalLocation = value;
                movementEnd = value;
            }
        }

        public override Image Sprite {
            get
            {
                return BiterRunSpritesheet.GetBiter(animationIndex, ConvertRadiansTo16thCardinal(Rotation));
            }
        }

        /// <summary>
        /// Converts radians into quarths of the cardinals.
        /// </summary>
        /// <param name="radians"></param>
        /// <returns></returns>
        private static int ConvertRadiansTo16thCardinal(float radians)
        {
            return ((int)Math.Round(radians / (Math.PI * 2) * 16)) % 16;
        }

        public void MoveTo(GameVector position)
        {
            float travelDistance = GameVector.Distance(internalLocation, position);
            totalTravelTime = travelDistance / PixelsPerSecond;

            timeSinceFrameSwap = 0;
            timeSinceTargetSet = 0;

            movementStart = internalLocation;
            movementEnd = position;

            GameVector differenceVector = movementEnd - movementStart;
            Rotation = (float)(Math.Atan2(differenceVector.Y, differenceVector.X) + (Math.PI * 2.5f));
        }

        public override void OnUpdate(float deltaSeconds)
        {
            timeSinceFrameSwap += deltaSeconds;
            timeSinceTargetSet += deltaSeconds;

            float lerpValue = timeSinceTargetSet / totalTravelTime;
            lerpValue = Math.Min(1, lerpValue);

            // interpolate biter position.
            internalLocation = GameVector.Lerp(movementStart, movementEnd, lerpValue);

            // reached target
            if (timeSinceTargetSet >= totalTravelTime)
            {
                // reset lerp values
                internalLocation = movementEnd;
                movementStart = internalLocation;
            }

            // if time passed for whole frame, then swap frame.
            if (timeSinceFrameSwap >= AnimationIntervalSecond)
            {
                timeSinceFrameSwap = 0;

                // swap to next animation frame
                animationIndex = (animationIndex + 1) % 16;
            }
        }

        public static class BiterRunSpritesheet
        {
            private static SpritesheetImage[] biterRun;
            private const int BiterAnimationFrameCount = 16;

            public static void Setup(Bitmap north, Bitmap east, Bitmap south, Bitmap west)
            {
                biterRun = new SpritesheetImage[4];
                biterRun[0] = new SpritesheetImage(north, 8, 8);
                biterRun[1] = new SpritesheetImage(east, 8, 8);
                biterRun[2] = new SpritesheetImage(south, 8, 8);
                biterRun[3] = new SpritesheetImage(west, 8, 8);
            }

            public static Bitmap GetBiter(int animationFrameIndex, int rotation)
            {
                // Divides the rotation between 0 and 15 into the 4 cardinal directions (integer division).
                // and stores the remaining to determine the particular rotation.
                int cardinalDirection = Math.DivRem(rotation, 4, out int quarterDivision);

                int biterAnimationOffset = quarterDivision * BiterAnimationFrameCount;

                return biterRun[cardinalDirection][biterAnimationOffset + animationFrameIndex];
            }
        }
    }
}
