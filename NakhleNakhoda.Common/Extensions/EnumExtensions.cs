using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace NakhleNakhoda.Common.Extensions
{
    public static class EnumExtensions
    {
        public static List<EnumValue> GetValues<T>()
        {
            List<EnumValue> values = new List<EnumValue>();
            foreach (var itemType in Enum.GetValues(typeof(T)))
            {
                values.Add(new EnumValue()
                {

                    Value = (int)itemType,
                    Name = Enum.GetName(typeof(T), itemType) ?? "",
                    DisplayName = GetDisplayName((Enum)itemType),
                });
            }

            return values;
        }

        public static List<SelectListItem> GetValuesAsSelectListItem<T>()
        {
            List<SelectListItem> values = new List<SelectListItem>();
            foreach (var itemType in Enum.GetValues(typeof(T)))
            {
                values.Add(new SelectListItem()
                {
                    Text = GetDisplayName((Enum)itemType),
                    Value = ((int)itemType).ToString(),
                });
            }

            //insert special item for the default value
            PrepareDefaultItem(values, false);

            return values;
        }

        public static string GetDisplayName(this Enum enu)
        {
            var attr = GetDisplayAttribute(enu);
            return attr?.Name ?? enu.ToString();
        }

        public static string GetDescription(this Enum enu)
        {
            var attr = GetDisplayAttribute(enu);
            return attr?.Description ?? enu.ToString();
        }

        private static DisplayAttribute? GetDisplayAttribute(object value)
        {
            Type type = value.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException(string.Format("Type {0} is not an enum", type));
            }

            // Get the enum field.
            var field = type.GetField(value.ToString() ?? "");
            return field?.GetCustomAttribute<DisplayAttribute>();
        }

        /// <summary>
        /// Prepare default item
        /// </summary>
        /// <param name="items">Available items</param>
        /// <param name="withSpecialDefaultItem">Whether to insert the first special item for the default value</param>
        /// <param name="defaultItemText">Default item text; pass null to use "All" text</param>
        private static void PrepareDefaultItem(IList<SelectListItem> items, bool withSpecialDefaultItem, string? defaultItemText = null)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            //whether to insert the first special item for the default value
            if (!withSpecialDefaultItem)
                return;

            //at now we use "0" as the default value
            const string value = "0";

            //prepare item text
            defaultItemText ??= Constants.NoneItemText;

            //insert this default item at first
            items.Insert(0, new SelectListItem { Text = defaultItemText, Value = value });
        }
    }
}
