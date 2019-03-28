# Object placement

In the unity editor we can define coordinates where holograms should be placed when the app is launched. The problem with this solution is when the application is deployed on another location, the holograms are placed on the wrong location as seen on the picture below.

![N|Solid](https://raw.githubusercontent.com/ilionx/qlabs-hololens-poc/master/1.%20Object%20placement/docs/images/current.png)

To solve this problem we want to persist the location of the hologram. Using the azure spatial anchor store we can solve this issue. We need to save a spatialanchor, and based on room data it can be placed again using a watcher.

![N|Solid](https://raw.githubusercontent.com/ilionx/qlabs-hololens-poc/master/1.%20Object%20placement/docs/images/wanted.png)



When the cube is tabbed it wil get the defined anchors. These anchors need to be set in the cubeInteraction.cs file.
Place the correct anchor in the variable chapterAnchorsFromBackend.

Besides the anchor id's you need to set the account key and id from azure.
Replace the next 2 variables in the cubeInteraction.cs file.

- string accountId = "set this";
- string accountKey = "set this";


To build the project click in the top left corner on File/Open scene. In this menu open the Main scene.
After opening the scene click in the top left corner on File/Build settings... Switch the platform to Universal Windows Platform with the target device set to HoloLens. Then build the solution in the desired folder (usually /App). After building the file explorer will show, click on the build folder (usually /App) and run the .sln with visual studio 2019. When launched you can deploy the application to the HoloLens using Release, x86 and the desired way of deploying.
