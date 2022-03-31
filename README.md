# Flappy Bird 3D

This project is a prototype for a 3D Flappy Bird kind of game.
The most noticeable difference is that the screen is divided by 2 cameras, one side view like in the original game and one behind the bird.
The gaps can be horizontal or vertical, making the use of both cameras useful.

### GameHandler.cs

A singleton MonoBehaviour that is responsible for:

- Retrieving and storing the player's highscore in the Player Preferences between game sessions
- Starting the Routine that creates the obstacles
- Updating the score on the player screen

### Bird.cs

The bird, represented by a capsule, is the 'Player character', and only handles movement.

### Obstacle.cs

Obstacles are programatically created and defined via Coroutine, with random values for layout, gap position and gap size.
