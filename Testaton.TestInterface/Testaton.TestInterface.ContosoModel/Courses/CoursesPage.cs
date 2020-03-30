using System.Linq;
using Bumblebee.Implementation;
using Bumblebee.Interfaces;
using Bumblebee.Setup;
using OpenQA.Selenium;
using Testaton.TestInterface.ContosoModel.Common;

namespace Testaton.TestInterface.ContosoModel.Courses
{
	public class CoursesPage : Page
	{
		public CoursesPage(Session session) : base(session)
		{
		}

		public ITable<CourseRow> Courses => new Table<CourseRow>(this, By.TagName("table"));

		public IClickable<CourseCreatePage> Create =>
			new Clickable<CourseCreatePage>(this, By.XPath("div//a[1]"));
	}
}