/// https://rubikscode.net/2022/07/04/implementing-simple-neural-network-in-c/
public class StepActivationFunction : IActivationFunction
{
    private readonly double threshold;

    public StepActivationFunction(double threshold)
    {
        this.threshold = threshold;
    }

    public double CalculateOutput(double input)
        => Convert.ToDouble(input > threshold);
}

