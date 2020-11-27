USE Finances;
GO

INSERT INTO Users (UserName, UserPassword) VALUES
('Mikhail', 1234),
('Ivan', 4321)
GO

INSERT INTO Income (IncomeName, IncomeValue, IncomeDate, UserId) VALUES
('Salary', 11000, '13.02.2020', 1),
('Sale', 3400, '15.02.2020', 1),
('Salary', 10000, '16.02.2020', 1),
('Salary', 12000, '17.02.2020', 1),
('Sale', 4000, '18.02.2020', 1),
('Sale', 12900, '19.02.2020', 1),
('Sale', 500, '21.02.2020', 2),
('Salary', 15000, '25.02.2020', 2),
('Salary', 16000, '27.02.2020', 2),
('Salary', 12000, '10.03.2020', 2),
('Sale', 3900, '13.03.2020', 1),
('Salary', 10000, '17.03.2020', 1),
('Salary', 10000, '23.03.2020', 2),
('Sale', 5000, '24.03.2020', 1),
('Sale', 100, '09.04.2020', 1),
('Sale', 500, '10.04.2020', 2),
('Salary', 12000, '15.04.2020', 2),
('Salary', 16000, '24.04.2020', 1)
GO

INSERT INTO Cost (CostName, CostValue, CostDate, UserId) VALUES
('Products', 1100, '13.02.2020', 1),
('Products', 1000, '14.02.2020', 1),
('Products', 1600, '15.02.2020', 2),
('Products', 1060, '16.02.2020', 1),
('Products', 1200, '17.02.2020', 2),
('Service', 3000, '18.02.2020', 1),
('Service', 1500, '01.03.2020', 2),
('Service', 1000, '02.03.2020', 1),
('Products', 1800, '03.03.2020', 1),
('Products', 5000, '04.03.2020', 1),
('Products', 3000, '05.03.2020', 1),
('Products', 350, '06.03.2020', 2),
('Products', 100, '07.04.2020', 2),
('Products', 1400, '08.04.2020', 2),
('Products', 7800, '09.04.2020', 1),
('Service', 3400, '10.04.2020', 2),
('Service', 1200, '13.04.2020', 2),
('Service', 500, '15.04.2020', 2),
('Products', 7000, '16.04.2020', 2),
('Products', 1000, '17.04.2020', 1)
GO