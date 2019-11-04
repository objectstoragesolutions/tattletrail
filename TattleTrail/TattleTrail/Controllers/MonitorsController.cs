using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TattleTrail.DAL;
using TattleTrail.Models;

namespace TattleTrail.Controllers {
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MonitorsController : ControllerBase, IMonitorReport {
        private ILogger<MonitorsController> _logger;
        private IRepository<MonitorModel> _repository;
        public MonitorsController(ILogger<MonitorsController> logger, IRepository<MonitorModel> repository) {
            _logger = logger;
            _repository = repository;
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
                return Ok(result);
            } catch(Exception ex) {
                _logger.LogError($"Something went wrong inside GetMonitorsAsync function: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateMonitorAsync(MonitorModel monitor) {
            try {
                var result = await _repository.AddMonitorAsync(monitor, null);
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
        public async Task<ActionResult<string>> SetProcessStatus(String id, double minutes) {
            try {
                var monitor = await _repository.GetMonitorAsync(id);
                if (!monitor.Value.HasValue) {
                    return NotFound();
                }
                var lifeTime = TimeSpan.FromMinutes(minutes);
                var result = await _repository.AddMonitorAsync(new MonitorModel(id, monitor.Value.ToString()), lifeTime);

                return Ok();
            } catch (Exception ex) {
                _logger.LogError($"Something went wrong inside SetProcessStatus function: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
            
        }

    }
}