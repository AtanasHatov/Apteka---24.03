CREATE DATABASE PharmacyDB

CREATE TABLE Medicines(
[id_medicine] INT PRIMARY KEY IDENTITY(1,1),
[name] VARCHAR(100) UNIQUE,
[manufacturer] VARCHAR(100) NOT NULL,
[price] DECIMAL(10,2) NOT NULL,
[quantity_in_stock] INT NOT NULL
);

CREATE TABLE Employees(
[employee_id] INT PRIMARY KEY IDENTITY(1,1),
[name] VARCHAR(100),
[position] VARCHAR(50),
[salary] DECIMAL(10,2)
);

CREATE TABLE Prescriptions(
[id_prescriptions] INT PRIMARY KEY IDENTITY(1,1),
[id_medicine] INT FOREIGN KEY REFERENCES Medicines([id_medicine]),
[doctor_name] VARCHAR(100) NOT NULL,
[patient_name] VARCHAR(100) NOT NULL,
[date_issued] DATE NOT NULL,
[employee_id] INT FOREIGN KEY REFERENCES Employees([employee_id])
);

CREATE TABLE Orders(
[id_order] INT PRIMARY KEY IDENTITY(1,1),
[id_medicine] INT FOREIGN KEY REFERENCES Medicines([id_medicine]),
[supplier_name] VARCHAR(100) NOT NULL,
[order_date] DATE NOT NULL,
[quantity_ordered] INT NOT NULL,
[employee_id] INT FOREIGN KEY REFERENCES Employees([employee_id])
);

INSERT INTO Employees (name, position, salary) VALUES 
('Elena Petrova', 'Pharmacist', 2500.00), 
('Ivan Ivanov', 'Cashier', 1200.00), 
('Mihail Hristov', 'Manager', 3500.00); 

 

INSERT INTO Medicines (name, manufacturer, price, quantity_in_stock) VALUES 
('Paracetamol', 'Bayer', 5.50, 100), 
('Ibuprofen', 'Pfizer', 8.20, 150), 
('Aspirin', 'Novartis', 4.75, 200); 

 

INSERT INTO Prescriptions (id_medicine, doctor_name, patient_name, date_issued, employee_id) VALUES 
(1, 'Dr. Ivan Petrov', 'Georgi Dimitrov', '2025-03-10', 1), 
(2, 'Dr. Maria Nikolova', 'Anna Ivanova', '2025-03-12', 1), 
(3, 'Dr. Peter Stoyanov', 'Nikolay Georgiev', '2025-03-14', 2); 

 

INSERT INTO Orders (id_medicine, supplier_name, order_date, quantity_ordered, employee_id) VALUES 
(1, 'PharmaSupply Ltd.', '2025-03-01', 500, 3), 
(2, 'MedExpress', '2025-03-05', 300, 3), 
(3, 'GlobalPharm', '2025-03-08', 400, 2); 

