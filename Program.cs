// ===============================
// AUTHOR           : Alejandro Fierro
// DATE             : 01 Sep 2020
// PURPOSE          : Show how to consume Netsuite REST Services (RESTlet or REST API)
// ===============================
// Change History:
//    - 01 Sep 2020 
//          Creation
//==================================

using System;
using System.IO;
using System.Net;
using System.Text;
using OAuth;
using Newtonsoft.Json;

namespace netsuiteToken
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //Fill These vars with your netsuite information **********************************************************************
                String urlString = "";              //RESTlet uri
                String ckey = "";                   //Consumer Key
                String csecret = "";                //Consumer Secret
                String tkey = "";                   //Token ID
                String tsecret = "";                //Token Secret
                String netsuiteAccount = "";        //Netsuite Account to connect to (i.e 5624525_SB1)
                //**********************************************************************************************************************

                Uri url = new Uri(urlString);
                OAuthBase req = new OAuthBase();
                String timestamp = req.GenerateTimeStamp();
                String nonce = req.GenerateNonce();                
                String norm = "";
                String norm1 = "";
                String signature = req.GenerateSignature(url, ckey, csecret, tkey, tsecret, "GET", timestamp, nonce, out norm, out norm1);
                
                //Percent Encode (Hex Escape) plus character
                if (signature.Contains("+"))
                {
                    signature = signature.Replace("+", "%2B");
                }

                String header = "Authorization: OAuth ";
                header += "oauth_signature=\"" + signature + "\",";
                header += "oauth_version=\"1.0\",";
                header += "oauth_nonce=\"" + nonce + "\",";
                header += "oauth_signature_method=\"HMAC-SHA1\",";
                header += "oauth_consumer_key=\"" + ckey + "\",";
                header += "oauth_token=\"" + tkey + "\",";
                header += "oauth_timestamp=\"" + timestamp + "\",";
                header += "realm=\""+ netsuiteAccount +"\"";
                HttpWebRequest request =
               (HttpWebRequest)WebRequest.Create(urlString);

                request.ContentType = "application/json";
                request.Method = "GET";
                request.Headers.Add(header);
                WebResponse response = request.GetResponse();
                HttpWebResponse httpResponse = (HttpWebResponse)response;

                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    Console.WriteLine(JsonConvert.DeserializeObject(reader.ReadToEnd()));
                }

            }
            catch (System.Net.WebException e)
            {
                HttpWebResponse response = e.Response as HttpWebResponse;
                switch ((int)response.StatusCode)
                {
                    case 401:
                        Console.WriteLine("Unauthorized, please check your credentials");
                        break;
                    case 403:
                        Console.WriteLine("Forbidden, please check your credentials");
                        break;
                    case 404:
                        Console.WriteLine("Invalid Url");
                        break;
                }
                Console.WriteLine("Code: " + (int)response.StatusCode);

                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    Console.WriteLine("Response: " + JsonConvert.DeserializeObject(reader.ReadToEnd()));

                }

            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }
    }
}
