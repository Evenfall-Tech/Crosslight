namespace Crosslight.Core;

public interface ILanguage
{
    Node? TransformInput(Resource resource);

    Resource? TransformOutput(Node node);

    Node? TransformModify(Node node);

    ResourceTypes? ResourceTypesInput { get; }

    ResourceTypes? ResourceTypesOutput { get; }
}
