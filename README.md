# OneStream

Before running the application you'll need to configure startup projects in Visual Studio.
- Choose "Multiple startup projects" radio button
- For each project (FrontendWebApi, BackendWebApi2, BackendWebApi3) action, choose Start
- Click Apply then OK

Now you can click Start to begin testing.

The FrontendWebApi browser will launch a swagger page by default (https://localhost:7037/swagger/index.html)

There are two Movies verb exposed, GET | POST

GET -> Try out and execute.  There are default movies defined in BackendWebApi2 and BackendWebApi3. 

POST -> Try out, create a request body and execute. Note: Id and FromSource are not required and will be set in the backend API's. 

After a successful POST, execute GET and you'll see the new movies in the result set.