using Bidder.Application.Common.GraphQLData;
using Bidder.BidService.Application.Interfaces.Utils;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using System.Net;
namespace Bidder.BidService.Api.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Authorization.AllowAnonymous]
    [Route("[controller]")]
    public class BidGraphController : ControllerBase
    {
        private readonly IDocumentExecuter _documentExecuter;
        private readonly IDocumentWriter _documentWriter;
        private readonly ISchema _schema;

        public BidGraphController(IDocumentExecuter documentExecuter, IDocumentWriter documentWriter, ISchema schema)
        {
            _documentExecuter = documentExecuter;
            _documentWriter = documentWriter;
            _schema = schema;
        }

        [HttpPost]
        public async Task Post([FromBody] GraphqlQueryParameter query)
        {
            if (query == null) { throw new ArgumentNullException(nameof(query)); }

            var executionOptions = new ExecutionOptions
            {
                Schema = _schema,
                Query = query.Query, 
            };

            try
            {
                var result = await _documentExecuter.ExecuteAsync(executionOptions).ConfigureAwait(false);
                if (result.Errors?.Count > 0)
                {
                    await WriteResponseAsync(HttpContext, result);
                }

                await WriteResponseAsync(HttpContext, result);
            }
            catch (Exception ex)
            {
                BadRequest(ex);
            }
        }
        private async Task WriteResponseAsync(HttpContext context, ExecutionResult result)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = result.Errors?.Any() == true ? (int)HttpStatusCode.BadRequest : (int)HttpStatusCode.OK;

            await _documentWriter.WriteAsync(context.Response.Body, result);
        }

    }
}