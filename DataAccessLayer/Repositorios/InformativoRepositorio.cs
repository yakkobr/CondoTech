﻿using Dapper;
using DataAccessLayer.Conexao;
using Model;
using Model.Enum;
using Model.QueryModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DataAccessLayer.Repositorios
{
    public class InformativoRepositorio : ConexaoSql
    {
        public void Inserir(Informativo informativo)
        {
            using (Connection = new SqlConnection(StringConnection))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@IdFuncionario", informativo.Funcionario.Id);
                parametros.Add("@Titulo", informativo.Titulo);
                parametros.Add("@Descricao", informativo.Descricao);
                parametros.Add("@DataCadastro", DateTime.Now);
                parametros.Add("@Ativo", EntidadeAtiva.Ativo);

                Connection.Execute("Insert_Informativo", parametros, commandType: CommandStoredProcedure);
            }
        }

        public void Atualizar(Informativo informativo)
        {
            using (Connection = new SqlConnection(StringConnection))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@IdInformativo", informativo.Id);
                parametros.Add("@IdFuncionario", informativo.Funcionario.Id);
                parametros.Add("@Titulo", informativo.Titulo);
                parametros.Add("@Descricao", informativo.Descricao);
                parametros.Add("@DataCadastro", DateTime.Now);
                parametros.Add("@Ativo", EntidadeAtiva.Ativo);

                Connection.Execute("Update_Informativo", parametros, commandType: CommandStoredProcedure);
            }
        }

        public void Excluir(int id)
        {
            using (Connection = new SqlConnection(StringConnection))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@IdInformativo", id);
                parametros.Add("@Ativo", EntidadeAtiva.Inativo);
                Connection.Execute("Delete_Informativo", parametros, commandType: CommandStoredProcedure);
            }
        }

        public IEnumerable<ObterInformativo> ObterInformativo()
        {
            using (Connection = new SqlConnection(StringConnection))
            {
                const string sqlQuery = "select i.IdInformativo, " +
                                        "f.Nome, " +
                                        "c.Nome as 'Cargo', " +
                                        "i.Titulo, " +
                                        "i.Descricao, " +
                                        "i.DataCadastro from Funcionario f " +
                                        "join Cargo c on c.IdCargo = f.IdCargo " +
                                        "join Informativo i on i.IdFuncionario = f.IdFuncionario " +
                                        "where i.Ativo = 0";

                return Connection.Query<ObterInformativo>(sqlQuery);
            }
        }
    }
}
