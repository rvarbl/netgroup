#Readme

Project was made using:
- InventoryApplication(backend): AspNet Core 6, IntelliJ Rider
- ClientApplication (frontend): Aurelia2 framework, Node.js, Webpack, Visual Studio Code.
- Database: PostgreSQL on Docker
- Windows 11


To run the database on Docker:
~~~sh
- Navigate to the ..\SolutionItems\" folder, where docker-compose.yml is located.
- use command: docker-compose up
~~~

To run inventory app:

~~~sh
- navigate to the : ..PublishedWebApp\ folder
- run WebApp.exe
- note: I never got the published version working on other machines. 
  Application should still work when run from \InventoryApplication\ using an IDE.
- The MVC is there just for testing purposes
~~~

To run client app:
~~~sh
- Navigate to the ..\ClientApplication folder
- run command: npm run start
- note: Reccomended browser  is Brave (https://brave.com/) with shields down.
  Apparantely the CORS configuration is not set up correctly.
  For me Chrome and Firefox threw CORS-errors when tryng to communicate with the Inventory app.
~~~

Note: At the moment, the project is still a work in progress.
Some requirements have not yet been implemented.

Github:
~~~sh
https://github.com/rvarbl/netgroup

branch main - project as it was on deadline.
branch later_edits - later changes to the project.
~~~

Rain Varblane 2022
