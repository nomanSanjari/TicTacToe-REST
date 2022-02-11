# VogChallenge
Vog JDP Challenge Prompt

Endpoint 1 -> Route -> http://localhost/api/TicTacToe/CreateGame

Request Format for Endpoint 1

{
  "id": 0,
  "playerA": {
    "id": 0,
    "name": "Noman",
    "symbol": "O"
  },
  "playerB": {
    "id": 0,
    "name": "Anin",
    "symbol": "X"
  },
  "moves": 0,
  "winner": {
    "id": 0,
    "name": "string",
    "symbol": "string"
  }
}

Endpoint 2 -> Route -> http://localhost/api/TicTacToe/UpdateGame

Request Format for Endpoint 2

{
   "gameID":1,
   "playerID":1,
   "index":0
}

Endpoint 3 -> Route -> http://localhost/api/TicTacToe/UpdateGame

----------------------------------------------------------------------

How to Run -> Unfortunately I couldn't get Docker Desktop or the Engine to work on my computer so I couldn't actually build the image 
              That being the case I'm afraid the only way to test my code is to get it off the GitHub Repository and Build and Run via Visual Studio

---------------------------------------------------------------------

Answer to Final Question -> Authorization Code Grant with PKCE (Proof Key for Code Exchange) as it can prevent attacks such as Token Injection, as well as it possibly being scraped from the Browser History. 
                            