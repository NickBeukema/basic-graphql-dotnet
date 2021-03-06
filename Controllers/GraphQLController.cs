﻿using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;
using StarWars.Api.Models;
using System.Threading.Tasks;

namespace StarWars.Api.Controllers
{
    [Route("graphql")]
    public class GraphQLController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GraphQLQuery query)
        {
            var schema = new Schema { Query = new StarWarsQuery() };

            var executionOptions = new ExecutionOptions { Schema = schema, Query = query.Query };
            var result = await new DocumentExecuter().ExecuteAsync(executionOptions).ConfigureAwait(false);

            if (result.Errors?.Count > 0)
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }
}