using DataSystem.Shared.Helpers.Enumerators;

namespace DataSystem.FrontendWpf.Models.Enumerators
{
    public class EnumDisplayItem<T>
    {
        public T Value { get; set; }
        public string DisplayName { get; set; }

        public EnumDisplayItem(T value)
        {
            Value = value;
            DisplayName = EnumHelper.GetDisplayName(value as Enum);
        }

        public override string ToString() => DisplayName;
    }

}
