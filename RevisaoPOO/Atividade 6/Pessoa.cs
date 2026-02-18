namespace Atividade06
{
    public class Pessoa
    {


        public string Nome;
        public int Idade;

        public void Apresentar()
        {
            Console.WriteLine($"Olá, meu nome é {Nome} e tenho {Idade} anos.");
        }
        public void Apresentar(string Sobrenome)
        {
            Console.WriteLine($"Olá, meu nome é {Nome} {Sobrenome} e tenho {Idade} anos.");
        }


    }


}