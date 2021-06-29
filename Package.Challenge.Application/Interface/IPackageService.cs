using Package.Challenge.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.Challenge.Application.Interface
{
   public interface IPackageService
    {
        string GetPackageItemIndexes(int maxWeight, List<PackageModel> packages);
    }
}
