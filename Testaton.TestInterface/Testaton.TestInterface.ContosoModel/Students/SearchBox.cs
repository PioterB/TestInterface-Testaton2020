using System.Linq;
using Bumblebee.Implementation;
using Bumblebee.Interfaces;
using Bumblebee.Setup;
using OpenQA.Selenium;

namespace Testaton.TestInterface.ContosoModel.Students
{
	public class SearchBox : Block
	{
		public SearchBox(IBlock parent, By by) : base(parent, by)
		{
		}

		public ITextField<SearchBox> Name => new TextField<SearchBox>(this, By.TagName("input"));

		public IClickable<StudentsPage> Search => new Clickable<StudentsPage>(this, By.TagName("input"));

		public IClickable<StudentsPage> BackToFullList => new Clickable<StudentsPage>(this, By.TagName("a"));

	}
}