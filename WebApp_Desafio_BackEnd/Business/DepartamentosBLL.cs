using System;
using System.Collections.Generic;
using WebApp_Desafio_BackEnd.DataAccess;
using WebApp_Desafio_BackEnd.Models;

namespace WebApp_Desafio_BackEnd.Business
{
    public class DepartamentosBLL
    {
        private DepartamentosDAL dal = new DepartamentosDAL();

        public bool GravarDepartamento(int id, string descricao)
        {
            return dal.GravarDepartamento(id, descricao);
        }

        public IEnumerable<Departamento> ListarDepartamentos()
        {
            return dal.ListarDepartamentos();
        }
    }
}
