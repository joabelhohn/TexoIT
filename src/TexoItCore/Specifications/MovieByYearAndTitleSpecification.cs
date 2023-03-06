using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexoIt.Core.Entities;

namespace TexoIt.Core.Specifications
{
    public class MovieByYearAndTitleSpecification : Specification<Movie>
    {
        public MovieByYearAndTitleSpecification(int year, string? title)
        {
            Query
                .Include(p => p.Studios).ThenInclude(p => p.Studio)
                .Include(p => p.Producers).ThenInclude(p => p.Producer)
                .Where(x => 
                    (year == 0 || x.Year == year) && 
                    (title == null || x.Title.ToUpper().Contains(title.ToUpper())));
        }
    }
}
