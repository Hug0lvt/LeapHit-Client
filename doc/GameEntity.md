```mermaid
classDiagram
    
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