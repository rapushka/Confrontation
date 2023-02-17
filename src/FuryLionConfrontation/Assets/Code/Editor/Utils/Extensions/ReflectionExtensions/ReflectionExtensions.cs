using System.Reflection;
using static System.Reflection.BindingFlags;

namespace Confrontation.Editor
{
	public static class ReflectionExtensions
	{
		public static void InvokePrivateMethod(this object @this, string methodName, params object[] parameters)
			=> @this.InvokeMethod(methodName, NonPublic | Instance, parameters);

		public static void InvokeMethod
			(this object @this, string methodName, BindingFlags bindingFlags, params object[] parameters)
			=> @this.GetType().GetMethod(methodName, bindingFlags)!.Invoke(@this, parameters);

		public static T GetPrivateField<T>(this object @this, string fieldName)
			=> @this.GetFieldValue<T>(fieldName, Instance | NonPublic);

		public static void SetPrivateField<T>(this object @this, string fieldName, T value)
			=> @this.SetFieldValue(fieldName, Instance | NonPublic, value);

		public static void SetPrivateProperty<T>(this object @this, string propertyName, T value)
			=> @this.SetPropertyValue(propertyName, value);

		private static T GetFieldValue<T>(this object @this, string fieldName, BindingFlags flags)
			=> (T)@this.GetType().GetField(fieldName, flags)!.GetValue(@this);

		private static void SetFieldValue<T>(this object @this, string fieldName, BindingFlags flags, T value)
			=> @this.GetType().GetField(fieldName, flags)!.SetValue(@this, value);

		public static T GetPropertyValue<T>(this object @this, string fieldName)
			=> (T)@this.GetType().GetProperty(fieldName)!.GetValue(@this);

		private static void SetPropertyValue<T>(this object @this, string propertyName, T value)
			=> @this.GetType().GetProperty(propertyName)!.SetValue(@this, value);
	}
}