using System.Threading.Tasks;
using I2AIExtension.Editor.AiProviders.Settings;
using I2AIExtension.Editor.Models;
using I2AIExtension.Editor.PromtFactories;

namespace I2AIExtension.Editor.AiProviders
{
    public interface IAiTranslateProvider
    {
        Task<TranslatedData> GetTranslate(TranslatedPromtData promtData, BaseTranslateProviderSettings settings, PromtFactoryBase promtFactory);
    }
}