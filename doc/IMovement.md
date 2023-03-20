```mermaid
classDiagram
    
    class LeapListener{
        +OnFrame()
    }
    LeapListener <-- LeapMotion : 1 listener

    class LeapMotion{
        -coordonate: float
        +SetCoordonate(value: float)
        +OnHandMade()
        +OnClosing()
    }
    LeapMotion --|> MotionSensor

    class Camera{
        -coordonate: float
        -thread: Thread
        -stopThread: bool
    }
    Camera --|> MotionSensor
    
    class IMovement{
        +GetMovement() : float
    }
    <<interface>> IMovement
    IMovement <|.. Aleatoire

    class Aleatoire{
        -speed: int
        -difficulty: int
        -targetY
    }
    Aleatoire --> GameEntity : 1 ball
    Aleatoire --> GameEntity : 1 paddle
    
    class MotionSensor{
        -ready : bool
        +StartMovement()
        +StopMovement()
        #SetReady(ready: bool)
        #NotifyPropertyChanged(propertyName: string)
    }
    <<abstract>> MotionSensor
    MotionSensor ..|> IMovement

    class Mouse
    Mouse --|> MotionSensor

    class Network
    Network --|> MotionSensor