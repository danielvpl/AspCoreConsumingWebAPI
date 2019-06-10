using AspCoreModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreApiClient
{
    public partial class ApiClient
    {
        public async Task<LedModel> GetLed(string id, int gpio)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "AspCoreConsumingWebApi/api/LedModels/" + id + "/" + gpio) );
            return await GetAsync<LedModel>(requestUrl);
        }

        public async Task<List<LedModel>> GetLeds()
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "AspCoreConsumingWebApi/api/LedModels"));
            return await GetAsync<List<LedModel>>(requestUrl);
        }

        public async Task<Message<LedModel>> CreateLed(string model)
        {
            LedModel ledModel = JsonConvert.DeserializeObject<LedModel>(model);
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "AspCoreConsumingWebApi/api/LedModels"));
            return await PostAsync<LedModel>(requestUrl, ledModel);
        }

        public async Task<Message<LedModel>> CreateLed(LedModel model)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "AspCoreConsumingWebApi/api/LedModels"));
            return await PostAsync<LedModel>(requestUrl, model);
        }

        public async Task<Message<LedModel>> SetLed(string model)
        {
            LedModel ledModel = JsonConvert.DeserializeObject<LedModel>(model);
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "AspCoreConsumingWebApi/api/LedModels/" + ledModel.id + "/" + ledModel.gpio));
            return await PutAsync(requestUrl, ledModel);
        }

        public async Task<Message<LedModel>> SetLed(LedModel model)
        {
            var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                "AspCoreConsumingWebApi/api/LedModels/" + model.id+"/"+model.gpio));
            return await PutAsync(requestUrl, model);
        }
    }
}
