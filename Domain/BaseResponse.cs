using Microsoft.AspNetCore.Http;

namespace SolutionsForBuisnesTestTask.Domain
{
    public class BaseResponse<T> : IBaseResponse<T>
    {
        public string Description { get; set; }

        public int StatusCode { get; set; }

        public T Data { get; set; }

    }

    public interface IBaseResponse<T>
    {
        string Description { get; }
        int StatusCode { get; }
        T Data { get; }
    }
}
