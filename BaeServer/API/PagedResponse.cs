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
    }
}
