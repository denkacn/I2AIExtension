using I2AIExtension.Editor.Models;

namespace I2AIExtension.Editor.PromtFactories
{
    public abstract class PromtFactoryBase
    {
        public abstract string GetPromt(TranslatedPromtData promtData);
    }
}