using System;
using System.IO;
using System.Net;
using System.Text;

namespace Schedule.wnu
{
    public static class WnuUpload
    {
        public static string UploadHttpPath = "http://wiki.nayanova.edu/_php/includes/";
        public static string UploadFtpPath = "ftp://wiki.nayanova.edu/";
        //public static string UploadPath = "http://localhost/phpstorm/wnu/_php/includes/";

        public static void UploadFile(string sourcefile, string destfile)
        {
            // Get the object used to communicate with the server.
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(UploadFtpPath + destfile);
            request.UseBinary = true;
            request.Method = WebRequestMethods.Ftp.UploadFile;

            // This example assumes the FTP site uses anonymous logon.
            request.Credentials = new NetworkCredential(UchOtd.Properties.Settings.Default.wnuUserName, UchOtd.Properties.Settings.Default.wnuPassword);

            byte[] b = File.ReadAllBytes(sourcefile);

            request.ContentLength = b.Length;
            using (Stream s = request.GetRequestStream())
            {
                s.Write(b, 0, b.Length);
            }

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            response.Close();
        }

        public static string UploadTableData(string postData)
        {
            // Create a request using a URL that can receive a post. 
            WebRequest request = WebRequest.Create(UploadHttpPath + "import.php");

            // Set the Method property of the request to POST.
            request.Method = "POST";
            // Create POST data and convert it to a byte array.
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);
            // Set the ContentType property of the WebRequest.
            request.ContentType = "application/x-www-form-urlencoded";
            // Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;
            // Get the request stream.
            Stream dataStream = request.GetRequestStream();
            // Write the data to the request stream.
            dataStream.Write(byteArray, 0, byteArray.Length);
            // Close the Stream object.
            dataStream.Close();
            // Get the response.
            WebResponse response = request.GetResponse();
            // Display the status.
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);
            // Get the stream containing content returned by the server.
            dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            if (dataStream != null)
            {
                var reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // Clean up the streams.
                reader.Close();
                dataStream.Close();
                response.Close();

                return responseFromServer;
            }

            return "dataStream == null";
        }
    }
}
