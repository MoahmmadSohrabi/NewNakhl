using AutoMapper;
using NakhleNakhoda.Domain.Identity;
using NakhleNakhoda.ViewModels.Admin.Identity;

namespace NakhleNakhoda.Web.Areas.Admin.Infrastructure.Mapper
{
    /// <summary>
    /// AutoMapper configuration for models
    /// </summary>
    public class AdminMapperConfiguration : Profile
    {

        #region Ctor

        public AdminMapperConfiguration()
        {
            //CreateMap<Shop, ShopModel>().ReverseMap()
            //    .ForMember(m => m.CreatedOnUtc, opt => opt.Ignore())
            //    .ForMember(m => m.UpdatedOnUtc, opt => opt.Ignore());

            //CreateMap<ShopReviewHelpfulness, ShopReviewHelpfulnessModel>().ReverseMap();


            CreateMap<MemberModel, Member>().ReverseMap()
                .ForMember(m => m.CreatedOnUtc, opt => opt.Ignore())
                .ForMember(m => m.UpdatedOnUtc, opt => opt.Ignore());

            CreateMap<Member, MemberPasswordModel>().ReverseMap();

            //CreateMap<TaskCategory, TaskCategoryModel>().ReverseMap()
            //    .ForMember(m => m.CreatedOnUtc, opt => opt.Ignore())
            //    .ForMember(m => m.UpdatedOnUtc, opt => opt.Ignore());

            //CreateMap<Organization, OrganizationModel>().ReverseMap()
            //    .ForMember(m => m.CreatedOnUtc, opt => opt.Ignore())
            //    .ForMember(m => m.UpdatedOnUtc, opt => opt.Ignore());
        }

        #endregion

    }
}