using System.ComponentModel.DataAnnotations;

namespace NakhleNakhoda.ViewModels.Admin.Catalog
{
    public class TaskCategoryModel : BaseEntityModel
    {
        public TaskCategoryModel(int id, string title, string description, string structure)
        {
            Id = id;
            Title = title;
            Description = description;
            Structure = structure;
        }
        public TaskCategoryModel()
        {
            Id = 0;
            Title = "";
            Description = "";
            Structure = "";
        }

        [Display(Name = "عنوان")]
        public string Title { get; set; }

        [Display(Name = "توضیحات")]
        public string? Description { get; set; }

        [Display(Name = "ساختار")]
        public string? Structure { get; set; }

        [Display(Name = "ایجاد شده در")]
        public DateTime? CreatedOnUtc { get; set; }

        [Display(Name = "بروز شده در")]
        public DateTime? UpdatedOnUtc { get; set; }
    }
}
