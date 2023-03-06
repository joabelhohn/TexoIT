using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexoIt.Core.Entities;

namespace TexoIt.Core.Specifications
{
    public class MovieWinnerSpecification : Specification<Movie>
    {
        public MovieWinnerSpecification()
        {
            Query
                .Include(p => p.Studios).ThenInclude(p => p.Studio)
                .Include(p => p.Producers).ThenInclude(p => p.Producer)
                .Where(p => p.Winner);
        }
    }
}
