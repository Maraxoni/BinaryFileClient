using System.ServiceModel;
using System.Text;

namespace BinaryFileClient
{
    class ClientMain
    {
        static void Main(string[] args)
        {

            var binding = new BasicHttpBinding
            {
                MaxReceivedMessageSize = 65536,
                MessageEncoding = WSMessageEncoding.Mtom
                //MessageEncoding = WSMessageEncoding.Text
            };

            var endpoint = new EndpointAddress("http://localhost:8080/FileService");
            var channelFactory = new ChannelFactory<IFileService>(binding, endpoint);


            IFileService client = channelFactory.CreateChannel();

            Console.Write("Enter the name of the file to download: ");
            string fileName = Console.ReadLine();
            var fileData = client.DownloadImage(fileName);

            if (fileData != null)
            {
                try
                {
                    string projectDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;
                    string filePath = Path.Combine(projectDirectory, fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    {
                        fileData.CopyTo(fileStream);
                        Console.WriteLine($"File saved successfully to: {filePath}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error saving the file: " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("No file data received.");
            }

            ((IClientChannel)client).Close();
        }

    }
}