using Motivision.Core.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motivision.Core.Business.Specifications.GoalSpecs
{
    public class GoalWithParamsSpecification : BaseSpecifications<Goal>
    {
        public GoalWithParamsSpecification(GoalSpecParams specParams)
            : base(g =>
                (string.IsNullOrEmpty(specParams.UserId) || g.UserId == specParams.UserId) &&
                (!specParams.IsCompleted.HasValue || g.IsCompleted == specParams.IsCompleted)
            )
        {
            // Includes
            AddInclude(g => g.Steps);

            // Sorting
            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                switch (specParams.Sort.ToLower())
                {
                    case "titleasc":
                        AddOrderBy(g => g.Title);
                        break;
                    case "titledesc":
                        AddOrderByDesc(g => g.Title);
                        break;
                    case "createdasc":
                        AddOrderBy(g => g.CreatedAt);
                        break;
                    case "createddesc":
                        AddOrderByDesc(g => g.CreatedAt);
                        break;
                    default:
                        AddOrderByDesc(g => g.CreatedAt);
                        break;
                }
            }
            else
            {
                AddOrderByDesc(g => g.CreatedAt);
            }

            // Pagination
            ApplyPagination(
                (specParams.PageIndex - 1) * specParams.PageSize,
                specParams.PageSize
            );
        }
    }


}
