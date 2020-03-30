using Bumblebee.Interfaces;

namespace Testaton.TestInterface.ContosoModel.Common
{
	public interface IValidatedDateField<out TResult> : IDateField<TResult> where TResult : IBlock
	{
		bool IsValid { get; }
	}
}