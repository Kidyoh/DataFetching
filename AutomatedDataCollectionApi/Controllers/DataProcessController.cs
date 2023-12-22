using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutomatedDataCollectionApi.Services;
using AutomatedDataCollectionApi.Models;
using AutomatedDataCollectionApi.Data;

namespace AutomatedDataCollectionApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataProcessController : ControllerBase
    {
        private readonly IDataProcessService _dataProcessService;

        public DataProcessController(IDataProcessService dataProcessService)
        {
            _dataProcessService = dataProcessService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UrlEntity>> GetEndpoints()
        {
            var endpoints = _dataProcessService.GetConfigFileEndPoints();
            return Ok(endpoints);
        }

        [HttpGet("parsed-data")]
        public ActionResult<IEnumerable<ParsedEntity>> GetParsedData()
        {
            var parsedData = _dataProcessService.GetParsedData();
            return Ok(parsedData);
        }

        [HttpPost("add")]
        public async Task<ActionResult<string>> AddEndpoints([FromBody] UrlEntity urlEntity)
        {
            await _dataProcessService.AddConfigFileEndPoints(urlEntity);
            return Ok("Endpoint added successfully");
        }

        [HttpPut("edit")]
        public async Task<ActionResult<string>> EditEndpoints([FromBody] UrlEntity urlEntity)
        {
            await _dataProcessService.EditConfigFileEndPoints(urlEntity);
            return Ok("Endpoint edited successfully");
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteEndpoint(int id)
        {
            try
            {
                await _dataProcessService.DeleteConfigFileEndPoints(id);
                return Ok("Endpoint deleted successfully");
            }
            catch (Exception ex)
            {
                // Handle errors and return appropriate response
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred");
            }
        }

        [HttpPost("run-python")]
        public async Task<IActionResult> RunPythonScript()
        {
            string result = await _dataProcessService.RunPythonScript();

            return Ok(result);
        }
    }
}