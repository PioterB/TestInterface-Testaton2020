using Bumblebee.Implementation;
using Bumblebee.Setup;

namespace Testaton.TestInterface.ContosoModel
{
    /// <summary>
    /// Represents main shell which hosts other parts of application. 
    /// Shell contains main menu and global actions.
    /// </summary>
    public class ShellPage : Page
    {
        public ShellPage(Session session) : base(session)
        {
            Navigation = new NavigationBox(this);
        }

        /// <summary>
        /// Gets application's navigation menu.
        /// </summary>
        public NavigationBox Navigation { get; }
    }
}
