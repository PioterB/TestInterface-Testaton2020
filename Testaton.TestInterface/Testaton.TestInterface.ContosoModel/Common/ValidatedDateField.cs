using Bumblebee.Implementation;
using Bumblebee.Interfaces;
using OpenQA.Selenium;

namespace Testaton.TestInterface.ContosoModel.Common
{
	public class ValidatedDateField<TResult> : DateField<TResult>, IValidatedDateField<TResult> where TResult : IBlock
	{
		private readonly IWebElement _container;

		public ValidatedDateField(IBlock parent, By @by) : base(parent,
			By.Function(ctx => parent.FindElement(by).FindElement(By.TagName("input"))))
		{
			_container = parent.FindElement(@by);
		}

		public bool IsValid => string.IsNullOrEmpty(_container.FindElement(By.TagName("span")).Text);
	}
}