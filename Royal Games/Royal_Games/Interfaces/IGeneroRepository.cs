using Royal_Games.Domains;

namespace Royal_Games.Interfaces
{
    public interface IGeneroRepository
    {
        List<Genero> Listar();
        Genero ObterPorId(int id);
        bool NomeExiste(string nome, int? generoIdAtual = null);
        void Adicionar(Genero genero);
        void Atualizar(Genero genero);
        void Remover(int id);
    }
}
