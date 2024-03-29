# restapi_practise
## About the API:
It is an API I created for the senior project: [System_Monitor_UI](https://github.com/GraceErickson3-14/System-Monitor-UI.git) (you can find it in my repository), which is a machine monitor system. This API serves not only the basic CRUD function to receive frontend request to interact with data from the connected MongoDB database, but also provides routes for special service, for example, generate csv report for data.
## Property:
* Language: C#
* Library used: CSVHelper, MongoDB.Driver
* API type: Rest
* service: 
  * MongoDB Service: interact with MongoDB collection
   * include: get, get based on ip address, get based on timeframe, post,  Get,Post,Update to the new threshold collection
   * get/csv: generate csv report and download
   * Dcokerized with docker support
     ```
     docker pull honkaidocker/restapi:latest
     ```
   * The datamodel now has array of disks data so the api is able to handle data from all disks (using mapping to flatten the array to CSVHelper can handle it)
   * (current) api is now have the function to connect to both metric and threshold collection, and able to apply Get,Post,Update to the new threshold collection.
   * get by ip address now reuturn all matched data instead of just the first one in the collection
  
 ## Future plan:
 * Add service for PDF generation (project switch to csv now)
 * Dockerizing the api (finished)
 * Adding ability to send alert message to front end(current)
