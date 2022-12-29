## Description

Derivco Assessment

## Step 1 - Stored Procedure

navigate: nothwind -> dbuddate -> scripts

script is executable against the AdventureWorks2016 Db


## Step 2 - Web Api

1. navigate: roulette -> services -> WebApi
2. Run the WebApi and navigate to https://localhost:7036/api/game/
3. **placeBet**  is post endpoint. Below is an example of inputs to send to the endpoint:
   
```bash
$ curl -X POST https://localhost:7036/api/game/placeBet -H "ContentType application/json" -d '{ "playerId": "PlayerX", "boardArea": "Outside", "bet": "Even", "moves": []  }'
```
