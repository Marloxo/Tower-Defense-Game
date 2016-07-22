# The Project Divided Into Multi Stages

## 1. Stage One:
Create the foundation of the map (Nodes,Ground)
with **start** and **end** Cube.

## 2. Stage Two:
Implement Basic Enemy AI

Add RoadMap (Waypoints) : which is `GameObject` container,

contain multi `GameObject` position on map, and stored in array on `Awake()`

Add Enemy Scripts which
- Start pointing to first point (as target)
- Then calculate the Vector of movement butween **current** position and **target** position
- Move on that Vector by calling `transform.Translate()`
- Then move on to next target


## 3. Stage Three:


  