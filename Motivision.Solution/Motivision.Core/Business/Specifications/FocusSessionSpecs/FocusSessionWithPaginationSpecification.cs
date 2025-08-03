using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motivision.Core.Business.Specifications.FocusSessionSpecs
{
    public class FocusSessionWithFiltersForPaginationSpecification : BaseSpecifications<FocusSession>
    {
        public FocusSessionWithFiltersForPaginationSpecification(string userId, int skip, int take, string? sort = null)
            : base(s => s.UserId == userId)
        {
            ApplyPagination(skip, take);

            // Sort dynamically
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort.ToLower())
                {
                    case "createdasc":
                        AddOrderBy(s => s.CreatedAt);
                        break;
                    case "createddesc":
                        AddOrderByDesc(s => s.CreatedAt);
                        break;
                    case "status":
                        AddOrderBy(s => s.SessionStatus);
                        break;
                    default:
                        AddOrderByDesc(s => s.CreatedAt); // default
                        break;
                }
            }
            else
            {
                AddOrderByDesc(s => s.CreatedAt); // default
            }
        }
    }

}
