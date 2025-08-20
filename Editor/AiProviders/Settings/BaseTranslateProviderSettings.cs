using System;
using I2AIExtension.Editor.AiProviders;

namespace I2.AiExtension.Editor.AiProviders.Settings
{
    [Serializable]
    public class BaseTranslateProviderSettings
    {
        public string Id { get; set; }
        public string Host { get; set; }
        public string Endpoint { get; set; }
        public string  Token { get; set; }
        public string  Model { get; set; }
        public AiProviderType ProviderType { get; set; }
    }
}