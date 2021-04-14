using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuNET
{
    class Program
    {
        static void Main(string[] args)
        {
            string sudokuStringOriginal = "1 3 2 5 7 9 4 6 8\n" +
                                          "4 9 8 2 6 1 3 7 5\n" +
                                          "7 5 6 3 8 4 2 1 9\n" +
                                          "6 4 3 1 5 8 7 9 2\n" +
                                          "5 2 1 7 9 3 8 4 6\n" +
                                          "9 8 7 4 2 6 5 3 1\n" +
                                          "2 1 4 9 3 5 6 8 7\n" +
                                          "3 6 5 8 1 7 9 2 4\n" +
                                          "8 7 9 6 4 2 1 5 3\n";

            #region Outros Sudokus
            string sudokuStringLinhaRepetida = "1 3 2 5 7 9 4 6 8\n" +
                                               "4 9 8 2 5 1 3 7 5\n" +
                                               "7 5 6 3 8 4 2 1 9\n" +
                                               "6 4 3 1 6 8 7 9 2\n" +
                                               "5 2 1 7 9 3 8 4 6\n" +
                                               "9 8 7 4 2 6 5 3 1\n" +
                                               "2 1 4 9 3 5 6 8 7\n" +
                                               "3 6 5 8 1 7 9 2 4\n" +
                                               "8 7 9 6 4 2 1 5 3\n";

            string sudokuStringColunaRepetida = "1 3 2 5 7 9 4 6 8\n" +
                                                "4 9 8 2 6 1 3 7 5\n" +
                                                "8 5 6 3 7 4 2 1 9\n" +
                                                "6 4 3 1 5 8 7 9 2\n" +
                                                "5 2 1 7 9 3 8 4 6\n" +
                                                "9 8 7 4 2 6 5 3 1\n" +
                                                "2 1 4 9 3 5 6 8 7\n" +
                                                "3 6 5 8 1 7 9 2 4\n" +
                                                "8 7 9 6 4 2 1 5 3\n";

            string sudokuStringBlocoRepetido = "1 3 2 5 7 9 4 6 8\n" +
                                               "4 9 8 2 6 1 3 7 5\n" +
                                               "7 5 6 3 8 4 2 1 9\n" +
                                               "6 4 3 1 5 8 7 9 2\n" +
                                               "5 2 1 7 9 3 8 4 6\n" +
                                               "9 8 7 4 2 6 5 3 1\n" +
                                               "2 1 4 9 3 5 6 8 7\n" +
                                               "3 6 5 8 1 7 9 2 4\n" +
                                               "8 7 2 6 4 9 1 5 3\n";
            #endregion

            int[,] sudokuMatriz = StringParaMatriz(sudokuStringOriginal);

            if (ValidarSudoku(sudokuMatriz))
            {
                Console.WriteLine("SIM");
            }
            else
            {
                Console.WriteLine("NAO");
            }

            Console.ReadLine();
        }

        private static int[,] StringParaMatriz(string sudokuString)
        {
            int[,] sudokuMatriz = new int[9, 9];

            sudokuString = sudokuString.Replace("\n", " ");
            string[] aux = sudokuString.Split(' ');

            int count = 0;

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    sudokuMatriz[i, j] = Convert.ToInt32(aux[count]);
                    count++;
                }
            }

            return sudokuMatriz;
        }

        private static bool VerificarRepeticao(int[] arr)
        {
            Hashtable tabelaVerificacao = new Hashtable();

            foreach (int i in arr)
            {
                if (tabelaVerificacao.ContainsKey(i))
                    return true;
                else
                    tabelaVerificacao.Add(i, i);
            }

            return false;
        }

        private static bool ValidarSudoku(int[,] sudokuMatriz)
        {
            return VerificarBlocos(sudokuMatriz) 
                && VerificarLinhas(sudokuMatriz) 
                && VerificarColunas(sudokuMatriz);
        }

        #region Verificação de linhas
        private static int[] LinhaParaArray(int i, int[,] sudokuMatriz)
        {
            int[] linha = new int[9];
            int count = 0;

            for (int j = 0; j < 9; j++)
            {
                linha[count] = sudokuMatriz[i, j];
                count++;
            }

            return linha;
        }

        private static bool VerificarLinhas(int[,] sudokuMatriz)
        {
            for (int i = 0; i < 9; i++)
            {
                int[] linha = LinhaParaArray(i, sudokuMatriz);

                if (VerificarRepeticao(linha))
                    return false;
            }

            return true;
        }
        #endregion

        #region Verificação de colunas
        private static int[] ColunaParaArray(int j, int[,] sudokuMatriz)
        {
            int[] coluna = new int[9];
            int count = 0;

            for (int i = 0; i < 9; i++)
            {
                coluna[count] = sudokuMatriz[i, j];
                count++;
            }

            return coluna;
        }

        private static bool VerificarColunas(int[,] sudokuMatriz)
        {
            for (int j = 0; j < 9; j++)
            {
                int[] coluna = ColunaParaArray(j, sudokuMatriz);

                if (VerificarRepeticao(coluna))
                    return false;
            }

            return true;
        }
        #endregion

        #region Verificação de blocos
        private static int[] BlocoParaArray(int iB, int jB, int[,] sudokuMatriz)
        {
            int tamanhoLinha = (iB * 3);
            int tamanhoColuna = (jB * 3);
            int[] bloco = new int[9];
            int count = 0;

            for (int i = tamanhoLinha; i < tamanhoLinha + 3; i++)
            {
                for (int j = tamanhoColuna; j < tamanhoColuna + 3; j++)
                {
                    bloco[count] = sudokuMatriz[i, j];
                    count++;
                }
            }
            return bloco;
        }

        private static bool VerificarBlocos(int[,] sudokuMatriz)
        {
            for (int iB = 0; iB < 3; iB++)
            {
                for (int jB = 0; jB < 3; jB++)
                {
                    int[] bloco = BlocoParaArray(iB, jB, sudokuMatriz);

                    if (VerificarRepeticao(bloco))
                        return false;
                }
            }

            return true;
        }
        #endregion
    }
}
