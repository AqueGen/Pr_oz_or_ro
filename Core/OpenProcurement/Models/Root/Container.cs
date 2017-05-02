using Newtonsoft.Json;

namespace Kapitalist.Core.OpenProcurement.Models.Root
{
	/// <summary>
	/// Універсальне упакування даних - містить тільки дані, без ніякої додаткової інформації.
	/// Використовується автоматично у всіх запитах, де не вказане особливе упакування.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	internal class Container<T> : IRootModel
	{
		/// <summary>
		/// Дані запиту
		/// </summary>
		[JsonProperty("data")]
		public T Data { get; set; }
	}
}
