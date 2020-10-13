# Netsuite REST C#

This ia an example of how to call Netsuite's RESTlet or REST API Using OAuth1.0 with C#

## Prerequisites
> This has been tested using dotnet for linux version 3.1.402 on ubuntu 20.04, but it should work on Mac or Windows

Install `dotnet` using the official documentation from microsoft [Install on ubuntu guide](https://docs.microsoft.com/es-mx/dotnet/core/install/linux-ubuntu)

## How to test
>This example uses a GET Method
1. Clone Repository
2. cd into **netsuite-rest-chsarp** folder
3. Edit **Program.cs** file
4. Fill **Consumer Token, Consumer Secret, Token Key, Token Secret** and **Account** with the netsuite tokens from your netsuite account.
5. Fill the **Url** you want to call 
6. Execute command `dotnet run`