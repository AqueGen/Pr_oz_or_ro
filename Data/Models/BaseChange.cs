using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models.Interfaces;

namespace Kapitalist.Data.Models
{
	/// <summary>
	/// Change
	/// </summary>
	public abstract class BaseChange : IChange
	{
		public BaseChange()
		{
		}

		public BaseChange(IChange change)
		{
		}

		// TODO Taras 0: В документації немає опису цього об'єкту. Знайти опис та реалізувати модель.
	}
}
