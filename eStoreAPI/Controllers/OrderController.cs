using BussinessObject.Entities;
using DataAccess.Repositories;
using eStoreAPI.DTO.Order;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace eStoreAPI.Controllers;

[ApiController]
[Route("api/orders")]
public class OrderController : ControllerBase
{
    private readonly IOrderRepository _orderRepo = new OrderRepository();
    private readonly IProductRepository _productRepo = new ProductRepository();

    [HttpGet]
    public async Task<IActionResult> GetOrdersAsync([FromQuery] string? member_id)
    {
        List<Order> orders;

        if (!string.IsNullOrWhiteSpace(member_id))
        {
            orders = await _orderRepo.GetOrdersByMemberIdAsync(member_id);
        }
        else
        {
            orders = await _orderRepo.GetOrderAsync();
        }

        var orderDTOs = orders.Adapt<List<OrderDTO>>();

        return Ok(orderDTOs);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetOrderByIdAsync(int id)
    {
        var order = await _orderRepo.GetOrderByIdAsync(id);
        if (order == null)
        {
            return NotFound($"Order with ID {id} not found");
        }

        var orderDTO = order.Adapt<OrderWithDetailDTO>();
        return Ok(orderDTO);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrderAsync([FromBody] CreateOrderDTO orderDTO)
    {
        if (orderDTO == null)
        {
            return BadRequest("Order data is required.");
        }

        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).Distinct().ToList();
            var errorMessage = string.Join("\n", errors);
            return BadRequest(errorMessage);
        }

        var productIds = orderDTO.OrderDetails.Select(od => od.ProductId!.Value).Distinct().ToList();
        if (productIds.Count != orderDTO.OrderDetails.Count)
        {
            return BadRequest("Duplicate products are not allowed in order details.");
        }

        foreach (var detail in orderDTO.OrderDetails)
        {
            var product = await _productRepo.GetProductByIdAsync(detail.ProductId!.Value);
            if (product == null)
            {
                return BadRequest($"Product with ID {detail.ProductId} not found.");
            }

            if (product.UnitsInStock < detail.Quantity!.Value)
            {
                return BadRequest($"Product '{product.Name}' has insufficient stock. Available: {product.UnitsInStock}, Requested: {detail.Quantity}");
            }
        }

        var order = orderDTO.Adapt<Order>();
        order.OrderDate = DateTime.UtcNow;

        await _orderRepo.AddOrderAsync(order);

        foreach (var detail in orderDTO.OrderDetails)
        {
            var product = await _productRepo.GetProductByIdAsync(detail.ProductId!.Value);
            product!.UnitsInStock -= detail.Quantity!.Value;
            await _productRepo.UpdateProductAsync(product);
        }

        return Created(nameof(GetOrderByIdAsync), new { id = order.Id });
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> PatchOrderAsync(int id, [FromBody] PatchOrderDTO orderDTO)
    {
        if (orderDTO == null)
        {
            return BadRequest("Order data is required");
        }

        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).Distinct().ToList();
            var errorMessage = string.Join("\n", errors);
            return BadRequest(errorMessage);
        }

        var existingOrder = await _orderRepo.GetOrderByIdAsync(id);
        if (existingOrder == null)
        {
            return NotFound($"Order with ID {id} not found");
        }

        if (orderDTO.RequiredDate.HasValue)
        {
            existingOrder.RequiredDate = orderDTO.RequiredDate;
        }
        if (orderDTO.ShippedDate.HasValue)
        {
            existingOrder.ShippedDate = orderDTO.ShippedDate;
        }
        if (orderDTO.Freight.HasValue)
        {
            existingOrder.Freight = orderDTO.Freight.Value;
        }

        await _orderRepo.UpdateOrderAsync(existingOrder);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteOrderAsync(int id)
    {
        var existingOrder = await _orderRepo.GetOrderByIdAsync(id);
        if (existingOrder == null)
        {
            return NotFound($"Order with {id} not found");
        }

        await _orderRepo.DeleteOrderAsync(id);
        return NoContent();
    }
}
