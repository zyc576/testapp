using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using ZTunnel.Pmms.Model.Entity;
using ZTunnel.Pmms.Repository.IRepository;
using ZTunnel.Pmms.Core.Dapper;

namespace ZTunnel.Pmms.Repository.Repository
{
    public class TunnelInfoRepository: BaseRepository<TunnelInfo>, ITunnelInfoRepository
    {
        public TunnelInfoRepository(IDbConnection db) : base(db)
        {
        }
    }
}
