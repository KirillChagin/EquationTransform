using System.Collections.Generic;

namespace EquationTransform.Transformator.Contract
{
    public interface IEquationTransformator
    {
        string Transform(string equation);

        List<string> TransformAll(List<string> equations);
    }
}
