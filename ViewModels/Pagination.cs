using System.Collections.Generic;

namespace MyCosts.ViewModels
{
    public class Pagination<T> where T : class
    {
        public IEnumerable<T> Records { get; set; }
        public int Page { get; set; }
        public int PerPage { get; set; }
        public int CountRecords { get; set; }
        public string Search { get; set; }
    }
}
