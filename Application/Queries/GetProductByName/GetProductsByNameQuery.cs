using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.GetProductByName
{
    public class GetProductsByNameQuery : IQuery
    {
        public string Name { get; set; }
    }
}
