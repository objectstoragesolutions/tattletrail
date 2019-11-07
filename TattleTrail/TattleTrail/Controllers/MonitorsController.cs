using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using TattleTrail.DAL;
using TattleTrail.Infrastructure.Factories;
using TattleTrail.Models;

namespace TattleTrail.Controllers {
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MonitorsController : ControllerBase, IMonitorReport {
        private readonly ILogger<MonitorsController> _logger;
        private readonly IRepository<Monitor> _repository;
        private readonly IBaseModelFactory<Monitor> _modelFactory;
        public MonitorsController(ILogger<MonitorsController> logger, IRepository<Monitor> repository, IBaseModelFactory<Monitor> modelFactory) {
            _logger = logger ?? throw new ArgumentNullException(nameof(MonitorsController));
            _repository = repository ?? throw new ArgumentNullException(nameof(MonitorsController));
            _modelFactory = modelFactory ?? throw new ArgumentNullException(nameof(MonitorsController));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMonitorAsync(String id) {
            try {
                var result = await _repository.GetMonitorAsync(id);
                if (result.Value.HasValue) {
                    return Ok(result.Value.ToString());
                }

                return NotFound();
            } catch (Exception ex) {
                _logger.LogError($"Something went wrong inside GetMonitorAsync function: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetMonitorsAsync() {
            try {
                var result = await _repository.GetAllMonitorsAsync();
                return Ok(JsonConvert.SerializeObject(result));
            } catch (Exception ex) {
                _logger.LogError($"Something went wrong inside GetMonitorsAsync function: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateMonitorAsync(Monitor monitor) {
            try {
                var result = await _repository.AddMonitorAsync(monitor);
                if (result.Value) {
                    return Ok();
                }
                return StatusCode(500, "Internal server error.");
            } catch (Exception ex) {
                _logger.LogError($"Something went wrong inside CreateMonitorAsync function: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMonitorAsync(String id) {
            try {
                var result = await _repository.DeleteMonitorAsync(id);
                if (result.Value) {
                    NoContent();
                }
                return NotFound();
            } catch (Exception ex) {
                _logger.LogError($"Something went wrong inside DeleteMonitorAsync function: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }

        }

        [HttpPost("{id}/report")]
        public async Task<ActionResult<string>> SetProcessStatus(String id, [FromBody] double minutes) {
            try {
                var monitor = await _repository.GetMonitorAsync(id);
                if (!monitor.Value.HasValue) {
                    return NotFound();
                }
                var lifeTime = TimeSpan.FromMinutes(minutes);
                var modelToAdd = _modelFactory.Create(Guid.Parse(id), monitor.Value.ToString(), lifeTime);
                var result = await _repository.AddMonitorAsync(modelToAdd);

                return Ok();
            } catch (Exception ex) {
                _logger.LogError($"Something went wrong inside SetProcessStatus function: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }

        }

    }
}