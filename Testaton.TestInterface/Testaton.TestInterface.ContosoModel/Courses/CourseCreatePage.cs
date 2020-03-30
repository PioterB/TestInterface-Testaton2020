using Bumblebee.Implementation;
using Bumblebee.Interfaces;
using Bumblebee.Setup;
using OpenQA.Selenium;
using Testaton.TestInterface.ContosoModel.Common;

namespace Testaton.TestInterface.ContosoModel.Courses
{
	public class CourseCreatePage : CreatePage<CoursesPage>
	{
		public CourseCreatePage(Session session) : base(session)
		{
			var xpath = By.XPath("//form//div");

Number = new ValidatedTextField<CoursesPage>(this, By.Ordinal(xpath, 0));
			Title = new TextField<CoursesPage>(this, By.Ordinal(xpath, 1));
			Credits = new ValidatedTextField<CoursesPage>(this, By.Ordinal(xpath, 2));
			Department = new SelectBox<CoursesPage>(this, By.Function(ctx => ctx.FindElement(By.Ordinal(xpath, 3)).FindElement(By.TagName("select"))));
		}

		public IValidatedTextField<CoursesPage> Number { get; }

		public ITextField<CoursesPage> Title { get; }

		public IValidatedTextField<CoursesPage> Credits { get; }

		public ISelectBox<CoursesPage> Department { get; }
	}
}