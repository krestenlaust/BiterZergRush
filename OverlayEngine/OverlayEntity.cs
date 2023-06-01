using System.Drawing;

namespace OverlayEngine
{
    public abstract class OverlayEntity
    {
        /// <summary>
        /// The location in pixels of the entity. Based in the center of the rendered graphics.
        /// </summary>
        public virtual Vector Location { get; set; }

        /// <summary>
        /// The scale of the rendered image.
        /// </summary>
        public float Scale { get; set; } = 1;

        /// <summary>
        /// Whether the overlay entity is to be drawn.
        /// </summary>
        public virtual bool Visible { get; set; } = true;

        /// <summary>
        /// The width of the render space available in <c>OnRender</c>.
        /// </summary>
        public virtual int Width { get; protected set; }

        /// <summary>
        /// The height of the render space available in <c>OnRender</c>.
        /// </summary>
        public virtual int Height { get; protected set; }

        internal bool IsDestroyed = false;

        /// <summary>
        /// Graphics of a bitmap with dimensions specified by <c>Width</c> and <c>Height</c>.
        /// Therefore it has it's own relative coordinate system, thus <c>Location</c> shouldn't be used in graphics calculations.
        /// </summary>
        /// <param name="graphics"></param>
        public abstract void OnRender(Graphics graphics);

        public virtual void OnUpdate(float deltaSeconds)
        {
        }

        public virtual void OnDestroy()
        {
        }
    }
}
