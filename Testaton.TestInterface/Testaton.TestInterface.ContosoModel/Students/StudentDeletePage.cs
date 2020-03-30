using System;
using System.Linq;
using Bumblebee.Setup;
using OpenQA.Selenium;
using Testaton.TestInterface.ContosoModel.Common;

namespace Testaton.TestInterface.ContosoModel.Students
{
	public class StudentDeletePage : DeletePage<StudentsPage>
	{
		public StudentDeletePage(Session session) : base(session)
		{
			var values = FindElements(By.TagName("dd")).ToArray();
			LastName = values[0].Text;
			FirstName = values[1].Text;
			EnrollmentDate = DateTime.Parse(values[2].Text);
		}

		public string LastName { get; }

		public string FirstName { get; }

		public DateTime EnrollmentDate { get; }
	}
}