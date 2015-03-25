namespace Oentities.Configurations
{
    internal interface IModelBuilderWithSetAllNullInverseReference : IModelBuilder
    {
        void SetAllNullInverseReferenceProperties();
    }
}