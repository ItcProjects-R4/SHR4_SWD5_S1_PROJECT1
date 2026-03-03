Create Database StudentDB

Create Table students
(
   Id int Primary key Identity(1,1),
   Name nvarchar(20) Not Null,
   Age int,
   Grade nvarchar(10)
)

GO

Create View View_Students AS 
Select Id,Name,Age,Grade
From students

GO
Create Procedure AddStudent
	@Name nvarchar(30),
	@Age int,
	@Grade nvarchar(15)
AS
Begin
	Insert Into students(Name,Age,Grade)
			      Values(@Name,@Age,@Grade)
End;

Select * From View_Students;

Exec AddStudent "Ahmed",21,"Good";
Exec AddStudent "Mohamed",22,"Very Good";
Exec AddStudent "Ibrahim",22,"Excellent";
Exec AddStudent "Ali",19,Fail;

Select * From View_Students;



