# Backend information

This unity solution contains a cube, textblock and a script. These trigger eachother to act when some events happen.

When the user taps the cube the script will do a http get call. This will be from the pokemon api where it will get the 6th pokemon, named charizard. This name will be displayed once the call succeeds.

To build the project click in the top left corner on File/Open scene. In this menu open the Main scene.
After opening the scene click in the top left corner on File/Build settings... Switch the platform to Universal Windows Platform with the target device set to HoloLens. Then build the solution in the desired folder (usually /App). After building the file explorer will show, click on the build folder (usually /App) and run the .sln with visual studio 2019. When launched you can deploy the application to the HoloLens using Release, x86 and the desired way of deploying.
