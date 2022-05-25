using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;   
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace Common
{
    /// <summary>
    /// Exposing Encryption functions
    /// </summary>
    public class CCrypt
    {
        /// <summary>
        /// Encrypt Message string in HEXA format
        /// </summary>
        /// <param name="str_msg">Message string to be encrypted</param>
        /// <param name="cert">Certificate to be used to encrypt</param>
        /// <returns></returns>
        public static string Encrypt_Message(string str_msg, X509Certificate2 cert)
        {
            //Gets the public key.
            string public_key = cert.GetPublicKeyString();
            // Encrypts the message using public key.
            var providerSender = (RSACryptoServiceProvider)cert.PublicKey.Key;
            var plainSender = Encoding.ASCII.GetBytes(str_msg);
            var cipher = providerSender.Encrypt(plainSender, false);
            //string e_msg = Encoding.Default.GetString(cipher);
            string e_msg = Convert.ToBase64String(cipher);
            return e_msg;
        }


        /// <summary>
        /// Decrypt Message string in HEXA format
        /// </summary>
        /// <param name="encryptd_message">Message string to be decrypted</param>
        /// <param name="cert">Certificate to be used to decrypt</param>
        /// <returns></returns>
        public static string Decrypt_Message(string encryptd_message, X509Certificate2 cert)
        {
            // Decrypts the Encrypted message using the private key.
            var providerReceiver = (RSACryptoServiceProvider)cert.PrivateKey;
            //var plainReceiver = providerReceiver.Decrypt(Encoding.Default.GetBytes(encryptd_message), false);
            var plainReceiver = providerReceiver.Decrypt(Convert.FromBase64String(encryptd_message), false);
            string decryptd_message = Encoding.Default.GetString(plainReceiver);
            return decryptd_message;
        }
    }
}
