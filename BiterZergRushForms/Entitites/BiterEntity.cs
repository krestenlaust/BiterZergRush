using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace BiterZergRushForms.Entities
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
        const float PixelsPerSecond = 220f; //50;

        /// <summary>
        /// Interval of sprite swap.
        /// Unit: 1 / second.
        /// </summary>
        float AnimationIntervalSecond => PixelsPerFrame / PixelsPerSecond;

        int animationIndex = 0;
        float timeSinceFrameSwap = 0;
        GameVector movementStart;
        GameVector movementEnd;
        WindowEntity targetWindow = null;

        float timeSinceTargetSet = 0;
        float totalTravelTime = 0;
        float timeSinceWindowSelected = 0;
        bool attacking = false;

        public BiterEntity()
        {
            Scale = 0.5f;
            MaxHealth = 10;
            Health = 10;
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
                if (attacking)
                {
                    return BiterAttackSpritesheet.GetBiter(animationIndex, ConvertRadiansTo16thCardinal(Rotation));
                }
                else
                {
                    return BiterRunSpritesheet.GetBiter(animationIndex, ConvertRadiansTo16thCardinal(Rotation));
                }
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

        public void Attack(GameEntity entity)
        {
            
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
            timeSinceWindowSelected += deltaSeconds;

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
                animationIndex = (animationIndex + 1) % (attacking ? 11 : 16);
            }

            if (timeSinceWindowSelected >= 3)
            {
                timeSinceWindowSelected = 0;

                WindowEntity shortestWindow = null;
                float? shortestDistance = null;
                foreach (var item in Engine.GetEntititesByType<WindowEntity>())
                {
                    float distance = GameVector.Distance(Location, item.Location);
                    if (distance < 0.1f)
                    {
                        continue;
                    }

                    if (shortestDistance is null || distance < shortestDistance.Value)
                    {
                        shortestDistance = distance;
                        shortestWindow = item;
                    }
                }

                targetWindow = shortestWindow;
            }

            if (targetWindow is null)
            {
                return;
            }


            bool currentlyAttacking = attacking;
            GameVector nearestPointToWindow = Location.NearestPointOnRectangle(targetWindow.WindowRect);
            attacking = GameVector.Distance(Location, nearestPointToWindow) < 2;

            if (currentlyAttacking != attacking)
            {
                timeSinceFrameSwap = AnimationIntervalSecond;
                animationIndex = 0;
            }

            if (attacking)
            {
                targetWindow.Health -= deltaSeconds;
            }
            else
            {
                MoveTo(nearestPointToWindow);
            }
        }

        public static class BiterAttackSpritesheet
        {
            readonly static SpritesheetImage[] biterAttack;
            private const int BiterAnimationFrameCount = 11;

            static BiterAttackSpritesheet()
            {
                biterAttack = new SpritesheetImage[]
                {
                    new SpritesheetImage(Properties.Resources.biter_attack_01, 11, 4),
                    new SpritesheetImage(Properties.Resources.biter_attack_02, 11, 4),
                    new SpritesheetImage(Properties.Resources.biter_attack_03, 11, 4),
                    new SpritesheetImage(Properties.Resources.biter_attack_04, 11, 4)
                };
            }

            public static Bitmap GetBiter(int animationFrameIndex, int rotation)
            {
                int cardinalDirection = Math.DivRem(rotation, 4, out int quarterDivision);

                int biterAnimationOffset = quarterDivision * BiterAnimationFrameCount;

                return biterAttack[cardinalDirection][biterAnimationOffset + animationFrameIndex];
            }
        }

        public static class BiterRunSpritesheet
        {
            readonly static SpritesheetImage[] biterRun;
            private const int BiterAnimationFrameCount = 16;

            static BiterRunSpritesheet()
            {
                biterRun = new SpritesheetImage[]
                {
                    new SpritesheetImage(Properties.Resources.biter_run_01, 8, 8),
                    new SpritesheetImage(Properties.Resources.biter_run_02, 8, 8),
                    new SpritesheetImage(Properties.Resources.biter_run_03, 8, 8),
                    new SpritesheetImage(Properties.Resources.biter_run_04, 8, 8),
                };
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
