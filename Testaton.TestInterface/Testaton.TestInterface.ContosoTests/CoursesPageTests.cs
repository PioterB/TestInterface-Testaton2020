using Bumblebee.Setup;
using Bumblebee.Setup.DriverEnvironments;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testaton.TestInterface.ContosoModel;
using Testaton.TestInterface.ContosoModel.Courses;

namespace Testaton.TestInterface.ContosoTests
{
	[TestClass]
	public class CoursesPageTests
	{
		private CoursesPage _pageUnderTest;
		private static Session _browserWindow;

		[ClassInitialize]
		public static void OnTimeSetup(TestContext context)
		{
			var session = Threaded<Session>.With(new Chrome(TimeSpan.FromSeconds(10)));
			_browserWindow = session;
		}

		[TestInitialize]
		public void TestSetup()
		{
			var homePage = _browserWindow.NavigateTo<ShellPage>(Settings.HomeAddress);
			_pageUnderTest = homePage.Navigation.Courses.Click();
		}

		[TestMethod]
		public void Delete_ExistingItem_ConfirmationHasCorrectTitle()
		{
			var firstCourse = _pageUnderTest.Courses.Rows.First();
			
			var confirmationPage = firstCourse.Delete.Click();

			Assert.AreEqual(firstCourse.Title, confirmationPage.Title);
		}

		[TestMethod]
		public void Delete_ExistingItem_ConfirmationHasCorrectNumber()
		{
			var firstCourse = _pageUnderTest.Courses.Rows.First();

			var confirmationPage = firstCourse.Delete.Click();

			Assert.AreEqual(firstCourse.Number, confirmationPage.Number);
		}

		[TestMethod]
		public void Delete_ExistingItem_ConfirmationHasCorrectCerdits()
		{
			var firstCourse = _pageUnderTest.Courses.Rows.First();

			var confirmationPage = firstCourse.Delete.Click();

			Assert.AreEqual(firstCourse.Credits, confirmationPage.Credits);
		}

		[TestMethod]
		public void Delete_ExistingItem_ConfirmationHasCorrectDepartment()
		{
			var firstCourse = _pageUnderTest.Courses.Rows.First();

			var confirmationPage = firstCourse.Delete.Click();

			Assert.AreEqual(firstCourse.Department, confirmationPage.Department);
		}

		[TestMethod]
		public void Add_NoNumber_ValidationMessage()
		{
			var createPage = _pageUnderTest.Create.Click();

			var validate = createPage.Create.Click<CourseCreatePage>();

			Assert.IsFalse(validate.Number.IsValid);
		}

		[TestMethod]
		public void Add_NoCredits_ValidationMessage()
		{
			var createPage = _pageUnderTest.Create.Click();

			var validate = createPage.Create.Click<CourseCreatePage>();

			Assert.IsFalse(validate.Credits.IsValid);
		}

		[TestMethod]
		public void Add_ValidaData_Created()
		{
			var createPage = _pageUnderTest.Create.Click();
			createPage.Number.EnterText("99");
			createPage.Title.EnterText("9999");
			createPage.Credits.EnterText("5");
			createPage.Department.Options.Last().Click();

			var afterCreation = createPage.Create.Click();

			var newOne = afterCreation.Courses.Rows.FirstOrDefault(course => course.Number == 99);

			Assert.IsNotNull(newOne);
			Assert.AreEqual("9999", newOne.Title);
		}

		[TestCleanup]
		public void TestCleanup()
		{
		}

		[ClassCleanup]
		public static void OnTimeCleanup()
		{
			_browserWindow.End();
		}
	}
}
