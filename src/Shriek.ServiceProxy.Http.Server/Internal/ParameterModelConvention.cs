﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Linq;
using System.Reflection;
using Shriek.ServiceProxy.Http.ParameterAttributes;

namespace Shriek.ServiceProxy.Http.Server.Internal
{
    internal class ParameterModelConvention : IParameterModelConvention
    {
        public ParameterModelConvention(Type serviceType)
        {
            this.serviceType = serviceType;
        }

        private Type serviceType { get; }

        public void Apply(ParameterModel parameter)
        {
            if (!serviceType.IsAssignableFrom(parameter.Action.Controller.ControllerType)) return;

            var actionParams = parameter.Action.ActionMethod.GetParameters();

            var method = serviceType.GetMethods().FirstOrDefault(mth =>
            {
                var mthParams = mth.GetParameters();
                return parameter.Action.ActionMethod.Name == mth.Name
                       && actionParams.Length == mthParams.Length
                       && actionParams.Any(x => mthParams.Where(o => x.Name == o.Name).Any(o => x.GetType() == o.GetType()));
            });

            var theParam = method.GetParameters().FirstOrDefault(x => x.GetType() == parameter.ParameterInfo.GetType());

            if (theParam == null) return;

            var attrs = theParam.GetCustomAttributes();
            var paramAttrs = attrs.OfType<JsonContentAttribute>().Select(att => Activator.CreateInstance(typeof(FromBodyAttribute)));

            if (!paramAttrs.Any()) return;

            parameter.BindingInfo = BindingInfo.GetBindingInfo(paramAttrs);
        }
    }
}