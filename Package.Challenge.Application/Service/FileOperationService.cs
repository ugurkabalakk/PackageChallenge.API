using Package.Challenge.Application.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package.Challenge.Application.Service
{
    public class FileOperationService : IFileOperationService
    {
        public bool IsFilePathValid(string filePath)
        {
            if (filePath.Trim() == string.Empty) return false;

            string pathname;
            string filename;
            try
            {
                pathname = Path.GetPathRoot(filePath);
                filename = Path.GetFileName(filePath);
            }
            catch (ArgumentException)
            {
                return false;
            }

            if (filename.Trim() == string.Empty) return false;
            if (pathname.IndexOfAny(Path.GetInvalidPathChars()) >= 0) return false;
            if (filename.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0) return false;

            return true;
        }
    }
}
