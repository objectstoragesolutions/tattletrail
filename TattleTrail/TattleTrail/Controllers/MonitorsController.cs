using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TattleTrail.DAL;
using TattleTrail.Infrastructure.Factories;
using TattleTrail.Models;

namespace TattleTrail.Controllers {
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MonitorsController : ControllerBase {
        private readonly ILogger<MonitorsController> _logger;
        private readonly IRepository _repository;
        private readonly IBaseModelFactory<MonitorProcess> _modelFactory;
        public MonitorsController(ILogger<MonitorsController> logger, IRepository repository, IBaseModelFactory<MonitorProcess> modelFactory) {
            _logger = logger ?? throw new ArgumentNullException(nameof(MonitorsController));
            _repository = repository ?? throw new ArgumentNullException(nameof(MonitorsController));
            _modelFactory = modelFactory ?? throw new ArgumentNullException(nameof(MonitorsController));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMonitorAsync(String id) {
            try {
                var result = await _repository.GetMonitorAsync(Guid.Parse(id));
                if (result is null) {
                    return NotFound();
                }
                return Ok(result.ToString());

            } catch (Exception ex) {
                _logger.LogError($"Something went wrong inside GetMonitorAsync function: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetMonitorsAsync() {
            try {
                var result = await _repository.GetAllMonitors();
                return Ok(result);
            } catch (Exception ex) {
                _logger.LogError($"Something went wrong inside GetMonitorsAsync function: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateMonitorAsync(MonitorProcess monitor) {
            try {
                await _repository.AddMonitorAsync(monitor);
                return Ok();
            } catch (Exception ex) {
                _logger.LogError($"Something went wrong inside CreateMonitorAsync function: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMonitorAsync(String id) {
            try {
                await _repository.DeleteMonitorAsync(Guid.Parse(id));
                return Ok();
            } catch (Exception ex) {
                _logger.LogError($"Something went wrong inside DeleteMonitorAsync function: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }

        }

        [HttpPost("{id}/report")]
        public async Task<ActionResult<string>> SetProcessStatus(String id, [FromBody] double minutes) {
            try {
                var monitor = await _repository.GetMonitorAsync(Guid.Parse(id));
                if (monitor is null) {
                    return NotFound();
                }
                var lifeTime = TimeSpan.FromMinutes(minutes);

                //TODO: Rework
                //var modelToAdd = _modelFactory.Create(Guid.Parse(id), monitor.Value.ToString(), lifeTime);
                //var result = await _repository.AddMonitorAsync(modelToAdd);

                return Ok();
            } catch (Exception ex) {
                _logger.LogError($"Something went wrong inside SetProcessStatus function: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }

        }

    }
}