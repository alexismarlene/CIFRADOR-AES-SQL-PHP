using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//Libreria para la Encriptacion
using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.IO;

namespace EncriptacionAES.Clases
{
    public class EncriptadorAES
    {
        #region Static Functions

        protected DataTable VectorInicial2;
        /// <summary>

        /// Encrypts a string

        /// </summary>

        /// <param name="PlainText">Text to be encrypted</param>

        /// <param name="Password">Password to encrypt with</param>

        /// <param name="Salt">Salt to encrypt with</param>

        /// <param name="HashAlgorithm">Can be either SHA1 or MD5</param>

        /// <param name="PasswordIterations">Number of iterations to do</param>

        /// <param name="InitialVector">Needs to be 16 ASCII characters long</param>

        /// <param name="KeySize">Can be 128, 192, or 256</param>

        /// <returns>An encrypted string</returns>

        public static string Encrypt(string PlainText, string Password,

        string Salt = "Kosher", string HashAlgorithm = "SHA1",

        int PasswordIterations = 2, string InitialVector = "OFRna73m*aze01xY",

        int KeySize = 256)
        {

            if (string.IsNullOrEmpty(PlainText))

                return "";

            byte[] InitialVectorBytes = Encoding.ASCII.GetBytes(InitialVector);
                        
            DataTable VectorBytes = new DataTable();
            DataTable SalidaVectores = new DataTable();
            DataTable SalidaTexto = new DataTable();
            DataTable LlavesByte = new DataTable();
            
            VectorBytes.Columns.Add("Valores");
            SalidaVectores.Columns.Add("Valores");
            SalidaTexto.Columns.Add("Valores");
            LlavesByte.Columns.Add("Valores");
           
            for (int i = 0; i < InitialVector.Length; i++)
            {
                VectorBytes.Rows.Add(InitialVectorBytes[i]);
            }

            VectorInicial = VectorBytes;
            
            byte[] SaltValueBytes = Encoding.ASCII.GetBytes(Salt);

            for (int i = 0; i < SaltValueBytes.Length; i++)
            {
                SalidaVectores.Rows.Add(SaltValueBytes[i]);
            }

            SalidadeVectores = SalidaVectores;

            byte[] PlainTextBytes = Encoding.UTF8.GetBytes(PlainText);

            for (int i = 0; i < PlainTextBytes.Length; i++)
            {
                SalidaTexto.Rows.Add(PlainTextBytes[i]);
            }

            SalidadeTexto = SalidaTexto;

            PasswordDeriveBytes DerivedPassword = new PasswordDeriveBytes(Password, SaltValueBytes, HashAlgorithm, PasswordIterations);

            byte[] KeyBytes = DerivedPassword.GetBytes(KeySize / 8);

            for (int i = 0; i < KeyBytes.Length; i++)
            {
                LlavesByte.Rows.Add(KeyBytes[i]);
            }

            LlavesEncriptacion = LlavesByte;

            RijndaelManaged SymmetricKey = new RijndaelManaged();

            SymmetricKey.Mode = CipherMode.CBC;

            byte[] CipherTextBytes = null;

            using (ICryptoTransform Encryptor = SymmetricKey.CreateEncryptor(KeyBytes, InitialVectorBytes))
            {

                using (MemoryStream MemStream = new MemoryStream())
                {

                    using (CryptoStream CryptoStream = new CryptoStream(MemStream, Encryptor, CryptoStreamMode.Write))
                    {

                        CryptoStream.Write(PlainTextBytes, 0, PlainTextBytes.Length);

                        CryptoStream.FlushFinalBlock();

                        CipherTextBytes = MemStream.ToArray();

                        MemStream.Close();

                        CryptoStream.Close();

                    }

                }

            }

            SymmetricKey.Clear();

            return Convert.ToBase64String(CipherTextBytes);

        }
        

        /// <summary>

        /// Decrypts a string

        /// </summary>

        /// <param name="CipherText">Text to be decrypted</param>

        /// <param name="Password">Password to decrypt with</param>

        /// <param name="Salt">Salt to decrypt with</param>

        /// <param name="HashAlgorithm">Can be either SHA1 or MD5</param>

        /// <param name="PasswordIterations">Number of iterations to do</param>

        /// <param name="InitialVector">Needs to be 16 ASCII characters long</param>

        /// <param name="KeySize">Can be 128, 192, or 256</param>

        /// <returns>A decrypted string</returns>

        public static string Decrypt(string CipherText, string Password,

        string Salt = "Kosher", string HashAlgorithm = "SHA1",

        int PasswordIterations = 2, string InitialVector = "OFRna73m*aze01xY",

        int KeySize = 256)
        {


            if (string.IsNullOrEmpty(CipherText))

                return "";

            byte[] InitialVectorBytes = Encoding.ASCII.GetBytes(InitialVector);

            byte[] SaltValueBytes = Encoding.ASCII.GetBytes(Salt);

            byte[] CipherTextBytes = Convert.FromBase64String(CipherText);

            PasswordDeriveBytes DerivedPassword = new PasswordDeriveBytes(Password, SaltValueBytes, HashAlgorithm, PasswordIterations);

            byte[] KeyBytes = DerivedPassword.GetBytes(KeySize / 8);

            RijndaelManaged SymmetricKey = new RijndaelManaged();

            SymmetricKey.Mode = CipherMode.CBC;

            byte[] PlainTextBytes = new byte[CipherTextBytes.Length];

            int ByteCount = 0;

            try
            {
                using (ICryptoTransform Decryptor = SymmetricKey.CreateDecryptor(KeyBytes, InitialVectorBytes))
                {

                    using (MemoryStream MemStream = new MemoryStream(CipherTextBytes))
                    {

                        using (CryptoStream CryptoStream = new CryptoStream(MemStream, Decryptor, CryptoStreamMode.Read))
                        {



                            ByteCount = CryptoStream.Read(PlainTextBytes, 0, PlainTextBytes.Length);

                            MemStream.Close();

                            CryptoStream.Close();

                        }

                    }

                }
            }
            catch (Exception ex)
            {
                return "La Contraseña para la Desencriptar no es la Correcta";
            }



            SymmetricKey.Clear();

            return Encoding.UTF8.GetString(PlainTextBytes, 0, ByteCount);
            
        }


        #endregion

        #region Vectores de Incializacion
        public static DataTable VectorInicial { get; set; }
        public static DataTable SalidadeVectores { get; set; }
        public static DataTable SalidadeTexto { get; set; }
        public static DataTable LlavesEncriptacion { get; set; }
          
        #endregion
    }
}