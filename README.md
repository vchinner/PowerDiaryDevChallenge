# Power Diary Dev Challenge

### Solution Content
| Project | Description |
| ----------- | ----------- |
| PowerDiaryDevChallenge | .Net Core 3.1 MVC Web Application |
| PowerDiaryDevChallenge.Chat | .Net Core 3.1 Class Library | 
| PowerDiaryDevChallenge.Data              | .Net Core 3.1 Class Library |
| PowerDiaryDevChallenge.IntegrationTests | nUnit & Moq Integration Tests |
| PowerDiaryDevChallenge.UnitTests  | xUnit Unit Tests |

## Running The Application
The application can be run using one the follwing methods.

### Docker
You can mount the docker image and run locally on host on Azure.

Docker image can be found here : [Docker Image](https://hub.docker.com/repository/docker/vincechinner/powerdiarydevchallenge)

The image port is setup on port 80 but can be overwritten with following command:

`docker run -p 8080:80 vincechinner/powerdiarydevchallenge`

You can change 8080 with your preferred port.

### Visual Studio
You can also run the application by running the **PowerDiaryDevChallenge** solution from Visual Studio

## Unit Tests
The are  10 unit tests in the **PowerDiaryDevChallenge.UnitTests** project and is run via the visual studio test runner

![unit tests](https://i.imgur.com/Zuy9sZz.png)

![unit tests](https://i.imgur.com/uZINB3w.png)

## Integration Tests
The are 9 integration tests in the **PowerDiaryDevChallenge.IntegrationTests** project and is run via the visual studio test runner

![integration tests](https://i.imgur.com/Mt9Xxlm.png)

![integration tests](https://i.imgur.com/ymJoCSg.png)

