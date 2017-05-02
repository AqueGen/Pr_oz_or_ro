using System;
using Newtonsoft.Json;

namespace Kapitalist.Core.OpenProcurement.Models.Root
{
	/// <summary>
	/// Містить сторінку масиву даних, та інформацію про наступну сторінку.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class ModificationsPage : IRootModel
	{
		/// <summary>
		/// Інформація про наступну сторінку даних
		/// </summary>
		[JsonProperty("next_page")]
		public NextPage NextPage { get; set; }

		/// <summary>
		/// Сторінка масиву даних
		/// </summary>
		[JsonProperty("data")]
		public ModifiedElement[] Items { get; set; }
	}

	public class NextPage
	{
		[JsonProperty("path")]
		public string Path { get; set; }

		[JsonProperty("uri")]
		public string Uri { get; set; }

		[JsonProperty("offset")]
		public DateTime? Offset { get; set; }
	}
}
