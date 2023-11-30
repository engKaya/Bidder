﻿using Bidder.Application.Common.GraphQLData;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
/*
namespace Bidder.BidService.Api.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Authorization.AllowAnonymous]
    [Route("[controller]")]
    public class BidGraphController : ControllerBase
    {
        private readonly IDocumentExecuter _documentExecuter;
        private readonly ISchema _schema;
        public BidGraphController(IDocumentExecuter documentExecuter, ISchema schema)
        {
            _documentExecuter = documentExecuter;
            _schema = schema;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GraphqlQueryParameter query)
        {
            if (query == null) { throw new ArgumentNullException(nameof(query)); }

            var executionOptions = new ExecutionOptions
            {
                Schema = _schema,
                Query = query.Query
            };

            try
            {
                var result = await _documentExecuter.ExecuteAsync(executionOptions).ConfigureAwait(false);

                if (result.Errors?.Count > 0)
                {
                    return BadRequest(result);
                }

                return Ok(result.Data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}*/