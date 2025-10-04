using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MES.Rastreabilidade.Api.DTOs;
using MES.Rastreabilidade.Core.Entities;
using MES.Rastreabilidade.Infrastructure.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace MES.Rastreabilidade.Api.Controllers
{
    [ApiController]
    public class ProductionOrderController : ControllerBase
    {
        public readonly AppDbContext _context;

        public ProductionOrderController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("ProductionOrder")]
        public IActionResult PostProductionOrder(CreateProductionOrderDTO orderDTO)
        {
            var newOrder = new ProductionOrder
            {
                OrderCode = orderDTO.OrderCode,
                QtyPlanned = orderDTO.QtyPlanned,
                ProductId = orderDTO.ProductId,
                Status = Core.Enums.ProductionOrderStatus.Planned,
                DateCreated = DateTime.UtcNow
            };

            _context.ProductionOrders.Add(newOrder);

            _context.SaveChanges();

            return Ok(newOrder);
        }

        [HttpPost("{orderId}/start-batch")]
        public IActionResult PostProductionOrderStartBatch(int orderId)
        {
            var productionOrder = _context.ProductionOrders.Find(orderId);

            if (productionOrder == null)
            {
                return NotFound("Ordem de produção não encontrada.");
            }

            if (productionOrder.Status != Core.Enums.ProductionOrderStatus.Planned)
            {
                return BadRequest($"A ordem {productionOrder.OrderCode} já foi iniciada ou está finalizada e não pode iniciar um novo lote.");
            }

            productionOrder.Status = Core.Enums.ProductionOrderStatus.OnGoing;

            var newBatch = new Batch
            {
                BatchCode = GenerateBatchCode(productionOrder),
                Status = Core.Enums.BatchStatus.InProgress,
                DateInitial = DateTime.UtcNow,
                ProductionOrderId  = productionOrder.Id,
            };

            _context.Batches.Add(newBatch);
            _context.SaveChanges();

            return Ok(newBatch);
        }

        private string GenerateBatchCode(ProductionOrder order) {
            return $"L-{DateTime.UtcNow:yyyyMMdd}-{order.OrderCode}";
        }

        [HttpGet("ProductionOrder/{id}")]
        public IActionResult GetProductionOrderById(int id)
        {
            var productionOrder = _context.ProductionOrders.Find(id);

            if (productionOrder == null)
            {
                return NotFound();
            }

            return Ok(productionOrder);
        }

        [HttpGet("ProductionOrder")]
        public IActionResult GetProductionOrder()
        {
            var productionOrder = _context.ProductionOrders.ToList();

            return Ok(productionOrder);
        }

        [HttpPut("ProductionOrder/{id}")]
        public IActionResult PutProductionOrder(int id, ProductionOrder productionOrder)
        {
            var productionOrderdb = _context.ProductionOrders.Find(id);

            if (productionOrderdb == null)
            {
                return NotFound();
            }

            productionOrderdb.QtyPlanned = productionOrder.QtyPlanned;
            productionOrderdb.Status = productionOrder.Status;

            _context.ProductionOrders.Update(productionOrderdb);
            _context.SaveChanges();

            return Ok(productionOrderdb);
        }

        [HttpDelete("ProductionOrder/{id}")]
        public IActionResult DeleteProductionOrder(int id)
        {
            var productionOrderdb = _context.ProductionOrders.Find(id);

            if (productionOrderdb == null)
            {
                return NotFound();
            }

            _context.ProductionOrders.Remove(productionOrderdb);
            _context.SaveChanges();

            return NoContent();
        }

    }
}