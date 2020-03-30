using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Bumblebee.Setup;
using Bumblebee.Setup.DriverEnvironments;
using Testaton.TestInterface.ContosoTests.Tools;
using Testaton.TestInterface.ContosoModel;
using Testaton.TestInterface.ContosoModel.Students;

namespace Testaton.TestInterface.ContosoTests
{
	[TestClass]
	public class StudentsPageTests
	{
		private StudentsPage _pageUnderTest;
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

		[TestMethod]
		public void Add_UseExistingName_Added()
		{
			var nameToAdd = "Alexander";
			var beforeCount = _pageUnderTest.Students.Rows.Count(row => row.LastName == nameToAdd);
			var createPage = _pageUnderTest.Create.Click();
					   
			createPage.LastName.EnterText(nameToAdd);
			createPage.FirstName.EnterText("To");
			createPage.EnrollmentDate.EnterDate(DateTime.Now.Date);
			
			var students = createPage.Create.Click();

			var itemsCount = students.Students.Rows.Count(row => row.LastName == nameToAdd);
			Assert.AreEqual(beforeCount + 1, itemsCount);						
		}


		[TestMethod]
		public void Add_PassedEnrollementDate_ValidationMessage()
		{
			var nameToAdd = "Alexander";
			var beforeCount = _pageUnderTest.Students.Rows.Count(row => row.LastName == nameToAdd);
			var createPage = _pageUnderTest.Create.Click();

			createPage.LastName.EnterText(nameToAdd);
			createPage.FirstName.EnterText("To");
			createPage.EnrollmentDate.EnterDate(DateTime.Now.Date.AddDays(-5));

			var againCreatePage = createPage.Create.Click<StudentCreatePage>();
			var isValidDate = againCreatePage.EnrollmentDate.IsValid;

			Assert.IsFalse(isValidDate);
		}

		[TestMethod]
		public void Edit_StudentRow_Edited()
		{
			var postfix = "edit";
			var firstStudent = _pageUnderTest.Students.Rows.First();

			var editPage = firstStudent.Edit.Click();

			editPage.EnrollmentDate.EnterDate(firstStudent.EnrollmentDate.AddDays(1));
			editPage.FirstName.EnterText(firstStudent.FirstName + postfix);
			editPage.LastName.EnterText(firstStudent.LastName + postfix);
			var students = editPage.Save.Click();

			var editedStudent = students.Students.Rows.First();

			Assert.AreEqual(firstStudent.FirstName + postfix, editedStudent.FirstName);
			Assert.AreEqual(firstStudent.LastName + postfix, editedStudent.LastName);
			Assert.AreEqual(firstStudent.EnrollmentDate.AddDays(1), editedStudent.EnrollmentDate);
		}

		[TestMethod]
		public void Edit_Student_opened()
		{
			var nameToFind = "Pio";
			var rowToEdit = _pageUnderTest.Students.Rows.First(row => row.LastName == nameToFind);

			string name = rowToEdit.FirstName;
			string surname = rowToEdit.LastName;

			var editPage = rowToEdit.Edit.Click();

			Assert.IsTrue(editPage.LastName.Text.Equals(surname));
			Assert.IsTrue(editPage.FirstName.Text.Equals(name));
		}

		[TestMethod]
		public void Edit_StudentRow_Edited_ByPT()
		{
			var nameToFind = "Alexander";
			var nameToFill = "AUniquePio";

			var rowToEdit = _pageUnderTest.Students.Rows.First(row => row.LastName == nameToFind);
			var editPage = rowToEdit.Edit.Click();

			editPage.LastName.EnterText(nameToFill);
			editPage.FirstName.EnterText("Tr");
			editPage.EnrollmentDate.EnterDate(DateTime.Now.AddDays(10));
			editPage.Save.Click();

			var beforeCounter = _pageUnderTest.Students.Rows.Count(row => row.FirstName == nameToFind);

			var itemsCount = _pageUnderTest.Students.Rows.Count(row => row.LastName == nameToFill);
			Assert.AreEqual(beforeCounter + 1, itemsCount);
		}

		[TestMethod]
		public void Add_NoData_ValidationMessage()
		{
			var editPage = _pageUnderTest.Create.Click();

			var validated = editPage.Create.Click<StudentCreatePage>();

			Assert.IsFalse(validated.FirstName.IsValid);
			Assert.IsFalse(validated.LastName.IsValid);
			Assert.IsFalse(validated.EnrollmentDate.IsValid);
		}

