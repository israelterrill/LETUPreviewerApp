Install NodeJS/npm (may come installed in the same package)
Install PHP
Install Composer (not using npm; follow the directions on the website)

Then, cd to the root directory of the project (not src), run:

LINUX:
sudo npm install -g bower && sudo npm install -g gulp
sudo npm install && bower install && composer update
sudo bower update

WINDOWS:
npm install -g bower && npm install -g gulp
npm install && bower install
bower update

FOR BOTH:
If any questions are asked during installation as to which version of Angular to install, use 1.4.x.

This should generate all you need to run the frontend.
Then, run:
gulp

This should run a server in the terminal. In your browser, go to localhost:8000, and you should see the application.
You cannot have two terminals with the gulp command running at the same time.

Lastly, if you want information/map data to actually show in the
front-end, then you MUST run and follow the instructions in the ../TestServer
directory (or write your own web server, following the API data formats
specified in the ../Documentation/APIDataFormat.txt file).
