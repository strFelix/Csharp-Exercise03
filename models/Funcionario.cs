using Aula03Colecoes.models.Enuns;
namespace Aula03Colecoes.models
{
    public class Funcionario
    {
        //public int id;
        // PROP + TAB para criar atalho propriedade
        public int Id { get; set; }
        
        public string Nome { get; set; } = string.Empty; //caso for string declarar igual a string vazia

        public string Cpf { get; set; } = string.Empty;

        public DateTime DataAdmissao { get; set; }

        public decimal Salario { get; set; }

        // inserir no topo --> using caminho.para.arquivo
        public TipoFuncionarioEnum TipoFuncionario { get; set; }

        public void ReajusteSalarial(){
            Salario = Salario + (Salario * 10/100);
        }

        public string ExibirPeriodoExperiencia(){

            string PeriodoExperiencia = string.Format(" Periodo de experiência: {0} até {1}", DataAdmissao, DataAdmissao.AddMonths(3)); //.AddMonths --> método do DateTime 
            return PeriodoExperiencia;                                                                                                                 //que soma meses
            
        }
        
        public decimal CalcularDescontoVT(decimal percentual){
            decimal desconto = this.Salario * percentual/100; //this. --> referencia uma variavel dentro do mesmo arquivo
            return desconto;
        }

        private int ContarCaracteres(string dado){
            return dado.Length; //.lenght --> conta a quantidade de caracteres de uma string
        }

        public bool ValidarCpf(){ // bool para verdadeiro ou falso 

            // caso a condicional seja apenas uma linha não tem necessidade escrever a condição dentro de chaves
            if(ContarCaracteres(Cpf) == 11)
                return true;
            else 
                return false;
        }
        
    }
}