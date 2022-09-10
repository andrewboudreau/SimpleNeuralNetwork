/// https://rubikscode.net/2022/07/04/implementing-simple-neural-network-in-c/
public class RectifiedActivationFunction : IActivationFunction
{
    public double CalculateOutput(double input)
        => Math.Max(0, input);
}

