<h1><b>There is no merge done to master, so, get development branch</b></h1>

# ReactStudy

Project it's setted to connect into my own REST API published at my Heroku. I haven't publised the RestAPI code at github because the purpose is only study front-end at this project, then it's not necessary to publish that simple CRUD API.

## Running Code
<ol>
  <li>Certify you've already installed Node.js, NPM/yarn and react</li>
  <li>Clone this repository</li>
  <li>Switch to development branch</li>
  <li>In your favorite prompt with project folder oppened, type: "npm start" or "yarn start" depends witch you use</li>
</ol>

Projet will be running!

## Running Code from Docker with HotReload
<ol>
  <li>Certify docker is already installed on your PC</li>
  <li>Clone this repository</li>
  <li>Switch to development branch</li>
  <li>In your favorite prompt with project folder oppened, type: docker build . -t reactstudy:latest</li>
  <li>After build done, type: docker run -v ${PWD}:/app -v /app/node_modules -p 3001:3000 --rm reactstudy:latest</li>
</ol>

Now, you're running from a docker container who will be removed when stopped. Another nice thing is that even your react app is running into a container, changes will imediattely updated into container by hotload when you change code.

Projet will be running!

<h2>Opened for Discussion</h2>
If your have any question I could help or something you did not agree about project decisions, be free to get in touch with me at dynagita@gmail.com and I'll be glad in schedulle a time for discussing project with you!

<h2>Social</h2>
<ul>
  <li><a href="https://www.linkedin.com/in/daniel-yanagita-88860770/" target="_blank">Linked In</a></li>
  <li><a href="https://twitter.com/Daniel_Yanagita" target="_blank">Twitter</a></li>
</ul>
