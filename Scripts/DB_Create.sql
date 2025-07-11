-- CREACION DE LA BASE DE DATOS --
CREATE DATABASE InventorySalesDB;
USE InventorySalesDB;
-- Tabla productos --
CREATE TABLE Productos (
    id_producto INT IDENTITY(1,1) PRIMARY KEY,
    nombre NVARCHAR(100) NOT NULL,
    categoria NVARCHAR(50),
    descrip NVARCHAR(255),
    precio_unit DECIMAL(10,2),
    exist INT
);
-- Tabla CLientes --
CREATE TABLE Clientes (
    id_cliente INT IDENTITY(1,1) PRIMARY KEY,
    nombre NVARCHAR(100) NOT NULL,
    telefono NVARCHAR(20),
    correo NVARCHAR(100),
    direcc NVARCHAR(255)
);
-- Tabla Ventas --
CREATE TABLE Ventas (
    id_venta INT IDENTITY(1,1) PRIMARY KEY,
    id_cliente INT FOREIGN KEY REFERENCES Clientes(id_cliente),
    fecha DATETIME DEFAULT GETDATE(),
    total DECIMAL(10,2)
);
-- Tabla DetalleVentas
CREATE TABLE DetalleVentas (
    id_detalle INT IDENTITY(1,1) PRIMARY KEY,
    id_venta INT FOREIGN KEY REFERENCES Ventas(id_venta),
    id_producto INT FOREIGN KEY REFERENCES Productos(id_producto),
    cantidad INT,
    precio_unit DECIMAL(10,2)
);
