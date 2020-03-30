using Bumblebee.Implementation;
using Bumblebee.Interfaces;
using OpenQA.Selenium;

namespace Testaton.TestInterface.ContosoModel.Common
{
	public class ValidatedTextField<TResult> : TextField<TResult>, IValidatedTextField<TResult> where TResult : IBlock
	{
		private readonly IWebElement _container;

		public ValidatedTextField(IBlock parent, By @by) : base(parent,
			By.Function(ctx => parent.FindElement(by).FindElement(By.TagName("input"))))
		{
			_container = parent.FindElement(@by);
		}

		public bool IsValid => string.IsNullOrEmpty(_container.FindElement(By.TagName("span")).Text);
		public string ErrorText => _container.FindElement(By.TagName("span")).Text;
	}
}