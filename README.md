# LeapHit


---

```mermaid
classDiagram
    

    class UserPlayer
    UserPlayer ..> User
    UserPlayer --|> Player
    

    class GameEntity{
        -/x : double
        -/y : double
        
        +/ChangeSize()
        +/ChangeVelocity()
        +/ChangeSkin()
    }
    <<abstract>> GameEntity
    GameEntity --> "1" Skin : skin

    class Paddle{
        +/Move()
    }
    GameEntity <|-- Paddle

    class Ball{
        -/velocity : Vector2
        +/Move()
    }
    GameEntity <|-- Ball

    class Player{
    }
    <<abstract>> Player
    Player --> "1" GameEntity : paddle
    Player --> "1" GameEntity : ball
    Player --> "1" IMovement : movementStrategy

    class UserStat{
        -/touchBallCount : int
        -/timePlayed : int
    }
    

    class Skin{
        -/asset : string
        -/name : string
    }
    <<abstract>> Skin

    class PaddleSkin{
    }
    Skin <|-- PaddleSkin
    
    class BallSkin{
    }
    Skin <|-- BallSkin

    class User{
        -/pseudo : string
    }
    User --> "1" UserStat : globalStat
    User --> "*" Skin : skins
    User --> "1" Skin : selectedPaddle
    

    class Bot{
    }
    Bot --|> Player

    class Score{
        -/winner : Pair
        -/loser : Pair
    }

    class GameStat{
        -/time : int
    }    
    GameStat "1" <-- Game : gameStat
    GameStat --> "1" Score : score

    class WebSocket{
    }
    WebSocket <.. Game


    class Game{
        -/pause : bool
    }
    Game --> "2" Player : players
    
    class IMovement{
        +getMovement()
    }
    <<interface>> IMovement
    
    IMovement <|-- LeapMotion
    IMovement <|-- Camera
    IMovement <|-- Mouse
    IMovement <|-- Aleatoire


    class LeapMotion{
        +getMovement()
    }

    class Camera{
        -/fileNamePath : string
        -/python : string
        -/process : Procss
        -/coordonate : float
        +start()
        +close()
        +update()
        +getMovement()
    }

    class Mouse{
        +getMovement()
    }

    class Aleatoire{
        +getMovement()
    }
    <<abstract>> Aleatoire
    Aleatoire --> "1" GameEntity : paddle
    Aleatoire --> "1" GameEntity : ball
    

    
    class Facile{
        +getMovement()
    }
    Aleatoire <|-- Facile