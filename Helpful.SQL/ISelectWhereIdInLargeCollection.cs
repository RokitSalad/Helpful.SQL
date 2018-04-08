using System.Collections.Generic;

namespace Helpful.SQL
{
    public interface ISelectWhereIdInLargeCollection<TIdType>
    {
        string GenerateSQL(IEnumerable<TIdType> referenceData);
    }
}