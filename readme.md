follow along at https://rubikscode.net/2022/07/04/implementing-simple-neural-network-in-c/

```csharp
Console.WriteLine("Hello, Neural Networks!");

var network = new SimpleNeuralNetwork(3);

var layerFactory = new NeuralLayerFactory();
network.AddLayer(
layerFactory.CreateNeuralLayer(3, new RectifiedActivationFunction(), new WeightedSumFunction()));

network.AddLayer(
layerFactory.CreateNeuralLayer(1, new SigmoidActivationFunction(0.7), new WeightedSumFunction()));

```