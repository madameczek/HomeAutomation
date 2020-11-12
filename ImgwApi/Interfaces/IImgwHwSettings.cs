using Shared.Models;

namespace ImgwApi.Models
{
    public interface IImgwHwSettings : IHwSettings
    {
        int StationId { get; set; }
        string Url { get; set; }
    }
}
