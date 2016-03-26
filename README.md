# LETU Previewer App

The following folders contain the different parts of the project:

SourceCode: The source code and all associated images and resources for the project. The source code will have to be built according to the "Setup.txt" file (this file is in the source code archives). This source code is written in AngularJS.


Documentation: The SRS and SDS documents and other project documents

DiaFiles: The diagrams for the project.


## Front-end Setup
Install NodeJS/npm (may come installed in the same package)


Install PHP


Install Composer (not using npm; follow the directions on the website: https://getcomposer.org/doc/00-intro.md)

Then, cd to SourceCode/[appropriatedirectory] which should be just above the /src directory, and run the following:

### Linux:


sudo npm install -g bower && sudo npm install -g gulp


sudo npm install && bower install && composer update


bower update

### Windows:


npm install -g bower && npm install -g gulp


npm install && bower install && composer update


bower update

### For Both:


This should generate all you need to run things.
Then, run:


gulp


This should run a server in the terminal. In your browser, go to localhost:8000, and you should see the application.
You cannot have two terminals with the gulp command running at the same time.


Lastly, if you want information/map data to actually show in the
front-end, then you MUST run and follow the instructions in the ../TestServer
directory (or write your own web server, following the API data formats
specified in the ../Documentation/APIDataFormat.txt file).

