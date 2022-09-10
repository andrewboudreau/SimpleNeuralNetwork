/// https://rubikscode.net/2022/07/04/implementing-simple-neural-network-in-c/
public class InputSynapse : ISynapse
{
    internal INeuron toNeuron;

    public InputSynapse(INeuron toNeuron)
    {
        this.toNeuron = toNeuron;
        Weight = 1;
    }

    public InputSynapse(INeuron toNeuron, double output)
    {
        this.toNeuron = toNeuron;
        Output = output;
    }

    public double Weight { get; set; }
    public double Output { get; set; }
    public double PreviousWeight { get; set; }

    public double GetOutput()
    {
        return Output;
    }

    public bool IsFromNeuron(Guid fromNeuronId) => false;

    public void UpdateWeight(double learningRate, double delta)
        => throw new InvalidOperationException("It is not allowed to call this method on Input Connecion");
}

