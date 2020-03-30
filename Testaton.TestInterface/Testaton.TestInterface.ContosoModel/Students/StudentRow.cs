using System;
using System.Linq;
using Bumblebee.Implementation;
using Bumblebee.Interfaces;
using OpenQA.Selenium;

namespace Testaton.TestInterface.ContosoModel.Students
{
	public class StudentRow : Block
	{
		public StudentRow(IBlock parent, By @by) : base(parent, @by)
		{
			ExtractProperties();
		}

		public DateTime EnrollmentDate { get; private set; }

		public string FirstName { get; private set; }

		public string LastName { get; private set; }

		public IClickable<StudentEditPage> Edit { get; private set; }

		public IClickable<StudentDetailsPage> Details { get; private set; }

		public IClickable<StudentDeletePage> Delete { get; private set; }

		private void ExtractProperties()
		{
			var columns = FindElements(By.TagName("td")).ToArray();
			LastName = columns[0].Text;
			FirstName = columns[1].Text;
			EnrollmentDate = DateTime.Parse(columns[2].Text);
			
			Edit = new Clickable<StudentEditPage>(this, By.Function(ctx => columns[3].FindElement(By.Ordinal(By.XPath("a"), 0))));
			Details = new Clickable<StudentDetailsPage>(this, By.Function(ctx => columns[3].FindElement(By.Ordinal(By.XPath("a"), 1))));
			Delete = new Clickable<StudentDeletePage>(this, By.Function(ctx => columns[3].FindElement(By.Ordinal(By.XPath("a"), 2))));
		}
	}
}