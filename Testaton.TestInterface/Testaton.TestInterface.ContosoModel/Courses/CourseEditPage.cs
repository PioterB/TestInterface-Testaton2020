using Bumblebee.Implementation;
using Bumblebee.Interfaces;
using Bumblebee.Setup;
using OpenQA.Selenium;
using Testaton.TestInterface.ContosoModel.Common;

namespace Testaton.TestInterface.ContosoModel.Courses
{
	public class CourseEditPage : EditPage<CoursesPage>
	{
		public CourseEditPage(Session session) : base(session)
		{
			var xpath = By.XPath("//form/div");

			Number = FindElement(By.Function( ctx => ctx.FindElement(By.Ordinal(xpath, 0)).FindElement(By.TagName("div")))).Text;
			Title = new TextField<CourseEditPage>(this, By.Function(ctx => ctx.FindElement(By.Ordinal(xpath, 1)).FindElement(By.TagName("input"))));
			Credits = new ValidatedTextField<CourseEditPage>(this, By.Ordinal(xpath, 2));
			Department = new SelectBox<CourseEditPage>(this, By.Function(ctx => ctx.FindElement(By.Ordinal(xpath, 3)).FindElement(By.TagName("select"))));
		}
		
		public string Number { get; }

		public ITextField<CourseEditPage> Title { get; }

		public IValidatedTextField<CourseEditPage> Credits { get; }

		public ISelectBox<CourseEditPage> Department { get; }
	}
}