using Royal_Games.Domains;
using Royal_Games.Exceptions;
using Royal_Games.Interfaces;
using RoyalGames.DTOs.GeneroDto;


namespace RoyalGames.Applications.Services
{
    public class GeneroService
    {
        private readonly IGeneroRepository _repository;

        public GeneroService(IGeneroRepository repository)
        {
            _repository = repository;
        }

        public List<LerGeneroDto> Listar()
        {
            List<Genero> generos = _repository.Listar();

            List<LerGeneroDto> generoDto = generos.Select(g => new LerGeneroDto
            {
                GeneroID = g.GeneroID,
                Nome = g.Nome
            }).ToList();

            return generoDto;
        }

        public LerGeneroDto ObterPorId(int id)
        {
            Genero genero = _repository.ObterPorId(id);

            if (genero == null)
            {
                throw new DomainException("Gênero não encontrado.");
            }

            LerGeneroDto generoDto = new LerGeneroDto
            {
                GeneroID = genero.GeneroID,
                Nome = genero.Nome
            };

            return generoDto;
        }

        private static void ValidarNome(string nome)
        {
            if (string.IsNullOrEmpty(nome))
            {
                throw new DomainException("Nome é obrigatório.");
            }
        }

        public void Adicionar(CriarGeneroDto criarDto)
        {
            ValidarNome(criarDto.Nome);

            if (_repository.NomeExiste(criarDto.Nome))
            {
                throw new DomainException("Gênnero já existente.");
            }

            Genero genero = new Genero
            {
                Nome = criarDto.Nome
            };

            _repository.Adicionar(genero);
        }

        public void Atualizar(int id, CriarGeneroDto criarDto)
        {
            ValidarNome(criarDto.Nome);

            Genero generoBanco = _repository.ObterPorId(id);

            if (generoBanco == null)
            {
                throw new DomainException("Gênero não foi encontrado");
            }

            if (_repository.NomeExiste(criarDto.Nome, generoIdAtual: id))
            {
                throw new DomainException("Já existe outra categoria com esse nome.");
            }

            generoBanco.Nome = criarDto.Nome;
            _repository.Atualizar(generoBanco);
        }

        public void Remover(int id)
        {
            Genero generoBanco = _repository.ObterPorId(id);

            if (generoBanco == null)
            {
                throw new DomainException("Gênero não encontrado.");
            }

            _repository.Remover(id);
        }
    }
}