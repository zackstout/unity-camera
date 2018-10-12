# Unity Basics
Have written some basic movement scripts and camera/player rotation scripts.

## Flow of Ideas
==========================
- Making the hoops 
    - Creating objects from scripts
    - Adding materials to them
- Generating them programmatically
- Finessing the force stuff. 
- Could add x-direction stuff 
==========================
- Finding whether we’ve gone *through*/within them 
- Add coins 
    - Collision detection
==========================
- Quaternions and rotation of camera stuff 
    - Cross-referencing scripts
- How to deploy unity to the browser ?
- How to add real images/skins/prefabs ?
==========================
- The interface: frame selector

==========================

## Personal Notes 
- got fog working by changing Skybox to default-Diffuse and turning fog back to exponential squared.
- learn to use frame selector
- try to understand the translation between euler angles and quaternions better.
- learn about lighting.
- get more comfortable with re-docking and organizing the GUI.
- look into accumulative force problem. I mean one solution is only setting it at the beginning. 
- I think you would have to store all the obstacles in an array in order to check whether each has been passed through. 
- Feels good to come back to Unity again and again and learn a bit more each time, each time using what we've built before.

- How to toggle certain game objects -- which leads to a deeper question: how to best structure our scripts (so that they are not all attached to player object)?