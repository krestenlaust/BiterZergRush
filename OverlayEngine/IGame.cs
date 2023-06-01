using System.Windows.Forms;

namespace OverlayEngine
{
    public interface IGame
    {
        /// <summary>
        /// Used for initializing assets.
        /// </summary>
        void OnLoad();

        void OnUpdate(float deltaSeconds);

        void OnKeyDown(KeyEventArgs keyEvent);

        void OnKeyUp(KeyEventArgs keyEvent);
    }
}
