namespace Atividade08
{
    public class Usuario : IAutenticavel
    {
        public string nome;

        public string Senha;

        public Usuario(string n, string s)
        {
           nome = n;
           Senha = s;
        }

        public void Autenticar(string senha)
        {

            if (Senha == senha)
            {
                Console.WriteLine("Usuario autenticado com sucesso!");
            }
            else
            {
                Console.WriteLine("Senha incorreta. Acesso negado.");
            }
        }

    }
}