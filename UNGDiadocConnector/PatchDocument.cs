using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Diadoc.Api;
using Diadoc.Api.Cryptography;
using Diadoc.Api.Proto;
using Diadoc.Api.Proto.Events;


namespace UNGDiadocConnector
{
    internal static class PatchDocument
    {
        public static string BoxID { get; set; } = Constants.DefaultToBoxId;
        public static string ClientID { get; set; } = Constants.DefaultClientId;
        public static void Run(string authToken,string docEntityId, string signPath, ILoger loger)
        {
            var crypt = new WinApiCrypt();
            var diadocApi = new DiadocApi(ClientID, Constants.DefaultApiUrl, crypt);

            var doclist = diadocApi.GetDocuments(authToken,new DocumentsFilter{BoxId = BoxID, FilterCategory = "Any.Inbound"});
            //Console.WriteLine($"doccount ={doclist.TotalCount} \n");
            loger.WriteToLog($"doccount ={doclist.TotalCount} \n");
           // var certificate = new X509Certificate2(File.ReadAllBytes(Constants.CertificatePath));
           // Console.WriteLine($"Algorithm : {certificate.SignatureAlgorithm}  ::  {certificate.GetKeyAlgorithm()}");
            


            foreach (var doc in doclist.Documents)
            {
                //Console.WriteLine($"-{doc.FileName} - EntityID : {doc.EntityId}");
                //var content = doc.SerializeToXml<Diadoc.Api.Proto.Documents.Document>();
                //var signature = crypt.Sign(content, certificate.GetRawCertData());

                if (doc.EntityId == docEntityId) //"8d96ce00-9d13-4176-8b1c-7b825ebb0232")
                {

                    var messagePatchToPost = new MessagePatchToPost()
                    {
                        BoxId = BoxID,
                        MessageId = doc.MessageId
                    };
                    //var signPath = @"C:\svkirtyanov\UNGDiadocConnector\TestDoc\SignFromESD.sig";   //"C:\svkirtyanov\UNGDiadocConnector\TestDoc\Sign.sig";
                    var signatureBase64 = File.ReadAllText(signPath);
                    byte[] signatureBytes = Convert.FromBase64String(signatureBase64);
                    var signature = new DocumentSignature()
                    {
                        ParentEntityId = doc.EntityId,                         
                        Signature = signatureBytes
                    };

                    
                   // signature.LoadFromFile(signPath);

                   

                    messagePatchToPost.AddSignature(signature);

                    var response = diadocApi.PostMessagePatch(authToken, messagePatchToPost);
                    loger.WriteToLog($"К документу {doc.EntityId} добавлена подпись {signature.ToString()} ");
                }

            }
        }
    }
}
