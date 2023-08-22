using System;
using Aula03Colecoes.models;
using Aula03Colecoes.models.Enuns;

namespace Aula03Colecoes
{
    class Program
    {
        static void Main(string[] args)
        {
            CriarLista();
            menuAplicacao();
        }

        //CRIANDO A LISTA
        static List<Funcionario> lista = new List<Funcionario>();

        //MENU
        public static void menuAplicacao()
        {
            Console.WriteLine("==================================================");
            Console.WriteLine("****** Exemplos - Aula 03 Listas e Coleções ******");
            Console.WriteLine("==================================================");
            int opcaoEscolhida = 0;
            do
            {
                Console.WriteLine("\n==================================================");
                Console.WriteLine("---Digite o número referente a opção desejada: ---");
                Console.WriteLine("1 - Visualizar primeiro ID");
                Console.WriteLine("2 - Adicionar funcionario");
                Console.WriteLine("3 - Pesquisa por ID");
                Console.WriteLine("4 - Pesquisa por salario");
                Console.WriteLine("5 - Pesquisa por nome");
                Console.WriteLine("6 - Visualizar funcionários recentes");
                Console.WriteLine("7 - Visualizar estatisticas");
                Console.WriteLine("8 - Visualizar funcionarios por contrato");
                Console.WriteLine("9 - Visualizar lista funcionarios");

                Console.WriteLine("==================================================");
                Console.WriteLine("-----Ou tecle qualquer outro número para sair-----");
                Console.WriteLine("==================================================");
                opcaoEscolhida = int.Parse(Console.ReadLine());
                switch (opcaoEscolhida)
                {
                    case 1:
                        visualizarPrimeiroID();
                        break;
                    case 2:
                        AdicionarFuncionario();
                        break;
                    case 3:
                        Console.WriteLine("Digite o ID do funcionário que você deseja buscar: ");
                        int id = int.Parse(Console.ReadLine());
                        pesquisaPorID(id);
                        break;
                    case 4:
                        Console.WriteLine("Digite o salário para obter todos acima do valor indicado: ");
                        decimal salario = decimal.Parse(Console.ReadLine());
                        PesquisaPorSalario(salario);
                        break;
                    case 5:
                        Console.WriteLine("Digite o nome do funcionário: ");
                        string nome = Console.ReadLine();
                        Funcionario fEncontrado = PesquisaPorNome(nome);
                        //validando o return do funcionario
                        if (fEncontrado != null)
                        {
                            Console.WriteLine($"Funcionario encontrado: {fEncontrado.Nome}");
                        }
                        else
                        {
                            Console.WriteLine("NÃO ENCONTRADO");
                        }
                        break;
                    case 6:
                        Console.WriteLine("Organizando funcionarios... ");
                        VisualizarFuncionariosRecentes();
                        break;
                    case 7:
                        Console.WriteLine("Realizando calculos...");
                        VisualizarEstatisticas();
                        break;
                    case 8:
                        Console.WriteLine("Organizando funcionarios... ");
                        VisualizarListaTipoFuncionario();
                        break;
                    case 9:
                        ExibirLista();
                        break;
                    default:
                        Console.WriteLine("Saindo do sistema....");
                        break;
                }
            } while (opcaoEscolhida >= 1 && opcaoEscolhida <= 10);
            Console.WriteLine("==================================================");
            Console.WriteLine("* Obrigado por utilizar o sistema e volte sempre *");
            Console.WriteLine("==================================================");
        }

        //1 - BUSCA ID 1
        public static void visualizarPrimeiroID(){
            lista = lista.FindAll(x => x.Id == 1);
            ExibirLista();
        }

