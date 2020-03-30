using System.Linq;
using Bumblebee.Setup;
using OpenQA.Selenium;
using Testaton.TestInterface.ContosoModel.Common;

namespace Testaton.TestInterface.ContosoModel.Courses
{
	public class CourseDeletePage : DeletePage<CoursesPage>
	{
		public CourseDeletePage(Session session) : base(session)
		{
			var values = FindElements(By.TagName("dd")).ToArray();
			Number = int.Parse(values[0].Text);
			Title = values[1].Text;
			Credits = int.Parse(values[2].Text);
			Department = values[3].Text;
		}

		public int Number { get; }

		public string Title { get; }

		public int Credits { get; }

		public string Department { get; }
	}
}