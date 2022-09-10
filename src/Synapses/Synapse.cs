/// https://rubikscode.net/2022/07/04/implementing-simple-neural-network-in-c/
internal class Synapse : ISynapse
{
    internal INeuron fromNeuron;
    internal INeuron toNeuron;

    public Synapse(INeuron fromNeuron, INeuron toNeuron, double weight)
    {
        this.fromNeuron = fromNeuron;
        this.toNeuron = toNeuron;

        Weight = weight;
        PreviousWeight = 0;
    }

    public Synapse(INeuron fromNeuron, INeuron toNeuron)
        : this(fromNeuron, toNeuron, Random.Shared.NextDouble())
    {
    }

    /// <inheritdoc />
    public double Weight { get; set; }

    /// <inheritdoc />
    public double PreviousWeight { get; set; }

    /// <inheritdoc />
    public double GetOutput()
    {
        return fromNeuron.CalculateOutput();
    }

    /// <inheritdoc />
    public bool IsFromNeuron(Guid fromNeuronId)
    {
        return fromNeuron.Id.Equals(fromNeuronId);
    }

    /// <inheritdoc />
    public void UpdateWeight(double learningRate, double delta)
    {
        PreviousWeight += Weight;
        Weight += learningRate * delta;
    }
}

