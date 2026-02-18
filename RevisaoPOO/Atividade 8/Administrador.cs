using System.Security.Cryptography.X509Certificates;

namespace Atividade08
{
    public class Administrador : IAutenticavel
    {
        public string nome;

        public string Senha;

        public Administrador(string n, string s)
        {
           nome = n;
           Senha = s;
        }

        public void Autenticar(string senha)
        {

            if (Senha == senha)
            {
                Console.WriteLine("Administrador autenticado com sucesso!");
            }
            else
            {
                Console.WriteLine("Senha incorreta. Acesso negado.");
            }
        }

    }
}