using Bumblebee.Implementation;
using Bumblebee.Interfaces;
using Bumblebee.Setup;
using Testaton.TestInterface.ContosoModel.Common;

namespace Testaton.TestInterface.ContosoModel.Students
{
	public class StudentsPage : ShellPage
	{
		public StudentsPage(Session session) : base(session)
		{
			Students = new Table<StudentRow>(this, By.TagName("table"));
			Create = new Clickable<StudentCreatePage>(this, By.Ordinal(By.XPath("div//a"), 0));
			Search = new SearchBox(this, By.TagName("form"));
		}

		public ITable<StudentRow> Students { get; }

		public IClickable<StudentCreatePage> Create { get; }

		public SearchBox Search { get; }
	}
}