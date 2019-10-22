using System;
using System.Collections.Generic;
using System.Text;

namespace ZTunnel.Pmms.Model.ViewModel
{
    public class ResultModel<T>
    {
        /// <summary>
        /// 操作是否成功
        /// </summary>
        public bool Success { get; set; } = true;
        /// <summary>
        /// 返回信息
        /// </summary>
        public string Msg { get; set; } 
        /// <summary>
        /// 返回数据集合
        /// </summary>
        public T Data { get; set; }
    }
}
