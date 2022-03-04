# Insurance Policy API

[![Build](https://github.com/patrickosm/insurance-api/actions/workflows/build.yml/badge.svg)](https://github.com/yanpitangui/dotnet-api-boilerplate/actions/workflows/build.yml)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=PatrickOSM_insurance-api&metric=coverage)](https://sonarcloud.io/dashboard?id=PatrickOSM_insurance-api)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=PatrickOSM_insurance-api&metric=alert_status)](https://sonarcloud.io/dashboard?id=PatrickOSM_insurance-api)

## Overview

This project was created as part of the hiring process at superformula and will be utilized as a sample project for C# .Net 6.0 API implementation.

## Authentication
Some routes in this API requires authentication. For that, the API call have to use the ``api/user/authenticate`` route to obtain the JWT.
As default (for test purposes), this project have two users, Admin and normal user.
- Normal user: 
	- email: user@email.com
	- password: userpassword
- Admin:
	- email: admin@email.com
	- password: adminpassword
