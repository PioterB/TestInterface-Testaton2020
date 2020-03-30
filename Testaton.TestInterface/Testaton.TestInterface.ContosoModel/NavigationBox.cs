using Bumblebee.Implementation;
using Bumblebee.Interfaces;
using OpenQA.Selenium;
using Testaton.TestInterface.ContosoModel.Courses;
using Testaton.TestInterface.ContosoModel.Students;

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


        /// <summary>
        /// Gets access to home page.
        /// </summary>
        public IClickable<StudentsPage> Students => new Clickable<StudentsPage>(this, By.XPath("li[3]"));

        /// <summary>
        /// Gets access to home page.
        /// </summary>
        public IClickable<CoursesPage> Courses => new Clickable<CoursesPage>(this, By.XPath("li[4]"));
    }
}