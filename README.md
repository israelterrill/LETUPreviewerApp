# LETU Previewer App

The following folders contain the different parts of the project:


FrontEnd: The front end source code and all associated images and resources. The source code will have to be built according to the "Setup.txt" file (this file is in the source code archives). This source code is written in AngularJS.


APIDataServer: The API and data server for the project.


DesktopApplication: The desktop application code for this project.


Documentation: The SRS and SDS documents and other project documents.


## Front-end Setup
Install NodeJS/npm (may come installed in the same package)


Then, cd to FrontEnd directory run the following:

### Linux:

```
sudo npm install -g bower && sudo npm install -g gulp
sudo npm install && bower install
sudo bower update
```

### Windows:

```
npm install -g bower && npm install -g gulp
npm install && bower install
bower update
```

To run the front-end, use the following:

```
gulp
```

This should run a server in the terminal and serve the webpage. In your browser, go to localhost:8000, and you should see the application.
You cannot have two terminals with the gulp command running at the same time.

Lastly, if you want information/map data to actually show in the front-end, then you must set up the [APIDataServer] (#api-data-server) (or write your own web server, following the API data formats specified in the ../Documentation/APIDataFormat.txt file).


## API Data Server 

###Setup


This server serves as a HTTP data API for the application. Build APIDataServer.sln. Once built, run "APIDataServer.exe". Make sure "DataClasses.dll" and "Newtonsoft.Json.dll" are in the same folder as "APIDataServer.exe." It may be necessary to run command in an administrator prompt.

Additionally, it may be necessary to create firewall rules allowing inbound traffic to the port used by APIDataServer.exe. Use the following commands in an administrator console:

```
netsh advfirewall firewall add rule name="APIDataServer" dir=in action=allow localport=44623 protocol=udp
netsh advfirewall firewall add rule name="APIDataServer" dir=in action=allow localport=44623 protocol=tcp
```

Note: If using Visual Studio to run the server (i.e. clicking start in Visual Studio), be sure to open Visual Studio in administrator mode.

### API Usage


API Server can be queried for various data types. Usage is: 

http://\<apiserverhost\>:44623/api/\<datatype\>/[key=value[&key2=value2]]

####Valid Datatypes:

-Questions: list of FAQs

-Activities: list of open optional activities on campus

-MapData: list of map data entries for campus buildings

-Schedules: list of schedules for preview events


####Valid Keys

-Skip (integer > 0): only retrieve entries after entry number <value>

-Take (integer > 0): only retrieve <value> entries. If 'skip' value is also provided, take applies after skip


####Examples:

http://\<apiserverhost\>:44623/api/activities

http://\<apiserverhost\>:44623/api/schedules/skip=1

http://\<apiserverhost\>:44623/api/questions/take=2

http://\<apiserverhost\>:44623/api/mapdata/take=3&skip=2
