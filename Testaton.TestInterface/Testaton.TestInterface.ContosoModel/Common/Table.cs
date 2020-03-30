using System.Collections.Generic;

using Bumblebee.Implementation;
using Bumblebee.Interfaces;
using OpenQA.Selenium;

namespace Testaton.TestInterface.ContosoModel.Common
{
    public class Table<TRow> : Table, ITable<TRow> where TRow : IBlock
    {
        public Table(IBlock parent, By @by) : base(parent, @by)
        {
        }

        public new IEnumerable<TRow> Rows => RowsAs<TRow>();
    }
}
