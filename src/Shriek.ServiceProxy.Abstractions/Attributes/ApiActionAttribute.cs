﻿using System;
using System.Threading.Tasks;
using Shriek.ServiceProxy.Abstractions.Context;

namespace Shriek.ServiceProxy.Abstractions.Attributes
{
    /// <summary>
    /// 表示与Api方法关联的特性
    /// </summary>
    public abstract class ApiActionAttribute : Attribute
    {
        /// <summary>
        /// 执行前
        /// </summary>
        /// <param name="context">上下文</param>
        /// <returns></returns>
        public abstract Task BeforeRequestAsync(ApiActionContext context);
    }
}