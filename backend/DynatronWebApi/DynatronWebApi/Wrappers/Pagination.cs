namespace DynatronWebApi.Wrappers;

/// <summary>
///     Pagination
/// </summary>
/// <typeparam name="T"></typeparam>
public class Pagination<T>
{
    /// <summary>
    ///     Empty Constructor
    /// </summary>
    public Pagination()
    {
    }

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="currentPage"></param>
    /// <param name="pageSize"></param>
    /// <param name="totalItems"></param>
    /// <param name="result"></param>
    public Pagination(int currentPage, int pageSize, int totalItems, List<T> result)
    {
        CurrentPage = currentPage;
        PageSize = pageSize;
        TotalItems = totalItems;
        Result = result;
        TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
    }

    /// <summary>
    ///     Properties of CurrentPage
    /// </summary>
    public int CurrentPage { get; set; }

    /// <summary>
    ///     PageSize
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    ///     TotalPages
    /// </summary>
    public int TotalPages { get; set; }

    /// <summary>
    ///     TotalItems
    /// </summary>
    public int TotalItems { get; set; }

    /// <summary>
    ///     List of result
    /// </summary>
    public List<T> Result { get; set; }
}