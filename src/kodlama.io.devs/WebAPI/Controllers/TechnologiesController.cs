using Application.Features.Technologies.Commands.CreateTechnology;
using Application.Features.Technologies.Commands.DeleteTechnology;
using Application.Features.Technologies.Commands.UpdateTechnology;
using Application.Features.Technologies.Dtos;
using Application.Features.Technologies.Models;
using Application.Features.Technologies.Queries.GetByIdTechnology;
using Application.Features.Technologies.Queries.GetListTechnology;
using Core.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class TechnologiesController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTechnologyCommand command)
        {
            CreatedTechnologyDto result = await Mediator?.Send(command)!;
            return Created("", result);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListTechnologyQuery query = new(pageRequest);
            TechnologyListModel result = await Mediator?.Send(query)!;
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdTechnologyQuery query)
        {
            TechnologyGetByIdDto result = await Mediator?.Send(query)!;
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateTechnologyCommand command)
        {
            UpdatedTechnologyDto result = await Mediator?.Send(command)!;
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteTechnologyCommand command)
        {
            DeletedTechnologyDto result = await Mediator?.Send(command)!;
            return Ok(result);
        }
    }
}