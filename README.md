Application can be just run simply, sqlite db is destroyed and created everytime the application is restarted for demo purpose. Such can be disabled by code referred by comment in the Program.cs file.

This repository presents an application with below qualities:

- Clean code
- Open API spec documented
- All business logic tested
- All functional requirements covered

**Non Functional Requirements:**

1. The application just requires an upsert (update or insert) endpoint with relational database agnostic capability. 

2. Application also should be able to support dozens of thousands of candidates in the long run.

**Design Decisions:**

1. As the application required relational database agnostic capability, EFCore ORM that provides ease of switching most of the popular SQL databases with just config changes and running migrations. For the task, choosing SQLite for ease of demo.
2. As on the long run, application should be able to scale to dozens of thousands of candidates, the maintenance required also may increase with a good number of developers maintaining the application with feature additions, so going with the layered architecture approach which is right in this case as the app can start with ease small and also supports growing application.
3. Also, the service-based approach is used which can be decoupled in the future on smaller services and deployed separately even for add or update separate case.
4. Clean code approach followed.
5. Unit test for only the services and controllers may be applied which covers 100% of the business logics.
6. Integration tests may be written for repository section in the future.

**Not Implemented:**

1. Caching could be implemented for caching just the user existence using email key that could reduce the cost of number of database requests, but the better approach, for this case would be to write a stored procedure or function based on the database used.

**Implemented Improvements:**

1. Documented the API using the open API spec for ease of use.
2. Use of only open-source MIT, Apache-2.0, BSD3-clause licensed libraries which is also suitable for growing application.
3. Use of only DI for dependency use case.
4. Use of Fluent Validation for validating model properties which can be extended with more complex validations if required in future.
5. Modular code with smaller functions.

**A Different Approach:**

1. Upsert can be broken down to add(POST), update(PUT) and check(GET) endpoints with separate responsibilities and to uphold the REST principles. Also the response could utilize proper response status codes such as created at action(201) with data, and for update just the status code response would suffice and for check the user exist Boolean data with success status code.
2. Unit test could also be covered for the repositories and other files. This may be done for the sake of company that has a requirement of high percentage of code coverage.
3. Linters may be introduced to enforce code quality.
