﻿/// https://rubikscode.net/2022/07/04/implementing-simple-neural-network-in-c/
/// <summary>
/// Factory used to create layers.
/// </summary>
public class NeuralLayerFactory
{
    public NeuralLayer CreateNeuralLayer(int numberOfNeurons, IActivationFunction activationFunction, IInputFunction inputFunction)
    {
        var layer = new NeuralLayer();
        for (int i = 0; i < numberOfNeurons; i++)
        {
            var neuron = new Neuron(activationFunction, inputFunction);
            layer.Neurons.Add(neuron);
        }

        return layer;
    }
}

