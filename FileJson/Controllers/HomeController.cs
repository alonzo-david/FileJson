using FileJson.Interfaces;
using FileJson.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FileJson.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        readonly IFileUploadService _bufferedFileUploadService;
        readonly IEmpresaService _empresaService;

        //public HomeController(IBufferedFileUploadService bufferedFileUploadService)
        //{
        //    _bufferedFileUploadService = bufferedFileUploadService;
        //}

        public HomeController(ILogger<HomeController> logger, 
            IFileUploadService bufferedFileUploadService,
            IEmpresaService empresaService)
        {
            _logger = logger;
            _bufferedFileUploadService = bufferedFileUploadService;
            _empresaService = empresaService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(IFormFile file)
        {
            try
            {
                if (await _bufferedFileUploadService.UploadFile(file))
                {
                    ViewBag.Message = "Datos cargados con éxito!";
                }
                else
                {
                    ViewBag.Message = "Ocurrió un problema";
                }
            }
            catch (Exception ex)
            {
                //Log ex
                ViewBag.Message = ex.Message;
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Listado()
        {
            var model = await _empresaService.Get();
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var model = await _empresaService.Get(id);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Editar(int id, Empresa empresa)
        {
            var success = await _empresaService.Update(id, empresa);
            return RedirectToAction(nameof(Listado));
        }

        public async Task<IActionResult> Eliminar(int id)
        {
            var success = await _empresaService.Delete(id);
            return RedirectToAction(nameof(Listado));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}