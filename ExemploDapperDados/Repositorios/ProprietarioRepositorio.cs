using System.Collections.Generic;
using Dapper;
using ExemploDapperDominio.Entidades;
using ExemploDapperDominio.Interfaces.Repositorios;

namespace ExemploDapperDados.Repositorios
{
    public class ProprietarioRepositorio : BaseRepositorio<Proprietario>, IProprietarioRepositorio
    {
        public void ListarComVinculos(string sql)
        {
            using (var db = AbrirConexao())
            {
                db.Query<Proprietario, ICollection<Animal>, Proprietario>(sql, (proprietario, animal) =>
                {
                    proprietario.Animais = animal;
                    return proprietario;
                });
            }
        }
    }
}
