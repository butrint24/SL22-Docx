CREATE DATABASE CODESAMPLE
USE CODESAMPLE

CREATE TABLE Proffessor (
    Proff_id  INTEGER NOT NULL,
    Name     VARCHAR(20),
    LastName  VARCHAR(30),
    Subject    VARCHAR(50)
);

ALTER TABLE Proffessor ADD CONSTRAINT Proffessor_pk PRIMARY KEY ( Proff_id );

CREATE TABLE Staff (
    Staff_id  INTEGER NOT NULL,
    Name      VARCHAR(20),
    LastName   VARCHAR(30),
    Title    VARCHAR(40)
);

ALTER TABLE Staff ADD CONSTRAINT Staff_pk PRIMARY KEY ( Staff_id );

CREATE TABLE Student (
    Student_id   INTEGER NOT NULL,
    Name          VARCHAR(20),
    LastName       VARCHAR(30),
    University  VARCHAR(50)  
                    
    );

ALTER TABLE Student ADD CONSTRAINT Student_pk PRIMARY KEY ( Student_id );

CREATE TABLE University (
    uni_id                INTEGER NOT NULL,
    name_uni              VARCHAR(40),
    location             VARCHAR(25),
    degree                VARCHAR(35),
    proffessor_prof_id     INTEGER NOT NULL,
    student_student_id  INTEGER NOT NULL,
    staff_staff_id        INTEGER NOT NULL
);

ALTER TABLE University ADD CONSTRAINT university_pk PRIMARY KEY ( uni_id );

ALTER TABLE University
    ADD CONSTRAINT university_proffessor_fk FOREIGN KEY ( proffessor_prof_id )
        REFERENCES Proffessor ( Proff_id );

ALTER TABLE University
    ADD CONSTRAINT university_staff_fk FOREIGN KEY ( staff_staff_id )
        REFERENCES Staff ( Staff_id );

ALTER TABLE University
    ADD CONSTRAINT university_student_fk FOREIGN KEY ( student_student_id )
        REFERENCES Student ( Student_id );