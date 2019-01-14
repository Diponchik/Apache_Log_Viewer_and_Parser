using System;

namespace LogViewer.Requests
{
    public class LogFilterRequest : PaginationModel
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? Top { get; set; }
        public bool IsHostRequest { get; set; }
    }
}
