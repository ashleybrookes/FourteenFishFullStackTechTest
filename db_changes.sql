create table speciality (
Id INT PRIMARY KEY auto_increment,
SpecialityName varchar(100)
);

INSERT INTO speciality (`Id`,`SpecialityName`) VALUES (1, 'Anaesthetics');
INSERT INTO speciality (`Id`,`SpecialityName`) VALUES (2, 'Cardiology');
INSERT INTO speciality (`Id`,`SpecialityName`) VALUES (3, 'Dermatology');
INSERT INTO speciality (`Id`,`SpecialityName`) VALUES (4, 'Emergency Medicine');
INSERT INTO speciality (`Id`,`SpecialityName`) VALUES (5, 'General Practice (GP)');
INSERT INTO speciality (`Id`,`SpecialityName`) VALUES (6, 'Neurology');
INSERT INTO speciality (`Id`,`SpecialityName`) VALUES (7, 'Obstetrics and Gynaecology');
INSERT INTO speciality (`Id`,`SpecialityName`) VALUES (8, 'Ophthalmology');
INSERT INTO speciality (`Id`,`SpecialityName`) VALUES (9, 'Orthopaedic Surgery');
INSERT INTO speciality (`Id`,`SpecialityName`) VALUES (10, 'Psychiatry');

create table peoplespeciality (
Id INT PRIMARY KEY auto_increment,
PersonID INT NOT NULL,
SpecialityId INT NOT NULL
);

/*Adding CONSTRAINTS for how the data should be stored and  */
ALTER TABLE peoplespeciality
ADD CONSTRAINT fk_peoplespeciality_speciality
FOREIGN KEY (SpecialityId)
REFERENCES speciality(Id);

/*Adding CONSTRAINTS for how the data should be stored */
ALTER TABLE peoplespeciality
ADD CONSTRAINT fk_peoplespeciality_people
FOREIGN KEY (PersonID)
REFERENCES people(Id);

/*Adding CONSTRAINTS for how the data should be stored */
ALTER TABLE addresses
ADD CONSTRAINT fk_addresses_people
FOREIGN KEY (PersonID)
REFERENCES people(Id);