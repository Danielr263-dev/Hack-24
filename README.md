# Hack-24
# 2D Unity Game - Circle Attack!

## Overview

This project is a **2D Unity game** where a player must survive against spawning enemies. The game has basic mechanics where enemies reduce the player’s health on collision, and they get destroyed afterward. Below, we explain how the game works, what features have been implemented so far, and what still needs improvement.

## Features Implemented

### Player Health (`PlayerHealth.cs`)
- The player starts with **100 health points**.
- When an enemy **collides** with the player, **10 health points** are lost.
- If health reaches **0**, the **game stops**. This is done by setting `Time.timeScale` to zero, effectively pausing all activity.

### Enemy Behavior (`Enemy.cs`)
- Enemies deal **10 damage** to the player upon collision.
- After they collide with the player, they are **destroyed immediately**.
- This behavior is added to ensure that enemies don’t repeatedly damage the player after the initial contact.

### Enemy Spawner (`EnemySpawner.cs`)
- Enemies spawn at random locations either within a specified area or **just outside the camera view**.
- Spawning is timed with a random interval between **minimum** and **maximum spawn times**.
- Each spawner starts with a slight **random delay** to make spawning unpredictable and dynamic.

### Player-Enemy Interaction
- The player takes damage when **colliding with an enemy**.
- Each enemy is destroyed after dealing damage, to ensure the gameplay is fair and to prevent repeated hits from the same enemy.

## How to Set Up

### Player Setup
1. **Player Tagging**:
   - The player GameObject must be tagged as `"Player"` for the collision detection to work.
2. **Components Needed**:
   - **PlayerHealth** script: Attach this to the player GameObject to manage health.
   - **Rigidbody2D**: Set the player’s Rigidbody to **Kinematic**.
   - **Collider2D**: Make sure `Is Trigger` is checked to allow detection without physical blocking.

### Enemy Setup
1. **General Enemy Script**:
   - Attach the `Enemy.cs` script to your enemy prefab to handle its general behavior.
2. **Collision Handling**:
   - Enemies use `OnTriggerEnter2D()` to damage the player and destroy themselves upon collision.
3. **Components Needed**:
   - **Rigidbody2D** set to **Kinematic**.
   - **Collider2D** with `Is Trigger` enabled.

### Enemy Spawning (`EnemySpawner.cs`)
- Attach the `EnemySpawner` script to any GameObject to make it an enemy spawner.
- Define spawn boundaries using the `minPosition` and `maxPosition` values.
- Enemies can be spawned outside the camera’s view for a more dynamic entry into the scene.

## Game Mechanics Summary

- **Collision and Damage**: Enemies reduce the player’s health by **10 points** on collision and then disappear.
- **Game Over**: If the player’s health reaches **0**, the game stops immediately.
- **Enemy Spawning**: Enemies are spawned in an unpredictable pattern, either inside a designated area or just outside the camera’s view.

## Current Status & Limitations

- We have set up **basic player health**, **enemy collision**, and **spawning** mechanics.
- **Game Over UI** and more advanced enemy behaviors have **not yet been implemented**. The current version just pauses the game when the player runs out of health.
- The player lacks additional mechanics, such as **attacking** or **healing**.

## Planned Improvements

1. **Game Over Screen**: Add a user interface to inform the player when the game is over, rather than simply stopping everything.
2. **Enhanced Enemy Behavior**: Add more intelligent enemy actions, such as following the player or attacking from a distance.
3. **Player Abilities**: Expand the player's capabilities, such as adding attacks, dashing, or picking up health packs.

## How to Play

- **Goal**: Survive for as long as possible while avoiding enemies.
- **Health**: You start with **100 health points**. Collisions with enemies will reduce health by **10** each time.
- **Game End**: The game stops if your health reaches **0**.
