using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Motivision.Core.Business.Specifications
{
    public interface ISpecifications<T>
    {
        Expression<Func<T, bool>>? Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }

        Expression<Func<T, object>>? OrderBy { get; }
        Expression<Func<T, object>>? OrderByDesc { get; }

        int Skip { get; }
        int Take { get; }
        bool IsPaginationEnabled { get; }
    }
}
