VisualNeuralNetwork
===================

Simple visual presentation of an artificial neural network
C# / WinForms

You can create a network in one statement: 
// Create a newwork with default linking, 3 layers of 3 input, 5 hidden and 3 output neurons.
var network = NetworkBuilder.Build("MyNetwork", NetworkBuilder.Link, 3, 5, 3);
