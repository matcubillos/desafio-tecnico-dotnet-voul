using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaDotNetApi7.Dtos.PurchaseOrder;
using PruebaTecnicaDotNetApi7.Services;

namespace PruebaTecnicaDotNetApi7.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/purchase-order")]
    public class PurchaseOrderController : ControllerBase
    {
        private readonly PurchaseOrderService _purchaseOrderService;
        public PurchaseOrderController(PurchaseOrderService purchaseOrderService)
        {
            _purchaseOrderService = purchaseOrderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchaseOrderDto>>> GetPurchaseOrders()
        {
            try
            {
                var purchaseOrders = await _purchaseOrderService.GetAllPurchaseOrdersAsync();
                return Ok(purchaseOrders);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request");
            }
        }

        [HttpPost]
        public async Task<ActionResult<PurchaseOrderDto>> CreatePurchaseOrder(CreatePurchaseOrderDto createDto)
        {
            try
            {
                var createdOrder = await _purchaseOrderService.CreatePurchaseOrderAsync(createDto);
                return Ok(createdOrder);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PurchaseOrderDto>> GetPurchaseOrderById(int id)
        {
            try
            {
                var foundPurchaseOrder = await _purchaseOrderService.GetPurchaseOrderByIdAsync(id);
                if (foundPurchaseOrder == null)
                {
                    return NotFound($"Purchase order with Id: {id}, not found.");
                }
                return Ok(foundPurchaseOrder);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePurchaseOrder(int id, UpdatePurchaseDto updatePurchaseOrderDto)
        {
            try
            {
                var updatedPurchaseOrder = await _purchaseOrderService.UpdatePurchaseOrderAsync(id,updatePurchaseOrderDto);
                return Ok(updatedPurchaseOrder);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePurchaseOrder(int id)
        {
            try
            {
                var isDeleted = await _purchaseOrderService.DeletePurchaseOrderAsync(id);
                if (!isDeleted)
                {
                    return NotFound($"Purchase order with Id: {id}, not found.");
                }
                return Ok($"Purchase order with Id: {id}, deleted successfully.");
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request");
            }
        }
    }
}
