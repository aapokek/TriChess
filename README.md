# 🏆 TriChess - A Three-Player Chess Game

TriChess is an innovative three-player chess game built in Unity. It expands on traditional chess by introducing a hexagonal board and modified chess movement rules to fit three players.  

## 🎮 Features

- **Three-Player Chess** – Play against two opponents in a three player chess game.
- **Hexagonal Chessboard** – A fresh take on the classic game with a unique board layout.
- **Custom Piece Movements** – Adjusted movement rules to fit the hexagonal board.

## 📸 Pictures
**Game starting layout:**
![Game Starting Layout](Screenshots/TriChess_early_stage_board.png)
**Used cube coordinates:**
![Cube Coordinate Illustration](Illustrations/TriChess_Cube_Coordinates.png)

## 🛠️ Current Development Stage

#### Current Status:
- **Surroundings**: Green base layer showcasing the tabletop.
- **Chessboard**: The hexagonal chessboard tiles instantiate inside tileMap dictionary upon game startup (Cube coordinates as keys, tiles as values). Each tile has a OccupyingPiece property which stores the piece on top (or null if the tile is empty).
- **Pieces**: The chesspieces instantiate inside pieceMap dictionary upon game startup (Cube coordinates as keys, pieces as values).

#### To do:
- Enable player interaction with pieces (click-to-move functionality).
- Implement movement rules based on the hexagonal board layout.
- Implement move validation base on the movement rules.
- Add turn-based mechanics, including player switching and move validation.
- Implement chesspiece destruction movements.
- Integrate win conditions (checkmate, stalemate, etc.).
- Make the game pretty ie. add environmental graphics.
