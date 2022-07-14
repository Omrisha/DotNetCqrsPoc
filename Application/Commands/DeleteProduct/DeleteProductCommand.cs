using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.DeleteProduct
{
    public class DeleteProductCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}
