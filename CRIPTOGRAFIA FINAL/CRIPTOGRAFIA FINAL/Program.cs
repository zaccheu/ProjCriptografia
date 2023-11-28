
            using System;
            using System.Collections.Generic;
            using System.Linq;
            using System.Security.Permissions;
            using System.Text;
            using System.Text.RegularExpressions;
            using System.Threading.Tasks;

namespace projetofaculfinal
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string textoescrito, textodesc = "", textodesctotal = "", textocriptoatual = "", textocriptototal = "";
            string confirmacao;
            int determinante;
            int[,] chave = new int[2, 2];
            int[,] chaveinversa = new int[2, 2];
            int tamanhomatriz = 2, indicemaximo = tamanhomatriz - 1, indicemaxgrupo = 2;
            int k = 0, l = 0;
            bool flag = true;
            char[] alfabeto = new char[]
            {
             '\\', 'A', 'a', 'B', 'b', 'C', 'c', 'D', 'd', 'E', 'e', 'F', 'f', 'G', 'g', 'H', 'h', 'I', 'i',
             'J', 'j', 'K', 'k', 'L', 'l', 'M', 'm', 'N', 'n', 'O', 'o', 'P', 'p', 'Q', 'q', 'R', 'r', 'S', 's',
             'T', 't', 'U', 'u', 'V', 'v', 'W', 'w', 'X', 'x', 'Y', 'y', 'Z', 'z', ',', '.', ':', ';', '!', '?',
             '+', '-', '*', '/', '@', '#', '$', '%', '&', '(', ')', '_', '=', '[', ']', '{', '}', '~', '^', 'ª',
             '"', ' ', '£', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '<', '>', 'Á', 'á', 'À', 'à', 'Ã',
             'ã', 'Â', 'â', 'É', 'é', 'Ê', 'ê', 'Í', 'í', 'Ó', 'ó', 'Õ', 'õ', 'Ô', 'ô', 'Ú', 'ú', 'Ç', 'ç', 'º',
             '°'
            };





            Random random = new Random();

            int a, b, c, d;
            do
            {
                textocriptoatual = "";
                textocriptototal = "";
                textodesc = "";
                textodesctotal = "";
                Console.Clear();
                Console.Write("Digite um texto qualquer: "); //Palavra a ser criptografada
                textoescrito = Console.ReadLine();

                do
                {
                    a = random.Next(1, 10000); // Valor aleatório para o elemento a da matriz
                    b = random.Next(-10000, 10000); // Valor aleatório para o elemento b da matriz
                    c = random.Next(-10000, 10000); // Valor aleatório para o elemento c da matriz
                    d = (b + 1) / a; // Cálculo do elemento d para que o determinante seja igual a 1

                    determinante = a * d - b * c; // Cálculo do determinante
                }
                while (determinante != 1); // Repete até que o determinante seja igual a 1

                chave[0, 0] = a; chave[1, 0] = b; chave[0, 1] = c; chave[1, 1] = d;

                int fatordeinversao = 1 / determinante;


                //Exibir a Matriz Chave
                Console.WriteLine("\nMatriz Chave: ");
                for (int i = 0; i < tamanhomatriz; i++)
                {
                    for (int j = 0; j < tamanhomatriz; j++)
                    {
                        Console.Write(chave[i, j] + " ");
                    }
                    Console.WriteLine();
                }

                Console.ReadKey();

                for (int i = 0; i < textoescrito.Length; i += 4)
                {
                    // Obtém um grupo de 4 letras
                    string grupo = textoescrito.Substring(i, Math.Min(4, textoescrito.Length - i)).PadRight(4, '\\');

                    // Gera uma nova matriz para o grupo de 4 letras
                    int[,] matriz = new int[tamanhomatriz, tamanhomatriz];
                    int[,] matrizcripto = new int[2, 2];
                    for (int linha = 0; linha < tamanhomatriz; linha++) //Percorrendo as matrizes
                    {
                        for (int coluna = 0; coluna < tamanhomatriz; coluna++)
                        {
                            flag = true;
                            k = 0;
                            while (flag) //Transformando as letras em seus índices
                            {
                                if (alfabeto[k] == grupo[l])
                                {
                                    matriz[linha, coluna] = k;
                                    flag = false; //Bateu a letra com o índice
                                }
                                else
                                {
                                    k++; //Passar para a próxima letra do alfabeto
                                }

                            }
                            if (l <= indicemaxgrupo)
                            {
                                l++;
                            }
                            else
                            {
                                l = 0;
                            }
                        }
                    }

                    // Mostra a matriz do grupo na tela
                    Console.WriteLine($"\nMatriz para o grupo {grupo}");
                    for (int linha = 0; linha < tamanhomatriz; linha++)
                    {
                        for (int coluna = 0; coluna < tamanhomatriz; coluna++)
                        {
                            Console.Write(matriz[linha, coluna] + " ");
                        }
                        Console.WriteLine();
                    }
                    // Mostra a matriz sendo multiplicada na tela
                    Console.WriteLine($"\nMultiplicada pela matriz chave");
                    for (int linha = 0; linha < tamanhomatriz; linha++)
                    {
                        for (int coluna = 0; coluna < tamanhomatriz; coluna++)
                        {
                            Console.Write(chave[linha, coluna] + " ");
                        }
                        Console.WriteLine();
                    }
                    //Multiplicar as matrizes
                    for (int linha = 0; linha < tamanhomatriz; linha++)
                    {
                        for (int coluna = 0; coluna < tamanhomatriz; coluna++)
                        {
                            k = 0;
                            do
                            {
                                matrizcripto[linha, coluna] += (chave[linha, k] * matriz[k, coluna]);
                                k++;
                            }
                            while (k < tamanhomatriz);

                        }
                    }
                    Console.Write("\nResultado: \n");

                    Console.WriteLine($"\nMatriz criptografada para o grupo {grupo}");
                    for (int linha = 0; linha < tamanhomatriz; linha++)
                    {
                        for (int coluna = 0; coluna < tamanhomatriz; coluna++)
                        {
                            Console.Write(matrizcripto[linha, coluna] + " ");
                        }
                        Console.WriteLine();
                    }

                    //Transformar os números criptografados em letras
                    for (int linha = 0; linha < tamanhomatriz; linha++)
                    {
                        for (int coluna = 0; coluna < tamanhomatriz; coluna++)
                        {
                            while (matrizcripto[linha, coluna] > alfabeto.Length - 1)
                            {
                                matrizcripto[linha, coluna] -= alfabeto.Length;
                            }
                            while (matrizcripto[linha, coluna] < 0)
                            {
                                matrizcripto[linha, coluna] += alfabeto.Length;
                            }
                        }
                    }
                    for (int linha = 0; linha < tamanhomatriz; linha++) //Percorrendo as matrizes
                    {
                        for (int coluna = 0; coluna < tamanhomatriz; coluna++)
                        {
                            flag = true;
                            k = 0;
                            while (flag) //Transformando as letras em seus índices
                            {
                                if (k == matrizcripto[linha, coluna])
                                {
                                    textocriptototal += alfabeto[k];
                                    textocriptoatual += alfabeto[k];
                                    flag = false; //Bateu a letra com o índice
                                }
                                else
                                {
                                    k++; //Passar para a próxima letra do alfabeto
                                }

                            }
                        }
                    }
                    Console.Write("\nModulada em 122: \n");
                    for (int linha = 0; linha < tamanhomatriz; linha++)
                    {
                        for (int coluna = 0; coluna < tamanhomatriz; coluna++)
                        {
                            Console.Write(matrizcripto[linha, coluna] + " ");
                        }
                        Console.WriteLine();
                    }

                    Console.WriteLine($"\nAs letras criptografadas do grupo {grupo} são: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(textocriptoatual);
                    Console.ResetColor();

                    textocriptoatual = "";
                    Console.WriteLine();
                    Console.WriteLine("\nDigite uma tecla para exibir o próximo grupo...\n");
                    Console.ReadKey();
                    Console.Clear();
                }//<--------- AQUI COMEÇA UM NOVO GRUPO
                 //
                 //
                 //
                 //
                 //
                 //
                 //
                Console.WriteLine("O texto criptografado completo é: ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(textocriptototal);
                Console.ResetColor();

                Console.WriteLine("\nCopie o texto acima e então aperte qualquer botão para avançar...\n");
                Console.ReadKey();


                do
                {
                    

                    Console.WriteLine("\nTem certeza que copiou? [s] [n]: \n");
                    confirmacao = Console.ReadLine().ToLower();
                    if (!(confirmacao.StartsWith("s") || confirmacao.StartsWith("n")))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("\nResposta inválida!\n");
                        Console.ResetColor();
                        continue;
                    }


                } while (!confirmacao.StartsWith("s"));

                Console.Clear();

                Console.WriteLine("Insira o texto que sofrerá a descriptografia: ");
                string palavracriptodigitada = Console.ReadLine();
                Console.Clear();

                // Inverter Matriz!
                // Matriz dos cofatores
                chaveinversa[0, 0] = chave[1, 1];
                chaveinversa[0, 1] = -chave[0, 1];
                chaveinversa[1, 0] = -chave[1, 0];
                chaveinversa[1, 1] = chave[0, 0];

                // Multiplicar os itens por 1/det
                for (int linha = 0; linha < tamanhomatriz; linha++)
                {
                    for (int coluna = 0; coluna < tamanhomatriz; coluna++)
                    {
                        chaveinversa[linha, coluna] *= fatordeinversao;
                    }
                }

                //Exibir a inversa da chave
                Console.WriteLine("Matriz Inversa da Chave ");
                for (int linha = 0; linha < tamanhomatriz; linha++)
                {
                    for (int coluna = 0; coluna < tamanhomatriz; coluna++)
                    {
                        Console.Write(chaveinversa[linha, coluna] + " ");
                    }
                    Console.WriteLine();
                }

                Console.ReadKey();

                // Segundo FORZAO
                //
                //
                //
                //
                // DESCRIPTOGRAFIA
                //
                //
                //
                // Segundo FORZAO

                for (int i = 0; i < textoescrito.Length; i += 4)
                {

                    int[,] matrizcripto = new int[2, 2];
                    int[,] matrizdesc = new int[2, 2];

                    // Obtém um grupo de 4 letras
                    string grupo = palavracriptodigitada.Substring(i, Math.Min(4, palavracriptodigitada.Length - i));

                    // Gera uma nova matriz para o grupo de 4 letras
                    int[,] matrizletrascripto = new int[tamanhomatriz, tamanhomatriz];

                    for (int linha = 0; linha < tamanhomatriz; linha++) //Percorrendo as matrizes
                    {
                        for (int coluna = 0; coluna < tamanhomatriz; coluna++)
                        {
                            flag = true;
                            k = 0;
                            while (flag) //Transformando as letras em seus índices
                            {
                                if (alfabeto[k] == grupo[l])
                                {
                                    matrizletrascripto[linha, coluna] = k;
                                    flag = false; //Bateu a letra com o índice
                                }
                                else
                                {
                                    k++; //Passar para a próxima letra do alfabeto
                                }

                            }
                            if (l <= indicemaxgrupo)
                            {
                                l++;
                            }
                            else
                            {
                                l = 0;
                            }
                        }
                    }

                    // Escreve a matriz na tela
                    Console.WriteLine($"\nMatriz para o grupo {grupo}\n");
                    for (int linha = 0; linha < tamanhomatriz; linha++)
                    {
                        for (int coluna = 0; coluna < tamanhomatriz; coluna++)
                        {
                            Console.Write(matrizletrascripto[linha, coluna] + " ");
                        }
                        Console.WriteLine();
                    }

                    // Mostra a matriz sendo multiplicada na tela
                    Console.WriteLine($"\nMultiplicada pela matriz chave");
                    for (int linha = 0; linha < tamanhomatriz; linha++)
                    {
                        for (int coluna = 0; coluna < tamanhomatriz; coluna++)
                        {
                            Console.Write(chaveinversa[linha, coluna] + " ");
                        }
                        Console.WriteLine();
                    }

                    //Multiplicar C-1 por M'
                    for (int linha = 0; linha < tamanhomatriz; linha++)
                    {
                        for (int coluna = 0; coluna < tamanhomatriz; coluna++)
                        {
                            k = 0;
                            do
                            {
                                matrizdesc[linha, coluna] += (chaveinversa[linha, k] * matrizletrascripto[k, coluna]);
                                k++;
                            }
                            while (k < tamanhomatriz);


                        }
                    }
                    Console.Write("\nResultado: \n");
                    //Exibir a matriz descriptografada do grupo
                    Console.WriteLine($"Matriz Descriptografada do grupo: {grupo}");
                    for (int linha = 0; linha < tamanhomatriz; linha++)
                    {
                        for (int coluna = 0; coluna < tamanhomatriz; coluna++)
                        {
                            Console.Write(matrizdesc[linha, coluna] + " ");
                        }
                        Console.WriteLine();
                    }
                    //Tranhsformar a matriz descriptografada em suas letras (mod120)
                    for (int linha = 0; linha < tamanhomatriz; linha++)
                    {
                        for (int coluna = 0; coluna < tamanhomatriz; coluna++)
                        {
                            while (matrizdesc[linha, coluna] > alfabeto.Length - 1)
                            {
                                matrizdesc[linha, coluna] -= alfabeto.Length;
                            }
                            while (matrizdesc[linha, coluna] < 0)
                            {
                                matrizdesc[linha, coluna] += alfabeto.Length;
                            }
                        }
                    }
                    Console.Write("\nModulada em 122: \n");
                    for (int linha = 0; linha < tamanhomatriz; linha++)
                    {
                        for (int coluna = 0; coluna < tamanhomatriz; coluna++)
                        {
                            Console.Write(matrizdesc[linha, coluna] + " ");
                        }
                        Console.WriteLine();
                    }
                    //Transformando as letras em seus índices
                    textodesc = "";

                    for (int linha = 0; linha < tamanhomatriz; linha++) //Percorrendo as matrizes
                    {
                        for (int coluna = 0; coluna < tamanhomatriz; coluna++)
                        {
                            flag = true;
                            k = 0;
                            while (flag) //Transformando as letras em seus índices
                            {
                                if (k == matrizdesc[linha, coluna])
                                {
                                    textodesc += alfabeto[k];
                                    textodesctotal += alfabeto[k];
                                    flag = false; //Bateu a letra com o índice
                                }
                                else
                                {
                                    k++; //Passar para a próxima letra do alfabeto
                                }

                            }
                        }
                    }
                    Console.WriteLine($"O texto descriptografada do bloco {grupo} é: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(textodesc);
                    Console.ResetColor();
                    Console.ReadKey();
                    Console.Clear();
                }//<--------- AQUI COMEÇA UM NOVO GRUPO
                Console.WriteLine($"\nA mensagem original é: {textodesctotal.Replace("\\", "")}.");

                Console.WriteLine();

                do
                {


                    Console.WriteLine("\nDeseja criptografar outra palavra? [s] [n]: \n");
                    confirmacao = Console.ReadLine().ToLower();
                    if (!(confirmacao.StartsWith("s") || confirmacao.StartsWith("n")))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Resposta inválida!");
                        Console.ResetColor();
                        continue;
                    }
                    else if (confirmacao.StartsWith("n"))
                    {
                        Environment.Exit(0);
                    }

                } while (!confirmacao.StartsWith("s"));
                Console.Clear();
            } while (confirmacao.StartsWith("s"));

        }
    }
}

    
