using Bumblebee.Implementation;
using Bumblebee.Interfaces;
using Bumblebee.Setup;
using OpenQA.Selenium;

namespace Testaton.TestInterface.ContosoModel.Common
{
	public class CreatePage<TSource> : Page where TSource : IBlock
	{
		public CreatePage(Session session) : base(session)
		{
			var body = By.ClassName("body-content");
			Create = new Clickable<TSource>(this, By.Function(ctx => ctx.FindElement(body).FindElement(By.XPath(".//form//input[@type='submit']"))));
			BackToList = new Clickable<TSource>(this, By.Function(ctx => ctx.FindElement(body).FindElement(By.TagName("a"))));
		}

		public IClickable<TSource> Create { get; }

		public IClickable<TSource> BackToList { get; }
	}
}