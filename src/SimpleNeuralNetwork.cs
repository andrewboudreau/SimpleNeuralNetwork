/// https://rubikscode.net/2022/07/04/implementing-simple-neural-network-in-c/
using System.Data;

public class SimpleNeuralNetwork
{
    private NeuralLayerFactory layerFactory;

    internal List<NeuralLayer> layers;
    internal double learningRate;
    internal double[][] expectedResult;

    /// <summary>
    /// Constructor of the Neural Network.
    /// </summary>
    /// <param name="numberOfInputsNeurons">Number of neurons in the input layer</param>
    /// <remarks>Initially input layer with adefined number of inputs will be created.</remarks>
    public SimpleNeuralNetwork(int numberOfInputsNeurons)
    {
        layers = new List<NeuralLayer>();
        layerFactory = new NeuralLayerFactory();

        // Create input layer that will collect inputs.
        CreateInputLayer(numberOfInputsNeurons);

        learningRate = 2.95;
        expectedResult = Array.Empty<double[]>();
    }

    /// <summary>
    /// Add layer to the neural network.
    /// Layer will automatically be added as the output layer to the last layer in the neural network.
    /// </summary>
    /// <param name="newLayer"></param>
    public void AddLayer(NeuralLayer newLayer)
    {
        if (layers.Any())
        {
            var lastLayer = layers.Last();
            newLayer.ConnectLayers(lastLayer);
        }

        layers.Add(newLayer);
    }

    /// <summary>
    /// Push input values to the neural network.
    /// </summary>
    /// <param name="inputs">The inputs.</param>
    public void PushInputValues(double[] inputs)
    {
        foreach (var neuron in layers.First().Neurons)
        {
            neuron.PushValueOnInput(inputs[layers.First().Neurons.IndexOf(neuron)]);
        }
    }

    /// <summary>
    /// Set the exepected values for the outputs.
    /// </summary>
    /// <param name="expectedOutputs"></param>
    public void PushExpectedValues(double[][] expectedOutputs)
    {
        expectedResult = expectedOutputs;
    }

    /// <summary>
    /// Calculate output of the neural network.
    /// </summary>
    public List<double> GetOutput()
        => layers.Last().Neurons
            .Select(n => n.CalculateOutput())
            .ToList();

    /// <summary>
    /// Train neural network.
    /// </summary>
    /// <param name="inputs">Input values.</param>
    /// <param name="numberOfEpochs">Number of epochs.</param>
    public void Train(double[][] inputs, int numberOfEpochs)
    {
        double totalError = 0;

        for (int i = 0; i < numberOfEpochs; i++)
        {
            for (int j = 0; j < inputs.GetLength(0); j++)
            {
                PushInputValues(inputs[j]);

                // Get outputs.
                var outputs = new List<double>();
                layers.Last().Neurons.ForEach(x =>
                {
                    outputs.Add(x.CalculateOutput());
                });

                // Calculate error by summing errors on all output neurons
                totalError = CalculateTotalError(outputs, j);
                HandleOutputLayer(j);
                HandleHiddenLayers();
            }
        }
    }

    /// <summary>
    /// Helper function that creates input layer of the neural network.
    /// </summary>
    /// <param name="numberOfInputNeurons"></param>
    private void CreateInputLayer(int numberOfInputNeurons)
    {
        var inputLayer = layerFactory.CreateNeuralLayer(numberOfInputNeurons, new RectifiedActivationFunction(), new WeightedSumFunction());
        inputLayer.Neurons.ForEach(x => x.AddInputSynapse(0));
        this.AddLayer(inputLayer);
    }

    private double CalculateTotalError(List<double> outputs, int row)
    {
        double totalError = 0;
        foreach (var output in outputs)
        {
            var error = Math.Pow(output - expectedResult[row][outputs.IndexOf(output)], 2);
            totalError += error;
        }

        return totalError;
    }


    /// <summary>
    /// Helper function that runs backpropagation algorithm on the output layer of the network
    /// </summary>
    /// <param name="row">Input/Expected output row</param>
    private void HandleOutputLayer(int row)
    {
        foreach (var neuron in layers.Last().Neurons)
        {
            foreach (var connection in neuron.Inputs)
            {
                var output = neuron.CalculateOutput();
                var netInput = connection.GetOutput();
                var expectedOutput = expectedResult[row][layers.Last().Neurons.IndexOf(neuron)];

                var nodeDelta = (expectedOutput - output) * output * (1 - output);
                var delta = -1 * netInput * nodeDelta;

                connection.UpdateWeight(learningRate, delta);
                neuron.PreviousPartialDerivate = nodeDelta;
            }
        }
    }

    /// <summary>
    /// Helper function that runs backpropagation algorithm on the hidden layer of the network.
    /// </summary>
    private void HandleHiddenLayers()
    {
        for (int k = layers.Count - 2; k > 0; k--)
        {
            foreach (var neuron in layers[k].Neurons)
            {
                foreach (var connection in neuron.Inputs)
                {
                    var output = neuron.CalculateOutput();
                    var netInput = connection.GetOutput();
                    double sumPartial = 0;

                    foreach (var outputNeuron in layers[k + 1].Neurons)
                    {
                        outputNeuron.Inputs
                            .Where(i => i.IsFromNeuron(neuron.Id))
                            .ToList()
                            .ForEach(outConnection =>
                            {
                                sumPartial += outConnection.PreviousWeight * outputNeuron.PreviousPartialDerivate;
                            });
                    }

                    var delta = -1 * netInput * sumPartial * output * (1 - output);
                    connection.UpdateWeight(learningRate, delta);
                }
            }
        }
    }
}

