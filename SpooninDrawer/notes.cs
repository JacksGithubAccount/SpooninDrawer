﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpooninDrawer
{
    internal class notes
    {
        /*8/22/2023
         * -the start
         * 8/27/2023
         * 9/4/2023
         * -Added basic engine stuff to display titlescreen
         * 9/5/2023
         * -Added input engine stuff
         * 9/9/2023
         * -added more engine stuff
         * 9/11/2023
         * -added all but gameplay state
         * 9/13/2023
         * -added game play state, done ch10
         * 9/16/2023
         * -added input for moving up and down, no animation yet for player
         * 9/19/2023
         * -started adding idle animation, also up an ddown doesn't work
         * -Idle animation works, also added method to make an animation loop
         * -up and down works now
         * 9/20/2023
         * -updated title screen to fit intended resolution for now
         * -added menu arrow and got arrow to move around title screen, also make arrow loop back around when going past first and last option
         * -trying to polymorph screens; solved using an interface for base screen
         * 9/21/2023
         * -need to make enter key context sensitive on title screen
         * 9/22/2023
         * -added start settings and exit, also a back button, no need for load yet
         * -will need input keys, maybe? unsure, future consideration
         * 9/23/2023
         * -maybe time to start on map?
         * 9/25/2023
         * -rmoved a null check in screen and added empty screen to load in place of null
         * -added FPS checker
         * -Spritefont issue where nothing would print, turns out the <end> variable in the spritefont needs to be 126 to allow all characters
         * 9/26/2023
         * -Added way to change FPS and player movement is now the same regardless of FPS.
         * 9/28/2023
         * -Added object pool
         * 9/30/2023
         * -added pipeline extension as a separate project then added through content pipeline
         * 10/1/2023
         * -doing same as last time, but for animations
         * -somehow broke BaseGameObject and I had to add an empty constructor to it, now the title screen doesn't load.
         * -implementation of object pool broke it, the render method activate bool check in base game object is doing it
         * 10/7/2023
         * -XML importing is not working. I can't seem to figure it out. Will skip for now.
         * -Got screen to work, instead of creating the splash screen in the add game object method, instantiated it as own varible and activated it
         * -arrow disappears on changing menu screens, the new screen is placed on top of the arrow
         * -added several methods to help with the object pool integration, but now all screens that deactivate don't get activated
         * 10/9/2023
         * -Is there a need to disable? Only one screen gets added to the list, so just make that the top one. right? 
         * -Doesn't work. top screen is still drawn to the top
         * -Fixed, all I had to do was use the zIndex to make the screen render on top of the other one.
         * 10/10/2023
         * -tryin XML stuff again, same issue
         * 10/23/2023
         * -Figured out the XML problem. Book I was using has <Asset Type="Engine2D.PipelineExtensions:AnimationData"> which could not resolve
         * -rb whitaker's site showed <Asset Type="MonoGameXmlContent.Weapon"> for their example. Changing : to . allowed it to build
         * 10/27/2023
         * -Changed right, left and idle animation data to XML files
         * -added a resource file to hold string values and added a way to translate/switch between same named resource
         * 10/30/2023
         * 11/7/23
         * -installed monogame extended
         * 11/14/23
         * -started on testing camera
         * 11/15/23
         * -Got a camera, and centered it on player. The camera only exists in test camera state/
         * -started on level editor, ran into issue where the overrides aren't working on forms, had to change target framework to 4.8
         * -properties wouldn't let me do that, had to edit csproj and change target framework to net48 to make it work
         * 11/19/23
         * -added more stuff to gameeditor, but lots of stuff not working. Says it's not in the Monogame framework? Will need to figure out
         * 11/21/23
         * -entered a possible fix to the graphics device not detected. need to test if it works.
         * 12/19/23
         * -forms isnt working, cant get it to work. installed tiled instead to try that. need to test it.
         * 12/23/23
         * -tiled doesn't work. latest version of extended(3.8.0) removed a reference that tiled uses to build it's map.
         * -current solution is to install an alpha pre-release version of the newest update(3.8.1), which worked.
         * -added test map through tiled
         * -need to move camera from test state to actual state.
         * 12/26/23
         * -there's an issue with spritebatch begins where you need the transformation matrixc of the camera to get it to work but the begins is called in a different class
         * 12/27/23
         * -added a method to retrieve the matrix and passed it to the main game, now the camera moves with the character
         * -new issue now where the map also moves with the character
         * -fixed, had to add the camera view matrix into the draw method for the tiled map
         * 12/29/23
         * -tiled map transparency isn't working. they render as black isntead
         * -adding GraphicsDevice.BlendState = BlendState.AlphaBlend; to the draw method made it work
         * -added pause, it works, but input doesn't work correctly. may need to remove current input control to add one that works better
         * -added a version of input control, needs further work. Turns out I already created a version of this in splash state, imported over to gameplay state
         * -release input doesn't work, will need to lookfurther into it, also add more that'll let you change keys ingame
         * -found out why release wasn't working. I was leaving in the part of the code where it was checking if key was pressed. Release works now
         * 12/30/23
         * -appemted to intgrate control change from prevous project. don't think it's working. may have to inport it instead
         * 1/2/24
         * -had to remove changes and ended up importing the inputter and action key from old project and it works
         * 1/3/24
         * -next step is adding collision to map, researching on tiled at the moment
         * 1/8/24
         * -installed TiledCs and TiledSharp, one or the other is needed to make coding for Tiled maps easier
         * 1/9/2024
         * -more work trying to get TiledSharp to display tiled map following https://www.youtube.com/watch?v=RzTaLFHBu88&t
         * 1/10/2024
         * -made maps display, now to work on collision. video above had code for getting the collision for map. Now to add palyer collision and interaction
         * 1/21/2024
         * -AABB collision only takes basegame object, two ways I see to proceed, either make new collision for rect or turn map rect to new object that inherits base game object
         *1/22/2024
         *-turned into basegame object, but the collision isn't working, will need more test
         *1/23/2024
         *-turns out I forgot to add the boundingbox, it now works. Now need to turn off the speed reduction
         *1/24/2024
         *-since direction is already booled, just need to find way to make it so you can't move in already moving directions, but other directions are fine
         *1/30/2024
         *-attempting movedirection detecting tiles that will collide with
         *1/31/2024
         *-adding movedirection works for collision, but lost the ability to move diagonally
         *-fixed by removing movedirection, but keeping the bools for stoping direction on collection, however, can move too into collidable object
         *2/1/2024
         *-added position readjustment if inside a collidable map tile, it's not perfect, but it's less worse
         *-the player actually has two bounding boxes. Need to fogure out which is colliding and reset position based on that
         *2/2/2024
         *-colliding up or down and moving left or right makes player go through collidable object, is probably related to position not matching up with the bounding boxes
         *2/3/2024
         *-added boxes to look ahead in moving direction to find collision. Also MGCB broke, can't build anything amymore.
         *2/4/2024
         *-MGCB broke, after must troubleshooting, conclusion is problem with this program. Maybe. More troubleshoot may be needed.
         *
         */
    }
}
