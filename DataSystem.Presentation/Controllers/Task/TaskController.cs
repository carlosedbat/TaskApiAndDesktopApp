using DataSystem.Application.Task.Service;
using DataSystem.Shared.DTOs.Response;
using DataSystem.Shared.DTOs.Task;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DataSystem.Presentation.Controllers.Task
{

    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskServices _taskServices;
        private const int _statusCodeTokenExpired = 498;


        public TaskController(ITaskServices taskServices)
        {
            this._taskServices = taskServices;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TaskCreateDTO taskCreateDTO)
        {
            ServiceResponseDTO<TaskDTO> serviceResponseDTO = await this._taskServices.Create(taskCreateDTO);

            return this.StatusCode(serviceResponseDTO.StatusCode, serviceResponseDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Read(int id)
        {
            ServiceResponseDTO<TaskDTO> seviceResponseDTO = await this._taskServices.Read(id);

            return this.StatusCode(seviceResponseDTO.StatusCode, seviceResponseDTO);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] TaskUpdateDTO taskUpdateViewModel)
        {
            ServiceResponseDTO<TaskDTO> serviceResponseDTO = await this._taskServices.Update(taskUpdateViewModel);

            return this.StatusCode(serviceResponseDTO.StatusCode, serviceResponseDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            ServiceResponseDTO<bool> serviceResponseDTO = await this._taskServices.Delete(id);

            return this.StatusCode(serviceResponseDTO.StatusCode, serviceResponseDTO);
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] TaskListFilterDTO taskListFilterDTO)
        {
            ServiceListResponseDTO<TaskDTO> serviceResponseDTO = await this._taskServices.List(taskListFilterDTO);

            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(serviceResponseDTO.PaginationDTO));

            return this.StatusCode(serviceResponseDTO.ServiceResponseDTO.StatusCode, serviceResponseDTO);
        }
    }

}
