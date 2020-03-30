using System.Collections.Generic;

namespace Testaton.TestInterface.ContosoModel.Common
{
    public interface ITable<out TRow>
    {
	    IEnumerable<TRow> Rows { get; }
    }
}