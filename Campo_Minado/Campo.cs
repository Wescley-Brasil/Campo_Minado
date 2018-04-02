using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Campo_Minado
{

    class Tabuleiro
    {

        //Minas para controlar a logica e Tabuleiro para servir como interface
        private char[,] tabuleiro = new char[5, 5];
        private int[,] minas = new int[5, 5];
        private char[] intervalos = { '1', '2', '3', '4', '5', '6', '7', '8' };
        private int rodadas = 1;
        private int contadorFimJogoTabuleiro = 0;
        private int contadorFimJogoMinas = 0;


        //Metodo para preencher tabuleiro de minas com zeros;
        public void IniciarMinas()
        {
            for (int linha = 0; linha < minas.GetLength(1); linha++)
            {
                for (int coluna = 0; coluna < minas.GetLength(1); coluna++)
                {
                    minas[linha, coluna] = 0;
                }
            }

        }

        //Sortear minhas entre as posicoes 1 e 9, adicionando o valor -1 para as minas nas posicoes vazias
        public void SortearMinas()
        {
            bool sorteado;
            int linha, coluna;
            Random gerarNumero = new Random();

            for (int i = 1; i <= 5; i++)
            {
                do
                {

                    linha = gerarNumero.Next(3) + 1;
                    coluna = gerarNumero.Next(3) + 1;

                    if (minas[linha, coluna] == 1)
                        sorteado = true;
                    else
                        sorteado = false;

                } while (sorteado);

                minas[linha, coluna] = 1;
                contadorFimJogoMinas++;
            }

        }

        public void IniciarTabuleiro()
        {

            for (int linha = 1; linha < tabuleiro.GetLength(1) - 1; linha++)
            {
                for (int coluna = 0; coluna < tabuleiro.GetLength(1); coluna++)
                {
                    tabuleiro[linha, coluna] = '-';
                    contadorFimJogoTabuleiro++;
                }
            }
        }

        public bool Jogada(int coluna, int linha, char acao)
        {
            bool resultado = false;
            //Incrementar rodadas

            rodadas++;

            Console.Clear();

            #region if
            if (tabuleiro[linha, coluna] == '?' & acao == 'm')
            {
                tabuleiro[linha, coluna] = '-';

                //Verificador fim de jogo
                if (minas[linha, coluna] == 1)
                {
                    contadorFimJogoMinas++;
                }
            }
            else if (tabuleiro[linha, coluna] == '-' & acao == 'm')
            {
                tabuleiro[linha, coluna] = '?';

                //Verificador fim de jogo
                if (minas[linha, coluna] == 1)
                {
                    contadorFimJogoMinas--;
                }
            }
            else if (tabuleiro[linha, coluna] == '?' & acao != 'm')
            {

            }
            else if (minas[linha, coluna] == 1)
            {
                resultado = true;
                GameOver(linha, coluna);
                contadorFimJogoMinas--;

            }
            else if (tabuleiro[linha, coluna] == ' ')
            {

            }
            else if (intervalos.Contains(tabuleiro[linha, coluna]))
            {

            }
            else
            {
                Revela(linha, coluna);
            }
            #endregion

            //Gravar os dados apos a jogada
            GravarDados(linha, coluna, acao);
            return resultado;
        }

        public void zerarJogo()
        {
            //Resetar variaveis para iniciar novo jogo
            string ficheiroMina = @"mina.txt";
            File.Delete(ficheiroMina);

            string ficheiroMenu = @"menu.txt";
            File.Delete(ficheiroMenu);

            string ficheiroTabuleiro = @"tabuleiro.txt";
            File.Delete(ficheiroTabuleiro);

            rodadas = 0;
            contadorFimJogoMinas = 0;
            contadorFimJogoTabuleiro = 0;
        }


        
        public void Revela(int linha, int coluna)
        {
            // preenche espaços com numeros de bombas ao redor, espaços vazios
            int contador = 0;

            #region for
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (minas[linha + i, coluna + j] == 1)
                        contador++;
                }

            }

            #endregion

            #region if
            if (contador != 0)
            {
                tabuleiro[linha, coluna] = Convert.ToString(contador).ToCharArray()[0];
            }
            else
            {
                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        if (intervalos.Contains(tabuleiro[linha + i, coluna + j]))
                        {
                            continue;
                        }
                        tabuleiro[linha + i, coluna + j] = ' ';
                    }
                }

            }
            #endregion

        }

        public void TabuleiroInterface()
        {
            Console.WriteLine("Rodada: " + rodadas);
            Console.Write("\n\nColunas");

            for (int coluna = 1; coluna < tabuleiro.GetLength(1) - 1; coluna++)
            {
                Console.Write("\t   " + coluna);
            }

            Console.Write("\nLinhas");

            for (int linha = 1; linha < tabuleiro.GetLength(1) - 1; linha++)
            {
                Console.Write("\n " + linha);
                for (int coluna = 1; coluna < tabuleiro.GetLength(1) - 1; coluna++)
                {
                    Console.Write("\t|  " + tabuleiro[linha, coluna] + "  |");
                    //Identar interface
                    if (coluna == 9)
                    {
                        Console.WriteLine();
                    }
                }
            }


        }

        public void RevelaTabuleiro()
        {
            Console.WriteLine("Rodada: " + rodadas);
            Console.Write("\n\nColunas");

            for (int coluna = 1; coluna < tabuleiro.GetLength(1) - 1; coluna++)
            {
                Console.Write("\t   " + coluna);
            }

            Console.Write("\nLinhas");

            for (int linha = 1; linha < tabuleiro.GetLength(1) - 1; linha++)
            {
                Console.Write("\n " + linha);
                for (int coluna = 1; coluna < tabuleiro.GetLength(1) - 1; coluna++)
                {
                    if (minas[linha, coluna] == 1 && tabuleiro[linha, coluna] == '-')
                    {
                        Console.Write("\t|  " + 'b' + "  |");
                    }
                    else
                    {
                        Console.Write("\t|  " + tabuleiro[linha, coluna] + "  |");
                    }
                    //Identar interface
                    if (coluna == 9)
                    {
                        Console.WriteLine();
                    }
                }
            }
        }


        public void GravarDados(int linha, int coluna, char acao)
        {
            string ficheiroMenu = @"menu.txt";
            string ficheiroTabuleiro = @"tabuleiro.txt";
            string ficheiroMina = @"mina.txt";

            StreamWriter swMenu;
            StreamWriter swTabuleiro;
            StreamWriter swMinas;


            swMenu = File.CreateText(ficheiroMenu);
            swTabuleiro = File.CreateText(ficheiroTabuleiro);
            swMinas = File.CreateText(ficheiroMina);

            //Salvar estado menu
            string linhaMenu = contadorFimJogoMinas.ToString() + ";" + contadorFimJogoTabuleiro.ToString() + ";" + rodadas.ToString();
            swMenu.WriteLine(linhaMenu);
            //Salvar estado do campo minado e estado tabuleiro

            for (int linhas = 1; linhas < tabuleiro.GetLength(1) - 1; linhas++)
            {
                for (int colunas = 1; colunas < tabuleiro.GetLength(1) - 1; colunas++)
                {

                    swTabuleiro.Write(tabuleiro[linhas, colunas] + ";");
                    swMinas.Write(minas[linhas, colunas] + ";");
                }
            }

            swMenu.Close();
            swMinas.Close();
            swTabuleiro.Close();
        }

        public bool RecuperarDados()
        {
            string ficheiroMenu = @"menu.txt";
            string ficheiroTabuleiro = @"tabuleiro.txt";
            string ficheiroMina = @"mina.txt";
            bool estado = false;

            StreamReader srMenu;
            StreamReader srTabuleiro;
            StreamReader srMinas;


            if (File.Exists(ficheiroMina))
            {
                estado = true;
                srMenu = File.OpenText(ficheiroMenu);
                srTabuleiro = File.OpenText(ficheiroTabuleiro);
                srMinas = File.OpenText(ficheiroMina);

                string[] infTabuleiro = srTabuleiro.ReadLine().Split(';');
                string[] infMinas = srMinas.ReadLine().Split(';');
                string[] infMenu = srMenu.ReadLine().Split(';');

                contadorFimJogoMinas = Convert.ToInt32(infMenu[0]);
                contadorFimJogoTabuleiro = Convert.ToInt32(infMenu[1]);
                rodadas = Convert.ToInt32(infMenu[2]);

                int contadorLista = 0;

                //Recuperar estado do Tabuleiro e Minas
                for (int linha = 1; linha < tabuleiro.GetLength(1) - 1; linha++)
                {
                    for (int coluna = 1; coluna < tabuleiro.GetLength(1) - 1; coluna++)
                    {
                        tabuleiro[linha, coluna] = Convert.ToChar(infTabuleiro[contadorLista]);
                        minas[linha, coluna] = Convert.ToInt32(infMinas[contadorLista]);
                        contadorLista++;
                    }
                }

                srMinas.Close();
                srTabuleiro.Close();
                srMenu.Close();
            }
            return estado;
        }

        public void CarregarJogo()
        {
            Console.Clear();
            IniciarMinas();
            SortearMinas();
            IniciarTabuleiro();
        }

        public void IniciarJogo()
        {
            bool estadoJogo = false;

            do
            {
                if (Ganhou() == true)
                {
                    estadoJogo = true;
                }

                else
                {
                    TabuleiroInterface();

                    Console.WriteLine("\nDigite linha e coluna entre 1 e 9 e 'a' para ABRIR e 'm' para MARCAR");

                    Console.Write("\nDigite o numero da coluna: ");
                    int coluna = int.Parse(Console.ReadLine());
                    Console.Write("Digite o numero da linha: ");
                    int linha = int.Parse(Console.ReadLine());
                    Console.Write("Digite uma acão: ");
                    char acao = Console.ReadLine().ToCharArray()[0];

                    estadoJogo = Jogada(coluna, linha, acao);
                }


            } while (!estadoJogo);
            if (Ganhou() == true)
            {

                Console.Write("\nParabéns voçe venceu");
                Console.Write("\n\n\n\nDigite Enter para continuar");

            }
            Console.ReadKey();
            zerarJogo();
            Console.Clear();
            
            
        }


        public void Menu()
        {

            char opcao = ' ';
            do
            {
                Console.Clear();
                Console.WriteLine("Campo Minado\n");
                Console.WriteLine("Digite 1 para Iniciar novo jogo");
                Console.WriteLine("Digite 2 para Continuar");
                Console.WriteLine("Digite 0 para Sair");

                Console.Write("Opcao: ");
                opcao = Convert.ToChar(Console.ReadLine());



                switch (opcao)
                {
                    case '1':
                        CarregarJogo();
                        IniciarJogo();
                        break;

                    case '2':
                        if (RecuperarDados())
                        {
                            Console.Clear();
                            IniciarTabuleiro();
                            IniciarMinas();
                            RecuperarDados();
                            IniciarJogo();
                        }
                        else
                        { 
                             Console.WriteLine("Não existe jogo salvo");
                             Console.ReadKey();
                        }
                        break;

                    case '0':
                        break;
                    default:
                        Console.WriteLine("Opcao inválida");
                        Console.ReadKey();
                        break;
                }
            } while (opcao != '0');

        }

        public void GameOver(int linha, int coluna)
        {
            RevelaTabuleiro();
            Console.WriteLine("\nFim de jogo");



        }


        public bool Ganhou()
        {

            bool venceu = false;
            int contadorTabuleiro = 0;

            for (int linha = 1; linha < tabuleiro.GetLength(1) - 1; linha++)
            {
                for (int coluna = 1; coluna < tabuleiro.GetLength(1) - 1; coluna++)
                {
                    if (tabuleiro[linha, coluna] == '-')
                    {
                        contadorTabuleiro++;
                    }
                }
            }

            contadorFimJogoTabuleiro = contadorTabuleiro;

            if (contadorFimJogoTabuleiro < 1 & contadorFimJogoMinas < 1)
            {
                venceu = true;
            }

            return venceu;
        }


    }


}