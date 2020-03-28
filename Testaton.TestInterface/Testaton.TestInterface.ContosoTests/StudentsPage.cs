using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Contoso.Model.Students;
using System.Linq;
using Bumblebee.Setup;
using Bumblebee.Setup.DriverEnvironments;
using Contoso.Model;
using Testaton.TestInterface.ContosoTests.Tools;

namespace Testaton.TestInterface.ContosoTests
{
	[TestClass]
	public class StudentsPageTests
	{
		private StudentsPage _pageUnderTest;
		private static Session _session;

		[ClassInitialize]
		public static void OnTimeSetup(TestContext context)
		{
			var session = Threaded<Session>.With(new Chrome(TimeSpan.FromSeconds(10)));
			_session = session;
		}

		[TestInitialize]
		public void TestSetup()
		{
			var homePage = _session.NavigateTo<ShellPage>(Settings.HomeAddress);
			_pageUnderTest = homePage.Navigation.Students.Click();
		}

		[TestMethod]
		public void Add_ValidRequiredData_Added()
		{
			var createPage = _pageUnderTest.Create.Click();

			createPage.LastName.EnterText("Pio");
			createPage.FirstName.EnterText("Ter");
			createPage.EnrollmentDate.EnterDate(DateTime.Now.Date);
			var students = createPage.Create.Click();

			var added = students.Students.Rows.FirstOrDefault(row => row.LastName == "Pio");
			Assert.IsNotNull(added);
		}

		[ClassCleanup]
		public static void OnTimeCleanup()
		{
			_session.End();
		}
	}
}
