using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Web;
using System.IO;

namespace OA.Data.Helper
{
    public static class RSACryptography
    {
        public class rsaKeys
        {
            public string publicKeyString;
            public string privateKeyString;
        }
        public static rsaKeys GenerateRSAKeys()
        {
            RSACryptoServiceProvider rSA = new RSACryptoServiceProvider(2048); //2048 
            var publicKey = rSA.ExportParameters(false);    //Generate Public Key
            var privateKey = rSA.ExportParameters(true);    //Generate Private Key

            var stringWriter1 = new System.IO.StringWriter();
            var xmlSerializer1 = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
            xmlSerializer1.Serialize(stringWriter1, publicKey);
            // return stringWriter.ToString();

            var stringWriter2 = new System.IO.StringWriter();
            var xmlSerializer2 = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
            xmlSerializer2.Serialize(stringWriter2, privateKey);

            rsaKeys rSAkeys = new rsaKeys();
            rSAkeys.publicKeyString = stringWriter1.ToString();
            rSAkeys.privateKeyString = stringWriter2.ToString();
            return rSAkeys;
        }
        public static string RSAEncrypt(string textToEncrypt)
        {
            //string publicKeyString = ConfigurationManager.AppSettings["RSAPublicKey"];

            string publicKeyLocation = HttpContext.Current.Server.MapPath("~/Content/rsaKeys/rsaPublicKey.xml");
            string publicKeyString = File.ReadAllText(@publicKeyLocation);

            var bytesToEncrypt = Encoding.UTF8.GetBytes(textToEncrypt);
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                try
                {
                    rsa.FromXmlString(publicKeyString.ToString());
                    var encryptedData = rsa.Encrypt(bytesToEncrypt, true);
                    var base64Encrypted = Convert.ToBase64String(encryptedData);
                    return base64Encrypted;
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }
        public static string RSADecrypt(string textToDecrypt)
        {
            //string privateKeyString = ConfigurationManager.AppSettings["RSAPrivateKey"];

            string privateKeyLocation = HttpContext.Current.Server.MapPath("~/Content/rsaKeys/rsaPrivateKey.xml");
            string privateKeyString = File.ReadAllText(@privateKeyLocation);

            var bytesToDescrypt = Encoding.UTF8.GetBytes(textToDecrypt);
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                try
                {
                    // server decrypting data with private key                    
                    rsa.FromXmlString(privateKeyString);

                    var resultBytes = Convert.FromBase64String(textToDecrypt);
                    var decryptedBytes = rsa.Decrypt(resultBytes, true);
                    var decryptedData = Encoding.UTF8.GetString(decryptedBytes);
                    return decryptedData.ToString();
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }
        private static string GenerateTestString()
        {
            Guid opportinityId = Guid.NewGuid();
            Guid systemUserId = Guid.NewGuid();
            string currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("opportunityid={0}", opportinityId.ToString());
            sb.AppendFormat("&systemuserid={0}", systemUserId.ToString());
            sb.AppendFormat("&currenttime={0}", currentTime);

            return sb.ToString();
        }
    }
}
