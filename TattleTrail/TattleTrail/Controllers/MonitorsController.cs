﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
        private readonly IMonitorModelFactory _monitorModelFactory;
        public MonitorsController(ILogger<MonitorsController> logger, IRepository repository, IMonitorModelFactory modelFactory) {
            _logger = logger ?? throw new ArgumentNullException(nameof(MonitorsController));
            _repository = repository ?? throw new ArgumentNullException(nameof(MonitorsController));
            _monitorModelFactory = modelFactory ?? throw new ArgumentNullException(nameof(MonitorsController));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMonitorAsync(Guid id) {
            try {
                var result = await _repository.GetMonitorAsync(id);
                return Ok(result);

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
        public async Task<IActionResult> CreateMonitorAsync(MonitorDetails details) { 
            try {
                var monitor = _monitorModelFactory.Create(details.ProcessName, details.LifeTime, details.Subscribers);
                var result = await _repository.CreateMonitorAsync(monitor);
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
                await _repository.DeleteMonitorAsync(id);
                return Ok();
            } catch (Exception ex) {
                _logger.LogError($"Something went wrong inside DeleteMonitorAsync function: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPost("{id}/report")]
        public async Task<IActionResult> ReportProcessStatus(Guid id) {
            try {
                var monitor = await _repository.GetMonitorAsync(id);
                if (monitor.Id.Equals(Guid.Empty)) {
                    return NotFound();
                }
                return Ok(monitor);
            } catch (Exception ex) {
                _logger.LogError($"Something went wrong inside SetProcessStatus function: {ex.Message}");
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}