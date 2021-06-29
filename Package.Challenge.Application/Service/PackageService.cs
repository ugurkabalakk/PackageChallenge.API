using Package.Challenge.Application.Interface;
using Package.Challenge.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.Challenge.Application.Service
{
    public class PackageService : IPackageService
    {
        public string GetPackageItemIndexes(int maxWeight, List<PackageModel> packages)
        {

            var row = packages.Count + 1;
            var column = maxWeight + 1;
            var weightArray = new int[row, column];

            packages.Sort((x, y) => x.Weight.CompareTo(y.Weight));

            for (var i = 1; i < row; i++)
            {
                var pack = packages[i - 1];

                for (var j = 1; j < column; j++)
                    if (pack.Weight > j)
                        weightArray[i, j] = weightArray[i - 1, j];
                    else
                        weightArray[i, j] = Math.Max(weightArray[i - 1, j],
                            weightArray[i - 1, j - Convert.ToInt32(pack.Weight)] + pack.Cost);
            }


            var N = packages.Count + 1;
            var res = weightArray[row - 1, column - 1];

            var w = maxWeight;
            var solutionItems = new List<int>();

            for (var i = N; i > 0 && res > 0; i--)
                if (res != weightArray[i - 1, w])
                {
                    solutionItems.Add(packages[i - 1].Index);
                    res = Convert.ToInt32(res - packages[i - 1].Cost);
                    w = Convert.ToInt32(w - packages[i - 1].Weight);
                }


            solutionItems.Sort((x, y) => x.CompareTo(y));

            var result = string.Join(",", solutionItems);

            return solutionItems.Count == 0 ? "-" : result;
        }
    }
}
