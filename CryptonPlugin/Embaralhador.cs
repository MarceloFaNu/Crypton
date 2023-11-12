using System.Diagnostics;

namespace CryptonPlugin
{
    public class Embaralhador
    {
        private static readonly string _prefixoDeSeguranca = "xxxxxxx";

        /// <summary>
        /// Mix up a string (re-order character positions) using a random number set based from a seed value
        /// from https://stackoverflow.com/questions/40498271/scramble-descramble-text
        /// </summary>
        /// <param name="StringToScramble"></param>
        /// <returns>Scrambled String with seed encoded</returns>
        public static string RandomizeString(string StringToScramble)
        {
            StringToScramble = _prefixoDeSeguranca + StringToScramble;

            int seed = new Random().Next(0, 250);  // Get a random number (will be encoded into string , this will be the seed
            Random r = new Random(seed);
            char[] chars = StringToScramble.ToArray();

            for (int i = 0; i < StringToScramble.Length; i++)
            {
                int randomIndex = r.Next(0, StringToScramble.Length);   // Get the next random number from the sequence
                Debug.Print(randomIndex.ToString());
                char temp = chars[randomIndex]; // Copy the character value
                                                // Swap them around
                chars[randomIndex] = chars[i];
                chars[i] = temp;

            }
            // Add the seed            
            return new string(chars) + seed.ToString("X").PadLeft(2, '0');
        }

        /// <summary>Unscramble a string that was previously scrambled </summary>
        /// <param name="ScrambledString">String to Unscramble</param>
        /// /// <returns>Unscrambled String</returns>
        public static string UnRandomizeString(string ScrambledString)
        {
            try
            {
                //ScrambledString = ScrambledString.Substring(4);

                // Get the last character from the string as this is the random number seed
                int seed = int.Parse(ScrambledString.Substring(ScrambledString.Length - 2), System.Globalization.NumberStyles.HexNumber);
                Random r = new Random(seed);
                char[] scramChars = ScrambledString.Substring(0, ScrambledString.Length - 2).ToArray();
                List<int> swaps = new List<int>();
                for (int i = 0; i < scramChars.Length; i++)
                {
                    int randomIndex = r.Next(0, scramChars.Length);
                    swaps.Add(randomIndex);
                }
                for (int i = scramChars.Length - 1; i >= 0; i--)
                {
                    char temp = scramChars[swaps[i]];
                    scramChars[swaps[i]] = scramChars[i];
                    scramChars[i] = temp;
                }

                var result = new string(scramChars);
                result = result.Substring(8);
                return result;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static string UnRandomizeGoogleMapsJavaScriptApiDecryptedUrl(string rawUrl)
        {
            string descriptPass;
            int start, end;
            start = rawUrl.IndexOf("key=") + 4;
            end = rawUrl.IndexOf("&", start);
            end = (end == -1) ? rawUrl.Length : end;

            descriptPass = UnRandomizeString(rawUrl.Substring(start, end - start));

            return rawUrl.Substring(0, start) + descriptPass + rawUrl.Substring(end);
        }
    }
}
