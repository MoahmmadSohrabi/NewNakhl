namespace NakhleNakhoda.ViewModels.Admin.Catalog
{
    public class OrganizationGraphModel
    {
        public long key { get; set; }
        public string name { get; set; } = "";
        public string title { get; set; } = "";
        public long parent { get; set; }
    }
}
