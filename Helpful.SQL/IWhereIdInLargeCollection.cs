using System.Collections.Generic;

namespace Helpful.SQL
{
    public interface IWhereIdInLargeCollection<in TIdType>
    {
        string GenerateSQL(IEnumerable<TIdType> referenceData);
    }
}