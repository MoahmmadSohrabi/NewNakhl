namespace NakhleNakhoda.ViewModels.Admin.Media
{
    public class PictureModel : BaseEntityModel
    {
        public string ImageUrl { get; set; } = "";

        public string ThumbImageUrl { get; set; } = "";

        public string FullSizeImageUrl { get; set; } = "";

        public string TitleAttribute { get; set; } = "";

        public string AltAttribute { get; set; } = "";
    }
}
