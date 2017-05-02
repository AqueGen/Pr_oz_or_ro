using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Kapitalist.Data.Models;
using Kapitalist.Data.Store.Models;
using LinqKit;

namespace Kapitalist.Services.Prozorro.Providers.Models
{
	internal static class TendersFilter
	{
		public static IQueryable<Tender> FilterIdentifiers(this IQueryable<Tender> tenders, ICollection<string> identifiers)
		{
			if (identifiers == null || identifiers.Count == 0)
				return tenders;

			var predicate = PredicateBuilder.False<Tender>();
			List<Guid> guids = new List<Guid>();
			List<string> names = new List<string>();
			Guid guid;
			foreach (string item in identifiers)
			{
				if (string.IsNullOrWhiteSpace(item))
					continue;
				string value = item.Trim();
				if (Guid.TryParse(value, out guid))
					guids.Add(guid);
				else
					predicate = predicate.Or(t => t.Identifier.Contains(value));
			}
			if (guids.Count > 0)
				predicate = predicate.Or(t => guids.Contains(t.Guid));

			return tenders.Where(predicate);
		}

        public static IQueryable<Tender> FilterKeywords(this IQueryable<Tender> tenders, ICollection<string> keywords)
		{
			if (keywords == null || keywords.Count == 0)
				return tenders;
			var predicate = PredicateBuilder.False<Tender>();
			Expression<Func<Tender, string, bool>> containsWord = (t, s) =>
				t.Title.Contains(s) || t.Description.Contains(s);
			foreach (string item in keywords)
			{
				if (item == null)
					continue;
				string[] words = item
					.Split(new char[] { ' ', ',', '.', ';', '-' }, StringSplitOptions.RemoveEmptyEntries)
					.Select(w => w.Trim())
					.Where(w => !string.IsNullOrEmpty(w)).ToArray();
				switch (words.Length)
				{
					case 0:
						continue;
					case 1:
						string word = words[0];
						predicate = predicate.Or(t => containsWord.Invoke(t, word));
						break;
					default:
						var inner = PredicateBuilder.True<Tender>();
						foreach (string key in words)
							inner = inner.And(t => containsWord.Invoke(t, key));
						predicate = predicate.Or(inner);
						break;
				}
			}
			return tenders.Where(predicate);
		}

		public static IQueryable<Tender> FilterPeriod(this IQueryable<Tender> tenders,
			Expression<Func<Tender, Period>> period, Period value)
		{
			if (value == null)
				return tenders;

			Expression<Func<Period, Period, bool>> checkStart = (p, v) =>
				!p.EndDate.HasValue || p.EndDate.Value > v.StartDate.Value;

			if (value.StartDate.HasValue)
				tenders = tenders.Where(t => checkStart.Invoke(period.Invoke(t), value));
			if (value.EndDate.HasValue)
				tenders = tenders.Where(t => period.Invoke(t).StartDate < value.EndDate.Value);

			return tenders;
		}

        public static IQueryable<Tender> FilterCPV(this IQueryable<Tender> tenders, ICollection<string> codes)
		{
			if (codes == null || codes.Count == 0)
				return tenders;
			var predicate = PredicateBuilder.False<Item>();
			foreach (string item in codes)
			{
				if (string.IsNullOrWhiteSpace(item))
					continue;
				string value = item.Trim();
				predicate = predicate.Or(i => i.Classification.Id.Contains(value));
			}
			return tenders.Where(t => t.Items.AsQueryable().Any(predicate));
		}

        public static IQueryable<Tender> FilterGSIN(this IQueryable<Tender> tenders, ICollection<string> codes)
		{
			if (codes == null || codes.Count == 0)
				return tenders;
			var predicate = PredicateBuilder.False<Classification>();
			foreach (string item in codes)
			{
				if (string.IsNullOrWhiteSpace(item))
					continue;
				string value = item.Trim();
				predicate = predicate.Or(c => c.Id.Contains(value));
			}
			return tenders.Where(t => t.Items.AsQueryable().SelectMany(i => i.AdditionalClassifications).Any(predicate));
		}

        public static IQueryable<Tender> FilterProcurers(this IQueryable<Tender> tenders, ICollection<string> procurers)
		{
			if (procurers == null || procurers.Count == 0)
				return tenders;
			var predicate = PredicateBuilder.False<ProcuringEntity>();
			foreach (string item in procurers)
			{
				if (item == null)
					continue;
				string[] words = item
					.Split(new char[] { ' ', ',', '.', ';', '-' }, StringSplitOptions.RemoveEmptyEntries)
					.Select(w => w.Trim())
					.Where(w => !string.IsNullOrEmpty(w)).ToArray();
				switch (words.Length)
				{
					case 0:
						continue;
					case 1:
						string word = words[0];
						predicate = predicate.Or(o => o.Name.Contains(word));
						break;
					default:
						var inner = PredicateBuilder.True<ProcuringEntity>();
						foreach (string key in words)
							inner = inner.And(o => o.Name.Contains(key));
						predicate = predicate.Or(inner);
						break;
				}
			}
			return tenders.Where(t => predicate.Invoke(t.ProcuringEntity));
		}

        public static IQueryable<Tender> FilterRegions(this IQueryable<Tender> tenders, ICollection<string> regions)
		{
			if (regions == null || regions.Count == 0)
				return tenders;
			var predicate = PredicateBuilder.False<ProcuringEntity>();
			foreach (string item in regions)
			{
				if (string.IsNullOrWhiteSpace(item))
					continue;
				string value = item.Trim();
				predicate = predicate.Or(o => o.Address.Region.Contains(value));
			}
			return tenders.Where(t => predicate.Invoke(t.ProcuringEntity));
		}

        public static IQueryable<Tender> FilterStatuses(this IQueryable<Tender> tenders, ICollection<string> statuses)
		{
			if (statuses == null || statuses.Count == 0)
				return tenders;
			var predicate = PredicateBuilder.False<Tender>();
			foreach (string item in statuses)
			{
				if (string.IsNullOrWhiteSpace(item))
					continue;
				string value = item.Trim();
				predicate = predicate.Or(t => t.Status == value);
			}
			return tenders.Where(predicate);
		}
	}
}
