using System.IO.Pipes;
using System.Net;
using System.Xml;
using System.Xml.Linq;

namespace system_xml_linq_xcontainer_element_returned_null
{
    internal class Program
    {
        private static string sampleXml = @"<response result=""0"">
    <prov>
        <getStatus result=""0"">
            <pay date=""2023-02-08T19:44:33+03:00"" fatal=""false"" id=""8022140013003"" result=""0"" status=""2"" uid=""26775263057008"" value-date=""2023-02-08T19:44:40+03:00"">
            </pay>
        </getStatus>
    </prov>
</response>";
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            XmlDocument doc = postXMLData(sampleXml);
            XElement e = XElement.Load(new XmlNodeReader(doc));
            var elements = e.Elements().ToList();
            Console.WriteLine(e); //here ok

            var response = e.Element("response");
            var prov = e.Element("prov");
            var getStatus = prov.Element("getStatus");
            var pay = getStatus.Element("pay");
            var attributes = pay.Attributes();
            var filter = attributes.Where(a => a.Name.LocalName.StartsWith("f"));
            
            var works = e.Element("prov").Element("getStatus").Element("pay").Attributes().Where(a => a.Name.LocalName.StartsWith("f"));
            ////Exception
            //IEnumerable<XAttribute> attrs1 = e.Element("response").Element("prov").Element("getStatus").Element("pay")?.Attributes().Where(a => a.Name.LocalName.StartsWith("f"));
            //Console.WriteLine(attrs1);
        }

        public static XmlDocument postXMLData(string xml)
        {
            //var request = (HttpWebRequest)WebRequest.Create(Requests.url);
            //byte[] bytes;
            //bytes = System.Text.Encoding.ASCII.GetBytes(xml);
            //request.ContentType = "text/xml; encoding='utf-8'";
            //request.ContentLength = bytes.Length;
            //request.Method = "POST";
            //Stream requestStream = request.GetRequestStream();
            //requestStream.Write(bytes, 0, bytes.Length);
            //requestStream.Close();
            //HttpWebResponse response;
            //response = (HttpWebResponse)request.GetResponse();

            //if (response.StatusCode == HttpStatusCode.OK)
            //{
            //    using (var streamReader = new StreamReader(response.GetResponseStream()))
            //    {
            //        var responseText = streamReader.ReadToEnd();
                    var result = new XmlDocument();
                    result.LoadXml(xml);
                    return result;
            //    }
            //}

            //throw new Exception();
        }
    }

}