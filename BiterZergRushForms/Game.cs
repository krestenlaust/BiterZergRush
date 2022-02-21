using System.Windows.Forms;

namespace BiterZergRushForms
{
    public abstract class Game
    {
        /// <summary>
        /// Used for initializing assets.
        /// </summary>
        public abstract void OnLoad();

        public abstract void OnUpdate(float deltaSeconds);

        public abstract void OnKeyDown(KeyEventArgs keyEvent);

        public abstract void OnKeyUp(KeyEventArgs keyEvent);
    }
}
