using Bumblebee.Implementation;
using Bumblebee.Interfaces;
using Bumblebee.Setup;
using OpenQA.Selenium;

namespace Testaton.TestInterface.ContosoModel.Common
{
	public class EditPage<TSource> : Page where TSource : IBlock
	{
		public EditPage(Session session) : base(session)
		{
			var body = By.ClassName("body-content");

			Save = new Clickable<TSource>(this,
				By.Function(ctx => ctx.FindElement(body).FindElement(By.XPath(".//form//input[@type='submit']"))));
			BackToList = new Clickable<TSource>(this, By.Function(ctx => ctx.FindElement(body).FindElement(By.TagName("a"))));
		}

		public IClickable<TSource> Save { get; }

		public IClickable<TSource> BackToList { get; }
	}
}