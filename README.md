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
A service factory is included in order to further promote code reuse, maintainability, and separation of concerns.<br>
Each service method includes a _try-catch block_ to handle any exceptions that may occur.
For simple, predictable errors, a custom result class is utilized to promote defensive coding and avoid unnecessary exceptions.
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
+ The controller methods were written in accord with RESTful philosophical standards.<br>
+ The endpoints were given names to intuitvely assist client consumption and development.<br>
<img width="800" height="273" alt="cafeapi-rest" src="https://github.com/user-attachments/assets/d8e82046-626f-4b30-81d6-4398fb325807" />

##

# [Cat Poker](https://www.jmarcello.dev/cat-poker/index.html)
This is a _front-end_ web application that is written in **HTML**, **CSS**, and **JavaScript**.<br>
The **Boostrap** framework is used for column layout, and _responsiveness_.<br>

<img width="797" height="603" alt="Catpoker" src="https://github.com/user-attachments/assets/326f161f-91c8-45cf-a480-0d52afdedfc1" />

##

# [Airport Locker Rental](https://www.jmarcello.dev/airport/menu)
This is the _MVC version_ of my Airport Locker Rental application.<br>

<img width="779" height="539" alt="airport-mvc" src="https://github.com/user-attachments/assets/8b1217eb-6a5c-4a8f-81dd-6f9c894beb7c" />

##

## Documentation and Diagrams
Most of the documentation can be found right here in this repository.<br>
However, I realize that it might take some folks much time to find the code they'd like to view.<br>
Therefore, I included a _Documentation and Diagrams_ section on the portfolio website.<br>
There are buttons that will navigate you to specific areas of the repository for your convenience.<br>
Additionally, there are _Entity Relastionship_ diagrams and _Class Modeling_ diagrams for additional visuals.<br>
Currently, I have one ERD and one CMD for the 4th Wall Cafe. I am currently working on more diagrams as we speak.<br>

##

### More projects and features coming soon!
<img width="498" height="60" alt="Portfoliofooter" src="https://github.com/user-attachments/assets/d3e38750-27e0-4320-932d-62c11cc3843d" />
