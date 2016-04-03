# LETU Previewer App

The following folders contain the different parts of the project:


FrontEnd: The front end source code and all associated images and resources. The source code will have to be built according to the "Setup.txt" file (this file is in the source code archives). This source code is written in AngularJS.


DesktopApplication: The desktop application code for this project.


NodeTestServer: A test server written in NodeJS.


C#TestBackend: A test API written in C#.


Documentation: The SRS and SDS documents and other project documents.


## Front-end Setup
Install NodeJS/npm (may come installed in the same package)


Install PHP


Install Composer (not using npm; follow the directions on the website: https://getcomposer.org/doc/00-intro.md)

Then, cd to FrontEnd directory which should be just above the /src directory, and run the following:

### Linux:


sudo npm install -g bower && sudo npm install -g gulp


sudo npm install && bower install && composer update


bower update

### Windows:


npm install -g bower && npm install -g gulp


npm install && bower install && composer update


bower update

### Both:


This should generate all you need to run things.
Then, run:


gulp


This should run a server in the terminal. In your browser, go to localhost:8000, and you should see the application.
You cannot have two terminals with the gulp command running at the same time.


Lastly, if you want information/map data to actually show in the
front-end, then you MUST run and follow the instructions in the ../TestServer
directory or in the [Test Server Setup] (#api-test-server-setup) section found below (or write your own web server, following the API data formats specified in the ../Documentation/APIDataFormat.txt file).


## API Node Test Server Setup


This server serves as a test API for the application. Start this after running
gulp from the FrontEnd directory after following the instructions in
README.txt or the [Front-end Setup](#front-end-setup) section above.


To use this server, first install http and httpdispatcher:


npm install http && npm install httpdispatcher


Then, in the TestServer directory run:
node Server.js


This should start a web server with the port set to 44623. The served data is 
according to the format in Documentation/APIDataFormats.txt.


## API C# Test Server Setup


Alternatively, you can just open the C# Test Server in Visual Studio and click the run button at the top to build and run the project.
If you have problems with the front-end not finding the API, you may need to adjust the base address/port variable in the front end source code's src/js/app.js file to match the port that the browser brings up when you run the Visual Studio project.

