namespace NakhleNakhoda.Data.Core.Mvc
{
    /// <summary>
    /// MvcController ViewModel
    /// </summary>
    public class MvcControllerViewModel
    {
        public MvcControllerViewModel(string areaName, IList<Attribute> controllerAttributes,
            string controllerDisplayName, string controllerName, IList<MvcActionViewModel> mvcActions)
        {
            AreaName = areaName;
            ControllerAttributes = controllerAttributes;
            ControllerDisplayName = controllerDisplayName;
            ControllerName = controllerName;
            MvcActions = mvcActions;
        }

        public MvcControllerViewModel()
        {
            AreaName = "";
            ControllerAttributes = new List<Attribute>();
            ControllerDisplayName = "";
            ControllerName = "";
            MvcActions = new List<MvcActionViewModel>();
        }

        /// <summary>
        /// Return `AreaAttribute.RouteValue`
        /// </summary>
        public string AreaName { get; set; }

        /// <summary>
        /// Returns the list of the Controller's Attributes.
        /// </summary>
        public IList<Attribute> ControllerAttributes { get; set; }

        /// <summary>
        /// Returns the `DisplayNameAttribute` value
        /// </summary>
        public string ControllerDisplayName { get; set; }

        /// <summary>
        /// It's set to `{AreaName}:{ControllerName}`
        /// </summary>
        public string ControllerId => $"{AreaName}:{ControllerName}";

        /// <summary>
        /// Return ControllerActionDescriptor.ControllerName
        /// </summary>
        public string ControllerName { get; set; }

        /// <summary>
        /// Returns the list of the Controller's action methods.
        /// </summary>
        public IList<MvcActionViewModel> MvcActions { get; set; } = new List<MvcActionViewModel>();

        /// <summary>
        /// Returns `[{controllerAttributes}]{AreaName}.{ControllerName}`
        /// </summary>
        public override string ToString()
        {
            const string attribute = "Attribute";
            var controllerAttributes = string.Join(",", ControllerAttributes.Select(a => a.GetType().Name.Replace(attribute, "")));
            return $"[{controllerAttributes}]{AreaName}.{ControllerName}";
        }
    }
}