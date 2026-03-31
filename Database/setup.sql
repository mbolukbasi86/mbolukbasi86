-- BootStrapECommerceDB Setup Script
-- Bu script, BootStrapECommerceDB veritabanını ve gerekli tabloları oluşturur.
-- SQL Server Management Studio veya Azure Data Studio ile çalıştırın.

-- Veritabanını oluştur
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'BootStrapECommerceDB')
BEGIN
    CREATE DATABASE BootStrapECommerceDB;
END
GO

USE BootStrapECommerceDB;
GO

-- Products tablosunu oluştur
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Products' AND xtype='U')
BEGIN
    CREATE TABLE [dbo].[Products] (
        [Id]          INT              IDENTITY (1, 1) NOT NULL,
        [Name]        NVARCHAR (200)   NOT NULL,
        [Description] NVARCHAR (1000)  NULL,
        [Price]       DECIMAL (18, 2)  NOT NULL,
        [OldPrice]    DECIMAL (18, 2)  NULL,
        -- ImagePath: wwwroot klasörüne göre göreli yol, örnek: /images/products/laptop.jpg
        [ImagePath]   NVARCHAR (500)   NULL,
        [Category]    NVARCHAR (100)   NULL,
        [Stock]       INT              NOT NULL DEFAULT 0,
        [IsActive]    BIT              NOT NULL DEFAULT 1,
        [CreatedAt]   DATETIME2 (7)    NOT NULL DEFAULT GETDATE(),
        CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED ([Id] ASC)
    );
END
GO

-- Örnek ürün verilerini ekle
-- NOT: ImagePath değerleri, wwwroot/images/products/ klasöründeki görsel dosyalarına işaret etmeli.
-- Görseller yoksa uygulama otomatik olarak /images/products/no-image.png gösterir.
IF NOT EXISTS (SELECT 1 FROM [dbo].[Products])
BEGIN
    INSERT INTO [dbo].[Products] ([Name], [Description], [Price], [OldPrice], [ImagePath], [Category], [Stock], [IsActive], [CreatedAt])
    VALUES
        (N'Laptop Pro X1',       N'Yüksek performanslı dizüstü bilgisayar, 16GB RAM, 512GB SSD', 24999.99, 29999.99, N'/images/products/laptop.jpg',     N'Elektronik', 10, 1, '2026-01-01'),
        (N'Kablosuz Kulaklık',   N'Gürültü engelleme özellikli bluetooth kulaklık',               1299.99,  1799.99,  N'/images/products/headphones.jpg', N'Elektronik', 25, 1, '2026-01-01'),
        (N'Akıllı Saat',         N'Sağlık takip özellikli akıllı saat, su geçirmez',               3499.99,  NULL,     N'/images/products/smartwatch.jpg', N'Elektronik', 15, 1, '2026-01-01'),
        (N'Mekanik Klavye',      N'RGB aydınlatmalı mekanik oyuncu klavyesi',                       899.99,   1199.99,  N'/images/products/keyboard.jpg',   N'Bilgisayar', 20, 1, '2026-01-01'),
        (N'Oyuncu Mouse',        N'16000 DPI hassasiyetli oyuncu faresi',                            449.99,   NULL,     N'/images/products/mouse.jpg',      N'Bilgisayar', 30, 1, '2026-01-01'),
        (N'4K Monitör',          N'27 inç 4K UHD IPS panel oyuncu monitörü',                        8999.99,  10999.99, N'/images/products/monitor.jpg',    N'Bilgisayar',  8, 1, '2026-01-01');
END
GO

PRINT 'BootStrapECommerceDB kurulumu tamamlandı!';
GO
