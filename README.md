## Context
<p align="justify">
&nbsp;&nbsp;&nbsp;&nbsp;After a pandemic cenário almost all hotels in the world went bankrupt, this is an application for the Cancun’s hotel who must be deployed into an on premise infrastructure.
</p>
<p align="justify">
<strong>
&nbsp;&nbsp;&nbsp;&nbsp;*** It's important to explain that this application was used as a POC for checking how easily would unit tests become if I could isolate any service method into a task, I've given name as UseCase, but you can imagine those UseCases as isolated tasks. I'll still setup a stryker (mutation test) to check how effective this development approach has been. Fell free to get in touch with me and comment your point of view of this approach!
</strong>
</p>

## Decisions
### Architecture Pattern
<p align="justify">
&nbsp;&nbsp;&nbsp;&nbsp;As Cancun is a high searched vacation place, there will be there a lot of concurrency at this system. Besides that on of non functional requirement we have to get a disponibility on 99-100%.
</p>
<p align="justify">
&nbsp;&nbsp;&nbsp;&nbsp;Because of that it was chosen a CQRS and Event Driven pattern for hold the application.
</p>

![Design](https://github.com/dynagita/book_room/blob/main/doc/BookRoomDiagram_v2.png?raw=true)


<p align="justify" style="color: red">
<strong>***Load balances are not contained at this project. Docker compose only contain: PostGree, Rabbit, MongoDb, BookRoom.WebApi, BookRoom.Service and BookRoom.Readness.WebApi. Soon, react project will be added too.</strong>
</p>

### Database
<p align="justify">
&nbsp;&nbsp;&nbsp;&nbsp;As an on premise infrastructure it was though about cost to mantain application, and because of that it was chosen:
<ol>
   <li>Postgre as relational database for receiving commands and processing the books</li>
   <li>MongoDB for receiving queries for the application</li>
</ol>
</p>

### Messaging
<p align="justify">
&nbsp;&nbsp;&nbsp;&nbsp;As concurrency should be a problem, it was clear that many people could book a room at the same time, for not letting people create and get confirmed a book at the same period, it was decided using a Queue system to receive books and processing them.
</p>
<p align="justify">
&nbsp;&nbsp;&nbsp;&nbsp;A queue was choices because of it’s policy FIFO (First in, First out) guaranteeing consistency for system.
</p>
<p align="justify">
&nbsp;&nbsp;&nbsp;&nbsp;For coast reason’s it was choosed RabbitMQ
</p>

### Development Technologies
<p align="justify">
&nbsp;&nbsp;&nbsp;&nbsp;Technologies for holding project was chosen by the IT’s hotel employees knowlodge:
<ol>
   <li>Backend => .Net 6</li>
   <li>Frontend => React</li>
</ol>
</p>
<p align="justify">
&nbsp;&nbsp;&nbsp;&nbsp;As a plus, those technologies will not bring high costs for project.
</p>
### Non Function Requirements
<p align="justify">
&nbsp;&nbsp;&nbsp;&nbsp;For making sure 99-100% disponibility will be needed an extra infrastructure resource beyond the architecture diagram bringing for the system:
<ol>
   <li>
      LoadBalancer for make sure new services may be scaled
      <ul>
        <li>NGINX would be a nice choice</li>
      </ul>
   </li>
   <li>
      API Gateway for organizing and centralizing routes for letting easy for website or another frontends who may be desired after MVP1
      <ul>
        <li>NGINX would be a nice choice</li>
      </ul>
   </li>
</ol>
</p>

# Executing Application Developed

<p align="justify">
&nbsp;&nbsp;&nbsp;&nbsp;Pay attention for requirements to follow step by step listed bellow:
<ol>
  <li>Docker</li>
  <li>Docker-Compose</li>
  <li>NPM</li>
  <li>GIT</li>
</ol>
</p>
<p align="justify">
<ol>
  <li>
      Clone repository https://github.com/dynagita/book_room.git
      <img src="https://github.com/dynagita/book_room/blob/main/doc/setps/step1.png?raw=true" alt="Step 1"/>
  </li>  
  <li>
      Access the book_room directory: cd “clone directory” 
      <img src="https://github.com/dynagita/book_room/blob/main/doc/setps/step2.png?raw=true" alt="Step 2"/>
  </li>
  <li>
      Start the back-end environment: “dokcer-compose up” 
      <img src="https://github.com/dynagita/book_room/blob/main/doc/setps/step3.png?raw=true" alt="Step 3"/>
      <img src="https://github.com/dynagita/book_room/blob/main/doc/setps/step4.png?raw=true" alt="Step 4"/>
  </li>
  <li>
      Access the ui-react directory: cd bookrrom-ui-react
      <img src="https://github.com/dynagita/book_room/blob/main/doc/setps/step5.png?raw=true" alt="Step 5"/>
  </li>
  <li>
      Install npm packages: npm i
      <img src="https://github.com/dynagita/book_room/blob/main/doc/setps/step6.png?raw=true" alt="Step 6"/>
  </li>
  <li>
      Run react environment: npm start
      <img src="https://github.com/dynagita/book_room/blob/main/doc/setps/step7.png?raw=true" alt="Step 7"/>
  </li>
  <li>
      You have just started all applications for checking system's functionalities! =D      
      <img src="https://github.com/dynagita/book_room/blob/main/doc/setps/step8.png?raw=true" alt="Step 8"/>
  </li>  
</ol>
</p>

### Happy Coding!

Author: Daniel Yanagita

LinkedIn: https://www.linkedin.com/in/daniel-yanagita-88860770/
