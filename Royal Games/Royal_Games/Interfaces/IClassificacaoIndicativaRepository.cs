using Royal_Games.Domains;

namespace Royal_Games.Interfaces
{
    public interface IClassificacaoIndicativaRepository
    {
       
            List<Classificacao> Listar();
            Classificacao ObterPorId(int id);
            bool NomeExiste(string nome, int? classificacaoIdAtual = null);
            void Adicionar(Classificacao classificacao);
            void Atualizar(Classificacao classificacao);
            void Remover(int id);
        
    }

}
