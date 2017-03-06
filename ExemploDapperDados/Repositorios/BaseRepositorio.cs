using ExemploDapperDominio.Interfaces.Repositorios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using ExemploDapperDominio.Entidades;

namespace ExemploDapperDados.Repositorios
{
    public abstract class BaseRepositorio<TEntidade> : IDisposable, IBaseRepositorio<TEntidade> where TEntidade : class 
    {

        private readonly string _strConexao = "";

        public SqlConnection AbrirConexao()
        {
            var conexaoBd = new SqlConnection(_strConexao);
            conexaoBd.Open();
            return conexaoBd;
        }

        public virtual TEntidade Inserir(TEntidade entidade)
        {
            using (var db = AbrirConexao())
            {
                return db.Insert<TEntidade>(entidade);
            }
        }

        public virtual bool Atualizar(TEntidade entidade)
        {
            using (var db = AbrirConexao())
            {
                return db.Update(entidade) > 0;
            }
        }

        public virtual bool Remover(TEntidade entidade)
        {
            using (var db = AbrirConexao())
            {
                return db.Delete(entidade) > 0;
            }
        }

        public virtual IEnumerable<TEntidade> InserirEmMassa(IEnumerable<TEntidade> entidades)
        {
            var entidadesAtualizadas = new List<TEntidade>();
            using (var db = AbrirConexao())
            {
                entidadesAtualizadas.AddRange(entidades.Select(entidade => db.Insert<TEntidade>(entidade)));
            }
            return entidadesAtualizadas.ToList();
        }

        public virtual int? AtualizarEmMassa(IEnumerable<TEntidade> entidades)
        {
            var entidadesAtualizadas = new List<int>();
            using (var db = AbrirConexao())
            {
                entidadesAtualizadas.AddRange(entidades.Select(entidade => db.Update(entidade)));
            }
            return entidadesAtualizadas.Count;
        }

        public virtual int? RemoverEmMassa(IEnumerable<TEntidade> entidades)
        {
            var entidadesAtualizadas = new List<int>();
            using (var db = AbrirConexao())
            {
                entidadesAtualizadas.AddRange(entidades.Select(entidade => db.Delete(entidade)));
            }
            return entidadesAtualizadas.Count;
        }

        public virtual TEntidade ObterPorChave(object chave)
        {
            return ObterPorChave(new[] { chave });
        }

        public virtual TEntidade ObterPorChave(object[] chave)
        {
            using (var db = AbrirConexao())
            {
                return db.Get<TEntidade>(chave);
            }
        }

        public virtual Contagem Contar(string condicoes = null)
        {
            using (var db = AbrirConexao())
            {
                return condicoes == null
                ? new Contagem { Total = db.RecordCount<TEntidade>() }
                : new Contagem { Total = db.RecordCount<TEntidade>(condicoes) };
            }
        }

        public virtual TEntidade Buscar(string condicoes)
        {
            using (var db = AbrirConexao())
            {
                return db.Get<TEntidade>(condicoes);
            }
        }

        public virtual IEnumerable<TEntidade> Listar(int deslocamento, int limite, string condicoes,string ordenarPor)
        {
            using (var db = AbrirConexao())
            {
                return db.GetListPaged<TEntidade>(deslocamento, limite, condicoes, ordenarPor);
            }
        }

        public void Dispose()
        {
            var conexaoBd = AbrirConexao();

            if (conexaoBd.State != ConnectionState.Closed)
            {
                conexaoBd.Close();
            }
        }
    }
}
