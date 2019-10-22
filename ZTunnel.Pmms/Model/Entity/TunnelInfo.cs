using System;
using System.Collections.Generic;
using System.Text;

namespace ZTunnel.Pmms.Model.Entity
{
    /// <summary>
    /// 隧道信息表
    /// </summary>
    public class TunnelInfo
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 隧道代码
        /// </summary>
        public string TunnelCode { get; set; }
        /// <summary>
        /// 入口桩号
        /// </summary>
        public string EntranceStation { get; set; }
        /// <summary>
        /// 隧道长度
        /// </summary>
        public string TunnelLength { get; set; }
        /// <summary>
        /// 隧道净高
        /// </summary>
        public string TunnelHeight { get; set; }
        /// <summary>
        /// 洞口形式
        /// </summary>
        public string HoleType { get; set; }
        /// <summary>
        /// 公路等级
        /// </summary>
        public string HighwayGrade { get; set; }
        /// <summary>
        /// 路面形式
        /// </summary>
        public string RodeType { get; set; }
        /// <summary>
        /// 隧道通风
        /// </summary>
        public string TunnelVentilation { get; set; }
        /// <summary>
        /// 竣工日期
        /// </summary>
        public DateTime CompletionTime { get; set; }
        /// <summary>
        /// 施工单位
        /// </summary>
        public string ConstructionCompany { get; set; }
        /// <summary>
        /// 设计时速
        /// </summary>
        public string DesignSpeed { get; set; }
        /// <summary>
        /// 隧道名称
        /// </summary>
        public string TunnelName { get; set; }
        /// <summary>
        /// 隧道分类（1：特长隧道，2：长隧道，3：中隧道，4：短隧道）
        /// </summary>
        public int TunnelType { get; set; }
        /// <summary>
        /// 隧道净宽
        /// </summary>
        public string TunnelWidth { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public bool IsDel { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime { get; set; }
    }
}
