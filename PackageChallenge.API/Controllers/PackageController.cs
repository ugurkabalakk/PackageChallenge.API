using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Package.Challenge.Application.Package.Commands;

namespace PackageChallenge.API.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    public class PackageController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PackageController(IMediator mediator)
        {
            _mediator = mediator;
        }


        /// <summary>
        /// Set of items that you put into a package will provide a new row in the output string
        /// </summary>
        /// <param name="filePath"></param>
        /// <remarks>Sample request D:\\resources\\example_input</remarks>
        [HttpPost]
        [Route("Pack")]
        [Consumes("application/json")]
        public async Task<PackageCommandResponse> Pack([FromBody] PackageCommand packageCommand)
        {
            return await _mediator.Send(packageCommand);
        }
    }
}