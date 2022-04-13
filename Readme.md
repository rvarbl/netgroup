##Readme

####Project was made using:
- InventoryApplication(backend): AspNet Core 6.0.201
- ClientApplication (frontend): Node.js, Aurelia2 framework, Webpack.
- Database: PostgreSQL on Docker
- Windows 11 


####To run the database on Docker:
~~~sh
- Navigate to the ..\SolutionItems\" folder, where docker-compose.yml is located.
- use command: docker-compose up
~~~

####To run inventory app:

~~~sh
- navigate to the : ..PublishedWebApp\ folder
- run WebApp.exe
- note: I never got the published version working on other machines. 
  Database was not created when the executable was run.
- Application should still work when run using an IDE.
~~~

####To run client app:
~~~sh
- Navigate to the ..\ClientApplication folder
- run command: npm run start
~~~

Note: At the moment, the project is still a work in progress.
Some requirements have not yet been implemented.

####Github link:
~~~sh
https://github.com/rvarbl/netgroup
~~~

