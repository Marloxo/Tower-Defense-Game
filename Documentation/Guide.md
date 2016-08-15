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
Create Wave Spawners that will spit out waves of enemies.

Add Wave Spawner Script Which
- Got `enemyPrefab` , `timeBetweenWaves` , Counter `countdown` 
  -  `spawnPoint` and `waveNumber`
- When `countdown` Hit Zero `SpawnWave();` Method Called by `StartCoroutine(SpawnWave());` 
   > which is method handler with ability to wait for specific time.
```
 private IEnumerator SpawnWave()
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
    }
```

## 4. Stage Four:
Import a cool turret model and make it lock on to our enemies.

First Update Enemy Prefab with 'Enemy' tag

Add Turret Script Which
- Start with `Start()` which we will call `InvokeRepeating()` Method that will Invoke a specific method in time seconds,
 then repeatedly every repeatRate seconds.
- `UpdateTarget()` which will update **enemies** Array with the current enemies
and store the closest target in **target** variable.
- `Update()` will check first if  **target** null it will do nothing.
- else will Generate Vector3 Between Turret position and the target enemy.
- Then Creates a rotation with the specified forward and upwards directions From the Vector3
- Next step transform lookRotation rotation to Angles (x,y,z)
- And Use `Quaternion.Lerp()` Method for Enhancement to smooth the move from one state to another
- Finally rotate the turret (or the specific part of turret) with the results directions on Y axis

- PS : `OnDrawGizmo()` used to show in game mode window the range of the turret

## 5. Stage five:
Let's shoot some enemies.

First thing we add small ball as a bullet

Then Create `Bullet` Script which
- Start with `Seek()` Method that will define the target from `Turret` Script
- Then `Update()` Method which start by checking if there is a defined target
- `if` So, create **Vector3** `dir` between *bullet firePoint* *(which is in turret script)*
    and the target Prefab 
- Then calculate `distanceThisFrame` which produce the distance of bullet in this frame
    and check that if it's less or equal to `dir.magnitude` *(the length of the Vector3)*
    then bullet Hit the target
    else continue moving in target direction by `transform.Translate`.
- In case of target get hit  we will call `HitTarget()`
    and `Instantiate` destruction particle system *(impactEffect)*
    then destroy bullet, target and impactEffect after **2 sec**.

second step

Update `Turret` Script 
in `Update()` Method  add check for `fireCountDown`
- `if` it less than `0f`
    - Then Shoot the Target !!
    - And set the `fireCountDown` to `fireCountDown = 1f / fireRate;`
- `else` decries the `fireCountDown` by 1 sec.

Add `Shoot()` Method which 
- Instantiate the bullet in `firePoint` position
- And call the `Seek()` method from `bullet` script.  

## 6. Stage six:
Let's add a functionality for building turrets in the game with little cool Mouse effect.

- We start by adding `Node` Script which will be responsable for
    - Keeping track whether if there is something top of that Node
    - And also handle some user input so it will make sure to our kind of
      highlight the note when we hover over it to give the user submission feedback
      that he can actually press it and something will happen 
    - It will also be responsible for checking whether or not the our player has pressed that
      particular node and then building something on top of it.

- The First thing to do is getting the `Renderer` to set a Color from `Start()` Method
So it's only find it at the very beginning of the game and then cash it.
- Then on `OnMouseEnter()` Method we set the Color.
- And on `OnMouseExit()` Method we set the Color back to initial color.
- Add `OnMouseDown()` Method so on click we build turret on top of that *clicked Node !!*

- But before doing that we need to manage that operation So we create `BuildManager` script in **GameManager**.
 - First Create `GetTurretToBuild()` Method to get the required **Turret** To Build.
 - Then Make `BuildManager` Object as a singleton: 
    - By Creating one instance from the `BuildManager` Object.
    - And `Awake()` Method which will be called once on Game Start.
- Finally add `Start()` Method which will set the *`turretToBuild`* referance.

## 7. Stage seven:
Playing around with Camera Controller that allow the player to move around the camera in an RTS-inspired way.

We start by adding `CameraController` Script which will be responsible for
 - Taking *user input* and move the camera according to that input
 > transform.Translate(Vector3.forward * panSpeed * Time.deltaTime);

     We multiply with `panSpeed` to control the moving speed 
     & multiply with `Time.deltaTime` to make the Translate independent  from the difference in CPU speed from device to another.

 - Then we change the Camera position according to mouse **`ScrollWheel`**
   - First get the `Axis` value of the virtual axis identified by `axisName`
     from unity project setting -> Input 
   - Then get the current position
   - Finally move the camera according to `Mouse ScrollWheel` multiply with big number like: ex 1000
     for transforming the (1,-1) range to **X** position
   - In addition, we use  **`Mathf.Clamp()`** Method to make sure surrounding **Y** Value between
     a minimum float and maximum float value.