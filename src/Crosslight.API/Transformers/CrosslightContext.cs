namespace Crosslight.API.Transformers
{
    public class CrosslightContext
    {
        public ITransformer InputTransformer { get; set; }
        public ITransformer OutputTransformer { get; set; }
    }
}
