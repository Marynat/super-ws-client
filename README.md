# super-ws

## Description
Simple application to learn and showcase my ability to learn new things.
I have split the application into two main ones.
.client which connects to the server subsribes to relevant topics and stores the data locally.
.api which collects the data prepares it for the graph and using plotly shows it on Razor Page. 

### About me 
My name is Mariusz.
It's my first time using razor pages in Asp.Net Core.
I see a lot of things that I could improve, for example the whole showcase of data is rushed, and also i should improve test coverage.

## Build and run.
After getting the package first thig is to create file application.json in .client and application.Development.json in .api.
Add relevant DB connections strings (for Database: "SuperWs" - as it is in database configuration).
Then run dotnet ef update-databse from .client aplication - thats where migrations are.
Next stage is to build and run the .client app to connect to server and gather data.
Afterwards you can run the .api app and go to the Bitcoin or Etherium page.
And run postam with route: localhost:port/quotes?symbol=string&from=long&to=long

## Things to improve / next steps
Give the user ability to select the range of the showed data.
Load new data automatically.
And much more :)