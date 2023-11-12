using System.Text;

namespace CryptonConsoleApp
{
    internal class Menu
    {
        public static void Run()
        {
            Console.Clear();
            var opcaoEscolhida = OpcoesEnum.Nada;

            while (opcaoEscolhida != OpcoesEnum.Sair)
            {
                Console.Write(MostraMenu());
                opcaoEscolhida = ObtemOpcaoEscolhida();

                if (opcaoEscolhida == OpcoesEnum.Encriptar)
                {
                    string palavra = ObtemPalavraDigitada();
                    Console.WriteLine("Palavra obtida: " + AgenteDeSeguranca.Encriptar(palavra));
                }
                else if (opcaoEscolhida == OpcoesEnum.Decriptar)
                {
                    string palavra = ObtemPalavraDigitada();
                    Console.WriteLine("Palavra obtida: " + AgenteDeSeguranca.Decriptar(palavra));
                }
                else if (opcaoEscolhida == OpcoesEnum.DecriptarUrlGoogleMaps)
                {
                    string palavra = ObtemPalavraDigitada();
                    Console.WriteLine("Palavra obtida: " + AgenteDeSeguranca.DecriptarUrlGoogleMaps(palavra));
                }
                else if (opcaoEscolhida == OpcoesEnum.Exibir)
                {
                    Console.WriteLine("\nChave privada: " + AgenteDeSeguranca.Exibir());
                }

                Console.WriteLine();
            }
        }

        private static string MostraMenu()
        {
            StringBuilder menu = new StringBuilder();
            menu.AppendLine("====================");
            menu.AppendLine("  MENU DE OPÇÕES");
            menu.AppendLine("====================");
            menu.AppendLine();
            menu.AppendLine("1 = Encriptar palavra");
            menu.AppendLine("2 = Decriptar palavra");
            menu.AppendLine("3 = Decriptar Url Google Maps");
            menu.AppendLine("4 = Exibir chave privada");
            menu.AppendLine("5 = Sair");
            menu.AppendLine();
            menu.Append("Escolha uma opção: ");

            return menu.ToString();
        }

        private static OpcoesEnum ObtemOpcaoEscolhida()
        {
            var opcaoEscolhida = Console.ReadKey();

            var opcaoValida = int.TryParse(opcaoEscolhida.KeyChar.ToString(), out int opcaoNumerica);

            while (!opcaoValida || (opcaoNumerica < 1 || opcaoNumerica > 5))
            {
                Console.WriteLine("\nOpção inválida.");
                Console.WriteLine("Opções disponíveis: de 1 a 4.");
                Console.WriteLine();
                Console.Write("Escolha uma opção válida: ");
                opcaoEscolhida = Console.ReadKey();

                opcaoValida = int.TryParse(opcaoEscolhida.KeyChar.ToString(), out opcaoNumerica);
            }

            return (OpcoesEnum)opcaoNumerica;
        }

        private static string ObtemPalavraDigitada()
        {
            Console.Write("\nDigite a palavra desejada: ");
            var palavraDigitada = Console.ReadLine() ?? string.Empty;
            return palavraDigitada.Trim();
        }
    }
}
