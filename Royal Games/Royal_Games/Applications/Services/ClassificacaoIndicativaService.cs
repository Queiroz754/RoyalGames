using Royal_Games.Domains;
using Royal_Games.DTOs.ClassificacaoDto;
using Royal_Games.Exceptions;
using Royal_Games.Interfaces;
using RoyalGames.DTOs.GeneroDto;

namespace Royal_Games.Applications.Services
{
    public class ClassificacaoIndicativaService
    {
        private readonly IClassificacaoIndicativaRepository _repository;

        public ClassificacaoIndicativaService(IClassificacaoIndicativaRepository repository)
        {
            _repository = repository;
        }

        public List<LerClassificacaoDto> Listar()
        {
            List<Classificacao> Classificacoes = _repository.Listar();

            List<LerClassificacaoDto> ClassificacaoDto = Classificacoes.Select(g => new LerClassificacaoDto
            {
                ClassificacaoID = g.ClassificacaoID,
                Nome = g.Nome
            }).ToList();

            return ClassificacaoDto;
        }

        public LerClassificacaoDto ObterPorId(int id)
        {
            Classificacao classificacao = _repository.ObterPorId(id);

            if (classificacao == null)
            {
                throw new DomainException("Classificação não encontrado.");
            }

            LerClassificacaoDto classificacaoDto = new LerClassificacaoDto
            {
                ClassificacaoID = classificacao.ClassificacaoID,
                Nome = classificacao.Nome
            };

            return classificacaoDto;
        }

        private static void ValidarNome(string nome)
        {
            if (string.IsNullOrEmpty(nome))
            {
                throw new DomainException("Nome é obrigatório.");
            }
        }

        public void Adicionar(CriarClassificacaoDto criarDto)
        {
            ValidarNome(criarDto.Nome);

            if (_repository.NomeExiste(criarDto.Nome))
            {
                throw new DomainException("Classificação já existente.");
            }

            Classificacao classificacao = new Classificacao
            {
                Nome = criarDto.Nome
            };

            _repository.Adicionar(classificacao);
        }

        public void Atualizar(int id, CriarClassificacaoDto criarDto)
        {
            ValidarNome(criarDto.Nome);

            Classificacao classificacaoBanco = _repository.ObterPorId(id);

            if (classificacaoBanco == null)
            {
                throw new DomainException("Classificacao não foi encontrado");
            }

            if (_repository.NomeExiste(criarDto.Nome, classificacaoIdAtual: id))
            {
                throw new DomainException("Já existe outra categoria com esse nome.");
            }

            classificacaoBanco.Nome = criarDto.Nome;
            _repository.Atualizar(classificacaoBanco);
        }

        public void Remover(int id)
        {
            Classificacao classificacaoBanco = _repository.ObterPorId(id);

            if (classificacaoBanco == null)
            {
                throw new DomainException("Classificação não encontrado.");
            }

            _repository.Remover(id);
        }
    }
}
