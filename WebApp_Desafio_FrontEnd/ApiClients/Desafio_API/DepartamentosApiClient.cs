using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using WebApp_Desafio_FrontEnd.ViewModels;

namespace WebApp_Desafio_FrontEnd.ApiClients.Desafio_API
{
    public class DepartamentosApiClient : BaseClient
    {
        private const string tokenAutenticacao = "AEEFC184-9F62-4B3E-BB93-BE42BF0FFA36";

        private const string departamentosListUrl = "api/Departamentos/Listar";
        private const string departamentosGravarUrl = "api/Departamentos/Gravar";
        private const string departamentosExcluirUrl = "api/Departamentos/Excluir";
        private const string departamentoEditarUrl = "api/Departamentos/editar";
        private const string departamentosObterUrl = "api/Departamentos/Obter";


        private string desafioApiUrl = "https://localhost:44388/"; // Endereço API IIS-Express

        private readonly Dictionary<string, object> _headers;

        public DepartamentosApiClient() : base()
        {
            //TODO
            _headers = new Dictionary<string, object>()
            {
                { "TokenAutenticacao", tokenAutenticacao }
            };
        }

        public List<DepartamentoViewModel> DepartamentosListar()
        {
            var querys = default(Dictionary<string, object>); // Não há parâmetros para essa chamada

            var response = base.Get($"{desafioApiUrl}{departamentosListUrl}", querys, _headers);

            base.EnsureSuccessStatusCode(response);

            string json = base.ReadHttpWebResponseMessage(response);

            return JsonConvert.DeserializeObject<List<DepartamentoViewModel>>(json);
        }

        internal bool DepartamentoGravar(DepartamentoViewModel model)
        {
            var response = Post($"{desafioApiUrl}{departamentosGravarUrl}", model, _headers);

            EnsureSuccessStatusCode(response);

            var json = ReadHttpWebResponseMessage(response);

            return JsonConvert.DeserializeObject<bool>(json);
        }

        internal bool DepartamentoExcluir(int id)
        {
            var response = Delete($"{desafioApiUrl}{departamentosExcluirUrl}/{id}", _headers);
            EnsureSuccessStatusCode(response);
            var json = ReadHttpWebResponseMessage(response);

            return JsonConvert.DeserializeObject<bool>(json);
        }

        internal DepartamentoViewModel DepartamentoObter(int idDepartamento)
        {
            var querys = new Dictionary<string, object>()
            {
                { "idDepartamento", idDepartamento }
            };
            var response = Get($"{desafioApiUrl}{departamentosObterUrl}", querys, _headers);

            EnsureSuccessStatusCode(response);

            var json = ReadHttpWebResponseMessage(response);

            return JsonConvert.DeserializeObject<DepartamentoViewModel>(json);
        }
    }
}
