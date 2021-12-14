using Application.Discount;
using Application.Dto;
using Discount.API.Controllers.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Discount.API.Controllers
{
    public class DiscountController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<DiscountDto>> Details(int id)
        {
            return await Mediator.Send(new Detail.Query { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Create.Command command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Update(int id, Update.Command command)
        {
            command.Id = id;
            return await Mediator.Send(command);
        }
    }
}