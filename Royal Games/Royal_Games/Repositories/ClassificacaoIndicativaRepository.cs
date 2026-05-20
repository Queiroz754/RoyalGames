using Royal_Games.Contexts;
using Royal_Games.Domains;
using Royal_Games.Interfaces;


namespace Royal_Games.Repositories
{
    public class ClassificacaoIndicativaRepository : IClassificacaoIndicativaRepository
    {
        private readonly RoyalGamesContext _context;

        public ClassificacaoIndicativaRepository(RoyalGamesContext context)
        {
            _context = context;
        }

        public List<Classificacao> Listar()
        {
            return _context.Classificacao.ToList();
        }

        public Classificacao ObterPorId(int id)
        {
            Classificacao classificacao = _context.Classificacao.FirstOrDefault(g => g.ClassificacaoID == id);

            return classificacao;
        }

        public bool NomeExiste(string nome, int? classificacaoIdAtual = null)
        {
            var consulta = _context.Classificacao.AsQueryable();

            if (classificacaoIdAtual.HasValue)
            {
                consulta = consulta.Where(g => g.ClassificacaoID != classificacaoIdAtual.Value);
            }

            return consulta.Any(g => g.Nome == nome);
        }

        public void Adicionar(Classificacao classificacao)
        {
            _context.Classificacao.Add(classificacao);
            _context.SaveChanges();
        }

        public void Atualizar(Classificacao classificacao)
        {
            Classificacao classificacaoBanco = _context.Classificacao.FirstOrDefault(g => g.ClassificacaoID == classificacao.ClassificacaoID);

            if (classificacaoBanco == null)
            {
                return;
            }

            classificacaoBanco.Nome = classificacao.Nome;

            _context.SaveChanges();
        }

        public void Remover(int id)
        {
            Classificacao classificacaoBanco = _context.Classificacao.FirstOrDefault(g => g.ClassificacaoID == id);

            if (classificacaoBanco == null)
            {
                return;
            }

            _context.Classificacao.Remove(classificacaoBanco);
            _context.SaveChanges();
        }
    }
}
