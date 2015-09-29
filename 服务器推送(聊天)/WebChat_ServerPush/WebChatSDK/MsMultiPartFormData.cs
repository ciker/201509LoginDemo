using System;
using System.Collections.Generic;
using System.Text;

namespace WebChatSDK
{
    public class MsMultiPartFormData
    {
        private List<byte> formData;
        public string Boundary = string.Format("--{0}--", Guid.NewGuid());
        private string fileContentType = "Content-Type: {0}";
        private string fileField = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"";
        private Encoding encode = Encoding.GetEncoding("UTF-8");
        public MsMultiPartFormData()
        {
            formData = new List<byte>();
        }

        public void AddFile(string FieldName, string FileName, byte[] FileContent, string ContentType)
        {
            string newFileField = fileField;
            string newFileContentType = fileContentType;
            newFileField = string.Format(newFileField, FieldName, FileName);
            newFileContentType = string.Format(newFileContentType, ContentType);
            formData.AddRange(encode.GetBytes("--" + Boundary + "\r\n"));
            formData.AddRange(encode.GetBytes(newFileField + "\r\n"));
            formData.AddRange(encode.GetBytes(newFileContentType + "\r\n\r\n"));
            formData.AddRange(FileContent);
            formData.AddRange(encode.GetBytes("\r\n"));
        }

        public void AddStreamFile(string FieldName, string FileName, byte[] FileContent)
        {
            AddFile(FieldName, FileName, FileContent, "application/octet-stream");
        }

        public void PrepareFormData()
        {
            formData.AddRange(encode.GetBytes("--" + Boundary + "--"));
        }

        public List<byte> GetFormData()
        {
            return formData;
        }
    }
}
