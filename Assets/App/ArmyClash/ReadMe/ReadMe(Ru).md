#Architecture
The project architecture is based on ECA (Entity, Component, Action)
on the AppCore framework.

##Entity
Contains a graph of Components, Actions and connections between them.
<br>
![Entity](./Images/Core Entity.png)
![Entity Graph](./Images/Entity Graph.png)

##Component
Stores data and provides access to it.
Monitors the integrity and consistency of data.
It also protects the component invariant from incorrect states.
<br>
![Component](./Images/Component.png)

##Action
Contains the business logic of processing component data.
<br>
![Action](./Images/Action.png)

#Дополнительня механника
Implemented the mechanics of Drag and Merge units, as one of the most relevant mechanics.
<br>
![](./Images/Merge.gif)


#Game Architecture
##State Machine
Assets/App/ArmyClash/Runtime/StateMachines
<br>
Controls the Core Loop of the game,
loading data models and transitions between states.
<br>
![State Machine](./Images/App State.png)

##Components
Assets/App/ArmyClash/Runtime/Components
<br>
![Game Components](./Images/Game Components.png)

##Actions
Assets/App/ArmyClash/Runtime/Actions
<br>
![Game Actions](./Images/Game Actions.png)

##Модели данных
Assets/App/ArmyClash/Runtime/Models
<br>
![Data Models](./Images/Data Models.png)
<br>
Implements the logic of storing data models in ScriptableObject.
Allows to save and load data models from the file system.

##Resources
### Event Buses
![Event Buses](./Images/Event Buses.png)

###Pools
![Pools](./Images/Pools.png)

###Settings
![Pools](./Images/Settings.png)

###Storable Data Models
![Pools](./Images/Storable Models.png)

#Issues
##Characteristic issue
In original description Small Green Sphere Unit starts with zero health.
Change Basic Unit Characteristics: 100HP to 150HP
![Pools](./Images/Characteristic issue.png)

##Feeling issue
Create alternative Target Searching and Movement.
In my opinion this add more battle feeling.
<br>
Assets/App/ArmyClash/Scenes/Main Scene Alternative Target Search

##Performance issue
Target Search System and Movement System are simplified non-parallel systems.
When there will be a lot of Unit, it may cause performance reduction.
The solution: create parallel Job Systems with fake physics.