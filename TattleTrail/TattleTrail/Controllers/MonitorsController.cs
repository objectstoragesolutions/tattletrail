using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using TattleTrail.DAL.Repository;
using TattleTrail.Infrastructure.EmailService;
using TattleTrail.Infrastructure.Factories;
using TattleTrail.Models;

namespace TattleTrail.Controllers {
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MonitorsController : ControllerBase {
        private readonly ILogger<MonitorsController> _logger;
        private readonly IMonitorRepository<MonitorProcess> _monitorRepository;
        private readonly ICheckInRepository<CheckIn> _checkInRepository;
        private readonly IMonitorModelFactory _monitorModelFactory;
        private readonly IEmailService _emailService;
        public MonitorsController(ILogger<MonitorsController> logger, 
            IMonitorModelFactory modelFactory, 
            IEmailService emailService, 
            IMonitorRepository<MonitorProcess> monitorRepository,
            ICheckInRepository<CheckIn> checkInRepository) {

            _logger = logger ?? throw new ArgumentNullException(nameof(MonitorsController));
            _monitorModelFactory = modelFactory ?? throw new ArgumentNullException(nameof(MonitorsController));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(EmailService));
            _monitorRepository = monitorRepository ?? throw new ArgumentNullException(nameof(MonitorRepository));
            _checkInRepository = checkInRepository ?? throw new ArgumentNullException(nameof(CheckInRepository));
        }

        [HttpGet]
        public async Task<IActionResult> GetMonitorsAsync() {
            try {
                var result = await _monitorRepository.GetAllAsync();
                return Ok(result);
            } catch (Exception ex) {
                _logger.LogError($"Something went wrong inside GetMonitorsAsync function: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMonitorAsync(Guid id) {
            try {
                var result = await _monitorRepository.GetAsync(id.ToString());
                if (result.Id.Equals(Guid.Empty)) {
                    return NotFound();
                }
                return Ok(result);

            } catch (Exception ex) {
                _logger.LogError($"Something went wrong inside GetMonitorAsync function: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateMonitorAsync(MonitorDetails details) { 
            try {
                var monitor = _monitorModelFactory.Create(details);
                var result = await _monitorRepository.CreateAsync(monitor);
                if (result) {
                    return Ok();
                }
                return StatusCode(500, "Internal server error.");
            } catch (Exception ex) {
                _logger.LogError($"Something went wrong inside CreateMonitorAsync function: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMonitorAsync(Guid id) {
            try {
                await _monitorRepository.DeleteAsync(id);
                return Ok();
            } catch (Exception ex) {
                _logger.LogError($"Something went wrong inside DeleteMonitorAsync function: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("{id}/checkin")]
        public async Task<IActionResult> GetMonitorStatusAsync(Guid id) {
            try {
                var monitor = await _monitorRepository.GetAsync(id.ToString());
                if (monitor.Id.Equals(Guid.Empty)) {
                    return NotFound($"Cant find monitor with an id:{id}");
                }

                var allCheckIns = await _checkInRepository.GetAllAsync();

                if (allCheckIns.Any(x => x.MonitorId.Equals(id))) {
                    return Ok();
                }
                await _emailService.SendEmailAsync(monitor.MonitorDetails.Subscribers, "Cant find check in for process", "Looks like your process goes off");
                return NotFound();
            } catch (Exception ex) {
                _logger.LogError($"Something went wrong inside SetProcessStatus function: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPost("{id}/checkin")]
        public async Task<IActionResult> PostMonitorStatusAsync(Guid id) {
            try {
                var monitor = await _monitorRepository.GetAsync(id.ToString());
                if (monitor.Id.Equals(Guid.Empty)) {
                    return NotFound($"Cant find monitor with an id:{id}");
                }
                await _checkInRepository.CreateAsync(id, monitor.MonitorDetails.IntervalTime);

                monitor.MonitorDetails.LastCheckIn = DateTime.UtcNow;

                var isUpdated = await _monitorRepository.CreateAsync(monitor);
                if (isUpdated) {
                    return Ok("Thank you! Updated! ");
                }

                return NotFound($"Cant update monitor with an id:{id}");

            } catch (Exception ex) {
                _logger.LogError($"Something went wrong inside SetProcessStatus function: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}