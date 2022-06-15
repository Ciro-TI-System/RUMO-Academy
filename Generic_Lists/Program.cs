using System;
using System.Collections.Generic;


namespace ListasGenericas
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" \n ", "\n", "\n");
            Console.WriteLine(" ================================================ ");
            Console.WriteLine("            EXEMPLO DE UMA LISTA GENÉRICA         ");
            Console.WriteLine(" =================================================");
            Console.WriteLine(" \n ", "\n");
            Console.WriteLine(" Primeiro digite o nome de 5 programadores: ");
            Console.WriteLine(" \n ");
            List<string> nomesProgramadores = new List<string>();

            int quantidadeMaximaPerguntas = 5;

            for (int i = 0; i < quantidadeMaximaPerguntas; i++)
            {
                Console.WriteLine(" Qual o nome do programador?");
                string nomeProgramador = Console.ReadLine();

                nomesProgramadores.Add(nomeProgramador);
            }

            Console.WriteLine(" \n ", "\n");
            Console.WriteLine(" Veja esta lista de programadores famosos! ");
            Console.WriteLine(" \n ");

            nomesProgramadores.Add(" Ray Tomlinson, que inventou o e-mail");
            nomesProgramadores.Add(" Richard Matthew Stallman, que defende o software livre");
            nomesProgramadores.Add(" James Gosling, que criou o Java");
            nomesProgramadores.Add(" Rasmus Lerdorf, que escreveu a primeira versão do PHP");
            nomesProgramadores.Add(" Bill Gates, que revolucionou o computador pessoal");
            nomesProgramadores.Add(" Donald Knuth, o cientista da computação");
            nomesProgramadores.Add(" Ada Lovelace e os números de Bernoulli");
            nomesProgramadores.Add(" Hedy Lamarr, criadora do salto de frequência");
            nomesProgramadores.Add(" Grace Hopper, uma PhD em matemática");

            foreach (var nomeProgramador in nomesProgramadores)
            {
                Console.WriteLine(nomeProgramador);
            }

            // espera entrada do teclado para encerrar um aplicativo
            Console.WriteLine(" \n ", "\n");
            Console.WriteLine(" Pressione ENTER para fechar o programa! ");
            Console.ReadKey();
            Console.WriteLine(" \n ", "\n");
        }
    }
}
