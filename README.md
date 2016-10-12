VisualNeuralNetwork
===================

A simple but pluggable artificial neural network implementation in C# / WinForms, with a class structure that makes the concept a bit explanatory. The application has a visual representation of the path strengths:

For learning purposes only
```c#
// Create a network with 3 layers of 8 input, 7 hidden and 3 output neurons.
var network = NetworkBuilder.Build("MyNetwork", NetworkBuilder.Link, 8, 7, 3);
```
![Graph](https://raw.githubusercontent.com/mharthoorn/VisualNeuralNetwork/master/ExampleGraph.png)
