using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexoIt.Core.Entities;

namespace TexoIt.Core.Specifications
{
    public class ProducerByNameSpecification : Specification<Producer>
    {
        public ProducerByNameSpecification(string name)
        {
            Query.Where(x => x.Name == name);
        }
    }
}
