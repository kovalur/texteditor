using System.IO;
using System.Text;

namespace TextEditor.BL
{
    public interface IFileManager
    {
        bool IsExist(string filePath);
        string GetContent(string filePath);
        string GetContent(string filePath, Encoding encoding);
        void SaveContent(string filePath, string content);
        void SaveContent(string filePath, string content, Encoding encoding);
        int GetSymbolCount(string content);
    }

    public class FileManager : IFileManager
    {
        private readonly Encoding _defaultEncoding = Encoding.GetEncoding(1251);

        public bool IsExist(string filePath)
        {
            bool isExist = File.Exists(filePath);

            return isExist;
        }
        public string GetContent(string filePath)
        {
            return GetContent(filePath, _defaultEncoding);
        }
        public string GetContent(string filePath, Encoding encoding)
        {
            string content = File.ReadAllText(filePath, encoding);

            return content;
        }
        public void SaveContent(string filePath, string content)
        {
            SaveContent(filePath, content, _defaultEncoding);
        }
        public void SaveContent(string filePath, string content, Encoding encoding)
        {
            File.WriteAllText(filePath, content, encoding);
        }
        public int GetSymbolCount(string content)
        {
            int count = content.Length;

            return count;
        }
    }
}
