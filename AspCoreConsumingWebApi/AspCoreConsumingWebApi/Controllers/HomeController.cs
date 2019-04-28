using System.Collections.Generic;
using System.Threading.Tasks;
using AspCoreConsumingWebApi.Factory;
using AspCoreConsumingWebApi.Models;
using AspCoreConsumingWebApi.Utility;
using AspCoreModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AspCoreConsumingWebApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOptions<MySettingsModel> appSettings;
        private static readonly Dictionary<int, LedModel> lstLeds = new Dictionary<int, LedModel>();

        public HomeController(IOptions<MySettingsModel> app)
        {
            appSettings = app;
            ApplicationSettings.WebApiUrl = appSettings.Value.WebApiBaseUrl;
        }

        public async Task<IActionResult> Index()
        {
            //Inicializa os leds caso não estejam disponíveis
            var data = await ApiClientFactory.Instance.GetLed(1);
            
            //TODO: Transferir configuração dos objetos LedModel para um conf
            //Inicializa POST
            if(data.Id == 0)
            {
                data.Id = 1;
                data.Gpio = 0;
                data.Status = 0;
                data.Desc = "Luz do Quarto";
            }
            if (!lstLeds.ContainsKey(data.Id))
            {
                data.Status = 0;
                lstLeds.Add(data.Id, data);
            }
            var response = await ApiClientFactory.Instance.SetLed(data);

            data = await ApiClientFactory.Instance.GetLed(2);
            //Inicializa POST
            if (data.Id == 0)
            {
                data.Id = 2;
                data.Gpio = 1;
                data.Status = 0;
                data.Desc = "Ventilador de Teto";
            }
            if (!lstLeds.ContainsKey(data.Id))
            {
                data.Status = 0;
                lstLeds.Add(data.Id, data);
            }
            response = await ApiClientFactory.Instance.SetLed(data);

            return View(lstLeds);
        }

        public async Task<IActionResult> SetLed(string model, string action)
        {
            LedModel ledModel = new LedModel();
            try
            {
                int index = int.Parse(action.Substring(0, 1));
                ledModel = lstLeds[index];
                ledModel.Status = action.Contains("DESLIGA") ? 0 : 1;
            }
            catch { }
            if (ledModel.Id != 0)
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