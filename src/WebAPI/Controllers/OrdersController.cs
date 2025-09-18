using Microsoft.AspNetCore.Mvc;
using Orders.Application.DTOs;
using Orders.Domain.Enums;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly CreateOrder _createOrder;
    private readonly UpdateOrderStatus _updateOrderStatus;

    public OrdersController(CreateOrder createOrder, UpdateOrderStatus updateOrderStatus)
    {
        _createOrder = createOrder;
        _updateOrderStatus = updateOrderStatus;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderRequest createOrderDTO)
    {
        var orderId = await _createOrder.Execute(createOrderDTO.CustomerId);
        return Ok(orderId);
    }

    [HttpPut("{id}/status")]
    public async Task<IActionResult> UpdateStatus([FromRoute] Guid id, [FromBody] UpdateOrderStatusRequest newStatus)
    {
        await _updateOrderStatus.Execute(id, newStatus.status);
        return NoContent();
    }
}
