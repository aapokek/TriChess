# 🏆 TriChess - A Three-Player Chess Game

TriChess is an innovative three-player chess game built in Unity. It expands on traditional chess by introducing a triangular board and modified chess movement rules to fit three players.  

## 🎮 Features

- **Three-Player Chess** – Play against two opponents in a three player chess game.
- **Triangular Chessboard** – A fresh take on the classic game with a unique board layout.  
- **Custom Piece Movements** – Adjusted movement rules to fit the triangular board. 

## 📸 Pictures
**Game starting layout:**
![Game Starting Layout](Screenshots/TriChess_early_stage_board.png)
**Used cube coordinates:**
![Cube Coordinate Illustration](Illustrations/TriChess_Cube_Coordinates.png)

## 🛠️ Current Development Stage

#### Current Status:
- **Surroundings**: Green base layer showcasing the tabletop.
- **Chessboard**: The triangular chessboard tiles instantiate inside tileMap dictionary upon game startup (Cube coordinates as keys, tiles as values).
- **Pieces**: The chesspieces are instantiated upon game startup.

#### To do:
- Save chesspieces inside appropriate dictionary.
- Enable player interaction with pieces (click-to-move functionality).
- Implement movement rules based on the triangular board layout.
- Implement move validation base on the movement rules.
- Add turn-based mechanics, including player switching and move validation.
- Implement chesspiece destruction movements.
- Integrate win conditions (checkmate, stalemate, etc.).
- Make the game pretty ie. add surrounding graphics.
