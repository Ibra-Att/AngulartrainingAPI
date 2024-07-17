using AngulartrainingAPI.DataContext;
using AngulartrainingAPI.DTO.Order;
using AngulartrainingAPI.DTO.OrderProduct;
using AngulartrainingAPI.DTO.Product;
using AngulartrainingAPI.Helper.Token;
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
        public async Task<IActionResult> GetAllProduct(/*[FromHeader] string token*/)
        {
            try
            {
                //if (TokenHelper.IsValidToken(token))
                //    { 
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
                //}
                //else { return Unauthorized(); }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Something went wrong{ex.Message}");

            }
        }


        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetProductById([FromRoute] int id)
        {
            try
            {

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
        public async Task<IActionResult> GetAllOrders([FromHeader] string token)
        {
            try {
                if (TokenHelper.IsValidToken(token))
                    { 
                var query = from o in _context.Orders
                            join op in _context.ProductsProducts
                            on o.Id equals op.Id
                            where o.IsDeleted == false
                            select new OrderCardDTO
                            {
                                Id = o.Id,
                                TotalPrice = _context.ProductsProducts.Where(ord => ord.OrderId == o.Id).Sum(x => x.Quantity * x.ProductPrice),
                                UserId = o.UserId,
                                Status = o.Status,
                            };
        var result = await query.ToListAsync();
                return Ok(result);
    }
    else { return Unauthorized();
}

}
            catch (Exception ex)
            {
                return StatusCode(500, $"Something went wrong{ex.Message}");
            }
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetOrderById([FromRoute] int id, [FromHeader] string token)
        {
            try
            {
                if (TokenHelper.IsValidToken(token))
                {
                    var query2 = from oP in _context.ProductsProducts
                                 join p in _context.Products
                                 on oP.ProductId equals p.Id
                                 where oP.OrderId == id
                                 select new ProductInfoDTO
                                 {
                                     ProductId = oP.ProductId,
                                     ProductName = p.Name,
                                     Quentity = oP.Quantity,
                                     ProductImage = p.ProductImage,
                                     UnitPrice = p.Price

                                 };
                    var result = await query2.ToListAsync();
                    var query = from o in _context.Orders
                                join op in _context.ProductsProducts
                                on o.Id equals op.Id
                                where o.Id == id
                                select new OrderDetailDTO
                                {
                                    Id = o.Id,
                                    Note = o.Note,
                                    Status = o.Status,
                                    UserId = o.UserId,
                                    TotalPrice = _context.ProductsProducts.Where(ord => ord.OrderId == o.Id).Sum(x => x.Quantity * x.ProductPrice),
                                    ListOfItems = result,
                                    CreationDate = o.CreationDate,


                                };

                    var order = await query.SingleOrDefaultAsync();


                    return Ok(order);
                }
                else { return Unauthorized(); }
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
        [Route("[action]")]
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
                return StatusCode(201, order.Id);
            }

            catch (Exception ex) {
                return StatusCode(500, $"Something went wrong{ex.Message}");
            }
        }



        //OrderProduct
        
        
        [HttpGet]
        [Route("[action]/{orderId}")]
        public async Task<IActionResult> GetAllOrderProducts(int orderId)
        {
            try
            {
                var query = from op in _context.ProductsProducts
                            join p in _context.Products
                            on op.ProductId equals p.Id
                            where op.IsDeleted == false
                            && op.OrderId == orderId
                            select new OrderProductDTO
                            {
                               ProductId=op.ProductId,
                               OrderId=op.OrderId,
                               ProductPrice=p.Price,
                            };
                var result = await query.ToListAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Something went wrong{ex.Message}");

            }
        }

        [HttpPost]
        [Route("[action]")]

        public async Task<IActionResult> CreateOrderProduct(CreateOrderProductDTO dto)
        {
            try
            {
                var ordPr = new OrderProduct()
                {
                    ProductId = dto.ProductId,
                    Quantity = dto.Quantity,
                    OrderId = dto.OrderId,                    
                    
                    CreationDate = DateTime.Now,
                    IsDeleted = false,
                };
                var selectedproduct = await _context.Products.SingleOrDefaultAsync(x => x.Id == dto.ProductId);
                ordPr.ProductPrice = selectedproduct.Price;
                await _context.ProductsProducts.AddAsync(ordPr);
                await _context.SaveChangesAsync();
                return StatusCode(201, "created successfully");
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"Something went wrong{ex.Message}");
            }
        }


        //[HttpGet]
        //[Route("[action]/{orderId}")]
        //public async Task<IActionResult> GetOrderProductByOrderID([FromRoute]int orderId)
        //{
        //    try
        //    {
        //        var query = from op in _context.ProductsProducts
        //                    join p in _context.Products
        //                    on op.ProductId equals p.Id
        //                    where op.OrderId == orderId
        //                    select new OrderProductDetailsDTO
        //                    {
        //                        ProductId = op.ProductId,
        //                        OrderId = op.OrderId,
        //                        ProductPrice = p.Price,

        //                    };
        //        var result = await query.SingleOrDefaultAsync();
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Something went wrong{ex.Message}");
        //    }
        //}






    }
}
