using System;
using System.Collections.Generic;
using System.Text;
using ZTunnel.Pmms.Core.Paged;
using ZTunnel.Pmms.Model.Entity;
using ZTunnel.Pmms.Repository.IRepository;
using ZTunnel.Pmms.Service.IService;

namespace ZTunnel.Pmms.Service.Service
{
    public class TunnelInfoService: ITunnelInfoService
    {
        private readonly ITunnelInfoRepository tunnelInfoRepository;
        public TunnelInfoService(ITunnelInfoRepository tunnelInfoRepository)
        {
            this.tunnelInfoRepository = tunnelInfoRepository;
        }

        public bool Add(TunnelInfo model)
        {
            return tunnelInfoRepository.Add(model);
        }

        public bool Delete(TunnelInfo model)
        {
            return tunnelInfoRepository.Delete(model);
        }
        public bool Modify(TunnelInfo model)
        {
            return tunnelInfoRepository.Update(model);
        }
        public TunnelInfo GetTunnelInfo(string key)
        {
            return tunnelInfoRepository.FindBy(key);
        }
        public PagedList<TunnelInfo> GetPagedList(int pageIndex,int pageSize)
        {
            string where = " where IsDel=0 ";
            return tunnelInfoRepository.FindPage(pageIndex, pageSize, where, "  TunnelCode ");
        }
    }
}
