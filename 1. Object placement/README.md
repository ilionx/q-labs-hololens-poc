# Object placement

This unity solution contains a cube, textblock and a script. These trigger eachother to act when some events happen.

When the cube is tabbed it wil get the defined anchors. These anchors need to be set in the cubeInteraction.cs file.
Place the correct anchor in the variable chapterAnchorsFromBackend.

Besides the anchor id's you need to set the account key and id from azure.
Replace the next 2 variables in the cubeInteraction.cs file.

- string accountId = "set this";
- string accountKey = "set this";

These texts come from the cubeInteraction.cs script in the Assets/script folder. The script is bound on the cube, together with the gaze and tapmanager it will be triggered.

To build the project click in the top left corner on File/Open scene. In this menu open the Main scene.
After opening the scene click in the top left corner on File/Build settings... Switch the platform to Universal Windows Platform with the target device set to HoloLens. Then build the solution in the desired folder (usually /App). After building the file explorer will show, click on the build folder (usually /App) and run the .sln with visual studio 2019. When launched you can deploy the application to the HoloLens using Release, x86 and the desired way of deploying.
