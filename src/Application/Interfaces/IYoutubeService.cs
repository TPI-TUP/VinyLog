namespace Application.Interfaces;

public interface IYoutubeService
{
    Task<string?> SearchAlbumVideoAsync(
        string album,
        string artist);
}