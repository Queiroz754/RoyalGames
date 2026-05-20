using Royal_Games.Domains;
using Royal_Games.DTOs.PlataformaDto;
using Royal_Games.Exceptions;
using Royal_Games.Interfaces;

namespace Royal_Games.Applications.Services
{
    public class PlataformaService
    {
            private readonly IPlataformaRepository _repository;

            public PlataformaService(IPlataformaRepository repository)
            {
                _repository = repository;
            }

            public List<lerPlataformaDto> Listar()
            {
                List<Plataforma> plataformas = _repository.Listar();

                List<lerPlataformaDto> plataformaDto = plataformas.Select(g => new lerPlataformaDto
                {
                    PlataformaID = g.PlataformaID,
                    Nome = g.Nome
                }).ToList();

                return plataformaDto;
            }

            public lerPlataformaDto ObterPorId(int id)
            {
            Plataforma plataforma = _repository.ObterPorId(id);

                if (plataforma == null)
                {
                    throw new DomainException("Plataforma não encontrado.");
                }

            lerPlataformaDto plataformaDto = new lerPlataformaDto
            {
                PlataformaID = plataforma.PlataformaID,
                    Nome = plataforma.Nome
                };

                return plataformaDto;
            }

            private static void ValidarNome(string nome)
            {
                if (string.IsNullOrEmpty(nome))
                {
                    throw new DomainException("Nome é obrigatório.");
                }
            }

            public void Adicionar(CriarPlataformaDto criarDto)
            {
                ValidarNome(criarDto.Nome);

                if (_repository.NomeExiste(criarDto.Nome))
                {
                    throw new DomainException("Plataforma já existente.");
                }

            Plataforma plataforma = new Plataforma
            {
                    Nome = criarDto.Nome
                };

                _repository.Adicionar(plataforma);
            }

            public void Atualizar(int id, CriarPlataformaDto criarDto)
            {
                ValidarNome(criarDto.Nome);

            Plataforma plataformaBanco = _repository.ObterPorId(id);

                if (plataformaBanco == null)
                {
                    throw new DomainException("Plataforma não foi encontrado");
                }

                if (_repository.NomeExiste(criarDto.Nome, plataformaIdAtual: id))
                {
                    throw new DomainException("Já existe outra Plataforma com esse nome.");
                }

            plataformaBanco.Nome = criarDto.Nome;
                _repository.Atualizar(plataformaBanco);
            }

            public void Remover(int id)
            {
            Plataforma plataformaBanco = _repository.ObterPorId(id);

                if (plataformaBanco == null)
                {
                    throw new DomainException("Plataforma não encontrado.");
                }

                _repository.Remover(id);
            }
        }
    }

