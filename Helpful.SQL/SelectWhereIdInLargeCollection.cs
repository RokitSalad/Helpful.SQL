using System;
using System.Collections.Generic;

namespace Helpful.SQL
{
    public class SelectWhereIdInLargeCollection<TIdType> : ISelectWhereIdInLargeCollection<TIdType>
    {
        private readonly string _baseSelectQuery;
        private readonly string _idIdentifier;
        private readonly string _idColumnSQLType;
        private readonly string _tempTableName = Guid.NewGuid().ToString().Replace("-", string.Empty);

        public SelectWhereIdInLargeCollection(string baseSelectQuery, string idIdentifier, string idColumnSQLType)
        {
            _baseSelectQuery = baseSelectQuery;
            _idIdentifier = idIdentifier;
            _idColumnSQLType = idColumnSQLType;
        }

        public string GenerateSQL(IEnumerable<TIdType> referenceData)
        {
            var insertSql = string.Empty;
            foreach (var ids in referenceData.ToPagedList(500))
            {
                insertSql +=
                    $" INSERT INTO #{_tempTableName} (Id) VALUES ('{string.Join("'),('", ids)}');";
            }

            var query =
                $@"IF OBJECT_ID('tempdb..#{_tempTableName}') IS NOT NULL DROP TABLE #{_tempTableName};
CREATE TABLE #{_tempTableName} (Id {_idColumnSQLType});

{insertSql}

{_baseSelectQuery} WHERE {_idIdentifier} IN (SELECT Id FROM #{_tempTableName});

IF OBJECT_ID('tempdb..#{_tempTableName}') IS NOT NULL DROP TABLE #{_tempTableName};";

            return query;
        }
    }
}
