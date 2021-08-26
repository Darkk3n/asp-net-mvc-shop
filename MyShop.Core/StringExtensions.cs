namespace MyShop.Core.Extensions
{
	public static class StringExtensions
	{
		public static bool HasValue(this string value) => !string.IsNullOrEmpty(value) && value.Trim() != string.Empty;

	}
}