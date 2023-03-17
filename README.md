# restapi_practise
## About the API:
It is an API I created for the senior project: System_Monitor_UI (you can find it in my repository), which is a machine monitor system. This API serves not only the basic CRUD function to receive frontend request to interact with data from the connected MongoDB database, but also provides routes for special service, for example, generate csv report for data.
## Property:
* Language: C#
* API type: Rest
* service: 
  * MongoDB Service: interact with MongoDB collection
   * include: get, get based on id, get based on timeframe, post
   * New added: get/csv: generate csv report and download
  
 ## Future plan:
 * Add service for PDF generation (project switch to csv now)
 * Dockerizing the api (current)
