using System.Security.Cryptography;
using System.Text;

namespace CryptonPlugin
{
    public class Criptografia
    {
        // Caracteres gerados pelo site Random.Org
        private static string qwerty = "xxxxxxxxxxxxxxxxxxxxxxx";
        private static string qwertyuiop = "xxxxxxxx";

        public static string Qwerty
        {
            get { return qwerty; }
            set { qwerty = value; }
        }

        /// <summary>
        /// Criptografa a string passada como parâmetro
        /// </summary>
        /// <param name="valor"></param>
        /// <returns>string criptografada</returns>
        public static string Criptografar(string valor)
        {
            //Cria o servico de criptografia
            var TDES = TripleDES.Create();

            byte[] bytesData = ASCIIEncoding.ASCII.GetBytes(valor);

            ICryptoTransform encrypt = TDES.CreateEncryptor(ASCIIEncoding.ASCII.GetBytes(qwerty), ASCIIEncoding.ASCII.GetBytes(qwertyuiop));

            MemoryStream memStreamEncryptedData = new MemoryStream();

            CryptoStream encStream = new CryptoStream(memStreamEncryptedData, encrypt, CryptoStreamMode.Write);

            try
            {

                //Encrypt the data, write it to the memory stream. 
                encStream.Write(bytesData, 0, bytesData.Length);
            }
            catch
            {
                throw new Exception("Erro ao escrever stream criptografado.");
            }

            encStream.FlushFinalBlock();

            string retorno = Convert.ToBase64String(memStreamEncryptedData.ToArray());
            encStream.Close();
            memStreamEncryptedData.Close();
            return retorno;
        }

        /// <summary>
        ///  Decriptografa a string passada como parametros
        /// </summary>
        /// <param name="input"></param>
        /// <returns>string Decriptografada</returns>
        public static string Decriptografar(string input)
        {
            var TDES = TripleDES.Create();

            //byte[] valor = Convert.FromBase64CharArray(input.ToCharArray(), 0, input.Length);
            byte[] valor = Convert.FromBase64String(input);

            byte[] fromEncrypt = new byte[valor.Length];

            ICryptoTransform encrypt = TDES.CreateDecryptor(ASCIIEncoding.ASCII.GetBytes(qwerty), ASCIIEncoding.ASCII.GetBytes(qwertyuiop));

            MemoryStream memStreamnDecryptedData = new MemoryStream(valor);

            CryptoStream encStream = new CryptoStream(memStreamnDecryptedData, encrypt, CryptoStreamMode.Read);

            try
            {
                //Encrypt the data, write it to the memory stream. 
                encStream.Read(fromEncrypt, 0, fromEncrypt.Length);
            }
            catch
            {
                throw new Exception("Erro ao ler stream decriptado.");
            }

            string retorno = ASCIIEncoding.ASCII.GetString(fromEncrypt);
            memStreamnDecryptedData.Close();
            //Tirar caracter de final de string
            return retorno.Replace('\0', ' ').Trim();
        }

        public static string GetConnectiongStringDecryptedPassword(string rawString)
        {
            string descriptPass;
            int start, end;
            start = rawString.IndexOf("Password=") + 9;
            end = rawString.IndexOf(";", start);
            end = (end == -1) ? rawString.Length : end;

            descriptPass = Decriptografar(rawString.Substring(start, end - start));

            return rawString.Substring(0, start) + descriptPass + rawString.Substring(end);
        }

        public static string GetGoogleMapsJavaScriptApiDecryptedUrl(string rawUrl)
        {
            string descriptPass;
            int start, end;
            start = rawUrl.IndexOf("key=") + 4;
            end = rawUrl.IndexOf("&", start);
            end = (end == -1) ? rawUrl.Length : end;

            descriptPass = Decriptografar(rawUrl.Substring(start, end - start));

            return rawUrl.Substring(0, start) + descriptPass + rawUrl.Substring(end);
        }
    }
}