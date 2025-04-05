using System;
using System.Drawing;
using System.IO;
using System.ServiceModel;
using SoapCore;

namespace BinaryFileClient
{
    [ServiceContract]
    public interface IFileService
    {
        [OperationContract]
        string Echo(string text);
        [OperationContract]
        Stream DownloadImage(string name);
    }
}
