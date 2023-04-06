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
specialityId INT NOT NULL
);