using Newtonsoft.Json;

namespace Kapitalist.Core.OpenProcurement.Models.Root
{
	/// <summary>
	/// Містить Token разом з даними
	/// Токени власника видаються, щоб забезпечити кілька ролей дійових осіб при створенні об’єкта.
	/// </summary>
	/// <typeparam name="T">Тип моделі даних</typeparam>
	public class Protected<T> : IRootModel
	{
		public Protected()
		{
		}

		public Protected(T data, string token)
		{
			Data = data;
			Access = new AccessData {
				Token = token
			};
		}

		/// <summary>
		/// Інформація про захист даних
		/// </summary>
		[JsonProperty("access")]
		public AccessData Access { get; set; }

		public class AccessData
		{
			/// <summary>
			/// Токен власника
			/// </summary>
			[JsonProperty("token")]
			public string Token { get; set; }
		}

		/// <summary>
		/// Токен власника
		/// </summary>
		[JsonIgnore]
		public string Token {
			get {
				return Access?.Token;
			}
			set {
				if (Access == null)
					Access = new AccessData();
				Access.Token = value;
			}
		}

		/// <summary>
		/// Дані запиту
		/// </summary>
		[JsonProperty("data")]
		public T Data { get; set; }
	}
}
