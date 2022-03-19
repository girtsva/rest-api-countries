# EU Countries Web API
An ASP.NET Core Web API .NET 6 application that consumes data from [REST Countries API](https://restcountries.com/) and returns selected information on European Union countries.

Uses [Refit REST library](https://github.com/reactiveui/refit) and NUnit testing framework. Endpoints are displayed with Swagger.

## How to run the application:

1. Download or clone repository from GitHub.
2. Open and run the project with .NET capable IDE of your choice. Below you can see how to run the project in Visual Studio 2022:

![screenshot](/scr/01.png "run-project-in-vs2022")

3. You should see an open browser window with Swagger project page (http://localhost:5157/swagger/index.html)

![screenshot](/scr/02.PNG "swagger-endpoints")

4. To try the API, you can click on GET requests (GET -> Try it out -> Execute). For last request you have to enter country name before clicking on 'Execute'.
5. You should see the result in JSON format. For the last GET request, if not a valid EU country name was entered, you will see an appropriate error message.
6. Should you like to run tests, go back to your IDE. Below you can see how to run the tests in Visual Studio 2022. 

![screenshot](/scr/04.png "run-tests-vs2022")

Tests results will be shown in Test Explorer.
