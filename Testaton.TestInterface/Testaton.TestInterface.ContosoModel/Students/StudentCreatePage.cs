using Bumblebee.Setup;
using Testaton.TestInterface.ContosoModel.Common;

namespace Testaton.TestInterface.ContosoModel.Students
{
	public class StudentCreatePage : CreatePage<StudentsPage>
	{
		public StudentCreatePage(Session session) : base(session)
		{
			var xpath = By.XPath("//form//div");
			LastName = new ValidatedTextField<StudentCreatePage>(this, By.Ordinal(xpath, 0));
			FirstName = new ValidatedTextField<StudentCreatePage>(this, By.Ordinal(xpath, 1));
			EnrollmentDate = new ValidatedDateField<StudentCreatePage>(this, By.Ordinal(xpath, 2));
		}

		public IValidatedTextField<StudentCreatePage> LastName { get; }

		public IValidatedTextField<StudentCreatePage> FirstName { get; }

		public IValidatedDateField<StudentCreatePage> EnrollmentDate { get; }
	}
}