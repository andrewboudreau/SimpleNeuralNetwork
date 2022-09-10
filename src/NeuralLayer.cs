/// https://rubikscode.net/2022/07/04/implementing-simple-neural-network-in-c/
public class NeuralLayer
{
    public List<INeuron> Neurons;

    public NeuralLayer()
    {
        Neurons = new List<INeuron>();
    }

    public void ConnectLayers(NeuralLayer inputLayer)
    {
        //foreach (var neuron in Neurons)
        //{
        //    foreach (var input in inputLayer.Neurons)
        //    {
        //        neuron.AddInputNeuron(input);
        //    }
        //}

        var combos = Neurons.SelectMany(_ => inputLayer.Neurons, (neuron, input) => new { neuron, input });
        combos.ToList().ForEach(x => x.neuron.AddInputNeuron(x.input));
    }
}

