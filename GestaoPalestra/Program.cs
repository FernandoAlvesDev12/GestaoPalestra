using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestaoPalestra
{
    class Program
    {
        public struct Pessoa
        {
            public string Nome;
            public string Email;
            public DateTime DataHora;
            public bool Cadastrado;
        }

        //Declaração das variaveis publicas            
        public static Pessoa[] assentos;
        public static int qtdeAssentos, qtdeAssentosPorFileras, qtdeAssentosConvidados, qtdeAssentosEspeciais, percentualAssentosEspecais, qtdeBrindes;

        static void Main(string[] args)
        {

            //Parametros da aplicação
            qtdeAssentos = 20;
            qtdeAssentosPorFileras = 3;
            percentualAssentosEspecais = 5; // em %
            qtdeBrindes = 2;

            //Gerando e reservando assentos
            assentos = new Pessoa[qtdeAssentos];
            qtdeAssentosConvidados = qtdeAssentosPorFileras * 2;
            qtdeAssentosEspeciais = Convert.ToInt32((assentos.Length * percentualAssentosEspecais) / 100);

            InicializarOpcoes(); //Identificar a operação que o usuário deseja executar

        }

        public static void InicializarOpcoes()
        {

            string opcaoEscolhida;

            ImprimirCabecalho(null);

            //Imprimindo opções para o usuário
            Console.WriteLine("1 - Cadastro de Participante");
            Console.WriteLine("2 - Consultar Participante por Nome/Email");
            Console.WriteLine("3 - Consultar Participante por Assento");
            Console.WriteLine("4 - Sorteio de Brindes");
            Console.WriteLine("5 - Fechar Programa");
            Console.WriteLine("");
            Console.Write("Digite o número de uma das opções acima e aperte [ENTER]: ");

            //Lendo opção escolhida e salvando na variavel
            opcaoEscolhida = Console.ReadLine();

            //Direcionando o usuário para a operação desejada
            switch (opcaoEscolhida)
            {
                case "1":
                    CadastroPessoa();
                    break;
                case "2":
                    ConsultarParticipante("Nome/Email");
                    break;
                case "3":
                    ConsultarParticipante("Assento");
                    break;
                case "4":
                    SorteioBrindes();
                    break;
                case "5":
                    FecharPrograma();
                    break;
                default:
                    //Limpando a tela para exibir a mensagem de erro
                    Console.Clear();
                    ImprimirCabecalho("");

                    //Indicando que opção digitada não existe, e voltando para tentar novamente
                    Console.WriteLine("");
                    Console.WriteLine("Não foi possivel identificar a opção " + opcaoEscolhida);
                    Console.WriteLine("");
                    Console.WriteLine("Insira apenas 1, 2, 3, 4 ou 5, para selecionar a operação desejada.");
                    Console.WriteLine("Aperte [ENTER] para tentar novamente...");
                    Console.ReadKey();
                    Console.Clear();
                    InicializarOpcoes();
                    return;
            }

        }

        public static void CadastroPessoa()
        {
            string opcaoEscolhida, descricaoOpcaoEscolhida;
            int? proximoAssento = null;

            ImprimirCabecalho("Cadastro de Pessoa");

            //Imprimindo opções para o usuário
            Console.WriteLine("1 - Visitante Comum");
            Console.WriteLine("2 - Visitante com Necessidades Especiais");
            Console.WriteLine("3 - Professores ou Convidados do Palestrante");
            Console.WriteLine("4 - Voltar para Tela Inicial");
            Console.WriteLine("");
            Console.Write("Digite o número de uma das opções acima e aperte [ENTER]: ");

            //Lendo opção escolhida e salvando na variavel
            opcaoEscolhida = Console.ReadLine();

            //Direcionando o usuário para a operação desejada
            switch (opcaoEscolhida)
            {
                case "1": //Visitante Comum

                    descricaoOpcaoEscolhida = "Visitante Comum";

                    for (int i = (qtdeAssentosConvidados + qtdeAssentosEspeciais); i < assentos.Length; i++)
                    {
                        if (!assentos[i].Cadastrado)
                        {
                            proximoAssento = i;
                            break;
                        }
                    }

                    break;

                case "2": //Visitante Necessidades Especiais

                    descricaoOpcaoEscolhida = "Visitante com Necessidades Especiais";

                    for (int i = qtdeAssentosConvidados; i < (qtdeAssentosConvidados + qtdeAssentosEspeciais); i++)
                    {
                        if (!assentos[i].Cadastrado)
                        {
                            proximoAssento = i;
                            break;
                        }
                    }

                    break;

                case "3": //professores ou convidados palestrante

                    descricaoOpcaoEscolhida = "Professores ou Convidados do Palestrante";

                    for (int i = 0; i < qtdeAssentosConvidados; i++)
                    {
                        if (!assentos[i].Cadastrado)
                        {
                            proximoAssento = i;
                            break;
                        }
                    }

                    break;

                case "4": //Voltar para tela inicial
                    InicializarOpcoes();
                    return;

                default:
                    //Limpando a tela para exibir a mensagem de erro
                    Console.Clear();
                    ImprimirCabecalho("Cadastro de Pessoa");

                    //Indicando que opção digitada não existe, e voltando para tentar novamente
                    Console.WriteLine("");
                    Console.WriteLine("Não foi possivel identificar a opção " + opcaoEscolhida);
                    Console.WriteLine("");
                    Console.WriteLine("Insira apenas 1, 2 ou 3, para selecionar a operação desejada.");
                    Console.WriteLine("Aperte [ENTER] para tentar novamente...");
                    Console.ReadKey();
                    Console.Clear();
                    CadastroPessoa();
                    return;
            }

            // Caso não tenha o próximo assento disponível a variavel, vai chegar como null, e então indicamos que está com limite esgotado nessa seção
            if (proximoAssento == null)
            {
                AvisoLimiteAssentos(descricaoOpcaoEscolhida);
                return;
            }

            ImprimirCabecalho("Cadastro de Pessoa - " + descricaoOpcaoEscolhida);

            //Cadastrando Nome
            Console.Write("Nome: ");
            assentos[proximoAssento.Value].Nome = Console.ReadLine();

            //Cadastrando Email
            Console.Write("Email: ");
            assentos[proximoAssento.Value].Email = Console.ReadLine();

            //Cadastrando Data/Hora Entrada
            assentos[proximoAssento.Value].DataHora = DateTime.Now;

            //Alterando status da posição do array para cadastrado
            assentos[proximoAssento.Value].Cadastrado = true;

            //Imprimindo ticket
            Console.WriteLine("");
            Console.WriteLine("Cadastro efetuado com sucesso! Retire o ticket abaixo:");
            Console.WriteLine("");

            ImprimirTicket(proximoAssento.Value);

        }

        public static void AvisoLimiteAssentos(string descricaoOpcaoEscolhida)
        {
            ImprimirCabecalho("Cadastro de Pessoa");

            Console.WriteLine("Assentos para " + descricaoOpcaoEscolhida + " está esgotado!");
            Console.WriteLine("Aperte [ENTER] para tentar novamente em outra opção...");

            Console.ReadKey();
            Console.Clear();
            CadastroPessoa();
        }

        public static void ImprimirTicket(int proximoAssento)
        {
            Console.WriteLine("");
            Console.WriteLine("**************** TICKET DE ENTRADA NA PALESTRA ********************");
            Console.WriteLine("");
            Console.WriteLine("Número do Ticket: SGP00" + (proximoAssento + 1));
            Console.WriteLine("Data/Hora Entrada: " + assentos[proximoAssento].DataHora);
            Console.WriteLine("Nome: " + assentos[proximoAssento].Nome);
            Console.WriteLine("");
            Console.WriteLine("********** Agradecemos pela visita, e boa Palestra! **************");

            Console.WriteLine("");
            Console.WriteLine("");

            Console.WriteLine("Aperte [ENTER] para voltar a tela inicial");

            Console.ReadKey();
            InicializarOpcoes();
        }

        public static void ConsultarParticipante(string filtro)
        {
            int assentoConsultado;
            string leituraCodigoAssento;

            ImprimirCabecalho("Consultar Participante por " + filtro);

            if (filtro == "Nome/Email")
            {
                Console.WriteLine("");
                Console.Write("Digite o nome ou email que deseja consultar: ");
                leituraCodigoAssento = Console.ReadLine();

                for (int i = 0; i < assentos.Length; i++)
                {
                    if (leituraCodigoAssento == assentos[i].Nome || leituraCodigoAssento == assentos[i].Email) //verificando se possui na array algum registro com o nome ou email consultado
                    {
                        ImprimirTicket(i);
                        break;
                    }
                }

                //Indicando que o nome/email digitado não existe, e voltando para tentar novamente
                Console.WriteLine("");
                Console.WriteLine("Nenhum nome ou email cadastrado com esses parametros!");
                Console.WriteLine("");
                Console.WriteLine("Aperte [ENTER] para tentar novamente...");
                Console.ReadKey();
                Console.Clear();
                ConsultarParticipante("Nome/Email");
                return;
            }
            else
            {
                Console.WriteLine("");
                Console.Write("Digite o código do ticket para consultar: SGP00");
                leituraCodigoAssento = Console.ReadLine();

                if ((Int32.TryParse(leituraCodigoAssento, out assentoConsultado)) && assentoConsultado > 0 && assentoConsultado < assentos.Length && assentos[assentoConsultado - 1].Cadastrado)
                {
                    ImprimirTicket(assentoConsultado - 1);
                    return;
                }

                //Indicando que o codigo digitado não existe, e voltando para tentar novamente
                Console.WriteLine("");
                Console.WriteLine("O assento indicado 'SGP00" + leituraCodigoAssento + "' não existe, ou ainda não foi cadastrado!");
                Console.WriteLine("");
                Console.WriteLine("Aperte [ENTER] para tentar novamente...");
                Console.ReadKey();
                Console.Clear();
                ConsultarParticipante("Assento");
                return;
            }

        }

        public static void SorteioBrindes()
        {
            int qtdeAssentosCadastrados = 0;
            int qtdeAssentosNaArray = 0;
            Random numAleatorio = new Random();
            Pessoa[] assentosCadastrados = new Pessoa[qtdeAssentosCadastrados];

            ImprimirCabecalho("Sorteio de Brindes");

            //queria fazer assim :)
            //Pessoa[] assentosCadastrados = assentos.Where(p => p.Cadastrado).ToArray();

            for (int i = qtdeAssentosConvidados; i < assentos.Length; i++) //verificando a quantidade de assentos cadastrados
            {
                if (assentos[i].Cadastrado)
                {
                    qtdeAssentosCadastrados++;
                }
            }

            if (qtdeAssentosCadastrados == 0) //verificando se existe algum assento cadastrado
            {
                Console.WriteLine("");
                Console.WriteLine("Nenhum participante cadastrado até o momento disponível para o sorteio.");
                Console.WriteLine("");

                Console.WriteLine("Aperte [ENTER] para voltar a tela inicial");
                Console.ReadKey();
                InicializarOpcoes();
                return;
            }
            else //alterando capacidade da array de cadastrados para a qtde de assentos cadastrados
            {
                assentosCadastrados = new Pessoa[qtdeAssentosCadastrados];
            }

            for (int i = qtdeAssentosConvidados; i < assentos.Length; i++) //inserindo cadastrados na nova array
            {
                if (assentos[i].Cadastrado)
                {
                    assentosCadastrados[qtdeAssentosNaArray] = assentos[i];
                    qtdeAssentosNaArray++;
                }
            }

            for (int i = 1; i <= qtdeBrindes; i++) // fazendo o sorteio apenas com a array de cadastrados, pela quantidade de brindes que existem
            {
                int ticketSorteado = numAleatorio.Next(0, assentosCadastrados.Length);
                Console.WriteLine("**************** SORTEADO " + i + " ********************");
                ImprimirSorteado(assentosCadastrados, ticketSorteado);
            }

            Console.WriteLine("");
            Console.WriteLine("Esses foram os sorteados!!");
            Console.WriteLine("Aperte [ENTER] para voltar a tela inicial");
            Console.ReadKey();
            InicializarOpcoes();
        }

        public static void FecharPrograma()
        {
            string opcaoEscolhida;

            ImprimirCabecalho("Fechar Programa");

            Console.WriteLine("");
            Console.WriteLine("Deseja realmente encerrar o programa? Todas as informações inseridas serão perdidas...");
            Console.Write("Digite S para fechar, ou N para voltar a tela inicial: ");

            //Lendo opção escolhida e salvando na variavel
            opcaoEscolhida = Console.ReadLine();

            switch (opcaoEscolhida.ToUpper())
            {
                case "S":
                    Environment.Exit(1);
                    break;
                case "N":
                    InicializarOpcoes();
                    break;
                default:
                    //Limpando a tela para exibir a mensagem de erro
                    Console.Clear();
                    ImprimirCabecalho("Fechar Programa");

                    //Indicando que opção digitada não existe, e voltando para tentar novamente
                    Console.WriteLine("");
                    Console.WriteLine("Não foi possivel identificar a opção " + opcaoEscolhida);
                    Console.WriteLine("");
                    Console.WriteLine("Digite apenas S ou N, para fechar o programa.");
                    Console.WriteLine("Aperte [ENTER] para tentar novamente...");
                    Console.ReadKey();
                    Console.Clear();
                    FecharPrograma();
                    return;
            }
        }

        public static void ImprimirCabecalho(string subtitulo)
        {
            Console.Clear();
            Console.WriteLine("*** SISTEMA DE GESTÃO DE PALESTRA ***");

            if (!string.IsNullOrEmpty(subtitulo))
            {
                Console.WriteLine("Operação Escolhida: " + subtitulo);
            }

            Console.WriteLine("");
        }

        public static void ImprimirSorteado(Pessoa[] assentosCadastrados, int sorteado)
        {
            Console.WriteLine("");
            Console.WriteLine("Nome: " + assentosCadastrados[sorteado].Nome);
            Console.WriteLine("Email: " + assentosCadastrados[sorteado].Email);
            Console.WriteLine("");
        }

    }
}
