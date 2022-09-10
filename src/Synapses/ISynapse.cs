/// https://rubikscode.net/2022/07/04/implementing-simple-neural-network-in-c/
public interface ISynapse
{
    /// <summary>
    /// Gets or set the weight of the connection.
    /// </summary>
    double Weight { get; set; }

    /// <summary>
    /// Gets or sets the Weight that conenctio had in a previous iteration.
    /// </summary>
    double PreviousWeight { get; set; }

    /// <summary>
    /// The output value of the connection.
    /// </summary>
    /// <returns>Output value of the connection.</returns>
    double GetOutput();

    /// <summary>
    /// Checks if Neuron has a certian number as in input neruon.
    /// </summary>
    /// <param name="fromNeuronId">The Neuron id.</param>
    /// <returns>True of the neron is the input of the connection, false if the neuron is not the the input of the connection.</returns>
    bool IsFromNeuron(Guid fromNeuronId);

    /// <summary>
    /// Update weight.
    /// </summary>
    /// <param name="learningRate">Choosen learning rate.</param>
    /// <param name="delta">Calculated difference for which weight of the connection needs to be modified.</param>
    void UpdateWeight(double learningRate, double delta);
}

