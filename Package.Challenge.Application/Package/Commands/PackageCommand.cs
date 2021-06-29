using Package.Challenge.Application.Common.CommandQueryWrappers;

namespace Package.Challenge.Application.Package.Commands
{
    public class PackageCommand : IRequestWrapper<PackageCommandResponse>
    {
        public string FilePath { get; set; }
    }
}