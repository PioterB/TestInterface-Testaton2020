using Bumblebee.Implementation;
using Bumblebee.Interfaces;
using OpenQA.Selenium;

namespace Testaton.TestInterface.ContosoModel
{
    /// <summary>
    /// Represents application Navigation.
    /// </summary>
    public class NavigationBox : Block
    {
        public NavigationBox(IBlock parent) : base(parent, By.XPath("//nav//ul"))
        {
        }

        /// <summary>
        /// Gets access to home page.
        /// </summary>
        public IClickable<ShellPage> Home => new Clickable<ShellPage>(this, By.XPath("li[1]"));
    }
}