namespace Atividade04
{
    public class Pessoa
    {


        public string Nome;
        public int Idade;

        public Pessoa(string nome, int idade)
        {
            Nome = nome;
            Idade = idade;
        }

        public void ExibirInformacoes()
        {
            Console.WriteLine($"Nome: {Nome}");
            Console.WriteLine($"Idade: {Idade}");

        }
    }


}