using FilmesMoura.WebAPI.BdContectFilme;
using FilmesMoura.WebAPI.Interface;
using FilmesMoura.WebAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FilmesMoura.WebAPI.Repositories;

public class GeneroRepository : IGeneroRepository
{
    private readonly FilmeContext _context;

    public GeneroRepository(FilmeContext context)
    {
        _context = context;
    }
    public void AtualizarIdCorpo(Genero generoAtualizado)
    {
        try
        {
            Genero generoBuscado = _context.Generos.Find(generoAtualizado.IdGenero)!;

            if(generoBuscado != null)
            {
                generoBuscado.Nome = generoAtualizado.Nome;
               
            }
                _context.Generos.Update(generoBuscado);
                _context.SaveChanges();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void AtualizarIdUrl(Guid id, Genero generoAtualizado)
    {
        try
        {
            Genero generoBuscado = _context.Generos.Find(id.ToString())!;

            if(generoBuscado != null)
            {
                generoBuscado.Nome = generoAtualizado.Nome;
               
            }
                _context.Generos.Update(generoBuscado);
                _context.SaveChanges();
        }
        
        catch (Exception ex)
        {
            throw;
        }
    }

    public Genero BuscarPorId(Guid id)
    {
        try
        { 
            Genero generoBuscado = _context.Generos.Find(id.ToString())!;
            return generoBuscado;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public void Cadastrar(Genero novoGenero)
    {
        try
        {
            novoGenero.IdGenero = Guid.NewGuid().ToString();
            _context.Generos.Add(novoGenero);

            _context.SaveChanges();
        }
        catch (Exception ex)
        {
            throw;
        }


    }

    public void Deletar(Guid id)
    {
        try
        {
            Genero generoBuscado = _context.Generos.Find(id.ToString())!;
            if (generoBuscado != null)
            {
                _context.Generos.Remove(generoBuscado);
            }
                _context.SaveChanges();
        }
        catch (Exception e)
        {

            throw;
        }
    }

    public List<Genero> Listar()
    {
        try
        {
            List<Genero> listaGeneros = _context.Generos.ToList();

            return listaGeneros;
        }

        catch (Exception ex)
        {
            throw;  
        }

    }
}
