# employee-benefits
This is an application written in Angular 8 and .Net Core which will calculate employees' benefits deductions based on their and their dependents' information.  It is a simple CRUD application which will update employee, dependents and their corresponding benefits info.

# database
I currently have the application hooked up to my Mongo Atlas cluster.  You can either run the app as-is against this db (until I disable the app user!) or just point it at some other Mongo instance.  This is a completely code-first approach so you can use whatever Mongo instance you like.  Also there is an implementation of the IEmployeeRepository that will store all employee info in-memory for the life cycle of the application.  Simply set "useInMemoryPersistence": true in appSettings.json and the application will use its own store.

# unit-tests
There are several back-end unit tests that cover some of the areas of greater cyclomatic complexity.  Specifically, the BenefitsCalculatorService has been covered extensively.

# other notes
All client-side "glue" components were generated using ng-openapi-gen.
There is currently no authentication or authorization.  This is a wide-open app that I threw together just to play with the technology stack.  Also note that there is no multi-tenancy.  In other words, a similar real-word application would most likely cover many customers all hosted on the same servers.  This does not.

# TODO
Host this app somewhere so that it can be used without having to run locally.
