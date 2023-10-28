using System;
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
         * -
         */
    }
}
