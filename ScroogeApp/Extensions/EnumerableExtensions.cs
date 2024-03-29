namespace ScroogeApp.Extensions;

using System.Text;

public static class EnumerableExtensions
{
    public static string AsHtml<T>(this IEnumerable<T> collection) {
        Type itemType = typeof(T);
        var properties = itemType.GetProperties();

        var sb = new StringBuilder();

        foreach (var item in collection)
        {
            sb.Append("<div>");
            foreach (var itemPropertyInfo in properties)
            {
                sb.Append($"<p><i>{itemPropertyInfo.Name}: </i>{itemPropertyInfo.GetValue(item)}</p>");
            }
            sb.Append("</div><hr>");
        }

        return sb.ToString();
    }
}