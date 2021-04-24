# Using Visual Code

Please install the editorconfig plugin, so we use the same settings   

# File structure

- Separate the Assets file into folders, depending on what they are  
- File naming practices:  # Try to not use underscores (_), whitespaces, hyphens (-) unless it belongs to the word  
  - Capitalize the folder names and use plural when possible.  
  - For all Unity files and scripts, use UpperCamelCase / PascalCase (ex: *LevelGenerator.cs*, *MyAwesomeScene.unity*, *Player.prefab*)  
  - For raw resources that are not scripts (and not Unity related), please use lowerCamelCase (ex: *playerRunning.png*, *deathRequiem.mp3*, *pillStats.json*)  
  - For scripts and ScriptableObject: **always** name your file with the class name. You *will* get a bug if you name a "thingObject.cs" file for a "ThingObject" class.  

Example with a standard tree structure (this is but an example, though the comments are useful):  
```  
- Scenes
  - Levels
    - Level01.unity
  - MainMenu.unity
  - Tests # Please put scenes that should not be shipped at the end within a "Tests" folder
    - Greg.unity
    - TestWithUsefulName.unity
- Scripts
  - Player
    - Movement.cs
  - UI
    - HealthBar.cs
- Sprites
  - Entities
    - slime.png
    - player.png
  - Items
    - projectile.png
- Sounds # Let's try to put music AND sound effects within one folder; we can separate them into subfolders
  - Music
    - frozenMemories.mp3
  - Effects
    - willhelmScream.mp3
- Prefabs
- Tiles
```  

# Unity practices

## Serialized fields

You might want to serialize fields to be able to edit them in the editor.  
If you do so, please don't initiate the value within the code. It's always confusing to have 2 sources of truth in this case, when you could have only one.  

## State pattern

Our code easily become convoluted because we don't do states (or not enough).  

We want to avoid using "if" conditions for complex objects behavior changes; example with a character that has to jump:  
```
void Player::handleInput(Input input) {
  if (input == PRESS_B) {
    if (!isJumping) {
      isJumping = true
      ...
    }
  }
}
```  
We want to avoid the above code. It looks fine as it is, but it will become messy once we need to:  
- implement a double jump  
- implement a crouch system  
- use animations  
- have attacks that can be triggered on the ground or in the air  
- ...  

Instead we will use states!  

The basic way to avoid this is to use a state with switch case.  
```
enum State {
  STANDING,
  JUMPING
}

void Player::handleInput(Input input) {
  switch (state) {
    case State.STANDING:
      if (input === PRESS_B) {
        state = State.JUMPING
        ...
      }
      break;
    case State.JUMPING:
      break;
  }
}

void Player::update(float deltaTime) {
  switch (state) {
    case State.JUMPING:
      jumpTime += deltaTime
      ...
      break;
  }
}
  
```
No "if hell" using this. Doesn't matter if you use switch or if for the state, but prefer switch if you have many states.  

*[Source](https://www.gameprogrammingpatterns.com/state.html)*

## Using scriptable objects

Scriptable Objects can be really useful to use a set of data on multiple objects without having these reference each other.  

Take the simple example of player health: the player needs to control its health, and we have other objects, like a health bar, that needs to read that health as well.  
Instead of having an unnecessary player reference in our health bar, we can put that health data into a scriptable object and refenrence it on both side.  

### Event system

We can use scriptable objects for an event system using pattern observer.  
Within your scriptable object:  
- Declare a list of listeners `List<GameEventListener>`
- Declare a `RegisterListener` and a `UnregisterListener` to add or remove a listener from the list
- Declare a `Raise` function that will call every listener's `OnEventRaised`
- From your object that need to watch the event, use `RegisterListener(this)` and `UnregisterListener(this)` within the `OnEnable` and `OnDisable` methods. Implement a `OnEventRaised` method.  

How is it useful?  
Simple example: your player just died, so you trigger the scriptableobject event. Now your UI, as well as everything that need to stop, is called as a listener. You did not need to make your player call every kind of object directly when he died.  

*[Source](https://unity.com/how-to/architect-game-code-scriptable-objects#what-are-scriptableobjects)*

# Configure Unity for Git

* Make the .meta files visible to avoid broken object references:
  * Go to *Edit > Project Settings > Editor*
  * In *Version Control*, select *Mode: "Visible Meta Files"*
* Use plain text serialization to avoid unresolvable merge conflicts
  * In *Asset Serialization*, select *Mode: "Force Text"*
Save your changes using *File > Save Project*  

*[Source](https://thoughtbot.com/blog/how-to-git-with-unity)*  