        //2 - NOVO FUNCIONARIO
        public static void AdicionarFuncionario(){
            Funcionario f = new Funcionario();

            int proximoId = lista.Count + 1;
            f.Id = proximoId;
            Console.WriteLine($"ID do funcionário: {proximoId}");

            Console.WriteLine("Digite o nome: ");
            string nome = Console.ReadLine();
            //f.Nome = Console.ReadLine();

            Console.WriteLine("Digite o cpf: ");
            f.Cpf = Console.ReadLine();

            Console.WriteLine("Digite a data de admissão: ");
            DateTime dataAdmissao = DateTime.Parse(Console.ReadLine());
            //f.DataAdmissao = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Digite o salário: ");
            decimal salario = decimal.Parse(Console.ReadLine());
            //f.Salario = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Escolha o tipo de Funcinário (1 - CLT | 2 - Aprendiz) ");
            int opcao = int.Parse(Console.ReadLine());
            // operador ternario
            f.TipoFuncionario = (opcao == 1) ? TipoFuncionarioEnum.CLT : TipoFuncionarioEnum.Aprendiz;

            //Validações
            if (ValidarNome(nome) == true)
            {
                f.Nome = nome;
            }
            else
            {
                return;
            }

            if (ValidarSalarioAdmissao(salario, dataAdmissao) == true)
            {
                f.Salario = salario;
                f.DataAdmissao = dataAdmissao;
            }
            else
            {
                return;
            }

            //ATUALIZANDO A LISTA
            lista.Add(f);
            ExibirLista();
        }

        //3 - BUSCA ID POR PESQUISA
        public static void pesquisaPorID(int id){
            Funcionario fBusca = lista.Find(x => x.Id == id);
            Console.WriteLine($"Personagem encontrado: {fBusca.Nome}");
        }

        //4 - FILTRA POR SALARIO
        public static void PesquisaPorSalario(decimal valor){
            lista = lista.FindAll(x => x.Salario >= valor);
            ExibirLista();
        }

