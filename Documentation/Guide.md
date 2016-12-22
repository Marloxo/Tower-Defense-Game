# The Project Divided Into Multi Stages 🎮

## 1. Stage One:
Create the foundation of the map (Nodes,Ground)
with **start** and **end** Cube.

## 2. Stage Two:
Implement Basic Enemy AI

Add RoadMap (Waypoints) : which is `GameObject` container,

contain multi `GameObject` position on map, and stored in array on `Awake()`

Create some enemies and make them walk down the path of destruction using waypoints.

Add Enemy Script which
- Start pointing to the first point (as target)
- Then calculate the Vector of movement between **current** position and **target** position
- Move on that Vector by calling `transform.Translate()`
- Then move on to next target

## 3. Stage Three:
Create Wave Spawners that will spit out waves of enemies.

Add Wave Spawner Script Which
- Got `enemyPrefab` , `timeBetweenWaves` , Counter `countdown` 
  -  `spawnPoint` and `waveNumber`
- When `countdown` Hit Zero `SpawnWave();` Method Called by `StartCoroutine(SpawnWave());` 
   
> which is method handler with an ability to wait for specific time.

```csharp
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
- Finally, rotate the turret (or the specific part of turret) with the results directions on Y axis

- PS : `OnDrawGizmo()` used to show in game mode window the range of the turret

## 5. Stage five:
Let's shoot some enemies.

First thing we add small ball as a bullet

Then Create `Bullet` Script which
- Start with `Seek()` Method that will define the target from `Turret` Script
- Then `Update()` Method which starts by checking if there is a defined target
- `if` So, create **Vector3** `dir` between *bullet firePoint* *(which is in turret script)*
    and the target Prefab 
- Then calculate `distanceThisFrame` which produce the distance of bullet in this frame
    and check that if it's less or equal to `dir.magnitude` *(the length of the Vector3)*
    then bullet Hit the target
    else continue moving in target direction by `transform.Translate`.
- In case of target get hit  we will call `HitTarget()`
    and `Instantiate` destruction particle system *(impactEffect)*
    then destroy bullet, target and impactEffect after **2 sec**.

Second step

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

- We start by adding `Node` Script which will be responsible for
    - Keeping track whether if there is something top of that Node
    - And also handle some user input so it will make sure to our kind of
      highlight the note when we hover over it to give the user submission feedback
      that he can actually press it and something will happen 
    - It will also be responsible for checking whether or not our player has pressed that
      particular node and then building something on top of it.

- The First thing to do is getting the `Renderer` to set a Color from `Start()` Method
So it only finds it at the very beginning of the game and then Cache it.
- Then on `OnMouseEnter()` Method we set the Color.
- And on `OnMouseExit()` Method we set the Color back to initial color.
- Add `OnMouseDown()` Method so on click we build turret on top of that *clicked Node !!*

- But before doing that we need to manage that operation So we create `BuildManager` script in **GameManager**.
 - First Create `GetTurretToBuild()` Method to get the required **Turret** To Build.
 - Then Make `BuildManager` Object as a singleton: 
    - By Creating one instance from the `BuildManager` Object.
    - And `Awake()` Method which will be called once on Game Start.
- Finally add `Start()` Method which will set the *`turretToBuild`* reference.

## 7. Stage seven:
Playing around with Camera Controller that allow the player to move around the camera in an RTS-inspired way.

**TODO: Enhancement use this reference: http://forum.unity3d.com/threads/rts-camera-script.72045/**

**TODO: Continue work on RTS camera from: https://github.com/Marloxo/RtsCameraTest thanks to garcialuigi**

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

## 8. Stage eight:
Start making a shop where the player can purchase different turrets.

- We start by adding new `Panel` the UI background for our *Shop* 
- Then we add `Horizontal Layout Group` from Inspector option, this will just make sure to stretch all of our
  different elements parented to the shop to fit the shop object
  so if I make another shop turret item and another one you can see that they are resized and
  repositioned so that they fill out the entire shop object
- Then we turn off `Child Force Expand` option because we don't want them to be stretched
- We just want them to have a fixed size So we Add `Layout Element` to Item **button** under **Shop UI**
- Then we change the Item Image.
- We have to change our navigation to **none** because we're using a mouse to navigate with and we don't want to be
  able to shift between UI elements using the arrow keys or a controller.

- Then we need to add `Shop` Script to manage the purchase process which will be linked to UI
  `onClick` event which will call **Turret Purchase method** depend on *selected turret*
  > `buildManager.SetTurretToBuild(buildManager.{selected Turret});`

- Then we edit `BuildManager` script
 - Adding `SetTurretToBuild()` Method to initialize the selected turret
 - And delete the `Start()` method because we don't need the default turret unless it's selected.

- Then we edit `Node` script
 - Editing `OnMouseDown()` Method to check if `GetTurretToBuild()` is not null which mean no turret selected.
 - Editing `OnMouseEnter()` Method to check if mouse over UI element then do nothing to avoid overflow clicking.

## 9. Stage nine:
 :construction: Little House Cleaning

PS: 
 - When u Import new asset always import the new asset in newly separated folder
   to avoid conflict with existed material and the imported material with the asset.

## 10. Stage ten:
Add a missile that will explode on impact.

- First, we will work on Missile Launcher  **Missile** by adding a new material
- Then change `HitTarget()` Method to check for **`explosionRadius`**
  - If in this range then call `Explode()` method 
     - which will see what it hits and look through all of the things that it hit
     - and check if they are indeed an enemy 
     - and if they are we will damage them which basically now just means destroy them,
     - that is basically all the logic that we are going to need now. 
  - Else call `Damage()` method which will `destroy` the **enemy**.

## 11. Stage eleven:
Show Me the Money :P 
add money to our game and tidy up the Shop and Build Manager.

- Start by Adding new Script "turretBluePrint"
- We add `[System.Serializable]` to class header to tell Unity to show class properties in Inspector window
- Then add `prefab` , `cost`.

- We go back to `Shop` Script and instead of our calling everything on the building manager
  here we can simply change `PurcherStandardTurret` to `SelectStandardTurret()`

- We go back to `BuildManager` Script and change `turretToBuild` type to `turretBluePrint`
- Then delete `GetTurretToBuild()` method and add `CanBuild` property.
  
- We go back to `Node` Script and change `OnMouseEnter()` instead of letting the node script build the turret
  we change it to let the `buildManager` script do that thing.
  with adding new Method `BuildTurretOn()`.
- In addition, we change the check for building turret to new `CanBuild`
- And add `GetBuildPosition()` to pass the BuildPosition to the `BuildManager`.

Now it's Time to start building CURRENCY system (PS: Show me the money!!)
- We start By adding new script in `GameManager` called `PlayerStats`
- Then create static money property and start money
- We go back to `BuildManager` Script and on `BuildTurretOn()` Method check if player got enough money
  to build the selected turret if so subtract the *Turret* cost from player money.  

## 12. Stage twelve:
Let's spice up the UI! 

- In this Stage, we will create text to display our money,
- Improve our hover animation on the nodes and the way that we display text in the game.
- We also add a price tag to the turrets and create a cool build effect.
- Also, we are going to change the Countdown in a way make it more lovely.
- Finally, we want to add Money Counter.

- We Start with hover animation by going to `Node` Script and add check if player has enough money
  by checking that from `BuildManager` if not show red warning color for the player.

- Then We add new UI Element `Canvas` and change the *Render Mode* to `world Space`
  Which will allow the Canvas to be fade with zooming.
  
## 13. Stage thirteen:
In this Stage, we add a lives counter, a game manager, enemy health and a cool death effect.

- We Start with `PlayerStats` Script to keep track of our lives by adding `lives` parameter
and `startlives`.
- PS: **`OverlayCanvas`**: is UI element on the screen rendered on top of the scene
    **`World Space Canvas`** is UI element behave as any other object in the scene.

- Add `LivesUI` Script to Show lives count.

- Then We modify `Enemy` script by adding new method `EndPath()` which will be responsible for reducing player lives and destroy the Enemy object,
  `TakeDamage()` which will be responsible for damaging the enemy Prefab with a specific amount from bullet and calling `Die()` method in case health bellow Zero,
  and `Die()` which will be responsible for destroying enemy Prefab and add some money for the player.
- Then we add `GameManager` Script which will be responsible for end game, restart the game and maybe pause the game.
- Then We modify `Bullet` script by modifies `Damage` method which will be call `TakeDamage()` method instead of destroying the Enemy object which will handle that for us as previously mentioned,
  and add `damage` parameter which will specify the amount of damage will be taken.

## 14. Stage fourteen:
Say Hello to the brand new *`laser Beamer`*.

- We start by adding `laser Beamer Turret` to shop menu.
- Then we modifies `shop` Script by adding `SelectlaserBeamer()` method.
- After that, we modify `buildManager` Script by removing prefab reference because we not using them at all
  we are using the `TurretBluePrint` instead.

- Going back to our `laser Beamer Turret` we start by adding *Line Renderer* to it
  then tweak the setting little bit ex(color,shadows,...).
- After that, we edit `Turret` Script and organize the parameter little bit
  Then we modify the `Update()` method by checking first if this turret uses laser
  Then we disable the *laser* if `target null` to turn off the laser when no target in the range
  and encapsulate the Target Lock in `LockOnTarge()` method.
  Finally, we add `Laser()` method to draw *Line Renderer* between the `turret` and `target`.

## 15. Stage fifteen:
Rock and roll with the Laser Beamer particle effects!

- We start by Adding new particle system for laser effect with a shape of *cone*.
- Then we add new materials and change the color to green.
- We optimize the particle collision by changing `collision with` option to `Environment` which is new layer
  we will add to all node and path object.
  So we select all node and path and add them new layer called `Environment`,
  by that, we specify the object which will collide with.
- Then we modify `Turret` Script by Adding reference to particle system,
  and enable it in `Laser()` method, and disable it in `Update()` method is there is *no target*.
- After that, we specify the position of the particle in `Laser()` method with offset,
  So we make a `Vector3` between the target and firePoint to get the direction,
  Finally, we change the rotation of particle to make the particle point to our turret by using:
> Quaternion.lookRotation(dir);

- Then we add **`Glow`** which will be nested child particular system, first we remove the shape
  to give it the `Glow` effect then we change the material to new material with new color,
  Finally, we change the `color over lifetime` option to make it a bit transparent at the end of its lifetime
  Which will give it a flash effect.
- Finally, We add a point light with green color with the default setting,
  and enable-disable the point light from `turret` Script as we did with the particle system.

## 16. Stage sixteen:
Slow & damage enemies over time by Laser Beamer!

- We start by modifying `turret` script with adding damageOverControl
  and editing `Laser()` by calling `TakeDamage()` method in `Enemy` script
  and passing the amount of damageOverControl multiply by `Time.deltaTime` to cancel any difference between computers.
  However instead of calling that every frame we cash our target in `targetEnemy` in `UpdateTarget()` method
- Then we modify our `Enemy` Script and separate the basic enemy logic from movement,
- After that, we sync our speed between the two script by adding `[RequireComponent(typeof(Enemy))]` in top of our class,
  In addition, we use `[HideInInspector]` tag to make our public variable hidden in Inspector.
- Then we add Slow functionality to our enemies by calling `Slow()` method in `laser()` method.
  In `Slow()` method We multiply by *startSpeed* to allow the slow function to decrease our speed
  only one time from the default speed. //or it will continue decreasing until it will stop! (and surely we don't want that!).
- Finally, we want to reset our speed after we become out of turret range,
  So we go to `EnemyMovement` Script in `Update()` method and reset the Enemy speed there.
  But we want to make sure to call the `Enemy` Script before `EnemyMovement` to make sure they synchronize correctly
  We do that by going to:
> Unity -> Edit -> ProjectSetting -> ScriptExecutionOrder

## 17. Stage seventeen:
Let's add a game over screen to the game!

- We start by sketching out the UI, So we go to `OverlayCanvas` and change `Canvas scaler` option to scale with screen size, and
 change the `Match` option to `1` to Match the *height* too. 
- Then we add the UI elements like GameObject which will be the `GameOverUI` and add a cool background color.
- After that, we modify our `GameManager` script by adding GameOverUI instance to enable and disable the UI.
  Then we want to disable the moving ability to camera when `GameOverUI` enabled,
  So we modify our `CameraController` script by checking if the game is over then we disable the `CameraController` script. 

- PS: one thing we need to be careful with is that static variable carry on from one scene to another
  Which mean that the value of static remain the same even if the scene changed or reloaded.  
> Always initialize the static variable in `start()` Method because `start()` Called every time scene start

- Then we add `Rounds` variable to our `PlayerStats` script to keep track of Rounds,
  Then we update `waveSpawner` Script to increase `Rounds`.
- Going back to `GameOverUI` we add new Script Called `GameOver` and add the `OnEnable()` method which called whenever this element enabled
  which will get the `Rounds` and print it to the screen.
- After that we want to enable the *Retry* button so we add method Called `Retry()` and we call `SceneManager`
  but we need an efficient way to do that So we call the *SceneManager* and get the current active scene index.

```csharp
 SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
