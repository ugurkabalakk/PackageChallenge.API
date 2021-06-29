using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Package.Challange.Infrastructure.Exceptions;
using Package.Challenge.Application.Models;
using Package.Challenge.Application.Common.CommandQueryWrappers;
using Package.Challenge.Application.Interface;

namespace Package.Challenge.Application.Package.Commands
{
    public class PackageCommandHandler : IRequestHandlerWrapper<PackageCommand, PackageCommandResponse>
    {

        private readonly IFileOperationService _fileOperation;
        private readonly ISerializeData _serializeData;
        private readonly IPackageService _packageService;

        // Using DI to inject infrastructure persistence Repositories
        public PackageCommandHandler(IFileOperationService fileOperation,
            ISerializeData serializeData,
            IPackageService packageService)
        {
            _fileOperation = fileOperation ?? throw new ArgumentNullException(nameof(fileOperation));
            _serializeData = serializeData ?? throw new ArgumentNullException(nameof(serializeData));
            _packageService = packageService ?? throw new ArgumentNullException(nameof(packageService));
        }
       
        public async Task<PackageCommandResponse> Handle(PackageCommand request, CancellationToken cancellationToken)
        {
            if (!_fileOperation.IsFilePathValid(request.FilePath)) throw new APIException("File is not valid");

            var result = string.Empty;

            var lines = await File.ReadAllLinesAsync(request.FilePath, cancellationToken);

            var packageList = _serializeData.SerializePackageModel(lines);

            result = packageList.Aggregate(result,
                (current, package) =>
                    current + _packageService.GetPackageItemIndexes(package.Key, package.Value) + "\n");

            return new PackageCommandResponse
            {
                Response = result
            };
        }
    }
}