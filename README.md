## Description

Derivco Assessment

## Step 1 - Stored Procedure

navigate: nothwind -> dbuddate -> scripts

script is executable against the AdventureWorks2016 Db


## Step 2 - Web Api

1. navigate: roulette -> services -> WebApi
2. Run the WebApi and navigate to https://localhost:7036/api/game/
3. **placeBet**  is post endpoint. Below is an example of inputs to send to the endpoint:
   - playerId e.g 'Player1' 
   - boardArea e.g 'Inside' or 'Outside'
   - outside bets e.g: 'Even' or 'Odd' etc (for inside bets this should be null)
   - in the request body enter an array of strings e.g ["1","3","5","7"] (for outside move this should be blank)
