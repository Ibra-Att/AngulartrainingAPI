using AngulartrainingAPI.DataContext;
using AngulartrainingAPI.DTO.Order;
using AngulartrainingAPI.DTO.OrderProduct;
using AngulartrainingAPI.DTO.Product;
using AngulartrainingAPI.Interfaces;
using AngulartrainingAPI.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static AngulartrainingAPI.Helper.Enum.Enums;

namespace AngulartrainingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly MyDbContext _context;

        public MainController(MyDbContext context)
        {
            _context = context;
        }



        //Product

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAllProduct()
        {
            try
            {
                var query = from p in _context.Products
                            where p.IsDeleted == false
                            select new ProductCardDTO
                            {
                                Id = p.Id,
                                Name = p.Name,
                                Price = p.Price,
                                ProductImage = p.ProductImage,
                            };
                var result = await query.ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Something went wrong{ex.Message}");

            }
        }


        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetProductById([FromRoute]int id)
        {
            try
            {
                //var result=await _context.Products.SingleOrDefaultAsync(x=>x.Id == id);

                var query = from p in _context.Products
                            where p.Id == id
                            select new ProductDetailDTO
                            {
                                Name = p.Name,
                                Price = p.Price,
                                ProductImage = p.ProductImage,
                                Description = p.Description,
                                CreationDate = DateTime.Now,
                            };
                
                var result = await query.SingleOrDefaultAsync();
                if (result != null)
                    return Ok(result);
                else throw new Exception("No product found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Something went wrong{ex.Message}");

            }
        }


        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateProduct(UpdateProduct dto)
        {        

            try
            {
                var query = from p in _context.Products
                            where p.Id == dto.Id
                            select p;
                var product = await query.SingleOrDefaultAsync();
                if (product != null)
                {
                    product.Name = dto.Name ?? product.Name;
                    product.Description = dto.Description ?? product.Description;
                    product.Price = dto.Price;
                    product.ProductImage = dto.ProductImage;

                }
                _context.Update(product);
                await _context.SaveChangesAsync();
                return StatusCode(201, "Updated successfully");
            }
            catch (Exception ex)
            {
              
                return StatusCode(500, $"Something went wrong{ex.Message}");
            }
        }

        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task<IActionResult> DeleteProductById(int id)
        {
            try
            {
                var query = from p in _context.Products
                            where p.Id == id
                            select p;
                var product = await query.SingleOrDefaultAsync();
                if (product != null)
                {
                    product.IsDeleted = true;
                }
                _context.Update(product);
                await _context.SaveChangesAsync();
                return StatusCode(200, "Successfully deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Something went wrong{ex.Message}");
            }
        }




        //Order

     

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAllOrders()
        {
            try {
                var query = from o in _context.Orders
                            where o.IsDeleted == false
                            select new OrderCardDTO
                            {
                                Id = o.Id,
                                TotalPrice = o.TotalPrice,
                                UserId = o.UserId,
                                Status = o.Status,                                
                            };
                var result= await query.ToListAsync();
                return Ok(result);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Something went wrong{ex.Message}");
            }
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetOrderById([FromRoute] int id)
        {
            try
            {
                var query = from o in _context.Orders
                            where o.Id == id
                            select new OrderDetailDTO
                            {
                                Note = o.Note,
                                Status = o.Status,
                                UserId = o.UserId,
                                TotalPrice = o.TotalPrice,
                                CreationDate = o.CreationDate
                            };
                var order = await query.SingleOrDefaultAsync();
                return Ok(order);
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"Something went wrong{ex.Message}");

            }
        }
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateOrder(UpdateOrderDTO dto)
        {
            try
            {
                var query = from o in _context.Orders
                            where o.Id == dto.Id
                            select o;
                var order = await query.SingleOrDefaultAsync();
                if (order != null)
                {
                    order.Note = dto.Note;
                    order.Status = dto.Status;
                }
                _context.Update(order);
                await _context.SaveChangesAsync();
                return StatusCode(200,"updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Something went wrong{ex.Message}");

            }
        }

        [HttpPost]
        [Route("[action]/{id}")]
        public async Task<IActionResult> CreateOrder(CreateOrderDTO dto)
        {
            try {
                var order = new Order()
                {
                    Note = dto.Note,
                    UserId = dto.UserId,
                    Status = OrderStatus.NONE,
                    CreationDate= DateTime.Now,
                    IsDeleted = false,
                };
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                return StatusCode(201, "created successfully");
            }

            catch (Exception ex) {
                return StatusCode(500, $"Something went wrong{ex.Message}");
            }
        }


        //OrderProduct
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAllOrderProducts()
        {
            try
            {
                var query = from op in _context.ProductsProducts
                            where op.IsDeleted == false
                            select new OrderProductDTO
                            {
                               ProductId=op.ProductId,
                               OrderId=op.OrderId,
                               ProductPrice=op.ProductPrice,
                            };
                var result = await query.ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Something went wrong{ex.Message}");

            }
        }





        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetOrderProductByOrderID([FromRoute]int orderId)
        {
            try
            {
                var query = from op in _context.ProductsProducts
                            where op.OrderId == orderId
                            select new OrderProductDetailsDTO
                            {
                                ProductId = op.ProductId,
                                OrderId = op.OrderId,
                                ProductPrice = op.ProductPrice,

                            };
                var result = await query.SingleOrDefaultAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Something went wrong{ex.Message}");
            }
        }

       

       

     
    }
}
