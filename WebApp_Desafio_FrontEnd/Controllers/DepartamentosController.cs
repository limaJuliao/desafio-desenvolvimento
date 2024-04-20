using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApp_Desafio_FrontEnd.ApiClients.Desafio_API;
using WebApp_Desafio_FrontEnd.ViewModels;
using WebApp_Desafio_FrontEnd.ViewModels.Enums;

namespace WebApp_Desafio_FrontEnd.Controllers
{
    public class DepartamentosController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction(nameof(Listar));
        }

        [HttpGet]
        public IActionResult Listar()
        {
            // Busca de dados está na Action Datatable()
            return View();
        }

        [HttpGet]
        public IActionResult Datatable()
        {
            try
            {
                var departamentosApiClient = new DepartamentosApiClient();
                var lstDepartamentos = departamentosApiClient.DepartamentosListar();

                var dataTableVM = new DataTableAjaxViewModel()
                {
                    length = lstDepartamentos.Count,
                    data = lstDepartamentos
                };

                return Ok(dataTableVM);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseViewModel(ex));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Cadastrar()
        {
            ViewData["Title"] = "Cadastrar Novo Departamento";

            return View(nameof(Cadastrar), new DepartamentoViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(DepartamentoViewModel model)
        {
            try
            {
                var departamentosClient = new DepartamentosApiClient();
                var realizadoComSucesso = departamentosClient.DepartamentoGravar(model);

                if (realizadoComSucesso)
                    return Ok(new ResponseViewModel(
                                "Departamento gravado com sucesso!",
                                AlertTypes.success,
                                RouteData.Values["controller"].ToString(),
                                nameof(Listar)));
                else
                    throw new ApplicationException($"Falha ao gravar o Departamento");


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
