using Royal_Games.Contexts;
using Royal_Games.Domains;
using Royal_Games.Interfaces;

namespace Royal_Games.Repositories
{
    public class PlataformaRepository : IPlataformaRepository
    {
        private readonly RoyalGamesContext _context;

        public PlataformaRepository(RoyalGamesContext context)
        {
            _context = context;
        }

        public List<Plataforma> Listar()
        {
            return _context.Plataforma.ToList();
        }

        public Plataforma ObterPorId(int id)
        {
            Plataforma plataforma = _context.Plataforma.FirstOrDefault(g => g.PlataformaID == id);

            return plataforma;
        }

        public bool NomeExiste(string nome, int? plataformaIdAtual = null)
        {
            var consulta = _context.Plataforma.AsQueryable();

            if (plataformaIdAtual.HasValue)
            {
                consulta = consulta.Where(g => g.PlataformaID != plataformaIdAtual.Value);
            }

            return consulta.Any(g => g.Nome == nome);
        }

        public void Adicionar(Plataforma plataforma)
        {
            _context.Plataforma.Add(plataforma);
            _context.SaveChanges();
        }

        public void Atualizar(Plataforma plataforma)
        {
            Plataforma plataformaBanco = _context.Plataforma.FirstOrDefault(g => g.PlataformaID == plataforma.PlataformaID);

            if (plataformaBanco == null)
            {
                return;
            }

            plataformaBanco.Nome = plataforma.Nome;

            _context.SaveChanges();
        }

        public void Remover(int id)
        {
            Plataforma plataformaBanco = _context.Plataforma.FirstOrDefault(g => g.PlataformaID == id);

            if (plataformaBanco == null)
            {
                return;
            }

            _context.Plataforma.Remove(plataformaBanco);
            _context.SaveChanges();
        }
    }
}

