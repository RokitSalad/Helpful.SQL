using NUnit.Framework;

namespace Helpful.SQL.Test.Unit
{
    public class given_the_ids_are_integers
    {
        private string _sql;

        [OneTimeSetUp]
        protected void Given()
        {
            ISelectWhereIdInLargeCollection<int>  sqlBuilder = new SelectWhereIdInLargeCollection<int>("SELECT t.Col1, t.Col2, t.Col3 FROM Tbl t", "t.Col1", "int");
            _sql = sqlBuilder.GenerateSQL(new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
        }

        [Test]
        public void sql_is_produced()
        {
            Assert.IsNotEmpty(_sql);
        }
    }
}
