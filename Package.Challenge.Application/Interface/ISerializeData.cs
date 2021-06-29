using Package.Challenge.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.Challenge.Application.Interface
{
    public interface ISerializeData
    {
        IEnumerable<KeyValuePair<int, List<PackageModel>>> SerializePackageModel(IEnumerable<string> lines);
    }
}
