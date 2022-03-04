# Insurance Policy API

[![Build](https://github.com/patrickosm/insurance-api/actions/workflows/build.yml/badge.svg)](https://github.com/yanpitangui/dotnet-api-boilerplate/actions/workflows/build.yml)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=PatrickOSM_insurance-api&metric=coverage)](https://sonarcloud.io/dashboard?id=PatrickOSM_insurance-api)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=PatrickOSM_insurance-api&metric=alert_status)](https://sonarcloud.io/dashboard?id=PatrickOSM_insurance-api)

## Overview

This project was created as part of the hiring process at superformula and will be utilized as a sample project for C# .Net 6.0 API implementation.

## API Documentation

To make it easier to consume and use this API, a postman collection was created to help with that:

[![Run in Postman](https://run.pstmn.io/button.svg)](https://god.gw.postman.com/run-collection/3956737-de3e2d6f-ccf7-4b19-b735-dc085ae9c9e5?action=collection%2Ffork&collection-url=entityId%3D3956737-de3e2d6f-ccf7-4b19-b735-dc085ae9c9e5%26entityType%3Dcollection%26workspaceId%3D1351243f-c35b-4006-9d81-3941aed0da97)

Please follow this tutorial to import this [Postman Collection](https://www.getpostman.com/docs/collections).

A Swagger UI is also available as API documentation for this project, when building the project, access the path ``/api-docs`` to check out the endpoints.

## Authentication
Some routes in this API requires authentication. For that, the API calls have to use the ``api/user/authenticate`` route to obtain the JWT.
As default (for test purposes), this project has two users, admin, and a normal user.
- Normal user: 
	- email: user@email.com
	- password: userpassword
- Admin:
	- email: admin@email.com
	- password: adminpassword

## How to run

### Docker
 - For docker-compose, run this command on the root folder: ``dotnet dev-certs https -ep https/aspnetapp.pfx -p yourpassword`` (replace "yourpassword" with something else in this command and the docker-compose.override.yml file). This creates the HTTPS certificate.
 - Run ``docker-compose up -d`` in the root directory, or, in visual studio, set the docker-compose project as startup and run. This should start the application and DB.

## Running Tests
Run ``dotnet test`` command in the root folder. It will try to find all test projects associated with the solution. Running this project in Visual Studio, it's also possible to access Test Explorer, where all tests are available to run as desired. 
