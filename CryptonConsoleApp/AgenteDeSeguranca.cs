using CryptonPlugin;

namespace CryptonConsoleApp
{
    internal class AgenteDeSeguranca
    {
        internal static string Encriptar(string palavra)
        {
            //return Criptografia.Criptografar(palavra);
            return Embaralhador.RandomizeString(palavra);
        }

        internal static string Decriptar(string palavra)
        {
            //return Criptografia.Decriptografar(palavra);
            return Embaralhador.UnRandomizeString(palavra);
        }

        internal static string DecriptarUrlGoogleMaps(string rawUrl)
        {
            return Embaralhador.UnRandomizeGoogleMapsJavaScriptApiDecryptedUrl(rawUrl);
        }

        internal static string Exibir()
        {
            return Criptografia.Qwerty;
        }
    }
}
