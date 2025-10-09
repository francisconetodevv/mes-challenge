using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MES.Rastreabilidade.Api.DTOs;
using MES.Rastreabilidade.Core.Entities;
using MES.Rastreabilidade.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MES.Rastreabilidade.Api.Controllers
{
    [ApiController]
    public class BatchesController : ControllerBase
    {
        public readonly AppDbContext _context;

        public BatchesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("{batchId}/avancar-etapa")]
        public async Task<IActionResult> PostBatchesSteps(int batchId, AdvanceBatchStepDTO advanceBatchStepDTO)
        {
            var batch = await _context.Batches.Include(b => b.RegistroDeEtapas)
                                              .FirstOrDefaultAsync(b => b.Id == batchId);
            if (batch == null)
            {
                return NotFound("Lote não encontrado no sistema.");
            }

            if (batch.Status != Core.Enums.BatchStatus.InProgress)
            {
                return BadRequest($"Este lote não está 'Em Processo' e não pode ser avançado.");
            }

            var actualStep = batch.RegistroDeEtapas.FirstOrDefault(r => r.DateFinal == null);
            if (actualStep != null)
            {
                actualStep.DateFinal = DateTime.UtcNow;
            }

            var newRegister = new RegistroDeEtapa
            {
                BatchId = batchId,
                EtapaDoProcessoId = advanceBatchStepDTO.NextStepId,
                DateInitial = DateTime.UtcNow,
                Operator = advanceBatchStepDTO.Operator,
                Observations = advanceBatchStepDTO.Observations
            };

            _context.RegistroDeEtapas.Add(newRegister);
            _context.SaveChanges();

            return Ok("Etapa avançada com sucesso.");
        }

        [HttpPost("{batchId}/finalizar")]
        public async Task<IActionResult> FinishBatch(int batchId, FinishBatchDTO finishBatchDTO)
        {
            var batch = await _context.Batches.Include(b => b.RegistroDeEtapas)
                                              .FirstOrDefaultAsync(b => b.Id == batchId);

            if (batch == null)
            {
                return NotFound("Lote não encontrado");
            }

            if (batch.Status != Core.Enums.BatchStatus.InProgress)
            {
                return BadRequest($"Este lote não está 'Em Processo' e não pode ser avançado.");
            }

            var lastStep = batch.RegistroDeEtapas.FirstOrDefault(r => r.DateFinal == null);
            if (lastStep != null)
            {
                lastStep.DateFinal = DateTime.UtcNow;
                lastStep.Observations = finishBatchDTO.Observations;
            }

            batch.Status = Core.Enums.BatchStatus.Completed;
            batch.DateFinal = DateTime.UtcNow;
            batch.QtyProduced = finishBatchDTO.QtyProduced;

            await _context.SaveChangesAsync();

            return Ok(batch);
        }

        [HttpGet("{batchId}/historico")]
        public async Task<IActionResult> GetBatchHistory(int batchId)
        {
            var batchWithHistory = await _context.Batches.Include(b => b.ProductionOrder)
                                                            .ThenInclude(po => po.Produto)
                                                         .Include(b => b.RegistroDeEtapas)
                                                            .ThenInclude(re => re.EtapaDoProcesso)
                                                         .FirstOrDefaultAsync(b => b.Id == batchId);

            if (batchWithHistory == null)
            {
                return NotFound("Lote não foi encontrando");
            }

            return Ok(batchWithHistory);
        }
    }
}