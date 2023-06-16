using Newtonsoft.Json;
using ServiceStack;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace Servicios
{
    public class DBHelper
    {

        public string EjecutarPost(string DatosRegistro, string urlApi)
        {
            string responseFromServer = string.Empty;


            WebRequest request = WebRequest.Create(urlApi + DatosRegistro);
            request.Method = "POST";
            string postData = "This is a test that posts this string to a Web server.";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;

            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            try
            {
                WebResponse response = request.GetResponse();
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);

                using (dataStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(dataStream);
                    responseFromServer = reader.ReadToEnd(); ;
                }
                return responseFromServer;
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        public string EjecutarPostEmpty(string urlApi)
        {
            string responseFromServer = string.Empty;
            WebRequest request = WebRequest.Create(urlApi);
            request.Method = "POST";
            string postData = "This is a test that posts this string to a Web server.";
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;

            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            try
            {
                WebResponse response = request.GetResponse();
                Console.WriteLine(((HttpWebResponse)response).StatusDescription);

                using (dataStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(dataStream);
                    responseFromServer = reader.ReadToEnd(); ;
                }
                return responseFromServer;
            }
            catch
            {
                return "";
            }

        }

        public string EjecutarPostJson(string DatosAcceso, string urlApi)
        {
            try
            {
                var response = urlApi
                                 .PostJsonToUrl(DatosAcceso);
                return response;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        //public string EjectutarGetJson(string urlApi)
        //{
        //    var response = urlApi
        //        .GetJsonFromUrl(webReq => webReq.Headers["X-CSCAPI-KEY"] = "cnV6N3l1S0lhaDdUSUV2djZGcWhWYUpPaUhDcDZscEZtMURnME44Sg==");
        //    var json = "http://example.org/users".GetJsonFromUrl(requestFilter: webReq => {
        //        webReq.Headers["X-Api-Key"] = apiKey;
        //    });
        //    return response;
        //}

        public string EjecutarGet(string urlApi, string urlRequest)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(urlApi);
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // List all Names.
            HttpResponseMessage response = client.GetAsync(urlRequest).Result;  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                var products = response.Content.ReadAsStringAsync().Result;
                var res = JsonConvert.DeserializeObject<string>(products);
                return res;
            }
            else
            {
                return response.StatusCode.ToString();
            }
        }



        public string GetRues(string url)
        {
            var Respuesta = string.Empty;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {

                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            // Do something with responseBody
                            Respuesta = responseBody;
                            Console.WriteLine(responseBody);
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                Respuesta = "NoEncontrado";
            }

            return Respuesta;
        }

        public string EjecutarGetWithKey(string urlApi, string urlRequest, string keyName, string key)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(urlApi);
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add(keyName, key);
            // List all Names.
            HttpResponseMessage response = client.GetAsync(urlRequest).Result;  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                var products = response.Content.ReadAsStringAsync().Result;
                var res = JsonConvert.DeserializeObject<string>(products);
                return res;
            }
            else
            {
                return response.StatusCode.ToString();
            }
        }

        public string EjecutarGetBasicAuth(string urlApi, string urlRequest, string auth)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(urlApi);
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Add("Authorization", "Basic " + auth);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);
            // List all Names.
            HttpResponseMessage response = client.GetAsync(urlRequest).Result;  // Blocking call!
            if (response.IsSuccessStatusCode)
            {
                var products = response.Content.ReadAsStringAsync().Result;
                //var res = JsonConvert.DeserializeObject<string>(products);
                return products;
            }
            else
            {
                return response.StatusCode.ToString();
            }
        }

        public string EjecutarPostNew(string urlApi, string urlRequest, string DatosPost)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApi);
                var response = client.PostAsJsonAsync(urlRequest, DatosPost).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
                else
                    return response.StatusCode.ToString();
            }
        }

        public string EjecutarPut(string datos, string urlApi, string urlRequest)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApi);
                var response = client.PutAsJsonAsync(urlRequest, datos).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.StatusCode.ToString();
                }
                else
                    return response.StatusCode.ToString();
            }
        }

        public string EjecutarDelete(string urlApi, string urlRequest, string id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(urlApi);
                var response = client.DeleteAsync(urlRequest + id).Result;
                if (response.IsSuccessStatusCode)
                {
                    return response.StatusCode.ToString();
                }
                else
                    return response.StatusCode.ToString();
            }
        }
    }
}
