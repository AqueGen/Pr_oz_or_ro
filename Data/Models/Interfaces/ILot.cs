using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kapitalist.Data.Models.Drafts.Interfaces;

namespace Kapitalist.Data.Models.Interfaces
{
	public interface ILot : IStringId, ITitled, IAuctioned, IDraftLot
	{
	}
}
