using System;

namespace LogViewer.Entities
{
    public class LogModel
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public string HostName { get; set; }
        public string Route { get; set; }
        public int Status { get; set; }
        public string Params { get; set; }
        public long Size { get; set; }
        public string Country { get; set; }
    }
}
