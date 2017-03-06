using System.Collections.Generic;
using System.Data.SqlClient;
using ExemploDapperDominio.Entidades;

namespace ExemploDapperDominio.Interfaces.Repositorios
{
    public interface IBaseRepositorio<TEntidade> where TEntidade : class
    {
        SqlConnection AbrirConexao();

        TEntidade Inserir(TEntidade entidade);

        bool Atualizar(TEntidade entidade);

        bool Remover(TEntidade entidade);

        IEnumerable<TEntidade> InserirEmMassa(IEnumerable<TEntidade> entidades);

        int? AtualizarEmMassa(IEnumerable<TEntidade> entidades);

        int? RemoverEmMassa(IEnumerable<TEntidade> entidades);

        TEntidade ObterPorChave(object chave);

        TEntidade ObterPorChave(object[] chave);

        Contagem Contar(string condicoes = null);

        TEntidade Buscar(string condicoes);

        IEnumerable<TEntidade> Listar(int deslocamento, int limite, string condicoes, string ordenarPor);
    }
}
