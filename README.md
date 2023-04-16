## Ashley Brookesmith: FourteenFish - Full Stack Technical Assessment

### Directory Hierarchy
```
|—— DAL
|—— FourteenFishFullStackTechTest.Tests
|—— FullStackTechTest
|   |—— Controllers
|   |—— Models
|   |   |—— Home
|   |   |—— Shared
|   |   |—— SpecialityViewModel
|   |—— Views
|   |   |—— Home
|   |   |—— Shared
|   |   |—— Speciality
|   |—— -wwwroot
|       |—— css
|       |—— js
|       |—— lib
|           |—— bootstrap
|           |   |—— dist
|           |       |—— css
|           |       |—— js
|           |—— jquery
|           |   |—— dist
|           |—— jquery-validation
|           |   |—— dist
|           |—— jquery-validation-unobtrusive
|—— Models
```

### How to get started with the app

It is assumed that the developement computer has the following installed:
- mySQL database server 
- visual studio 2022 or visual studio code

Before running the app you first need to run the db_changes.sql on your mySQL database. This sql file will add the new tables "speciality", "peoplenspeciality", as well as foreign keys constraints and the seed data for the speciality table. It is assumed that you already have seed.sql already in your database.

You will need to set the DB_CONNECTION_STRING to your local mySQL database as an environment variable. In visual studio 2022 right click on the FullStackTechTest project go to properties then Debu, you will see the environment variables there.

### Guide to Import Data Feature

On the home page you will see the "choose file" button. Click on this button to open the file dialogue, then select the file with the import json data, this needs to be a .json file type. Only .json file types are supported.

After your file has been uploaded you will see the new people in the table on the home page.

People with the same GMC number in the import file will update the people and address data on the app.

### Guide to Speciality Feature

In the header you will see the speciality page. This page lists all the specialities in the app, they can be deleted, edited and inserted.

To assign a speciality to a person go to the person details page e.g. /Home/Details/1 , click on edit, then use the checkboxes to select the speciality you want to apply/remove for that person. Now click on the save button.

The Speciality Name is limited to 100 characters in length.

### Future Implementations
- After the file has been imported the user could be show a detailed breakdown of the changes to the person and address data on the system. For each person imported it would say if they were inserting or updating. The user would then click "Accept changes" or "Discard".
- Add GMC to Home/Index table view - this is the real Id for each person.
- Validation messages returned back from the controller requests and displayed a meaningful message to the user about the problem with the input. 
- Adding spinner animations after clicking a button that saves/edits/imports/deletes. This tell the user the system is dealing with there request.
- More unit tests to ensure robustness across the entire app.
- Authentication to secure the admin functionality from the public

