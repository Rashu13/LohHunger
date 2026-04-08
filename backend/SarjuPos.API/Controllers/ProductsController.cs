using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SarjuPos.API.Data;
using SarjuPos.API.Models;
using SarjuPos.API.Services;

namespace SarjuPos.API.Controllers
{
    [Authorize]
    public class ProductsController : BaseController<Product>
    {
        private readonly IImageService _imageService;

        public ProductsController(IRepository<Product> repository, IImageService imageService) : base(repository)
        {
            _imageService = imageService;
        }

        [HttpPost("{id}/image")]
        public async Task<IActionResult> UploadImage(int id, IFormFile file)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null) return NotFound();

            if (file != null)
            {
                _imageService.DeleteImage(product.ImagePath); // Delete old
                product.ImagePath = await _imageService.SaveImageAsync(file, "products");
                _repository.Update(product);
                await _repository.SaveChangesAsync();
            }

            return Ok(new { product.ImagePath });
        }
    }

    [Authorize]
    public class CategoriesController : BaseController<Category>
    {
        public CategoriesController(IRepository<Category> repository) : base(repository)
        {
        }
    }

    [Authorize]
    public class TablesController : BaseController<RestaurantTable>
    {
        public TablesController(IRepository<RestaurantTable> repository) : base(repository)
        {
        }
    }

    [Authorize]
    public class CustomersController : BaseController<Customer>
    {
        public CustomersController(IRepository<Customer> repository) : base(repository)
        {
        }
    }

    [Authorize]
    public class OrdersController : BaseController<Order>
    {
        public OrdersController(IRepository<Order> repository) : base(repository)
        {
        }
    }
}
