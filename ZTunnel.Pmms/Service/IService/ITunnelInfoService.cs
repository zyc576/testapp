using System;
using System.Collections.Generic;
using System.Text;
using ZTunnel.Pmms.Core.Paged;
using ZTunnel.Pmms.Model.Entity;

namespace ZTunnel.Pmms.Service.IService
{
    public interface ITunnelInfoService
    {
        bool Add(TunnelInfo mode);
        bool Modify(TunnelInfo model);
        bool Delete(TunnelInfo model);
        TunnelInfo GetTunnelInfo(string key);
        PagedList<TunnelInfo> GetPagedList(int pageIndex, int pageSize);
    }
}
