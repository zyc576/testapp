using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ZTunnel.Pmms.Core.Excel;
using ZTunnel.Pmms.Core.Paged;
using ZTunnel.Pmms.Model.Entity;
using ZTunnel.Pmms.Model.ViewModel;
using ZTunnel.Pmms.Service.IService;

namespace ZTunnel.Pmms.Api.Controllers
{
    /// <summary>
    /// 隧道信息Api
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    public class TunnelInfoController : ControllerBase
    {
        private readonly ITunnelInfoService tunnelInfoService;
        public TunnelInfoController(ITunnelInfoService tunnelInfoService)
        {
            this.tunnelInfoService = tunnelInfoService;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ResultModel<string> Add(TunnelInfo model)
        {
            model.AddTime = DateTime.Now;
            model.IsDel = false;
            var result = new ResultModel<string>();
            result.Success = tunnelInfoService.Add(model);
            if (result.Success)
            {
                result.Msg = "添加成功";
            }
            else
            {
                result.Msg = "添加失败";
            }
            return result;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ResultModel<string> Modify(TunnelInfo model)
        {
            var result = new ResultModel<string>();
            result.Success = tunnelInfoService.Modify(model);
            if (result.Success)
            {
                result.Msg = "修改成功";
            }
            else
            {
                result.Msg = "修改失败";
            }
            return result;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ResultModel<string> Delete(string id)
        {
            var result = new ResultModel<string>();
            var model = tunnelInfoService.GetTunnelInfo(id);
            if (model != null)
            {
                model.IsDel = true;
                if (tunnelInfoService.Modify(model))
                {
                    result.Msg = "删除成功";
                }
                else
                {
                    result.Msg = "删除失败";
                }

            }
            else
            {
                result.Msg = "要删除的隧道信息不存在";
            }

            return result;
        }
        /// <summary>
        /// 根据主键获取隧道信息
        /// </summary>
        /// <param name="id"></param>
        [HttpGet]
        public ResultModel<TunnelInfo> GetTunnelInfo(string id)
        {
            var result = new ResultModel<TunnelInfo>();
            result.Data = tunnelInfoService.GetTunnelInfo(id);
            result.Success = true;
            return result;
        }
        /// <summary>
        /// 分页获取隧道信息
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public ResultModel<PagedList<TunnelInfo>> GetPagedList(int pageIndex=1, int pageSize=20)
        {
            var result = new ResultModel<PagedList<TunnelInfo>>();
            result.Data = tunnelInfoService.GetPagedList(pageIndex, pageSize);
            result.Success = true;
            return result;
        }
        /// <summary>
        /// 导出
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public FileResult DownloadTunnelInfo()
        {
            var columns = new Dictionary<string, string>() {
                               { "TunnelCode","隧道代码"},
                               {"EntranceStation","入口桩号" },
                               { "TunnelLength","隧道长度"},
                               { "TunnelHeight","隧道净高"},
                               { "HoleType","洞口形式"},
                               { "HighwayGrade","公路等级"},
                               { "RodeType","路面形式"},
                               { "TunnelVentilation","隧道通风"},
                               { "CompletionTime","竣工日期"},
                               { "ConstructionCompany","施工单位"},
                               { "DesignSpeed","设计时速"},
                               { "TunnelName","隧道名称"},
                               { "TunnelType","隧道分类"},
                               { "TunnelWidth","隧道净宽"},
                               { "AddTime","添加时间"}

              };
            var list = tunnelInfoService.GetPagedList(1, int.MaxValue).Data;
            var data = list.Select(x => new
            {
                x.TunnelCode,
                x.EntranceStation,
                x.TunnelLength,
                x.TunnelHeight,
                x.HoleType,
                x.HighwayGrade,
                x.RodeType,
                x.TunnelVentilation,
                x.CompletionTime,
                x.ConstructionCompany,
                x.DesignSpeed,
                x.TunnelName,
                TunnelType = x.TunnelType == 1 ? "特长隧道" : (x.TunnelType == 2 ? "长隧道" : (x.TunnelType == 3 ? "中隧道" : "短隧道")),
                x.TunnelWidth,
                x.AddTime
            }).ToList();
            string filename = DateTime.Now.ToString("yyyyMMdd") + ".xlsx";
            var fs = ExcelHelper.GetByteToExportExcel(data, columns, new List<string>(), "隧道信息", "");
            return File(fs, "application/vnd.android.package-archive", filename);
        }       
    }
}