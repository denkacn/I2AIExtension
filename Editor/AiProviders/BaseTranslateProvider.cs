using System;
using System.IO;
using System.Threading.Tasks;
using I2AIExtension.Editor.AiProviders.Settings;
using I2AIExtension.Editor.Models;
using I2AIExtension.Editor.PromtFactories;
using UnityEngine;

namespace I2AIExtension.Editor.AiProviders
{
    public abstract class BaseTranslateProvider : IAiTranslateProvider
    {
        public abstract Task<TranslatedData> GetTranslate(TranslatedPromtData promtData,
            BaseTranslateProviderSettings settings, PromtFactoryBase promtFactory);

        protected async Task<string> GetToken(BaseTranslateProviderSettings settings)
        {
            if (!settings.IsTokenFromFile || string.IsNullOrEmpty(settings.TokenFilePath)) return settings.Token;
    
            try
            {
                var result = await File.ReadAllTextAsync(settings.TokenFilePath);
                return result?.Trim() ?? settings.Token;
            }
            catch (Exception ex)
            {
                Debug.LogError($"File read error: {ex.Message}");
                return settings.Token;
            }
        }
    }
}