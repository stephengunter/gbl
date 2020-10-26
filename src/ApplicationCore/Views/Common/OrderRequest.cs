using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Views
{
    public class OrderRequest  //處理變更排序專用
    {
        public int TargetId { get; set; }

        public int ReplaceId { get; set; }

        public bool Up { get; set; }
    }


    public class SubOrderRequest
    {
        public int Id { get; set; }   //ParentId

        public List<int> Orders { get; set; }
    }
}
