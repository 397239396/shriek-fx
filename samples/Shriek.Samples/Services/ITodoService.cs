﻿using Shriek.Samples.Models;
using System.Threading.Tasks;
using Shriek.ServiceProxy.Abstractions.Attributes;
using Shriek.ServiceProxy.Http.ActionAttributes;
using Shriek.ServiceProxy.Http.ParameterAttributes;

namespace Shriek.Samples.Services
{
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