		[TestMethod]
		public void Add_NoFirstName_ValidationMessage(string value)
		{
			var editPage = _pageUnderTest.Create.Click();

			editPage.LastName.EnterText(value);

			var validated = editPage.Create.Click<StudentCreatePage>();

			Assert.IsFalse(validated.FirstName.IsValid);
		}

		[TestMethod]
		public void Add_NoLastName_ValidationMessage()
		{
			var editPage = _pageUnderTest.Create.Click();

			var validated = editPage.Create.Click<StudentCreatePage>();

			Assert.IsFalse(validated.LastName.IsValid);
		}

		[TestMethod]
		public void Add_NoEnrollement_ValidationMessage()
		{
			var editPage = _pageUnderTest.Create.Click();

			var validated = editPage.Create.Click<StudentCreatePage>();

			Assert.IsFalse(validated.EnrollmentDate.IsValid);
		}

		[TestMethod]
		public void Edit_Name_Changed()
		{
			var firstNameToFind = "To";
			var rowToEdit = _pageUnderTest.Students.Rows.First(row => row.FirstName == firstNameToFind);

			var editPage = rowToEdit.Edit.Click();
			editPage.FirstName.AppendText("edited");
			editPage.Save.Click();
			var editedRow = _pageUnderTest.Students.Rows.FirstOrDefault(row => row.FirstName == String.Concat(firstNameToFind, "_edited"));

			Assert.IsNotNull(editedRow, "After edit process item is not present on list");
			Assert.AreEqual(String.Concat(firstNameToFind, "_edited"), editedRow.FirstName, "After edit process firtst name was not changed");
		}

