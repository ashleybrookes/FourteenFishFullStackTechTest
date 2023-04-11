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

It is assumed that the laptop running has the following installed:
- mySQL database server 
- visual studio or visual studio code

Before running the project you will first need to run the db_changes.sql on your mySQL database. This sql file will add the new tables "speciality", "personspeciality", "foreign keys" and the seed data for the speciality table. It is assumed that you already have seed.sql already in your database.

You will need to set the DB_CONNECTION_STRING to you local mySQL database as an environment variable. In visual studio 2022 right click on the FullStackTechTest project goto properties then Debug.

### Guide Feature 1 - Import Data

On the home page use the "choose file" button press this button to open the file dialogue then select the file with the import data in, this needs to be a .json file type. Not using a json filetype will cause an error.

After your file has been uploaded you will see the new people in the table on the home page.

People with the same GMC number in the import file will update the people and address data on the system.

### Guide Feature 2 - Speciality

In the header you will see the speciality page. This page lists all the specialities in the database, they can be deleted, edited and new specialities can be added.

To add a speciality for a person go to the person details page e.g. /Home/Details/1 , click on edit, then use the checkboxes to select the speciality you want to apply/remove for that person. But dont forget to hit save :floppy_disk:

The Speciality Name is limited to 100 characters in length.

### Future Implementations
- Adding a spinner animation after clicking a button that saves/edits/imports/deletes.
- More unit tests to ensure robustness across the entire app.
- Authentication to secure the admin functionality of the app from the public
- Show the multiple addresses
- Validation messages returned back from the controller requests and displayed a meaningful message to the user about the problem with the input. 
