using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Xunit;
using ZTunnel.Pmms.Model.Entity;
using ZTunnel.Pmms.Repository.Repository;
using ZTunnel.Pmms.Service.IService;
using ZTunnel.Pmms.Service.Service;

namespace tests
{
    public class TunnelInfoServiceTest
    {
        private ITunnelInfoService tunnelInfoService;
        private  string conn = "Data Source=.;Initial Catalog=ZTunnelPmms;User Id = sa;Password = 425735997;";
        public TunnelInfoServiceTest()
        {
            tunnelInfoService = new TunnelInfoService(new TunnelInfoRepository(new SqlConnection(conn)));
        }
        [Fact]
        public void Add()
        {
            TunnelInfo model = new TunnelInfo()
            {
                Id = Guid.NewGuid(),
                TunnelCode = "111",
                EntranceStation = "111",
                TunnelLength = "212",
                TunnelHeight = "12",
                HoleType = "212",
                HighwayGrade = "121",
                RodeType = "666",
                TunnelVentilation = "666",
                CompletionTime = DateTime.Now,
                ConstructionCompany = "66",
                DesignSpeed = "66",
                TunnelName = "66",
                TunnelType = 3,
                TunnelWidth = "66",
                AddTime = DateTime.Now
            };
            var result = tunnelInfoService.Add(model);
            Assert.True(result);
        }
        [Fact]
        public void Modify()
        {
            TunnelInfo model = new TunnelInfo()
            {
                Id = new Guid("2801AF69-57DD-4AF0-8206-CDEE68ACE2F1"),
                TunnelCode = "111",
                EntranceStation = "111",
                TunnelLength = "212",
                TunnelHeight = "12",
                HoleType = "212",
                HighwayGrade = "121",
                CompletionTime = DateTime.Now,
                RodeType = "666",
                TunnelVentilation = "666",
                ConstructionCompany = "66",
                DesignSpeed = "66",
                TunnelName = "66",
                TunnelType = 3,
                TunnelWidth = "66",
                AddTime = DateTime.Now
            };
            var result = tunnelInfoService.Modify(model);
            Assert.True(result);
        }
        [Fact]
        public void Delete()
        {
            var model = tunnelInfoService.GetPagedList(1, 1).Data.FirstOrDefault();
            Assert.NotNull(model);
            var result = tunnelInfoService.Delete(model);
            Assert.True(result);
        }
        [Fact]
        public void GetTunnelInfo()
        {
            string id = "2801AF69-57DD-4AF0-8206-CDEE68ACE2F1";
            var model = tunnelInfoService.GetTunnelInfo(id);
            Assert.NotNull(model);
        }

        [Fact]
        public void GetPagedListTest()
        {
            var data = tunnelInfoService.GetPagedList(1, 20).Data;
            Assert.True(data.Any());
        }
    }
}
