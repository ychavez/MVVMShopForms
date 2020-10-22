using Nancy.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProductsApi
{
    public class PushNotificationHandle
    {
        public bool Successful
        {
            get;
            set;
        }

        public string Response
        {
            get;
            set;
        }
        public Exception Error
        {
            get;
            set;
        }
        public PushNotificationHandle SendNotification(string _title, string _message, string _topic)
        {
            PushNotificationHandle result = new PushNotificationHandle();
            try
            {
                result.Successful = true;
                result.Error = null;
                // var value = message;
                var requestUri = "https://fcm.googleapis.com/fcm/send";

                WebRequest webRequest = WebRequest.Create(requestUri);
                webRequest.Method = "POST";
                webRequest.Headers.Add(string.Format("Authorization: key={0}", "AAAA7yrfYJc:APA91bG-MaXZPIcf-8mILSgAgzLnyTxZATsltWULM7o5Rrm9Jksh-jDnhAZL2YjPWDqLi7rABx_1YDRkzSzeCs1bsYruFiTMMFdTB2SB4MP-ER_-S4aa8lr-RpOvPSqHFZY_Um5yxwsY"));
                webRequest.Headers.Add(string.Format("Sender: id={0}", "1027216466071"));
                webRequest.ContentType = "application/json";

                var data = new
                {
                    //   to = "feDhOqgopT0:APA91bF85UVomm4pZjCbMJYddKp8GmXQ7QQ-f2cCn_aAlFrL9MMRP0p7YheztUn1itXtim908y5RqDM2FhcfJQ4N-QksfqaJEoeNfRqFAIxXzVx6h0QPUGzUvVuCTmGRiBcJUf5xPG1S", // Uncoment this if you want to test for single device
                    to = "/topics/" + _topic,
                    notification = new
                    {
                        title = _title,
                        body = _message,
                        //icon="myicon"
                    },
                    priority = "high"
                };
                var serializer = new JavaScriptSerializer();
                var json = serializer.Serialize(data);

                Byte[] byteArray = Encoding.UTF8.GetBytes(json);

                webRequest.ContentLength = byteArray.Length;
                using (Stream dataStream = webRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);

                    using (WebResponse webResponse = webRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = webResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                result.Response = sResponseFromServer;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                result.Successful = false;
                result.Response = null;
                result.Error = ex;
            }
            return result;
        }
    }
}
