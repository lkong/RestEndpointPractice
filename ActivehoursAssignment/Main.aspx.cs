using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace ActivehoursAssignment
{
    public partial class Main : System.Web.UI.Page
    {
        private string RESTWebServiceURL = @"http://localhost:13182/";
        private string parameters = @"?format=xml";
        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected void SpellCheckButton_Click(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            var request = (HttpWebRequest)WebRequest.Create(RESTWebServiceURL+@"/spellcheckrequest/" +InputTextBox.Text + parameters);
            request.Method = "GET";
            request.ContentLength = 0;
            request.ContentType = ContentType;
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                var responseValue = string.Empty;

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    var message = String.Format("Request failed. Received HTTP {0}", response.StatusCode);
                    throw new ApplicationException(message);
                }

                // grab the response
                using (var responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                        using (var reader = new StreamReader(responseStream))
                        {
                            responseValue = reader.ReadToEnd();
                        }
                }
                StringBuilder output = new StringBuilder();
                using (XmlReader reader = XmlReader.Create(new StringReader(responseValue)))
                {
                    XmlWriterSettings ws = new XmlWriterSettings();
                    ws.Indent = true;
                    using (XmlWriter writer = XmlWriter.Create(output, ws))
                    {

                        // Parse the file and display each of the nodes.
                        while (reader.Read())
                        {
                            switch (reader.NodeType)
                            {
                                case XmlNodeType.Element:
                                    writer.WriteStartElement(reader.Name);
                                    break;
                                case XmlNodeType.Text:
                                    writer.WriteString(reader.Value);
                                    break;
                                case XmlNodeType.XmlDeclaration:
                                case XmlNodeType.ProcessingInstruction:
                                    writer.WriteProcessingInstruction(reader.Name, reader.Value);
                                    break;
                                case XmlNodeType.Comment:
                                    writer.WriteComment(reader.Value);
                                    break;
                                case XmlNodeType.EndElement:
                                    writer.WriteFullEndElement();
                                    break;
                            }
                        }

                    }
                }
                SpellSuggestion.Text = output.ToString();
                    //return responseValue;
            }
        }
        protected void InsertWordButton_Click(object sender, EventArgs e)
        {
            RestClient client = new RestClient(RESTWebServiceURL);
            // client.Authenticator = new HttpBasicAuthenticator(username, password);

            RestRequest request = new RestRequest("spellcheckrequest/", Method.POST);
            request.AddParameter("Word", InputTextBox.Text); // adds to POST or URL querystring based on Method
                                                             // execute the request
            var response = client.Execute(request);
            var content = response.Content;
            SpellSuggestion.Text = "New word added";
        }
    }
}