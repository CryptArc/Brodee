# Brodee
In-Game Addon(s) and tweaks for Hearthstone. This is expected to 'fix' things, and allow for changes which are inline with blizzards asthetics. Brodee does not alter any Hearthstone files, although it does inject into the Unity AppDomain. Use at your own discretion. 

### Development "Rules"
* Nothing should automate something else. 
    * Currently Deck trackers/importers copy/paste and screen click which is automation which is a "bad thing. 
    * This could mean something as simple as auto-squelch is automation
* Hearthstone asthetic should hold even with addition UI elements.
    * Blizzard devs have stated at talks and online that the UI really matters to them for many reasons. Ruining the UI for the sake of features is something the devs do not want, and nor do I.
* Do not touch the store


### Other Development considerations
* Should text allowed to be altered? ala [Orphans?](https://www.reddit.com/r/hearthstone/comments/4hhznz/orphans_in_hearthstone/?ref=search_posts)
* Should touching of battle.net and related things be allowed?

## Features

### Done
* Button Menu Options for Brodee

### In progress
* Options menu for brodee
* Gem colour changing(No customization UI)

### Confirmed technically able
* Shortening spell and attack animation(No customization UI)
* Deck creation 'tile' in-game(like deck tracking)(No UI)
* Import Deck from string(No UI)

### Would like to do
In general, aiming for accessibility things first such as colour-blind options and keyboard inputs.

https://www.reddit.com/r/hearthstone/comments/49zlv5/a_compendium_of_hearthstone_ideas_v06_collated/
https://www.reddit.com/r/hearthstone/comments/4hvmuo/blizzard_please_move_the_create_deck_button_to/
https://www.reddit.com/r/hearthstone/comments/4i4f0e/blizzard_can_you_please_develop_an_interface_that/

## Building
When building you need set your references to Unity and Hearthstone assemblies, located in: `Hearthstone\Hearthstone_Data\Managed`

## Running
After building in either Debug or Release, run from Launcher.exe


