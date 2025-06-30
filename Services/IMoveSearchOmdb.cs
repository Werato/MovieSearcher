using MovieSearcher.SharedModels;

namespace MovieSearcher.Services
{
    public interface IMoveSearchOmdb
    {
        Task<MovieDto> SearchByTitleAsync(string title);
    }
}
