﻿using Azure;
using Agenda.Domain.Entity;
using Agenda.Domain.Interface;
using Agenda.Domain.Models;
using Agenda.Domain.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Controller;
using System.Diagnostics;
using System.IO.Pipelines;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Agenda.Web.Helpers
{
    public class ExceptionLog : ExceptionFilterAttribute, IActionFilter
    {
        private string ControllerName;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITempDataDictionaryFactory _tempDataDictionaryFactory;

        public ExceptionLog(IConfiguration configuration, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, ITempDataDictionaryFactory tempDataDictionaryFactory)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _tempDataDictionaryFactory = tempDataDictionaryFactory;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }


        public void OnActionExecuting(ActionExecutingContext context)
        {
            this.ControllerName = context.Controller?.ToString().Split(".").Last();
        }

        public override void OnException(ExceptionContext context)
        {
            var ex = context.Exception;

            if (Convert.ToBoolean(_configuration.GetSection(Constants.SYSTEM_SETTINGS)[Constants.SYSTEM_SETTINGS_REGISTERERRORLOG]))
            {
                LogError log = new LogError();

                var stringBuilder = new StringBuilder();
                stringBuilder.Append("{");

                foreach (var item in context.ModelState.Keys)
                {
                    stringBuilder.Append($"{item}:{context.ModelState[item]?.RawValue} ,");
                }

                stringBuilder.Append("}");

                //log.Object = JsonSerializer.Serialize(ex.Data[Constants.SYSTEM_EXCEPTION_OBJ]);
                //log.Method = ex.TargetSite.DeclaringType?.FullName;
                log.Object = stringBuilder.ToString();
                log.Message = ex.Message.Length > 2000 ? ex.Message.Substring(0, 2000) : ex.Message;
                
                var s = new StackTrace(context.Exception);
                var r = s.GetFrame(0);
                log.Method = GetMethodName(r.GetMethod());

                if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                {
                    log.Username = _httpContextAccessor.HttpContext.User.Identity.Name;
                }

                _unitOfWork.ErrorLogs.Insert(log);
                _unitOfWork.ErrorLogs.Save();
                _unitOfWork.Dispose();
            }

            var tempData = _tempDataDictionaryFactory.GetTempData(context.HttpContext);

            context.ExceptionHandled = true;

            if (context.HttpContext.Request.Method == "POST")
            {
                context.Result = new JsonResult(new
                {
                    Success = false,
                    Message = Constants.SYSTEM_ERROR_MSG
                });
            }
            else if (context.HttpContext.Request.Method == "GET")
            {
                tempData[Constants.SYSTEM_ERROR_KEY] = Constants.SYSTEM_ERROR_MSG;
                context.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                    { "controller", "Home" },
                    { "action", "Index" }
                });
            }
        }

        private string GetMethodName(System.Reflection.MethodBase method)
        {
            string _methodName = method.DeclaringType.FullName;

            if (_methodName.Contains(">") || _methodName.Contains("<"))
            {
                _methodName = _methodName.Split('<', '>')[1];
            }
            else
            {
                _methodName = method.Name;
            }

            return _methodName;
        }
    }
}
