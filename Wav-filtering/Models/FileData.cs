using System;
using System.IO;

namespace Wav_filtering.Models
{
    public class FileData: IEquatable<FileData>
    {
        public FileData(string fullPath)
        {
            if (string.IsNullOrEmpty(fullPath))
                throw new ArgumentNullException(nameof(fullPath));

            FullPath = fullPath;
            FileName = Path.GetFileName(fullPath);
        }

        public string FileName { get; }
        public string FullPath { get; }

        public int Low { get; set; }

        public bool Equals(FileData other)
        {
            if (other == null)
                return false;

            return FullPath.Equals(other.FullPath);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            var toCompare = obj as FileData;
            return Equals(toCompare);
        }

        public override int GetHashCode()
        {
            return FileName.GetHashCode();
        }
    }
}
