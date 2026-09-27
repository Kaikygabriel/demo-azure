using System.Collections;

namespace Shopping.Application.Commum;

public class PagedResponse<T>  where T: IEnumerable  
{
    private PagedResponse()
    {
        
    }
    public PagedResponse(int page, int pageSize, T value, int totalPage)
    {
        Page = page;
        PageSize = pageSize;
        Value = value;
        TotalPage = totalPage;
    }

    public int Page { get; init; }
    public int PageSize { get; init; }
    public T Value { get; init; }
    public int TotalPage { get; init; }
}