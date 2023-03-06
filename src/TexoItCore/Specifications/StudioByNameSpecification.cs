using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexoIt.Core.Entities;

namespace TexoIt.Core.Specifications
{
    public class StudioByNameSpecification : Specification<Studio>
    {
        public StudioByNameSpecification(string name)
        {
            Query.Where(x => x.Name == name);
        }
    }
}
