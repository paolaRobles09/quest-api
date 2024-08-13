# Quest API

A Service to manage player's quest' progress.

## Description

The service takes the proper request to store the player's quest progress in a SQLite DB that can be consulted to review the current player's quest state.
It will also get the player's quest state by player's id.

## Getting Started

### Dependencies

* .NET 8 SDK 
* Entity Framework Core
* Newtonsoft
* Swagger
* SQLite
* XUnit
* Moq

### Installing

* Clone the repository: git clone https://github.com/paolaRobles09/quest-api.git
* Install any missing dependencies: 
```
    npm install
    dotnet add package Newtonsoft.Json
    dotnet add package Swashbuckle.AspNetCore
    dotnet add package Microsoft.EntityFrameworkCore
    dotnet add package Microsoft.EntityFrameworkCore.Sqlite
    dotnet add package Micorosft.EntityFrameworkCore.Design
    dotnet add package xunit
    dotnet add package xunit.runner.visualstudio
    dotnet add package Moq
    dotnet add Microsoft.NET.Tests.Sdk
```
* Apply database migrations to create the db from the command line within the Quest.Infrastructure project folder
```
    dotnet ef database update
```

### Executing program

* Run the program directly from Visual Studio or using
```
dotnet run
```
* To see the endpoints in Swagger use the following url: https://localhost:7222/swagger/index.html
  You should see the following:
  ![image](https://github.com/user-attachments/assets/cb23d578-b670-4b8e-b397-9b5e11cb0897)

* Update questConfig.json as needed (it's under Quest.API project folder), it has the following structure:
```
{
    "TotalQuestPointsNeeded": number,
    "RateFromBet": decimal,
    "LevelBonusRate": decimal,
    "Milestones": [
      {
        "Index": number,
        "ChipsAwarded": number,
        "QuestPointsRequired": number
      }
    ]
}
```
Properties for questConfig.json are:
* TotalQuestPointsNeeded (integer): It's the total number of points needed to complete the quest.
* RateFromBet (decimal): The rate to be used to calculate quest points based on the player's bet (ChipAmountBet).
* LevelBonusRate (decimal): The rate to be used to calculate the level bonus based the player's level (PlayerLevel).
* Milestones (array of objects): The list of milestones along the quest, each milestone has:
    * Index (integer): A unique index for each milestone
    * ChipsAwarded (integer): The number of chips awarded to the player when the milestone has been reached.
    * QuestPointsRequired (integer): The number of quest points (accumulated) required to reach the milestone.

#### Endpoints

* /api/progress
```
Method: POST
Request body 
{
    "PlayerId": string
    "PlayerLevel": number,
    "ChipAmountBet": number
}

Success response:

Code: 200
Body:
{
    "QuestPointsEarned": number,
    "TotalQuestPercentCompleted": number,
    "MilestonesCompleted": [{
        "MilestoneIndex": number,
        "ChipsAwarded": number
    }]
}

```

* /api/state
```
Method: GET
URL Parameters: PlayerId

Success response (when player exists):

Code: 200
Body:
{
    "TotalQuestPercentCompleted": number,
    "LastMilestoneIndexCompleted": number
}

Error response (when player don't exists):

Code: 404
Message: Not Found

```

## Notes

* Points earned displayed in the ProgressResponse are calculated using the current player's progress
```
(ChipAmountBet * RateFromBet) + (PlayerLevel * LevelBonusRate)
```
* "RateFromBet" and "LevelBonusRate" are derived from questConfig.json file.
* "ChipAmountBet" and "PlayerLevel" are provided in the request.
* The points earned are accumulated and added to the player's quest progress into the DB.
* The Milestones achieved are calculated based on the player's accumulated points and the last Milestone achieved so it won't achieve the same Milestone twice. 

## Help

If there are errors related to the Database, try the following:
* Go to the terminal console, make sure you're within the Quest.Infrastructure project
* Update the database by running:
```
dotnet ef database update
```
from Visual Studio's Package Console Terminal:  `Update-Database -Project Quest.Infrastructure`

OR

* Remove "quest.db" from Quest.API project folder
* Still in the terminal console, within the Quest.Infrastructure project run:
```
dotnet ef migrations add CreateDatabase
dotnet ef database update
```
from Visual Studio's Package Console Terminal: 
```
Add-Migration CreateDatabase
Update-Database
```

## Authors

Paola Robles

## Version History

* 0.1
    * Initial Release
* 0.2
    * Reestructured project


## Acknowledgments

Inspiration, code snippets, etc.
* [awesome-readme](https://github.com/matiassingers/awesome-readme)
* [Swashbuckle](https://learn.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-8.0&tabs=visual-studio)
* [Tracking vs No-Tracking Queries](https://learn.microsoft.com/en-us/ef/core/querying/tracking)
* [Unit Testing XUnit](https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-dotnet-test)