        //5 - BUSCA POR NOME
        public static Funcionario PesquisaPorNome(string nome){
            //dando find do nome digitado na lista, ignorando o case da string
            Funcionario fEncontrado = lista.Find(x => x.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
            return fEncontrado;
        }

        //6 - BUSCA FUNCIONARIOS RECENTES
        public static void VisualizarFuncionariosRecentes(){
            //lista.RemoveAll(funcionario => funcionario.Id < 4);
            //var fFiltrados = lista.OrderByDescending(funcionario => funcionario.Salario); 
            //duplicidade pois a lista está sendo gerada mais de uma vez dentro da aplicação

            var fFiltrados = lista.Where(funcionario => funcionario.Id >= 4).OrderByDescending(funcionario => funcionario.Salario);
            //para cada funcionario da nova lista mostrar suas informações
            foreach (var funcionario in fFiltrados)
            {
                Console.WriteLine($"ID: {funcionario.Id}, Nome: {funcionario.Nome}, Cargo: {funcionario.TipoFuncionario}, Salário: {funcionario.Salario}");
            }
        }

        //7 - VISUALIZAR ESTATISTICAS
        public static void VisualizarEstatisticas(){
            int totalFuncionarios = lista.Count();
            decimal TotalSalario = lista.Sum(funcionario => funcionario.Salario);

            Console.WriteLine($"Total de funcionarios: {totalFuncionarios}");
            Console.WriteLine($"Total salario funcionarios: {TotalSalario}");
        }

        //8 - VISUALIZAR POR TIPOFUNCIONARIO
        public static void VisualizarListaTipoFuncionario(){
            Console.WriteLine("Escolha o tipo de Funcinário (1 - CLT | 2 - Aprendiz) ");
            int opcao = int.Parse(Console.ReadLine());

            if (opcao == 1)
            {
                var fFiltrados = lista.Where(funcionario => funcionario.TipoFuncionario == TipoFuncionarioEnum.CLT);
                foreach (var funcionario in fFiltrados)
                {
                    Console.WriteLine($"ID: {funcionario.Id}, Nome: {funcionario.Nome}, Cargo: {funcionario.TipoFuncionario}, Salário: {funcionario.Salario}");
                }
            }
            else if (opcao == 2)
            {
                var fFiltrados = lista.Where(funcionario => funcionario.TipoFuncionario == TipoFuncionarioEnum.Aprendiz);
                foreach (var funcionario in fFiltrados)
                {
                    Console.WriteLine($"ID: {funcionario.Id}, Nome: {funcionario.Nome}, Cargo: {funcionario.TipoFuncionario}, Salário: {funcionario.Salario}");
                }
            }
            else
            {
                Console.WriteLine("ERRO: Opção Invalida");
                return;
            }
        }

        //VALIDA NOME FUNCIONARIO
        public static bool ValidarNome(string nome){
            var tamanhoNome = nome.Length;

            if (tamanhoNome <= 2)
            {
                Console.WriteLine("ERRO: O nome deve conter mais de dois caracteres");
                return false;
            }
            else
            {
                return true;
            }
        }

        //VALIDA DATA ADMISSAO E SALARIO
        public static bool ValidarSalarioAdmissao(decimal salario, DateTime dataAdmissao){
            DateTime dataAtual = DateTime.Now.Date;

            if (dataAdmissao < dataAtual || salario <= 0)
            {
                Console.WriteLine("Erro: Data de admissão ou salario com parametros errados.");
                return false;
            }
            else
            {
                return true;
            }
        }
        
        //EXIBIR LISTA
        public static void ExibirLista()
        {
            string dados = "";
            for (int i = 0; i < lista.Count; i++)
            {
                dados += "===============================\n";
                dados += string.Format("Id: {0} \n", lista[i].Id);
                dados += string.Format("Nome: {0} \n", lista[i].Nome);
                dados += string.Format("CPF: {0} \n", lista[i].Cpf);
                dados += string.Format("Admissão: {0:dd/MM/yyyy} \n", lista[i].DataAdmissao);
                dados += string.Format("Salário: {0:c2} \n", lista[i].Salario);
                dados += string.Format("Tipo: {0} \n", lista[i].TipoFuncionario);
                dados += "===============================\n";
                Console.WriteLine(dados);
            }

        }

        //FUNCIONARIOS PRÉ CRIADOS
        public static void CriarLista()
        {
            Funcionario f1 = new Funcionario();
            f1.Id = 1;
            f1.Nome = "Lucas";
            f1.Cpf = "12345678910";
            f1.DataAdmissao = DateTime.Parse("01/01/2000");
            f1.Salario = 1100;
            f1.TipoFuncionario = TipoFuncionarioEnum.Aprendiz;
            lista.Add(f1);

            Funcionario f2 = new Funcionario();
            f2.Id = 2;
            f2.Nome = "Brendinha";
            f2.Cpf = "12345678910";
            f2.DataAdmissao = DateTime.Parse("01/01/2000");
            f2.Salario = 1200;
            f2.TipoFuncionario = TipoFuncionarioEnum.CLT;
            lista.Add(f2);

            Funcionario f3 = new Funcionario();
            f3.Id = 3;
            f3.Nome = "Eduardo";
            f3.Cpf = "12345678910";
            f3.DataAdmissao = DateTime.Parse("01/01/2000");
            f3.Salario = 1300;
            f3.TipoFuncionario = TipoFuncionarioEnum.Aprendiz;
            lista.Add(f3);

            Funcionario f4 = new Funcionario();
            f4.Id = 4;
            f4.Nome = "Aninha";
            f4.Cpf = "12345678910";
            f4.DataAdmissao = DateTime.Parse("01/01/2000");
            f4.Salario = 1250;
            f4.TipoFuncionario = TipoFuncionarioEnum.Aprendiz;
            lista.Add(f4);

            Funcionario f5 = new Funcionario();
            f5.Id = 5;
            f5.Nome = "Mateus";
            f5.Cpf = "12345678910";
            f5.DataAdmissao = DateTime.Parse("01/01/2000");
            f5.Salario = 1800;
            f5.TipoFuncionario = TipoFuncionarioEnum.CLT;
            lista.Add(f5);

            Funcionario f6 = new Funcionario();
            f6.Id = 6;
            f6.Nome = "Marcio";
            f6.Cpf = "12345678910";
            f6.DataAdmissao = DateTime.Parse("01/01/2000");
            f6.Salario = 1450;
            f6.TipoFuncionario = TipoFuncionarioEnum.CLT;
            lista.Add(f6);
        }
    }
}