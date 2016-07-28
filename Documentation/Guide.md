# The Project Divided Into Multi Stages

## 1. Stage One:
Create the foundation of the map (Nodes,Ground)
with **start** and **end** Cube.

## 2. Stage Two:
Implement Basic Enemy AI

Add RoadMap (Waypoints) : which is `GameObject` container,

contain multi `GameObject` position on map, and stored in array on `Awake()`

Create some enemies and make them walk down the path of destruction using waypoints.

Add Enemy Script which
- Start pointing to first point (as target)
- Then calculate the Vector of movement between **current** position and **target** position
- Move on that Vector by calling `transform.Translate()`
- Then move on to next target

## 3. Stage Three:
Create Wave Spawner that will spit out waves of enemies.

Add Wave Spawner Script Which
- Got `enemyPrefab` , `timeBetweenWaves` , Counter `countdown` 
  -  `spawnPoint` and `waveNumber`
- When `countdown` Hit Zero `SpawnWave();` Method Called by `StartCoroutine(SpawnWave());` 
   > which is method handler with ability to wait for specific time.
-  ``` private IEnumerator SpawnWave()
    {
        waveNumber++;
  
        for (int i = 0; i < waveNumber; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }   
    } 
  
    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    } ```

## 4. Stage Four:
Import a cool turret model and make it lock on to our enemies.

First Update Enemy Prefabe with 'Enemy' tag

Add Turret Script Which
- Start with `Start()` which we will call `InvokeRepeating()` Method that will Invoke a specific method in time seconds,
 then repeatedly every repeatRate seconds.
- `UpdateTarget()` which will update **enemies** Array with the current enemies
and store the closest target in **target** variable.
-`Update()` will check first if  **target** null it will do nothing.
- else will Generate Vector3 Between Turret position and the target enemy.
- then Creates a rotation with the specified forward and upwards directions From the Vector3
- Next step transform lookRotation rotation to Angles (x,y,z)
- and Use `Quaternion.Lerp()` Method for Enhancment to smooth the move from one state to another
- finally rotate the turret (or the specific part of turret) with the results directions on Y axis

-PS : `OnDrawGizmo()` used to show in game mode window the range of the turret

