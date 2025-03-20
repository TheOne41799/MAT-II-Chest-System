# 🛒 Unity Chest System

# 🎥 WATCH THE DEMO VIDEO
[![Watch the video](https://img.youtube.com/vi/VvXNZMFVdY8/maxresdefault.jpg)](https://www.youtube.com/watch?v=VvXNZMFVdY8&t=1s)

---

## Playable Link
[Outscal](https://outscal.com/anudeepramadugu/game/play-module-assessment-test-ii-gda-game-1)

## 🎮 Description

A simple chest system built with Unity and C#.  
- **Uses the following programming patterns and concepts**
- Service Locator, Singleton Pattern ,Dependency Injection, Model-View-Controller (MVC),
- Scriptable Objects, StateMachine, Observer Pattern, Object Pooling

---

## 🖼️ UML Diagram

![UML Diagram](https://github.com/TheOne41799/MAT-II-Chest-System/blob/Branch18Refactor1/MAT%20II%20Chest%20System/Assets/Important%20Images/UML%20Diagram/ChestSystem.drawio.png#:~:text=346-,KB,-MAT%2DII%2DChest)  

---

## ⚙️ Features

1. **Different kinds of chests that can be randomly generated**  
   - 4 basic kinds of chests - Common, Rare, Epic, Legendary
   - Each chest has unique properties like time to unlock, coins and gems gained which will also be random but within a range
   - If needed, you can create more kinds of chests in the future
   - Scriptable Objects used to create both the Chest Asset and also the database
   - 2 Main kinds of curreny used in the game - coins and gems
   - Player gets both coins and gems on unlocking a chest
   - Chests can be unlocked using 2 methods - Timer amd gems to unlock the chest
   - Gems to unlock the chest will decrease with timer's time value if a chest has been initiated unlock process using timer
   - Chests have 4 different kinds of states - locked, unlocking, unlocked, collected
   - In addition to the above, a chest can also be queued to unlock when another chest is already unlocking

2. **Different Programming patterns used**  
   - *Object Pooling*     : Chests are created using object pooling
   - *State Machine*      : Chest states are maintained using a state machine
   - *Service Locator*    : Main game is maintained and tracked through a central service locator
   - *MVC*                : Individual chest is designed using Model-View-Controller principle
   - *Observer Pattern* : An event service is used to fire events and communicate between different components of the game
   - *Command Pattern* : An Undo mechanism is used to reverse the unlocking of a chest that is unlocked with gems

3. **Other Important Components**  
   - **UI Management** carried out by separate independent system
   - **UI Controllers** Each individual component of the game has its own UI
   - **UI Popups** Different UI Popups for different in game events like unlock, ui slots full, collect rewards, etc
   - **Player** Keeps track of the currencies - coins and gems
   - **Audio** Separate system which keeps track of background music and in-game sound effects

4. **Mouse Cursor Manager**  
   - Mouse cursor changes the cursor graphic on hovering over UI elements like buttons 

5. **Flexibility for designers**  
   - can easily change the chest data and also the database itself i.e, you can add more types of chests
   - database is also maintained for audio

---

## 🖼️ Screenshots

![Alt Text](https://github.com/TheOne41799/MAT-II-Chest-System/blob/Branch18Refactor1/MAT%20II%20Chest%20System/Assets/Important%20Images/Screenshots/Screenshot%20(135).png?raw=true)

![Alt Text](https://github.com/TheOne41799/MAT-II-Chest-System/blob/Branch18Refactor1/MAT%20II%20Chest%20System/Assets/Important%20Images/Screenshots/Screenshot%20(136).png?raw=true)  

![Alt Text](https://github.com/TheOne41799/MAT-II-Chest-System/blob/Branch18Refactor1/MAT%20II%20Chest%20System/Assets/Important%20Images/Screenshots/Screenshot%20(137).png?raw=true)  

![Alt Text](https://github.com/TheOne41799/MAT-II-Chest-System/blob/Branch18Refactor1/MAT%20II%20Chest%20System/Assets/Important%20Images/Screenshots/Screenshot%20(138).png?raw=true)  

![Alt Text](https://github.com/TheOne41799/MAT-II-Chest-System/blob/Branch18Refactor1/MAT%20II%20Chest%20System/Assets/Important%20Images/Screenshots/Screenshot%20(139).png?raw=true) 

## 🎵 Music Track
[Pixabay](https://pixabay.com/users/freesound_community-46691455/)

