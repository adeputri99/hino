using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SkeletonApi.Shared
{
    public class PaginatedResult<T> : Result<T>
    {
        public PaginatedResult()
        {
            
        }

        public PaginatedResult(List<T> data)
        {
            Data = data;
        }

        public PaginatedResult(bool succeeded, List<T> data = default, List<string> messages = null, int count = 0, int pageNumber = 1, int pageSize = 10)
        {
            Data = data;
            PageNumber = pageNumber;
            Status = succeeded;
            Messages = messages;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
        }

        public new List<T> Data { get; set; }
        [JsonPropertyName("page_number")]
        public int PageNumber { get; set; }
        [JsonPropertyName("total_pages")]
        public int TotalPages { get; set; }
        [JsonPropertyName("page_size")]
        public int PageSize { get; set; }
        [JsonPropertyName("total_count")]
        public int TotalCount { get; set; }

        [JsonPropertyName("has_previous")]
        public bool HasPrevious => PageNumber > 1;
        [JsonPropertyName("has_next")]
        public bool HasNext => PageNumber < TotalPages;




        public static PaginatedResult<T> Create(List<T> data, int count, int pageNumber, int pageSize)
        {
            return new PaginatedResult<T>(true, data, null, count, pageNumber, pageSize);

        }
     



    }
}
