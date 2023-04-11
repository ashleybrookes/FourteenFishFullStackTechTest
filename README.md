## Ashley Brookesmith: FourteenFish - Full Stack Technical Assessment

## How to get started with the project

It is assumed that the laptop running has the following installed:
- mySQL database server 
- visual studio or visual studio code

Before running the project you will first need to run the db_changes.sql on your mySQL database. This sql file will add the new tables "speciality", "personspeciality", "foreign keys" and the seed data for the speciality table.

## Guide Feature 1 - Import Data

On the home page use the "choose file" button press this button to open the file dialogue then select the file with the import data in, this needs to be a .json file type. Not using a json filetype will cause an error.

After your file has been uploaded you will see the new people in the table on the home page.

People with the same GMC number in the import file will update the people and address data on the system.

## Guide Feature 2 - Speciality

In the header you will see the speciality page. This page lists all the specialities in the database, they can be deleted, edited and new specialities can be added. To add a speciality for a person go to the person details page e.g. /Home/Details/1 , click on edit, then use the checkboxes to select the speciality you want to apply/remove for that person. But dont forget to hit save :smiley:


# Future Implementations
- Adding a spinner animation after clicking a button that saves/edits/imports/deletes.
- More unit tests to ensure robustness across the entire app.
- Authentication to secure the admin functionality of the app from the public
- Show the multiple addresses
- Validate the 

----

## Testing Goals

We are testing your ability to build upon an existing application, underpinned by good software engineering practices that demonstrate a broad range of skills within a full stack.

Your solution does not need to be a complete application but should showcase your approach to software engineering and demonstrate that you understand how to build software.

## The Task

You are working on an existing app that stores information about medical professionals, such as doctors. You have been given a barebones C# .NET app to build upon, along with seed data for a MySQL database.

Currently, the app has a simple home page to display a list of the stored records. Individual records can be selected to view/edit specific details. The app works, but maybe it's not that great - please make any observations and improvements as you see fit.

### Feature 1: Import data

Your task is to implement an import feature to parse, validate, and store new entries. We have provided some sample data for you to work with in the expected JSON format. Here are user stories and acceptance criteria such a feature should satisfy:

#### User stories

- As an admin, I want to import users from an external source so that I can migrate new users into our app.

#### Acceptance criteria:

- All doctors have a unique 7-digit GMC number.
- Given that admins may accidentally import the same data more than once, ensure new entries are only imported once.

### Feature 2: Add specialities

We have a requirement to record each doctor's medical specialities. Example specialities include:

- Anaesthetics
- Cardiology
- Dermatology
- Emergency Medicine
- General Practice (GP)
- Neurology
- Obstetrics and Gynaecology
- Ophthalmology
- Orthopaedic Surgery
- Psychiatry

#### User stories

- As an admin, I want to add and remove medical specialities so I can manage them in the system.
- As an admin, I want to associate an existing speciality with a doctor's profile so that we can record what specialities they can perform.
- As an admin, I want to see the specialities associated with a doctor when viewing their details page so that we know what specialities they can perform.

#### Acceptance criteria:

- Given that a doctor may have more than one speciality, admins should be able to associate a doctor with more than one.
- Given that a doctor may stop practising a speciality, admins should be able to remove them for a user.
- Given that an admin may make a mistake when adding a speciality name, updating the speciality should update it for all doctors associated with it.

How you implement these features is up to you.

You will have approximately 1 week to complete this task and should focus on an MVP. You can use as much or as little time as you deem necessary to demonstrate your understanding of the problem, your range of skills, and approach to problem solving.

## Evaluation

To evaluate your solution, we will test your import function using new data that follows the format of the sample data you have been provided. Please provide instructions if any setup or changes are required in order for your solution to work.

We will look at the design, structure, and implementation of your solution. We would like to see good practices, taking into account robustness, extensibility, readability, documentation, etc.

If there are any ideas that you would have liked to implement if you had more time, we would love to hear about it.

## Submitting your solution

Create a public Github repository and push your solution, including any documentation you feel is necessary. Commit often - we would rather see a history of trial and error than a single monolithic push. When you’re finished, please send us the URL to the repository.
