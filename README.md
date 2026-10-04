# BLM5026 – AI in Computer Games

Unity project developed throughout the **BLM5026 – AI in Computer Games** course. Each week the topic covered in class is implemented in the same project, and that week's summary and homework are added to this page.

**▶ [Play the demo in your browser](https://muratunlu0.github.io/BLM5026_AI_InComputerGames/)**: the demo is here. It is a WebGL build that fills the browser window and starts right away, nothing to install. Move with W/A/S/D or the arrow keys.

![The pig chases the player after it enters the view cone](Docs/Week2/fov-chase.png)

## Weekly progress

| Week | Topic | Scene | Demo |
|:----:|-------|-------|:----:|
| 2 | Math for game AI: vectors, moving toward a goal, field of view (FOV) | `Assets/Moving.unity`, `Assets/Scenes/SampleScene.unity` | [Play](https://muratunlu0.github.io/BLM5026_AI_InComputerGames/week2/) |

A new row and a new section are added as the weeks go on.

## Running the project

1. Clone the repository and add the folder in Unity Hub with **Add → Add project from disk**.
2. Open the project with **Unity 6.3 LTS (6000.3.24f1)**.
3. Open the scene of the week you want to try (`Assets/Moving.unity` for week 2).
4. Press **Play** and click on the Game window.

| Key | Action |
|-----|--------|
| W / A / S / D or arrow keys | Move the player (Pumpkin) |

---

## Week 2 – Math for Game AI

Game AI looks like behavior from the outside, but underneath it is geometry, algebra and update rules that run every frame. This week builds the basic tools for movement, pursuit and perception.

### Covered in class

- **Points and vectors:** a point is a location, a vector is a displacement; adding a vector to a point gives a new point.
- **Magnitude and normalization:** `v.normalized` keeps the direction and makes the length 1; for a consistent speed, direction and speed are kept separate.
- **Moving toward a goal:** `direction = goal - position`, then `position += direction.normalized * speed * Time.deltaTime`.
- **Stop distance and facing:** comparisons use `sqrMagnitude`, and the character is turned toward the goal with `transform.forward`.
- **Dot product and FOV cone:** the sign of the dot product tells front from behind, while `Vector3.Angle(forward, toTarget) <= halfFOV` gives a view cone.

### Implementation

| Object | Script | Role |
|--------|--------|------|
| Pumpkin (player) | [`PumpkinController.cs`](Assets/Scripts/PumpkinController.cs) | Reads the `Vector2` input from the `OnMove` message sent by the Player Input component and moves the player on the ground plane. The camera is a child of Pumpkin, so it follows the player. |
| Pig (AI) | [`Moving.cs`](Assets/Moving.cs) | If it can see the goal, turns toward it and approaches at a constant speed until the stop distance. |
| DrawVectors | [`DrawVectrors.cs`](Assets/Scripts/DrawVectrors.cs) | In-class activity: adds the vector v = <4, −1> to the points P = (2, 3) and Q = (−3, 2) and draws them in the Scene view. |

![Pumpkin Controller and Player Input components on Pumpkin](Docs/Week2/editor-pumpkin.png)

### Homework: follow only inside the field of view

> When the player stays behind the pig, outside its field of view, the pig cannot see the player and must not follow. Once the player gets in front of the pig, inside its view angle, the pig should start following.

Every frame the pig computes the vector to the player and checks two conditions: is the angle between that vector and its forward direction smaller than half of the view angle, and is the player within the view distance. If both hold it chases the player, otherwise it stays where it is. The view cone is drawn in `OnDrawGizmos`: **red** when the player is not visible, **green** when it is.

Try it in the [week 2 demo](https://muratunlu0.github.io/BLM5026_AI_InComputerGames/week2/): the pig faces right at the start and ignores you while you stay behind it; walk in front of it and it turns and follows. Gizmos only exist in the editor, so the cone itself is not drawn in the browser build.

| Player behind the pig: no chase | Player inside the cone: chase |
|:--:|:--:|
| ![Player behind the pig, red cone](Docs/Week2/fov-behind.png) | ![Player inside the cone, green cone](Docs/Week2/fov-chase.png) |

The settings can be changed on the **Moving** component of the Pig:

| Field | Default | Description |
|-------|:-------:|-------------|
| Goal | Pumpkin | Object to follow |
| Speed | 4 | Movement speed (units/s) |
| Stop Distance | 1.5 | Stops when this close to the goal |
| Field Of View | 90 | Total view angle (degrees) |
| View Distance | 10 | How far the pig can see |

![Moving component on the Pig and the view cone in the Scene view](Docs/Week2/editor-pig.png)

---

## Folder structure

```
Assets/
├── Easy Primitive People/        Ready-made characters from the starter package (Pig, Pumpkin, ...)
├── Materials/
├── Scenes/
│   └── SampleScene.unity         Vector drawing activity
├── Scripts/
│   ├── DrawVectrors.cs
│   └── PumpkinController.cs
├── InputSystem_Actions.inputactions
├── Moving.cs
└── Moving.unity                  Pursuit and FOV scene
Docs/
└── Week2/                        Screenshots used in this README
```

## Environment

- Unity 6.3 LTS (6000.3.24f1), Built-in Render Pipeline
- Input System 1.20.0
