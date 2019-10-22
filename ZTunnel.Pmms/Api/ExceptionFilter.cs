using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZTunnel.Pmms.Core.Log;
using ZTunnel.Pmms.Model.ViewModel;

namespace ZTunnel.Pmms.Api
{
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILoggerHelper loggerHelper;
        public ExceptionFilter(ILoggerHelper loggerHelper)
        {
            this.loggerHelper = loggerHelper;
        }
        public void OnException(ExceptionContext context)
        {
            string message = context.Exception.Message;
            var result = new ResultModel<string>();
            result.Success = false;
            result.Msg = "操作失败";
            context.Result = new ObjectResult(result);
            loggerHelper.Error(message,WriteLog(message,context.Exception));
            context.ExceptionHandled = true;
        }
        /// <summary>
        /// 自定义返回格式
        /// </summary>
        /// <param name="throwMsg"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        public string WriteLog(string throwMsg, Exception ex)
        {
            return string.Format("【自定义错误】：{0} \r\n【异常类型】：{1} \r\n【异常信息】：{2} \r\n【堆栈调用】：{3}", new object[] { throwMsg,
                ex.GetType().Name, ex.Message, ex.StackTrace });
        }
    }
}
