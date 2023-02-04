```mermaid
classDiagram

    class WebSocket{

    }

    class Game{


    }
    class GameStats{
    }

    class Profile{
        
    }

    class Skin{
        -/Image Asset 
        -/string name
    }

    class Player{
        -/string Pseudo
    }

    class PlayerStats{
        
    }

    class IMouvement{
        +getMouvement()
    }
    <<interface>> IMouvement


    class IControlMouvement{
        +getUp()
        +getDown()
        +getLeft()
        +getRight()
    }
    <<interface>> IControlMouvement

    class Ball{
        -/posX : float
        -/posY : float
        -/angle : Rad
        -/velocity : int
    }

    class CapacityBall{
        +changeSize()
        +changeVelocity()
    }

    class Paddle{
        -/posY : float
        -/velocity : Vector
    }

    Game <-- "2" Player : players
    GameStats --> "1" Game : statGame 
    Player ..> Profile
    Profile --> "1" PlayerStats : statGame
    Profile --> "*" Skin :skins
    Player --> "1" IMouvement : strategyMouvement
    Bot --|> IMouvement
    IControlMouvement --|> IMouvement
    LeapMotion --|> IControlMouvement
    Mouse --|> IControlMouvement
    Camera --|> IControlMouvement
    Game ..> WebSocket
    PaddleSkin --|> Skin
    BallSkin --|> Skin
    Player --> "1" BallSkin : SelectedPaddle
    Bot --> "1" Ball : ball
    Ball --> "1" BallSkin : skin
    Ball --> "1" CapacityBall : capacity
    Player --> "1" Paddle : paddle
```