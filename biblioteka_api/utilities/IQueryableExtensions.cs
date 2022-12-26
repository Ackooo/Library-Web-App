using biblioteka_api.DTOs;

namespace biblioteka_api.utilities
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> q, PaginationDTO paginationDTO)
        {
            return q.Skip((paginationDTO.Page - 1) * paginationDTO.UnitsPerPage).Take(paginationDTO.UnitsPerPage);
        }
    }
}
