﻿using System;
using System.IO;
using System.Net;
using System.Text;

namespace Schedule.wnu
{
    public static class WnuUpload
    {
        public static string UploadPath = "http://wiki.nayanova.edu/_php/includes/";
        //public static string UploadPath = "http://localhost/phpstorm/wnu/_php/includes/";

        public static string UploadTableData(string postData)
        {
            // Create a request using a URL that can receive a post. 
            WebRequest request = WebRequest.Create(UploadPath + "import.php");

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
