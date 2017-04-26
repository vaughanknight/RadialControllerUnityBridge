# RadialControllerUnityBridge 
## Connecting your Surface Dial to your Unity UWP Project

![demo2](https://cloud.githubusercontent.com/assets/252951/25439130/f190b710-2ade-11e7-811f-fcc7d4e87228.gif)

Unity Bridge for the Surface Dial / Radial Controller for use in UWP apps.
* RadialController.UWP - The UWP Library that the Unity Editor uses
* RadialController.UWP.WSA - THe UWP Library that is used by the UWP build
* RadialControllerProject - The Unity Demo project to show you how to link everything up

## Inspector
The inspector is pretty simple.  Two behaviours exists, although this might change to a single one.  You have the event manager, which is used to hook events with teh radial controller, and the RadialCongrollerBehaviour is used to update the configuration.  It has a custom editor, which as you can see from the image below, gives you the ability to set default precision and haptic feedback settings.

![inpector](https://cloud.githubusercontent.com/assets/252951/25438134/d9498f72-2adb-11e7-9acb-3da813f967a9.PNG)

The next section is where you add menu items.  Set a menu title, and then set the icon image.  The icon is best at 64x64 pixels, and must be configured as a cursor.  Example below on how to set that up.

![inpector](https://cloud.githubusercontent.com/assets/252951/25438134/d9498f72-2adb-11e7-9acb-3da813f967a9.PNG)


## The Demo Project Folders

There are a few folders, the most important to note is the plugins folder.  This contains the bridge DLL and the bridge WSA DLL for the UWP application build.

![folders](https://cloud.githubusercontent.com/assets/252951/25438129/d6e5ea14-2adb-11e7-95f0-ad44f9124c2b.PNG)


