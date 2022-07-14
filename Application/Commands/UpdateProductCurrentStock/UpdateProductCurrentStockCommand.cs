using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.UpdateProductCurrentStock
{
    public class UpdateProductCurrentStockCommand : ICommand
    {
        public Guid Id { get; set; }
        public int CurrentStock { get; set; }
    }
}
