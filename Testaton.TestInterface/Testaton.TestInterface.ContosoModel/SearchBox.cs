using Bumblebee.Implementation;
using Bumblebee.Interfaces;
using OpenQA.Selenium;
using System;

namespace Testaton.TestInterface.ContosoModel
{
	public class SearchBox : Block
	{
		public SearchBox(IBlock parent, By by) : base(parent, by)
		{
		}

		public SearchBox(IBlock parent, By by, TimeSpan timeout) : base(parent, by, timeout)
		{
		}
	}
}