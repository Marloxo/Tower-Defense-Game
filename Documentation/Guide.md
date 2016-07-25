# The Project Divided Into Multi Stages

## 1. Stage One:
Create the foundation of the map (Nodes,Ground)
with **start** and **end** Cube.

## 2. Stage Two:
Implement Basic Enemy AI

Add RoadMap (Waypoints) : which is `GameObject` container,

contain multi `GameObject` position on map, and stored in array on `Awake()`

Add Enemy Script which
- Start pointing to first point (as target)
- Then calculate the Vector of movement between **current** position and **target** position
- Move on that Vector by calling `transform.Translate()`
- Then move on to next target

## 3. Stage Three:
Create Wave Spawner 

Add Wave Spawner Script Which
- Got `enemyPrefab` , `timeBetweenWaves` , Counter `countdown` 
  -  `spawnPoint` and `waveNumber`
- When `countdown` Hit Zero `SpawnWave();` Method Called by `StartCoroutine(SpawnWave());` 
   > which is method handler with ability to wait for specific time.
- > `private IEnumerator SpawnWave()
  >  {
  >      waveNumber++;`
  >
  >      for (int i = 0; i < waveNumber; i++)
  >      {
  >          SpawnEnemy();
  >          yield return new WaitForSeconds(0.5f);
  >      }   
  >  `} `
  >
  >  `private void SpawnEnemy()
  >  {
  >      Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
  >  } `

  