```

- IMP: One small issue here that the light does not always start when we reload the scene,
  So we fix that by removing the *auto* checkbox in lighting option. (And that's it!) 

- Finally, we spice up our UI with some animation.

## 18. Stage eighteen:
Create popup UI for upgrading and selling turrets

- We start by adding new UI called `NodeUI` and position it right top of our turret
  then we add `Canvas` to hold the *UI buttons* and the upgrading and selling buttons

- Then we modify `buildManager` Script by adding reference to our Node which will be selected to show the `NodeUI`,
  and reference to our `NodeUI` script,
  Then we Create new function Called `selectNode()` and because we want only to select a node or turret at time so we set
`turretToBuild=null;` and the same `selectedNode=null;` in `SelectTurretToBuild()` method.

- Then we modify `OnMouseDown()` method in `Node` Script by calling `selectNode()` method in case the node already got turret

- After that, we Create new Script called `NodeUI` which will be responsible for showing **UI** for that node
  then we create `SetTarget()` method to enable the `Canvas` to that node in its position using
  `GetBuildPosition()` which return the position with offset.
- Then we need way to hide UI when we click the node again or when we `SelectTurretToBuild` so we create `Hide()` method which will handle that
  by setting Canvas active to false.
- Finally, we add cool small fade-in animation whenever we click the node to show the **UI**.

## 19. Stage nineteen:
Let's make an upgrade system for our turrets :)

- First of all, we fix a previous bug with the *UI* that the top cells got under the UI so it's not always get the mouse event,
  so with the quick fix, we disable `Graphic Raycaster` for `TopCanvas` and that's it.
- Then We start by modifying the `Node` script by adding `BuildTurret()` method which will be instead of `BuildTurretOn()` method in `buildManager`
  because it will be reasonable to be on the node.
- Then We modify the `BuildManager` script by adding `GetTurretToBuild()` method to return the turret to build.
- Going back to `Node` script we add new method `UpgradeTurret()`
- After that we modify `TurretBluePrint` script by adding `UpgradeTurret` prefab and `upgradeCost`,
  By that, we are going to have only two version for each turret the ordinary one and the upgraded one.

- Then we want to add a way to call our `UpgradeTurret()` method so we go to `NodeUI` script and add new method called `Upgrade()`
  Which will simply call `UpgradeTurret()` method and hide the menu after that.
- Finally, We modify `NodeUI` script by Creating reference to our text to change the *Cost*,
  and modify the `SetTarget()` method to show the *upgradeCost* and check if turret already upgraded.

## 20. Stage twenty:
Let's make some functionality for selling turrets :)

- First of all, we create upgraded version for **MissileLauncher** and **LaserBeamer**
- Then we modify the `Node` Script by adding `SellTurret()` method the same as for upgrade we edit `NodeUI` script to hook everything.
- Finally, we create a Sell effect for a turret.

## 21. Stage twenty one:
Let's add a Pause Menu to our game!

- First of all, we modify `CameraController` Script and delete the toggle pause to make new Script called `PauseMenu`
  which will be the central place to control the Pause Menu.
- Then we Create the `PauseMenuUI` and add buttons.
- Finally, we create button animation by changing button transition to *animation* and Generate animation.

## 22. Stage twenty two:
Let's add a cool Main Menu to our game!

- We just create a new 3D UI for Main Menu and upgrade unity to v5.5.0f3

## 23. Stage twenty three:
Let's add a health bar to each of our enemies!

- First of all, we create new Canvas to hold the `healthbar` and change it to **World space** to stick with the *enemy*
- Then we add `healthbar` which will be an Image.
- After that, we import white Image sprite (we don't use the default ones because they scale very wired)
- Then we change setting to   
  - texture type: sprite 
  - filter mode:  point (no filter)
  - Generate mip maps: off
- After that we go to Image type and change it to `Filled` and fill method to `Horizontal`
- Finally, we edit our `Enemy` Script and reference the Image and control the `healthbar` according to health amount.

## 24. Stage twenty four:
Let's add a fade in/out when we change scenes!

- First of all, we create simply UI to animate between on and off to fade between scene
- Then we remove `Graphic recast` so it's dosen't block user input,
- After that, we create new script Called `SceneFader` which will animate fade in and out between scenes.
- Finally, we use our new `SceneFader` prefab to animate loading scenes in all over our game.

## 25. Stage twenty five:
Let's improve our Wave Spawner to make it more customizable!

- First of all, we start by Editing `waveSpawner` Script to keep track of how many enemies in the scene
  so we only spawn enemies when there is no enemies live.
- Then we specify waves configuration separately by creating new Script Called `Wave`.
- After that we create three type of enemies with different material, speed and cost.
- Finally, we edit `SpawnWave()` method to make sure if player pass all waves then he will be won the level.