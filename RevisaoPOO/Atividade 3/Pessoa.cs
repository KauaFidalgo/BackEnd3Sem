namespace Atividade03
{
    public class Pessoa
    {
    
        public string nome;
            public int Idade;
            
            public void ExibirInformacoes()
            {
                if (Idade <= 0)
                {
                    Console.WriteLine("Idade Negativa.");
                }
                else
                {
                Console.WriteLine($"Idade: {Idade}");
                }
            
                Console.WriteLine($"Nome: {nome}");
            


            }
    

    }
}