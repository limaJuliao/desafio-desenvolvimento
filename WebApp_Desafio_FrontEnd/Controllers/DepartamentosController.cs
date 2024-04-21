using AspNetCore.Reporting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using WebApp_Desafio_FrontEnd.ApiClients.Desafio_API;
using WebApp_Desafio_FrontEnd.ViewModels;
using WebApp_Desafio_FrontEnd.ViewModels.Enums;

namespace WebApp_Desafio_FrontEnd.Controllers
{
    public class DepartamentosController : Controller
    {
        private readonly IHostingEnvironment _hostEnvironment;

        public DepartamentosController(IHostingEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

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

        [HttpGet]
        public IActionResult Editar([FromRoute] int id)
        {
            ViewData["Title"] = "Editar Departamento";

            try
            {
                var client = new DepartamentosApiClient();
                var model = client.DepartamentoObter(id);

                return View(nameof(Cadastrar), model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult Excluir([FromRoute] int id)
        {
            try
            {
                var client = new DepartamentosApiClient();
                var realizadoComSucesso = client.DepartamentoExcluir(id);

                if (realizadoComSucesso)
                    return Ok(new ResponseViewModel(
                                $"Departamento {id} excluído com sucesso!",
                                AlertTypes.success,
                                "Departamentos",
                                nameof(Listar)));
                else
                    throw new ApplicationException($"Falha ao excluir o Departamento {id}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Report()
        {
            var contentRootPath = _hostEnvironment.ContentRootPath;
            var path = Path.Combine(contentRootPath, "wwwroot", "reports", "rptDepartamentos.rdlc");
            var localReport = new LocalReport(path);
            var client = new DepartamentosApiClient();
            var departamentos = client.DepartamentosListar();
            localReport.AddDataSource("dsDepartamentos", departamentos);
            var result = localReport.Execute(RenderType.Pdf);

            return File(result.MainStream, "application/octet-stream", "rptDepartamentos.pdf");

            throw new NotImplementedException();
        }
    }
}
