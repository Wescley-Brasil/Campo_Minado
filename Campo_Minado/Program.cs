using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campo_Minado {

    class Tabuleiro {

        //Minas para controlar a logica e Tabuleiro para servir como interface
        private char[,] tabuleiro = new char[5, 5];
        private int[,] minas = new int[5, 5];
        private char[] intervalos = { '1', '2', '3', '4', '5', '6', '7', '8' };
        private int rodadas = 1;
        private int contadorFimJogoTabuleiro = 0;
        private int contadorFimJogoMinas = 0;


        //Metodo para preencher tabuleiro de minas com zeros;
        public void IniciarMinas() {
            for (int linha = 1; linha < minas.GetLength(1); linha++) {
                for (int coluna = 1; coluna < minas.GetLength(1); coluna++) {
                    minas[linha, coluna] = 0;
                }
            }

        }

        //Sortear minhas entre as posicoes 1 e 9, adicionando o valor -1 para as minas nas posicoes vazias
        public void SortearMinas() {
            bool sorteado;
            int linha, coluna;
            Random gerarNumero = new Random();

            for (int i = 1; i <= 6; i++) {
                do {

                    linha = gerarNumero.Next(3) + 1;
                    coluna = gerarNumero.Next(3) + 1;

                    if (minas[linha, coluna] == -1)
                        sorteado = true;
                    else
                        sorteado = false;

                } while (sorteado);

                minas[linha, coluna] = -1;
                contadorFimJogoMinas++;
            }

        }

        public void IniciarTabuleiro() {

            for (int linha = 1; linha < tabuleiro.GetLength(1) - 1; linha++) {
                for (int coluna = 1; coluna < tabuleiro.GetLength(1) - 1; coluna++) {
                    tabuleiro[linha, coluna] = '-';
                    contadorFimJogoTabuleiro++;
                }
            }
        }

        public bool Jogada() {

            //Incrementar rodadas
            bool resultado = false;

            if (Ganhou()) {
                resultado = true;
            }

            else {
                rodadas++;



                int coluna;
                int linha;
                char acao;
                Console.WriteLine("\nDigite linha e coluna entre 1 e 9 e Ação de 'a' para ABRIR e 'm' para MARCAR");


                Console.Write("\nDigite o numero da coluna: ");
                coluna = int.Parse(Console.ReadLine());
                Console.Write("Digite o numero da linha: ");
                linha = int.Parse(Console.ReadLine());
                Console.Write("Digite uma acão: ");
                acao = Console.ReadLine().ToCharArray()[0];


                Console.Clear();


                #region if
                if (tabuleiro[linha, coluna] == '?' & acao == 'm') {
                    tabuleiro[linha, coluna] = '-';

                    //Verificador fim de jogo
                    if (minas[linha, coluna] == -1) {
                        contadorFimJogoMinas++;
                    }
                }
                else if (tabuleiro[linha, coluna] == '-' & acao == 'm') {
                    tabuleiro[linha, coluna] = '?';

                    //Verificador fim de jogo
                    if (minas[linha, coluna] == -1) {
                        contadorFimJogoMinas--;
                    }
                }
                else if (tabuleiro[linha, coluna] == '?' & acao != 'm') {

                }
                else if (minas[linha, coluna] == -1) {
                    //resultado = true;
                    tabuleiro[linha, coluna] = '?';
                    //GameOver(linha, coluna);
                    contadorFimJogoMinas--;

                }
                else if (tabuleiro[linha, coluna] == ' ') {

                }
                else if (intervalos.Contains(tabuleiro[linha, coluna])) {

                }
                else {
                    Revela(linha, coluna);
                }
                #endregion
            }

            return resultado;

        }




        // preenche espaços com numeros de bombas ao redor, espaços vazios
        public void Revela(int linha, int coluna) {
            int contador = 0;

            #region for
            for (int i = -1; i < 2; i++) {
                for (int j = -1; j < 2; j++) {
                    if (minas[linha + i, coluna + j] == -1)
                        contador++;
                }

            }

            #endregion

            #region if
            if (contador != 0) {
                tabuleiro[linha, coluna] = Convert.ToString(contador).ToCharArray()[0];
            }
            else {
                for (int i = -1; i < 2; i++) {
                    for (int j = -1; j < 2; j++) {
                        if (intervalos.Contains(tabuleiro[linha + i, coluna + j])) {
                            continue;
                        }
                        tabuleiro[linha + i, coluna + j] = ' ';
                    }
                }

            }
            #endregion

        }

        public void TabuleiroInterface() {
            Console.WriteLine("Rodada: " + rodadas);
            Console.Write("\n\nColunas");

            for (int coluna = 1; coluna < tabuleiro.GetLength(1) - 1; coluna++) {
                Console.Write("\t   " + coluna);
            }

            Console.Write("\nLinhas");

            for (int linha = 1; linha < tabuleiro.GetLength(1) - 1; linha++) {
                Console.Write("\n " + linha);
                for (int coluna = 1; coluna < tabuleiro.GetLength(1) - 1; coluna++) {
                    Console.Write("\t|  " + tabuleiro[linha, coluna] + "  |");
                    //Identar interface
                    if (coluna == 9) {
                        Console.WriteLine();
                    }
                }
            }


        }



        public void IniciarJogo() {

            IniciarMinas();
            SortearMinas();
            IniciarTabuleiro();

            bool estadoJogo = false;

            do {
                TabuleiroInterface();
                estadoJogo = Jogada();


            } while (!estadoJogo);
            if (estadoJogo & Ganhou() == true) {

                Console.Clear();
                Console.Write("\nParabéns voçe venceu");

            }
            Console.ReadKey();

        }

        public void GameOver(int linha, int coluna) {
            TabuleiroInterface();
            Console.WriteLine("Fim de jogo");
        }

        public bool Ganhou() {

            bool venceu = false;
            int contadorTabuleiro = 0;

            for (int linha = 1; linha < tabuleiro.GetLength(1) - 1; linha++) {
                for (int coluna = 1; coluna < tabuleiro.GetLength(1) - 1; coluna++) {
                    if (tabuleiro[linha, coluna] == '-') {
                        contadorTabuleiro++;
                    }
                }
            }

            contadorFimJogoTabuleiro = contadorTabuleiro;

            if (contadorFimJogoTabuleiro < 1 & contadorFimJogoMinas < 1) {
                venceu = true;
            }

            return venceu;
        }


        static void Main(string[] args) {

            //bool verify = true;
            //if (!verify) {
            //    Console.WriteLine("Deu certo");
            //}



            //Console.Write("Linha: ");
            //int linha = int.Parse(Console.ReadLine());
            //Console.Write("Coluna: ");
            //int Coluna = int.Parse(Console.ReadLine());
            //Console.Write("Digite uma acão: ");
            //char acao = char.Parse(Console.ReadLine());
            //Console.ReadKey();

            Tabuleiro tb = new Tabuleiro();
            tb.IniciarJogo();




        }
    }


}

