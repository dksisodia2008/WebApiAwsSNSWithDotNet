using Amazon.Runtime;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebApiAwsSNSWithDotNet.Core.Dtos;

namespace WebApiAwsSNSWithDotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        [HttpGet]
        [Route("GetEmployee")]
        public async Task<IActionResult> Get()
        {
            var employees = await GetEmployees();
            return Ok(employees);
        }

        [HttpPost]
        [Route("Produce")]
        public async Task Post(EmployeeDto employeeDto)
        {
            var credentials = new BasicAWSCredentials("<key>", "<Secret>"); // Not to use hardcoded value.
            var client = new AmazonSimpleNotificationServiceClient(credentials, Amazon.RegionEndpoint.AFSouth1);
            var request = new PublishRequest()
            {
                TopicArn = "arn:aws:sns:ap-south-1:339713119745:mysnstopic",
                Message = JsonSerializer.Serialize(employeeDto),
                Subject = "Employee Data"
            };
            //await client.PublishAsync(request);
            //OR
            var response = await client.PublishAsync(request);
        }

        private async Task<List<EmployeeDto>> GetEmployees()
        {
            List<EmployeeDto> employees = new List<EmployeeDto>()
            {
               new EmployeeDto() { Id=Guid.NewGuid(), FirstName="Niraj", LastName="Kumar"},
               new EmployeeDto() { Id=Guid.NewGuid(), FirstName="Dheeraj", LastName="Kumar"},
               new EmployeeDto() { Id=Guid.NewGuid(), FirstName="Suraj", LastName="Kumar"}
            };

            return employees;

        }

    }
}
