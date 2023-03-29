```mermaid
classDiagram

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