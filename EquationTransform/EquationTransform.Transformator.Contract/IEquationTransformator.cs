namespace EquationTransform.Transformator.Contract
{
    public interface IEquationTransformator
    {
        /// <summary>
        /// Transform equation string to a canonical form
        /// </summary>
        /// <param name="equation">input equation</param>
        /// <returns>canonical form of equation</returns>
        string TransformToCanonical(string equation);
    }
}
