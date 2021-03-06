﻿using Shriek.Samples.WebApiProxy.Models;
using System.Threading.Tasks;
using Shriek.ServiceProxy.Abstractions.Attributes;
using Shriek.ServiceProxy.Http.ParameterAttributes;
using Shriek.ServiceProxy.Http.ActionAttributes;

namespace Shriek.Samples.WebApiProxy.Contracts
{
    [HttpHost("http://localhost:8080")]
    [Route("api")]
    public interface ITodoService
    {
        [HttpPost("todo")]
        Task<Todo> Create([JsonContent] Todo todo);

        [Route("route/{id:int}")]
        [HttpGet("route2/{id:int}")]
        Task<Todo> Get(int id);

        [HttpGet("{name}")]
        Task<Todo> Get(string name);
    }
}