using Application.Interfaces;

namespace Application.Common
{
    public class ProductInventory : IResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsOutOfStock { get; set; }
    }
}