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
    
    class Item {
        +BallHitItem(ball: Ball)
    }
    
    class SnipeItem
    SnipeItem --|> Item
    
    class ExceptionItemDelete
    ExceptionItemDelete <.. Item

    class Paddle{
        +BallHitPaddle(ball: Ball)
    }
    Paddle --|> GameEntity
    class Ball{
        -speed: int
        -difficulty: int
        +Reset(screenHeight: int, screenWidth: int, local: bool)
    }

    
    class Skin {
        -asset: string
        -name: string
    }
    <<abstract>> Skin
    Skin <|-- BallSkin
    Skin <|-- PaddleSkin

    class PaddleSkin
    class BallSkin

    class GameEntity{
        -x: float
        -y: float
        +Move()
    }
    GameEntity <|-- Ball
    GameEntity <|-- Item
    GameEntity --> Skin : 1 skin

    class Player {
        -ready: bool
    }
    <<abstract>> Player
    Player --> IMovement : 1 strategyMovement
    Player --> GameEntity : 1 paddle
    
    Player <|-- UserPlayer

    class Bot
    Bot --|> Player

    class UserStat{
        -timePlayed: int
        -toucheBall: int
    }
    
    class User {
        -pseudo: string
    }
    User --> Skin : * skins
    User --> UserStat : 1 globalStat

    class UserPlayer
    UserPlayer ..> User

    class GameOnline
    GameOnline --|> Game

    class Game{
        +Play(screenWidth: int, screenHeight: int, elapsedSecond: float)
        +SetScore(ball: Ball, screenWidth: int, screenHeight: int, elapsedSecond: float)
    }
    Game --> Player : 1 localPlayer
    Game --> Player : 1 externalPlayer
    Game --> GameEntity : 1 ball
    Game --> GameEntity : 1 item
    GameStat <-- Game : 1 gameStat
    
    class GameStat
    GameStat --> Score : 1 score

    class Score{
        -/player1 : Tuple<Player, int>
        -/player2 : Tuple<Player, int>
        +GetWinner() : Player
        +GetScore() : Tuple<int, int>
        +IncrementScore()
    }