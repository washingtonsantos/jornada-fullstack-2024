using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Fina.Core.Common;

public static class EnumExtensions
{
    public static string GetDisplayName(this Enum enumValue)
    {
        var memberInfo = enumValue.GetType().GetMember(enumValue.ToString());

        if (memberInfo.Length > 0)
        {
            var attribute = memberInfo[0].GetCustomAttribute<DisplayAttribute>();
            if (attribute != null)
            {
                return attribute.GetName();
            }
        }

        return enumValue.ToString();
    }
}
