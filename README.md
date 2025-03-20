# 🛒 Unity Inventory System

# 🎥 WATCH THE DEMO VIDEO
[![Watch the video](https://img.youtube.com/vi/VvXNZMFVdY8/maxresdefault.jpg)](https://www.youtube.com/watch?v=VvXNZMFVdY8&t=1s)

---

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

2. **Shop System**  
   - Ability to **buy items**.  
   - Ability to **sell items**.  

3. **Inventory Management**  
   - **Maximum Weight and Size** constraints for the inventory.  

4. **UI Feedback**  
   - Notifications for game events like:  
     - Inventory full in size or weight.  
     - Confirmation dialogs (Yes/No) for buying or selling items.  
     - Alerts for items purchased or sold.  
   - Top bar displaying:  
     - Player's money.  
     - Inventory size.  
     - Inventory weight.  

5. **Item Attributes**  
   - Each item has a **weight**.  
   - Inventory tracks the **total weight**.

---

## 🖼️ Screenshots

![Alt Text](https://github.com/TheOne41799/MAT-II-Chest-System/blob/Branch18Refactor1/MAT%20II%20Chest%20System/Assets/Important%20Images/Screenshots/Screenshot%20(135).png?raw=true)

![Alt Text](https://github.com/TheOne41799/MAT-II-Chest-System/blob/Branch18Refactor1/MAT%20II%20Chest%20System/Assets/Important%20Images/Screenshots/Screenshot%20(136).png?raw=true)  

![Alt Text](https://github.com/TheOne41799/MAT-II-Chest-System/blob/Branch18Refactor1/MAT%20II%20Chest%20System/Assets/Important%20Images/Screenshots/Screenshot%20(137).png?raw=true)  

![Alt Text](https://github.com/TheOne41799/MAT-II-Chest-System/blob/Branch18Refactor1/MAT%20II%20Chest%20System/Assets/Important%20Images/Screenshots/Screenshot%20(138).png?raw=true)  

![Alt Text](https://github.com/TheOne41799/MAT-II-Chest-System/blob/Branch18Refactor1/MAT%20II%20Chest%20System/Assets/Important%20Images/Screenshots/Screenshot%20(139).png?raw=true) 

## 🎵 Music Track
RoleMusic - Juglar Street  
https://freemusicarchive.org/music/Rolemusic

Kenny Assets - Inventory Sounds

