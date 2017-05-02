using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kapitalist.Data.Models.Interfaces
{
    public interface IProject
    {
        string Id { get; set; }

        string Name { get; set; }
    }
}
