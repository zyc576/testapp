using System.Collections.Generic;

namespace ZTunnel.Pmms.Core.Paged
{
    public class PagedList<T> where T : class
    {
            /// <summary>
            /// 数据集
            /// </summary>
            public IEnumerable<T> Data { get; set; }
            /// <summary>
            /// 总条数
            /// </summary>
            public int Total { get; set; }
        
    }
}
