/// https://rubikscode.net/2022/07/04/implementing-simple-neural-network-in-c/
public class SigmoidActivationFunction : IActivationFunction
{
    private readonly double coeficient;

    public SigmoidActivationFunction(double coeficient)
    {
        this.coeficient = coeficient;
    }

    public double CalculateOutput(double input)
        => 1 / (1 + Math.Exp(-input * coeficient));
}

