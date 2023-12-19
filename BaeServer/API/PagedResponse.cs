namespace BaeServer.API
{
    /// <summary>
    /// Represents a paginated API response.
    /// </summary>
    /// <typeparam name="T">The data type.</typeparam>
    public class PagedResponse<T>
    {
        public required int CurrentPage { get; set; }

        public required int TotalPages { get; set; }

        public required List<T> Data { get; set; }

        public static PagedResponse<T> FromList(List<T> list, int page, int max)
        {
            var reversed = new List<T>(list);
            reversed.Reverse();
            return new PagedResponse<T>
            {
                CurrentPage = page,
                TotalPages = list.Count / max,
                Data = reversed.Skip(page * max).Take(max).ToList()
            };
        }
    }
}
