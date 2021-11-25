# Vocational guidance system - Descubre
> Vocational Guidance application containing psychometric test sections and career information as a guide for students.


## Table of Contents
* [General Info](#general-information)
* [Technologies Used](#technologies-used)
* [HTTP API](#http-api)
* [Setup](#setup)
* [Usage](#usage)
* [Contribution](#contribution)
* [Interfaces](#interfaces)
* [Project Status](#project-status)
* [Room for Improvement](#room-for-improvement)
* [Contact](#contact)


## General Information

### **Problem**
<p align="justify">The lack of vocational orientation in young people in the pre-university stage can be attributed to the problem of choosing a career that is not in accordance with their interests, abilities and aptitudes. An erroneous career decision could lead to various problems in the course of the decision-maker's personal, university and work life, such as desertion, poor university performance, work frustration, repercussions on physical health, stress, anxiety, poor work performance, feelings of failure and personal dissatisfaction.</p>

### **Objectives**
<p align="justify">The objective of this project is to develop a vocational guidance application with web technologies, based on psychometric tests of vocational preferences, aptitudes and behavioral profile, with the purpose of supporting pre-university students in the choice of a university or technical career.</p>


### **Requirements**


|   **User story**                |          **Description**         |
| ------------------------------- | -------------------------------- |
| Register student information    | The user (student) will be able to register his        personal data (user name, e-mail, father's last name, mother's last name, first names, telephone number, e-mail) in the system. |
| Record student psychology test  | The user (student) will be able to perform the different tests (preferences, aptitudes, behavioral profile) of the vocational orientation. |
| Display information on university careers | The user (student) will be able to visualize the professional profile of each university career. |
| Show final test report of the vocational orientation process. | The user (student) will be able to visualize the results (preferences, aptitudes and professional careers) according to the psychological tests. |
| Print final report | The user (student) will be able to print the final report of the vocational orientation process. |
| Login | The user (student) will be able to authenticate himself/herself to log in to the application. |
| Log out | The user (student) will be able to log out to the application. |

## Technologies Used

- .NET - version 6.0
- Microsoft.EntityFrameworkCore - version 6.0.0
- Npgsql.EntityFrameworkCore.PostgreSQL - version 6.0.0
- Lizelaser0310.Utilities - version 0.1.17


## HTTP API

According to the requirements, the api must contain the following end points.

#### **`Home`**

* `GET /`

#### **`Login`**

* `POST /login`

#### **`User`**

* `GET /user`
* `GET /user/:id`
* `POST /user`
* `PUT /user/:id`
* `DELETE /user/:id`

#### **`Role`**

* `GET /role`
* `GET /role/:id`
* `POST /role`
* `PUT /role/:id`
* `DELETE /role/:id`

#### **`Institution`**

* `GET /instituion`
* `GET /institution/:id`
* `POST /institution`
* `PUT /institution/:id`
* `DELETE /institution/:id`

#### **`Career`**

* `GET /career`
* `GET /career/:id`
* `POST /career`
* `PUT /career/:id`
* `DELETE /career/:id`

#### **`Test`**

* `GET /test`
* `GET /test/:id`
* `POST /test`
* `PUT /test/:id`
* `DELETE /test/:id`

#### **`Alternative`**

* `GET /alternative`
* `GET /alternative/:id`
* `POST /alternative`
* `PUT /alternative/:id`
* `DELETE /alternative/:id`

#### **`Result`**

* `GET /result`
* `GET /result/:id`
* `POST /result`

#### **`Recommendation`**

* `GET /recommendation`
* `GET /recommendation/:id`
* `POST /recommendation`


## Setup

### Requirements
* You must have [.NET](https://dotnet.microsoft.com/download) version 6.0
* You also must have [GIT](https://git-scm.com/) if you want to contribute to the project.

### Get the repository locally
First of all, clone the repository:

```bash
git clone git@github.com:lizelaser/descubre-back-end.git
cd <path_to_project>
```

## Usage

### Build and launch for development
Start a development server and launch the project on localhost:5000

```bash
dotnet run
```

### Build and minifies for production
Builds the project and its dependencies into a set of binaries.

```bash
dotnet build
```

## Contribution
To contribute to MPB, follow these steps:

1. Fork this repository.
2. Create a branch: `git checkout -b <branch_name>`.
3. Make your changes and commit them: `git commit -m '<commit_message>'`
4. Push to the original branch: `git push origin <project_name>/<location>`
5. Create the pull request.

Alternatively see the GitHub documentation on [creating a pull request](https://help.github.com/en/github/collaborating-with-issues-and-pull-requests/creating-a-pull-request).


## Interfaces

### **Home**
![home](./Images/home.jpg)

### **Login**
![home](./Images/login.jpg)

### **Register user**
![home](./Images/user.jpg)

### **Psychometric tests**
![test](./Images/test-vocational-preferences.jpg)

### **Recommendation**
![home](./Images/test-result.jpg)



## Project Status

[![Project Status: Inactive – The project has reached a stable, usable state and is being actively developed.](https://www.repostatus.org/badges/latest/inactive.svg)](https://www.repostatus.org/#active)



## Room for Improvement

Room for improvement:
- Expand the web system by implementing a module for counseling psychologists that allows them to manage psychometric tests, upload information on careers and universities, and schedule meetings.

- Conduct comprehensive planning to support the implementation of other functionalities associated with vocational guidance variables targeting undergraduate and graduate students in the system.

To do:
- Perform unit and integration testing.
- Serve to production on live environment.


## Contact

Lizeth La Serna - [@lizelaser](https://github.com/lizelaser) - lizeth.lasernafelices@gmail.com

Project Link: [https://github.com/lizelaser/descubre-back-end](https://github.com/lizelaser/descubre-back-end.git)

