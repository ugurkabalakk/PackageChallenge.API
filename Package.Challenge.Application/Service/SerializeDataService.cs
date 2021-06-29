using Package.Challange.Infrastructure.Exceptions;
using Package.Challenge.Application.Interface;
using Package.Challenge.Application.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.Challenge.Application.Service
{
    public class SerializeDataService : ISerializeData
    {
        public IEnumerable<KeyValuePair<int, List<PackageModel>>> SerializePackageModel(IEnumerable<string> lines)
        {
            var dicPackages = new List<KeyValuePair<int, List<PackageModel>>>();

            foreach (var line in lines)
                if (!string.IsNullOrEmpty(line))
                {
                    int weightLimit = Convert.ToInt32(line.Split(":")[0].Trim());

                    var listOfPackage = line.Split(":")[1].Split(")");

                    var packageList = new List<PackageModel>();
                    foreach (var packValue in listOfPackage)
                    {

                        var package = new PackageModel();
                        //(1,53.38,€45) 
                        if (string.IsNullOrEmpty(packValue)) continue;
                        package.Index = int.Parse(packValue.Split(",")[0].Trim()
                            .Substring(1, packValue.Split(",")[0].Length - 2).Trim());

                        if (package.Index > 15 || package.Index < 0)
                        {
                            throw new APIException("There might be up to maximum 15 items");
                        }

                        package.Weight = Convert.ToInt32(double.Parse(packValue.Split(",")[1].Trim(),
                            CultureInfo.InvariantCulture));

                        if (package.Weight > 100 || package.Weight < 0)
                        {
                            throw new APIException("Weight can be max 100");
                        }

                        package.Cost = int.Parse(packValue.Split(",")[2].Substring(1).Trim(),
                            CultureInfo.InvariantCulture);
                        packageList.Add(package);

                        if (package.Cost > 100 || package.Cost < 0)
                        {
                            throw new APIException("Cost of an item can be max 100");
                        }
                    }

                    dicPackages.Add(new KeyValuePair<int, List<PackageModel>>(weightLimit, packageList));
                }

            return dicPackages;
        }
    }
}
