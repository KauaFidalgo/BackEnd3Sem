using Atividade08;

Usuario usuario1 = new Usuario("João", "12345");
Administrador admin1 = new Administrador("Maria", "54321");
Console.WriteLine("Autenticando usuário:");
usuario1.Autenticar("12345");
Console.WriteLine("Autenticando administrador:");
admin1.Autenticar("54326");