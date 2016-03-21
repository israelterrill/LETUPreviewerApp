# LETUPreviewerApp

The following folders contain the different parts of the project:

SourceCode: The source code and all associated images and resources for the project. The source code will have to be built according to the "Setup.txt" file (this file is in the source code archives). This source code is written in AngularJS.


Documentation: The SRS and SDS documents and other project documents

DiaFiles: The diagrams for the project.


## Machine Setup
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

This should generate all you need to run things.
Then, run:


gulp

This should open a server. In your browser, go to localhost:8000, and you should see the application.
Be careful not to have two instances of gulp running with the current code--errors will probably occur.





