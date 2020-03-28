using System;
using Bumblebee.Interfaces;

namespace Testaton.TestInterface.ContosoTests.Tools
{
	public static class IDateFieldExtensions
	{
		public static DateTime DateTime(this IDateField field)
		{
			return System.DateTime.Parse(field.Text);
		}
	}
}