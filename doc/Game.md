```mermaid
classDiagram

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