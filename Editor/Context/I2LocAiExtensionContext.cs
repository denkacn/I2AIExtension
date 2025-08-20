using System;
using I2.AiExtension.Editor.AiProviders;
using I2.AiExtension.Editor.AiProviders.Settings;
using I2AIExtension.Editor.AiProviders;
using I2AIExtension.Editor.PromtFactories;

namespace I2AIExtension.Editor.Context
{
    [Serializable]
    public class I2LocAiExtensionContext
    {
        public IAiTranslateProvider TranslateProvider { get; set; }
        public BaseTranslateProviderSettings TranslateSettings { get; set; }
        public string BaseLanguage { get; set; }
        public PromtFactoryBase PromtFactory { get; set; }
    }
}