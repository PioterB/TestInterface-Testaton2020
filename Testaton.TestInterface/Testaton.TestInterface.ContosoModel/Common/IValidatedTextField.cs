using Bumblebee.Interfaces;

namespace Testaton.TestInterface.ContosoModel.Common
{
	public interface IValidatedTextField<out TResult> : ITextField<TResult> where TResult: IBlock
	{ 
		bool IsValid { get; }
		string ErrorText { get; }
	}
}