using Bumblebee.Implementation;
using Bumblebee.Interfaces;
using Bumblebee.Setup;
using OpenQA.Selenium;
using Testaton.TestInterface.ContosoModel.Common;

namespace Testaton.TestInterface.ContosoModel.Students
{
	public class StudentEditPage : EditPage<StudentsPage>
	{
		public StudentEditPage(Session session) : base(session)
		{
			var xPath = By.XPath("//form//input[@class='form-control']");
			LastName = new TextField<StudentCreatePage>(this, By.Ordinal(xPath, 0));
			FirstName = new TextField<StudentCreatePage>(this, By.Ordinal(xPath, 1));
			EnrollmentDate = new DateField<StudentCreatePage>(this, By.Ordinal(xPath, 2));
		}
		
		public ITextField<StudentCreatePage> LastName { get; }

		public ITextField<StudentCreatePage> FirstName { get; }

		public IDateField<StudentCreatePage> EnrollmentDate { get; }
	}
}