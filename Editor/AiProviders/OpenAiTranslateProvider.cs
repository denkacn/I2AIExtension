using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using I2.AiExtension.Editor.AiProviders;
using I2.AiExtension.Editor.AiProviders.Settings;
using I2AIExtension.Editor.Models;
using I2AIExtension.Editor.PromtFactories;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace I2AIExtension.Editor.AiProviders
{
    public class OpenAiTranslateProvider : IAiTranslateProvider
    {
        public virtual async Task<TranslatedData> GetTranslate(TranslatedPromtData promtData, BaseTranslateProviderSettings settings, PromtFactoryBase promtFactory)
        {
            var request = new Request()
            {
                model = settings.Model,
                input = promtFactory.GetPromt(promtData),
            };
            
            var jsonBody = JsonConvert.SerializeObject(request);
            Debug.Log(jsonBody);
            
            var bodyRaw = Encoding.UTF8.GetBytes(jsonBody);
            var apiUrl = string.Concat(settings.Host, settings.Endpoint);

            using (var www = new UnityWebRequest(apiUrl, "POST"))
            {
                www.uploadHandler = new UploadHandlerRaw(bodyRaw);
                www.downloadHandler = new DownloadHandlerBuffer();
                www.SetRequestHeader("Content-Type", "application/json");

                if (!string.IsNullOrEmpty(settings.Token))
                    www.SetRequestHeader("Authorization", $"Bearer {settings.Token}");

                www.timeout = 300;

                await www.SendWebRequest();
                
                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError("[LmStudioTranslateProvider] Web Request error: " + www.error);
                }
                else
                {
                    Response? response = null;
                    
                    try
                    {
                        Debug.Log($"[LmStudioTranslateProvider] The text of the model:  {www.downloadHandler.text}");
                        response = JsonConvert.DeserializeObject<Response>(www.downloadHandler.text);
                    }
                    catch (Exception exp)
                    {
                        Debug.LogError(exp);
                        return new TranslatedData(promtData.Term, string.Empty).Failure();
                    }
                    
                    var output = response?.output.Find(o=>o.type == "message");
                    if (output != null)
                    {
                        var content = output.content[0].text;
                        
                        if (content.Length >= 2)
                        {
                            var result = content;
                            
                            if (content.StartsWith("{") && content.EndsWith("}"))
                            {
                                result = content.Substring(1, content.Length - 2);
                            }
                            
                            return new TranslatedData(promtData.Term, result);
                        }
                    }

                    return new TranslatedData(promtData.Term, string.Empty).Failure();
                }

            }

            return new TranslatedData(promtData.Term, string.Empty).Failure();
        }
        
        private struct Request
        {
            public string model { get; set; }
            public string input { get; set; }
        }
        
        private class Response
        {
            public List<Output> output { get; set; }
        }

        private class Output
        {
            public string type { get; set; }
            public string message { get; set; }
            public Content[] content { get; set; }
            public string[] summary { get; set; }
        }
        
        private class Content
        {
            public string type { get; set; }
            public string text { get; set; }
        }
    }
}