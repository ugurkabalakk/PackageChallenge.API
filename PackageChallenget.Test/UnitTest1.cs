using Package.Challange.Infrastructure.Exceptions;
using Package.Challenge.Application.Interface;
using Package.Challenge.Application.Service;
using System.Linq;
using Xunit;

namespace PackageChallenget.Test
{
    public class UnitTest1
    {
        [Fact]
        public void InCorrectFilePath_ReturnTrue()
        {
            IFileOperationService _fileOperation = new FileOperationService();

            string filePath = "D:\\resources\\example_input";

            var result = _fileOperation.IsFilePathValid(filePath);

            Assert.True(result);
        }

        [Fact]
        public void Get_APIException_When_Cost_IS_200()
        {

            ISerializeData _serializeData = new SerializeDataService();

            string[] data = { "81 : (1,53.38,€200)" };

            var ex = Assert.Throws<APIException>(() => _serializeData.SerializePackageModel(data));

            Assert.Equal("Cost of an item can be max 100", ex.Message);
        }


        [Fact]
        public void Get_APIException_When_Weight_IS_250()
        {

            ISerializeData _serializeData = new SerializeDataService();

            string[] data = { "81 : (1,250,€200)" };

            var ex = Assert.Throws<APIException>(() => _serializeData.SerializePackageModel(data));

            Assert.Equal("Weight can be max 100", ex.Message);
        }


        [Fact]
        public void Get_APIException_When_Index_Negative()
        {

            ISerializeData _serializeData = new SerializeDataService();

            string[] data = { "81 : (-1,250,€200)" };

            var ex = Assert.Throws<APIException>(() => _serializeData.SerializePackageModel(data));

            Assert.Equal("There might be up to maximum 15 items", ex.Message);
        }

        [Fact]
        public void Return_4SlashN_When_Data_Is_The_Same_Line_Input_Example()
        {

            string[] data = { "81 : (1,53.38,€45) (2,88.62,€98) (3,78.48,€3) (4,72.30,€76) (5,30.18,€9) (6,46.34,€48)" };

            ISerializeData _serializeData = new SerializeDataService();
            var packageList = _serializeData.SerializePackageModel(data);


            var result = string.Empty;

            IPackageService _packageService = new PackageService();

            result = packageList.Aggregate(result,
               (current, package) =>
                   current + _packageService.GetPackageItemIndexes(package.Key, package.Value) + "\n");


            Assert.Equal("4\n", result);

        }
    }
}