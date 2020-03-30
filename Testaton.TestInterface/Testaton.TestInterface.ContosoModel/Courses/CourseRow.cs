using System;
using System.Linq;
using Bumblebee.Implementation;
using Bumblebee.Interfaces;
using OpenQA.Selenium;

namespace Testaton.TestInterface.ContosoModel.Courses
{
	public class CourseRow : Block
	{
		public CourseRow(IBlock parent, By @by) : base(parent, @by)
		{
			ExtractProperties();
		}


		public int Number { get; private set; }
		public string Title { get; private set; }
		public int Credits { get; private set; }
		public string Department { get; private set; }
		
		public IClickable<CourseEditPage> Edit { get; private set; }

		public IClickable<CourseDetailsPage> Details { get; private set; }

		public IClickable<CourseDeletePage> Delete { get; private set; }

		private void ExtractProperties()
		{
			var columns = FindElements(By.TagName("td")).ToArray();
			Number = int.Parse(columns[0].Text);
			Title = columns[1].Text;
			Credits = int.Parse(columns[2].Text);
			Department = columns[3].Text;
			
			Edit = new Clickable<CourseEditPage>(this, By.XPath("a[1]"));
			Details = new Clickable<CourseDetailsPage>(this, By.XPath("a[2]"));
			Delete = new Clickable<CourseDeletePage>(this, By.XPath("a[3]"));
		}
	}
}