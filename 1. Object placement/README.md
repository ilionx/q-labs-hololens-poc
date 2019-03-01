# Object placement

Placing objects can be done fairly easy. In unity you give some coordinates and it will show based on your first appearance. The problem with this solution, if a users starts the application one meter further then it was build, the objects are placed on the wrong place. While investigating this issue we discovered the WorldAnchorTransfer class. This class gives multiple HoloLenses the same locations of objects. The problem with this solution we found out that a single worldanchor can be between 20 - 60 mb. This will take a long time to load initially.

A week after investigating this issue Microsoft released a new service in azure. This service can store and get the objects. This will solve the location problems, what existed in the other solution.

Start SampleHoloLens.sln
In ViewController.cpp replace:
SpatialAnchorsAccountId with the id from azure
SpatialAnchorsAccountKey with the key from azure


[Create a HoloLens app with Azure Spatial Anchors, in C++/WinRT and DirectX](https://docs.microsoft.com/nl-nl/azure/spatial-anchors/quickstarts/get-started-hololens "Microsoft tutorial")