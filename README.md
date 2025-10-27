Istvan implemented the observer pattern

For the Builder Pattern I first created a class that contained variables and components each object would have, then a Builder class with methods that resulted in a completed, built game object. The fish director assigned certain values to variables and components, and the fish spawner takes all that and fully creates the fish in the scene.

The Object Pool uses queues and prefabs to create as many bullets as the player can shoot, but will never go over that amount.

Saving/Loading: ISaveable was created which contains the Save and Load methods. TransformSaver records data for player position and positions of all fish at time of saving. ScoreSaver does the same for score. SaveLoadController ties the save and load functions to the S and L keys.
