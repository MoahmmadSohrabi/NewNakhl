using AutoMapper;
using NakhleNakhoda.Domain.Catalog;
using NakhleNakhoda.ViewModels.Catalog;

namespace NakhleNakhoda.Web.Infrastructure.Mapper
{
    /// <summary>
    /// AutoMapper configuration for models
    /// </summary>
    public class MapperConfiguration : Profile
    {

        #region Ctor

        public MapperConfiguration()
        {
            //CreateMap<Shop, ShopModel>().ReverseMap()
            //    .ForMember(m => m.CreatedOnUtc, opt => opt.Ignore())
            //    .ForMember(m => m.UpdatedOnUtc, opt => opt.Ignore());

            CreateMap<UserBook, UserBookModel>().ReverseMap();
        }

        #endregion

    }
}