using I2AIExtension.Editor.Models;

namespace I2AIExtension.Editor.PromtFactories
{
    public class PromtFactorySimple : PromtFactoryBase
    {
        public override string GetPromt(TranslatedPromtData promtData)
        {
            return string.Format(promtData.Promt, promtData.Language, promtData.From);
        }
    }
}