		[TestMethod]
		public void Edit_Surname_Changed()
		{
			var firstNameToFind = "Alexander";
			var rowToEdit = _pageUnderTest.Students.Rows.First(row => row.LastName == firstNameToFind);

			var editPage = rowToEdit.Edit.Click();
			editPage.FirstName.AppendText("_edited");
			editPage.Save.Click();
			var editedRow = _pageUnderTest.Students.Rows.FirstOrDefault(row => row.LastName == String.Concat(firstNameToFind, "_edited"));

			Assert.IsNotNull(editedRow, "After edit process item is not present on list");
			Assert.AreEqual(String.Concat(firstNameToFind, "_edited"), editedRow.LastName, "After edit process firtst name was not changed");
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


		/*---- passed---*/

		[TestMethod]
		public void Add_ValidRequiredData_Added_x()
		{
			var createPage = _pageUnderTest.Create.Click();

			createPage.LastName.EnterText("Pio");
			createPage.FirstName.EnterText("Ter");
			createPage.EnrollmentDate.EnterDate(DateTime.Now.Date);
			var students = createPage.Create.Click();

			var added = students.Students.Rows.FirstOrDefault(row => row.LastName == "Pio");
			Assert.IsNotNull(added);
		}

		[TestMethod]
		public void Delete_FirstPosition_ConfirmPageCorrect()
		{
			var firstStudent = _pageUnderTest.Students.Rows.First();

			var confirmPage = firstStudent.Delete.Click();

			Assert.AreEqual(firstStudent.FirstName, confirmPage.FirstName);
			Assert.AreEqual(firstStudent.LastName, confirmPage.LastName);
			Assert.AreEqual(firstStudent.EnrollmentDate, confirmPage.EnrollmentDate);
		}

		[TestMethod]
		public void Delete_FirstPosition_Deleted()
		{
			var firstStudent = _pageUnderTest.Students.Rows.First();

			var confirmPage = firstStudent.Delete.Click();
			var studentsWithoutFirst = confirmPage.Delete.Click();

			var second = studentsWithoutFirst.Students.Rows.First();

			Assert.AreNotEqual(firstStudent.FirstName, second.FirstName);
		}

		[TestMethod]
		public void Edit_StudentRow_PrefieldForm()
		{
			var firstStudent = _pageUnderTest.Students.Rows.First();

			var editPage = firstStudent.Edit.Click();

			Assert.AreEqual(firstStudent.FirstName, editPage.FirstName.Text);
			Assert.AreEqual(firstStudent.LastName, editPage.LastName.Text);
			Assert.AreEqual(firstStudent.EnrollmentDate, editPage.EnrollmentDate.DateTime());
		}


		[TestMethod]
		public void Edit_StudentRow_Edited_x()
		{
			var postfix = "edit";
			var firstStudent = _pageUnderTest.Students.Rows.First();

			var editPage = firstStudent.Edit.Click();

			editPage.EnrollmentDate.EnterDate(firstStudent.EnrollmentDate.AddDays(1));
			editPage.FirstName.EnterText(firstStudent.FirstName + postfix);
			editPage.LastName.EnterText(firstStudent.LastName + postfix);
			var students = editPage.Save.Click();

			var editedStudent = students.Students.Rows.First();

			Assert.AreEqual(firstStudent.FirstName + postfix, editedStudent.FirstName);
			Assert.AreEqual(firstStudent.LastName + postfix, editedStudent.LastName);
			Assert.AreEqual(firstStudent.EnrollmentDate.AddDays(1), editedStudent.EnrollmentDate);
		}

		[TestMethod]
		public void CancelEdition_StudentRow_NoChanges()
		{
			var postfix = "edit";
			var firstStudent = _pageUnderTest.Students.Rows.First();

			var editPage = firstStudent.Edit.Click();

			editPage.FirstName.EnterText(firstStudent.FirstName + postfix);

			var students = editPage.BackToList.Click();

			var editedStudent = students.Students.Rows.First();

			Assert.AreEqual(firstStudent.FirstName, editedStudent.FirstName);
		}

		[TestMethod]
		public void CancelCreation_StudentRow_NoChanges()
		{
			var postfix = "edit";
			var firstStudent = _pageUnderTest.Students.Rows.First();

			var createPage = _pageUnderTest.Create.Click();

			createPage.FirstName.EnterText(firstStudent.FirstName + postfix);

			var students = createPage.BackToList.Click();

			var editedStudent = students.Students.Rows.First();

			Assert.AreEqual(firstStudent.FirstName, editedStudent.FirstName);
		}

		[TestMethod]
		public void Search_NameOfFirstStudent_Found()
		{
			var firstStudent = _pageUnderTest.Students.Rows.First();
			var search = _pageUnderTest.Search;
			search.Name.EnterText(firstStudent.FirstName);
			var results = search.Search.Click();

			var firstSearched = results.Students.Rows.First();

			Assert.AreEqual(firstStudent.FirstName, firstSearched.FirstName);
		}

		[TestMethod]
		public void ReturnFromSearch_FilteredList_FullList()
		{
			var itemsOnPage = _pageUnderTest.Students.Rows.Count();

			var filtered = _pageUnderTest.Search.Name.EnterText(_pageUnderTest.Students.Rows.First().FirstName).Search.Click();

			var full = filtered.Search.BackToFullList.Click();

			Assert.AreEqual(itemsOnPage, full.Students.Rows.Count());
		}

		[TestMethod]
		public void Create_MissingName_NameInvalid()
		{
			var newForm = _pageUnderTest.Create.Click();

			var saved = newForm.Create.Click<StudentCreatePage>();

			Assert.IsFalse(saved.LastName.IsValid);
		}

		[TestMethod]
		public void Create_DuplicatedName_Allowed()
		{
			var firstStudent = _pageUnderTest.Students.Rows.First();

			var createPage = _pageUnderTest.Create.Click();

			createPage.LastName.EnterText(firstStudent.LastName);
			createPage.FirstName.EnterText(firstStudent.FirstName);
			createPage.EnrollmentDate.EnterDate(DateTime.Now.Date);
			var students = createPage.Create.Click();

			var aleksandersCount = students.Students.Rows.Count(row => row.LastName == firstStudent.LastName);
			Assert.AreEqual(2, aleksandersCount);
		}

		[TestMethod]
		public void Edit_FirstItem_Works()
		{
			var byloWierszy = _pageUnderTest.Students.Rows.Count();
			var firstStudent = _pageUnderTest.Students.Rows.First();
			string newName = Guid.NewGuid().ToString();
			var editPage = firstStudent.Edit.Click();
			editPage.FirstName.EnterText(newName);

			StudentsPage changed = editPage.Save.Click();
			var jestWierszy = changed.Students.Rows.Count();
			var channedRow = changed.Students.Rows.FirstOrDefault(row => row.FirstName == newName);
			Assert.IsNotNull(channedRow);
			Assert.AreEqual(byloWierszy, jestWierszy);
		}
	}
}
