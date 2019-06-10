using System.Collections.Generic;
using System.Threading.Tasks;
using AspCoreConsumingWebApi.Factory;
using AspCoreConsumingWebApi.Models;
using AspCoreConsumingWebApi.Utility;
using AspCoreModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AspCoreConsumingWebApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOptions<MySettingsModel> appSettings;
        private readonly ModelContext _context;

        public HomeController(IOptions<MySettingsModel> app, ModelContext context)
        {
            appSettings = app;
            ApplicationSettings.WebApiUrl = appSettings.Value.WebApiBaseUrl;
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            IEnumerable<LedModel> lstLeds = new LedModelsController(_context).GetLedModels();
            return View(lstLeds);
        }

        public async Task<IActionResult> SetLed(string model, string id, string action)
        {
            LedModel ledModel = new LedModel();
            try
            {
                int gpio = int.Parse(action.Substring(0, 2));
                ledModel = await _context.LedModels.SingleOrDefaultAsync(m => m.id == id && m.gpio == gpio);
                ledModel.status = action.Contains("DESLIGA") ? 0 : 1;
            }
            catch { }
            if (!string.IsNullOrEmpty(ledModel.id))
            {
                var response = await ApiClientFactory.Instance.SetLed(ledModel);
            }
            if (!string.IsNullOrEmpty(model))
            {
                var response = await ApiClientFactory.Instance.SetLed(model);
            }
            return RedirectToAction("Index");
        }
    }
}