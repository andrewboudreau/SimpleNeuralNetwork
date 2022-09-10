/// https://rubikscode.net/2022/07/04/implementing-simple-neural-network-in-c/
using System.Data;

public class WeightedSumFunction : IInputFunction
{
    public double CalculateInput(List<ISynapse> inputs)
        => inputs.Select(x => x.Weight * x.GetOutput()).Sum();
}

