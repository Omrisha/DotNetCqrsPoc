using Application;
using Application.Commands;
using Application.Commands.AddNewProductCommand;
using Application.Commands.DeleteProduct;
using Application.Commands.UpdateProductCurrentStock;
using Application.Commands.UpdateProductUnitPrice;
using Application.Common;
using Application.Interfaces;
using Application.Queries;
using Application.Queries.FindOutOfStockProduct;
using Application.Queries.GetProductByName;
using Microsoft.AspNetCore.Mvc;

namespace cqrs_poc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ApiController
    {
        private readonly ICommandHandler<AddNewProductCommand> _addNewProductCommandHandler;
        private readonly ICommandHandler<DeleteProductCommand> _deleteProductCommand;
        private readonly ICommandHandler<UpdateProductCurrentStockCommand> _updateProductCurrentStockCommand;
        private readonly ICommandHandler<UpdateProductUnitPriceCommand> _updateProductUnitPriceCommand;
        private readonly IQueryHandler<FindOutOfStockProductsQuery> _findOutOfStockProductsQueryHandler;
        private readonly IQueryHandler<GetProductsByNameQuery> _getProductsByNameQueryHandler;

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
        
        public ProductsController(ICommandHandler<AddNewProductCommand> addNewProductCommandHandler,
                                  ICommandHandler<DeleteProductCommand> deleteProductCommand,
                                  ICommandHandler<UpdateProductCurrentStockCommand> updateProductCurrentStockCommand,
                                  ICommandHandler<UpdateProductUnitPriceCommand> updateProductUnitPriceCommand,          
                                  IQueryHandler<FindOutOfStockProductsQuery> findOutOfStockProductsQueryHandler,
                                  IQueryHandler<GetProductsByNameQuery> getProductsByNameQueryHandler,
                                  IHttpContextAccessor httpContextAccessor)
        {
            _addNewProductCommandHandler = addNewProductCommandHandler;
            _deleteProductCommand = deleteProductCommand;
            _updateProductCurrentStockCommand = updateProductCurrentStockCommand;
            _updateProductUnitPriceCommand = updateProductUnitPriceCommand;
            _findOutOfStockProductsQueryHandler = findOutOfStockProductsQueryHandler;
            _getProductsByNameQueryHandler = getProductsByNameQueryHandler;

            _httpContextAccessor = httpContextAccessor;

            _commandDispatcher = new CommandDispatcher(_httpContextAccessor.HttpContext.RequestServices);
            _queryDispatcher = new QueryDispatcher(_httpContextAccessor.HttpContext.RequestServices);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetProductsByName(string name)
        {
            var result = await _queryDispatcher.Send(new GetProductsByNameQuery { Name = name }, CancellationToken.None);
            List<ProductDisplay> response = new List<ProductDisplay>();
            if (result.Count > 0)
            {
                foreach (var r in result)
                {
                    response.Add((ProductDisplay)r);

                }
            }
            return Ok(response);
        }

        [HttpGet()]
        public async Task<IActionResult> GetOutOfStockProducts()
        {
            var result = await _queryDispatcher.Send(new FindOutOfStockProductsQuery(), CancellationToken.None);
            List<ProductInventory> response = new List<ProductInventory>();
            if (result.Count > 0)
            {
                foreach (var r in result)
                {
                    response.Add((ProductInventory)r);

                }
            }
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddNewProductCommand command)
        {
            await _commandDispatcher.Send(command, CancellationToken.None);
            return NoContent();
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateProductCurrentStock(UpdateProductCurrentStockCommand command)
        {
            await _commandDispatcher.Send(command, CancellationToken.None);
            return NoContent();
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateProductUnitPrice(UpdateProductUnitPriceCommand command)
        {
            await _commandDispatcher.Send(command, CancellationToken.None);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteProductCommand command)
        {
            await _commandDispatcher.Send(command, CancellationToken.None);
            return NoContent();
        }
    }
}