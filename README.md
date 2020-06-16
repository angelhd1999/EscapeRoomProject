# Cave Escape
##  Final Project - Interactive Systems<br />

**Name:** Àngel Herrero <br />
**NIA:** 205310<br />
**User:** U149946<br />
**Emails:** angelhd1999@gmail.com / angel.herrero01@estudiant.upf.edu<br />

**Name:** David Ciria <br />
**NIA:** 206038<br />
**User:** U150281<br />
**Emails:** david.ciria01@estudiant.upf.edu<br />

**Name:** Marc Furió <br />
**NIA:** 204832<br />
**User:** U149920<br />
**Emails:** marc.furio01@estudiant.upf.edu<br />

## Game intro
You embarked on an adventure with a group of explorers to find a treasure that is hidden in a cave. After walking cautiously for a while, you have reached a very dangerous spot deep inside the cave. The members of your group are abandoning the place because they do not want to take the risk. But you still have a chance. Do you think you can get the treasure? Do you dare to continue?

## Controls

This is the **list of key interactions** that we implemented on the game:<br />
 * **Restart** the game by pressing **return.**
 * **End** the game by pressing **escape.**
 
The rest **must be controlled* with the body thanks to posenet**.
How to use **posenet**:
* PoseNet to OSC adaptation
  * https://github.com/tommymitch/posenetosc 
* extOSC unity package
  * https://assetstore.unity.com/packages/tools/input-management/extosc-open-sound-control-72005

You will need to follow the instructions to run PoseNet OSC and then you will be able to run this project.

## Credits

The **authorship of the model of the hands is from Poly Google.**

The rest of the **assets are from the Asset store** and the **sounds** come **from https://freesound.org/**

## Spoiler and extensive explanation

**If you want to enjoy the game, we do not recommend reading any further**, as we will explain all the **interactions, tracks and events** of the game below, and you will lose the emotion of discovering it for yourself.

This is the **list of full-body interactions** that we implemented on the game:<br />
* **Decision-making capacity with the head:**
  * **Nod to continue.** (movement must be clear / exaggerated sometimes)
  * **Shake your head** to get back to your group members.
* **Ability to move the camera and flashlight with your head:** 
  * You can **raise your head, crouch, and move within range to widen your field of vision.**
* **Take and release spheres (minerals):** 
  * **Hold either hand** on the mineral for less than a second. (until you hear a gripping sound)
  * **keep your hand** in about the **same position** for a second.
* **Ability to rotate cranks:**
  * With the **right hand the cranks turn to the right** and the **corresponding color of the crank is added to the smoke.**
  * With the **left hand the cranks turn to the left** and the **corresponding color of the crank is subtracted to the smoke**.

This is the **list of tracks** that we added to the game:<br />
* **There is a main track under the table in the first scene.**
  * You have to **bend down** to see it well.
  * Provides information on the **appearance and density of minerals** to balance the weight scales with them.
* If you bend down on the **second stage** you can see a **small hole where the rock will open**, that indicates that you will find an exit there.
* To the **right of the second stage** you can see **two signs** that **indicate** how each hand interacts with the cranks, **the right turns them to the right and the left to the left.**

This is the **list of events** that we added to the game:<br />
* At the **beginning the player can decide whether or not to enter the cave** to continue the adventure with its corresponding animations.
* If the player have decided to enter **by placing two spheres on the scales, you will hear the fall of a flint**, which is to indicate that interacting with the scales causes movements in the structure of the cave. (Premonition of the earthquake)
* When **obtaining the treasure,** the gold, **an earthquake will occur that will obstruct the hole** through which the main character has entered, that will cause him to flee towards a column of smoke that **will direct him to the next scene.**
* **Achieving a color of smoke matching the color of the upper sphere** of the tubes **will start the animation of the first opening of the rock** and the player will be able to **see a flash of the sun indicating that there is an exit from the cave.**
* When the **second matching color is achieved, the rock will open fully and the light from the exit will dazzle the player as he heads towards it.**

## Demo of the game

*YouTube* link: https://www.youtube.com/watch?v=2N69BOkHjs4
