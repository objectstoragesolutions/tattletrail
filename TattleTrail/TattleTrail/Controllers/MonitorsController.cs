﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TattleTrail.DAL.Repository;
using TattleTrail.Infrastructure.Extensions;
using TattleTrail.Infrastructure.Factories;
using TattleTrail.Models;

namespace TattleTrail.Controllers {
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class MonitorsController : ControllerBase {
        private readonly ILogger<MonitorsController> _logger;
        private readonly IMonitorRepository<MonitorProcess> _monitorRepository;
        private readonly ICheckInRepository<CheckIn> _checkInRepository;
        private readonly IMonitorModelFactory _monitorModelFactory;
        public MonitorsController(ILogger<MonitorsController> logger,
            IMonitorModelFactory modelFactory,
            IMonitorRepository<MonitorProcess> monitorRepository,
            ICheckInRepository<CheckIn> checkInRepository) {

            _logger = logger ?? throw new ArgumentNullException(nameof(MonitorsController));
            _monitorModelFactory = modelFactory ?? throw new ArgumentNullException(nameof(MonitorsController));
            _monitorRepository = monitorRepository ?? throw new ArgumentNullException(nameof(MonitorRepository));
            _checkInRepository = checkInRepository ?? throw new ArgumentNullException(nameof(CheckInRepository));
        }

        [HttpGet]
        public async Task<IActionResult> GetMonitorsAsync() {
            try {
                var result = await _monitorRepository.GetAllAsync();
                return Ok(result);
            } catch (Exception ex) {
                _logger.LogError($"Something went wrong inside GetMonitorsAsync: {ex.Message}");
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
                _logger.LogError($"Something went wrong inside GetMonitorAsync: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateMonitorAsync(MonitorDetails details) {
            try {
                var monitor = _monitorModelFactory.Create(details);
                await _monitorRepository.CreateAsync(monitor);
                var result = monitor.GetResultJson(Request.Host, Request.Scheme, Request.Path);
                return Ok(result.ToString());
            } catch (Exception ex) {
                _logger.LogError($"Something went wrong inside CreateMonitorAsync: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMonitorAsync(Guid id, MonitorDetails details) {
            try {
                var monitor = await _monitorRepository.GetAsync(id.ToString());
                if (id.Equals(monitor.Id)) {
                    monitor.MonitorDetails.UpdateMonitorDetails(details);
                    await _monitorRepository.CreateAsync(monitor);
                    return NoContent();
                }

                return NotFound();
            } catch (Exception ex) {
                _logger.LogError($"Something went wrong inside UpdateMonitorAsync: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMonitorAsync(Guid id) {
            try {
                await _monitorRepository.DeleteAsync(id);
                return Ok();
            } catch (Exception ex) {
                _logger.LogError($"Something went wrong inside DeleteMonitorAsynс: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("{id}/checkin")]
        [AllowAnonymous]
        public async Task<IActionResult> PostMonitorStatusAsync(Guid id) {
            try {
                var monitor = await _monitorRepository.GetAsync(id.ToString());
                if (monitor.Id.Equals(Guid.Empty)) {
                    return NotFound($"Cant find monitor with an id:{id}");
                }
                await _checkInRepository.CreateAsync(id, monitor.MonitorDetails.IntervalTime);

                monitor.MonitorDetails.LastCheckIn = DateTime.UtcNow;
                monitor.MonitorDetails.IsDown = false;

                await _monitorRepository.CreateAsync(monitor);
                
                return Ok("Thank you! Updated! ");

            } catch (Exception ex) {
                _logger.LogError($"Something went wrong inside PostMonitorStatusAsync: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}