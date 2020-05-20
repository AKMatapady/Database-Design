Use Company;

Insert into EMPLOYEE(fname,lname,ssn, bdate,address,sex,salary,dno)
Values ('Amith Kumar','Matapady', '111111111','09-22-1992','3560 Alma Road, Dallas, TX','M','42000','6');

select *
from employee
where ssn=111111111;

Use Company;


select dno, COUNT(*)
from EMPLOYEE
Group by dno
having COUNT(*) > 2