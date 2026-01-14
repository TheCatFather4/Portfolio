# Portfolio

## About
This is my software developer portfolio website.<br>
It currently features **4 software applications** that I have built with _much care_.<br>
I built all of these applications from scratch and I can elucidate every detail of them.<br>
I am continually updating the site regularly, so check back frequently for new features!<br>

##

---> **Visit my site at** [https://www.jmarcello.dev](https://www.jmarcello.dev) <---

<img width="825" height="563" alt="portfolio" src="https://github.com/user-attachments/assets/5d7ffb8a-24ed-4fd8-880a-b6fa83968bbb" />

##

## Featured Software Applications

# [The 4th Wall Café](https://www.jmarcello.dev/Home/Cafe)
This is a _full-stack_ web application that utilizes **ASP.NET Core (MVC)** for the _front-end_ portion.<br>
The _back-end_ is written in **C#** and utilizes both **Entity Framework Core** and **Dapper** (Micro-ORM) for data persistence.<br>
**ASP.NET Core Identity** is also utilized for authentication and authorization.<br>
Additionally, the **Bootstrap** framework is used to make the website _responsive_.<br>

<img width="825" height="563" alt="cafe" src="https://github.com/user-attachments/assets/65c6b95c-b485-4fbc-90eb-944e6ebbbc57" />

##

## Café Features

### Async Await
+ All behavior members in this application (except class constructors) are written to run asynchronously.<br>
This promotes the "async all the way down" philosophy, as well as improves application performance.<br>
<img width="602" height="136" alt="4thwall-async" src="https://github.com/user-attachments/assets/1a02427f-92db-49b9-82b9-d1f8d6452a14" />


##
### Authentication
Registration, Logging In, and Logging Out are all implemented in this application.<br>
Users must register an account with the café in order to add items to the shopping cart.<br>
Users must also be logged in before they may proceed with ordering and payments.<br>
##
### Authorization
Two areas of the website cannot be accessed unless the user possesses the required roles.<br>
**1.) The Sales Report Area:** an _Accountant_ role is required to view this area.<br>
**2.) The Management Area:** a _Manager role_ is required to view this area.<br>
<br>
In the case that a user attempts to access an area without authorization,<br>
they are redirected to an "access denied" page with further instructions for their convenience.<br>
>[!NOTE]
>I have provided some sample credentials on the [login page](https://www.jmarcello.dev/account/login) so that you can access these areas.
##
### Layered Architecture
+ **Business Logic**<br>
All services are separated into a [business logic layer](https://github.com/TheCatFather4/Portfolio/tree/main/Portfolio/Cafe.BLL) for maximum modularity.<br>
A service factory is included in the project, in order to further promote code reuse, maintainability, and separation of concerns.<br>
Each service method includes a _try-catch block_ to handle any exceptions that may occur.<br>
For simple, predictable errors, a custom result class is utilized to promote defensive coding and avoid unnecessary exceptions.<br>
In addition, all service classes have a logger in order to record exception and other relevant messages.<br>
##
+ **Data Persistence**<br>
This application is connected to a database and uses both LINQ syntax and SQL to execute queries.<br>
**See the Two Database Modes section below for more details concerning LINQ vs SQL**<br>
All data persistence methods are separated into organized classes within the [data layer](https://github.com/TheCatFather4/Portfolio/tree/main/Portfolio/Cafe.Data).<br>
In the methods that use SQL, all queries are parameterized with an affixed @ symbol to prevent SQL injection attacks.
<img width="750" height="315" alt="4thwall-sql" src="https://github.com/user-attachments/assets/9137db1b-3313-44ad-9240-91c35657830a" />

##
### Menu Form
A menu form exists to allow users to filter through menu items.<br>
The items can be filtered in any combination of category, date, or time of day.<br>
Each item has a conveniently placed "Add to Cart" button, so that users may order immediately.<br>
Users must be logged in however, in order to add to their shopping carts.<br>
##
### Online Ordering
Customers can add, update, and delete items to and from their shopping carts.<br>
They can prepare their order for payment and add a tip.<br>
Additionally, they can pay for their order using one of several payment types.<br>

>[!NOTE]
>Payment processing is simulated and no actual financial transactions occur.
##
### Two Database Modes
There are two _different_ database modes that can be set up for effective data persistence.<br>
**1.) Entity Framework Core:** A framework ideal for _light weight queries_ (Uses **LINQ**).<br>
**2.) Dapper:** A micro-ORM ideal for _greater granular control_ (Uses **SQL**).<br>

>[!TIP]
>Change the _value_ of the "DatabaseMode" key to switch between modes.<br>
>The default value is set to "ORM" which uses Entity Framework Core.<br>
>If you would like to use Dapper, change the value to "Dapper".<br>
>The key can be found in the [_appsettings.json_](https://github.com/TheCatFather4/Portfolio/blob/main/Portfolio/Portfolio/appsettings.json) file.<br>
##
### Unit Tests
The [Cafe.Tests](https://github.com/TheCatFather4/Portfolio/tree/main/Portfolio/Cafe.Tests) project is where you can find the unit tests for this application.<br>
3 NuGet Packages were used for testing: NUnit, NUnit3TestAdapter, and Microsoft.NET.Test.Sdk.<br>
Additionally, mock repository classes and mock loggers are used to get the tests to run where needed.
##

### Café Diagrams
## I have made a few visuals to assist in reading the code for this application.<br>
+ An [Entity Relationship Diagram](https://jmarcello.dev/diagrams/cafe-erd.png) (which shows the database schema for the café)
+ A [Class Modeling Diagram](https://jmarcello.dev/diagrams/cafe-ld-cmd.png) (which shows the service class models, as well as the data persistence models for my code)
>[!Note]
>I am currently finishing up a class modeling diagram for the controllers and their relation to the logic classes.<br>
>There are a few artistic adjustments I need to make. After that, I will upload it for viewing. Stay tuned!<br>

# [Café API](https://www.jmarcello.dev/Home/CafeAPI)
This is a REST API that utilizes **ASP.NET Core (Web API)** for its controllers.<br>
It uses **The 4th Wall Café** [_back-end_](https://github.com/TheCatFather4/Portfolio/blob/main/README.md#layered-architecture) for its business logic and data persistence.<br>
**ASP.NET Core Identity** is also used for authentication and authorization.

<img width="779" height="539" alt="Cafeapi" src="https://github.com/user-attachments/assets/e66ad376-9884-4720-8843-abc7c5c37acf" />

##

## Café API Features

### JWT Authentication
This API uses a JSON Web Token (JWT) service in order to generate tokens for authentication.<br>
##
### RESTful Philosophy
+ The controller methods were designed in accord with RESTful philosophical standards.<br>
+ The endpoints were given names to intuitvely assist client consumption and development.<br>
<img width="800" height="273" alt="cafeapi-rest" src="https://github.com/user-attachments/assets/d8e82046-626f-4b30-81d6-4398fb325807" />

##

# [Cat Poker](https://www.jmarcello.dev/cat-poker/index.html)
This is a _front-end_ web application that is written in **HTML**, **CSS**, and **JavaScript**.<br>
The **Boostrap** framework is used for columns, rows, and _responsiveness_.<br>

<img width="797" height="603" alt="Catpoker" src="https://github.com/user-attachments/assets/326f161f-91c8-45cf-a480-0d52afdedfc1" />

##

## Directions for Cat Poker
1. Click on the roll button to generate five random cat cards from 1 to 6.<br>
2. The numbers you roll will be added up and displayed in the scoreboard area.<br>
3. After three rolls, the game is done and your high score is displayed.<br>
4. You may finish the game before three rolls by clicking the score button.<br>
5. To start a new game, simply click on the play again button.<br>
##

## Function Modeling Diagram
I made a "function" modeling diagram for Cat Poker based on the class modeling diagram concept.<br>
I drew up some pseudo-classes for the diagram that organizes the functions by responsibility.<br>
Check it out here --->[Cat Poker "Function" Modeling Diagram](https://jmarcello.dev/diagrams/catPokerFmd.png)<---<br>
##

# [Airport Locker Rental](https://www.jmarcello.dev/airport/menu)
This is the _MVC version_ of my Airport Locker Rental application.<br>
If you would like to see the repository for my console version, click here ---->[Airport Locker Rental Console Version](https://www.github.com/thecatfather4/airportlockerrental)<----<br>

<img width="779" height="539" alt="airport-mvc" src="https://github.com/user-attachments/assets/351eb29a-4c3d-4c81-b33d-7808c0899795" />

##

## Background Information
The inspiration for this application was to make a web version of my console app, so that users may see the code working in real time.<br>
I used custom CSS and Bootstrap to create a visual simulation of the windows command prompt.

##

## Portfolio Documentation and Diagrams
Most of the documentation can be found right here in this repository.<br>
However, I realize that it might take some folks much time to find the code they'd like to view.<br>
Therefore, I included a [_Documentation and Diagrams_](https://www.jmarcello.dev/home/documentation) section on my website.<br>
Each software application has buttons that will take you to specific areas of the repository for your convenience.<br>
Additionally, there are _Entity Relastionship_ diagrams and _Class Modeling_ diagrams for additional visual assistance.<br>
##

### More projects and features coming soon!
<img width="498" height="60" alt="Portfoliofooter" src="https://github.com/user-attachments/assets/d3e38750-27e0-4320-932d-62c11cc3843d" />
