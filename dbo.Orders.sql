CREATE TABLE [dbo].[Orders] (
    [OrderID]   INT            IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (MAX) NOT NULL,
    [Line1]     NVARCHAR (MAX) NOT NULL,
    [Line2]     NVARCHAR (MAX) NULL,
    [Email]     NVARCHAR (MAX) NULL,
    [Telephone] NVARCHAR (MAX) NULL,
    [City]      NVARCHAR (MAX) NOT NULL,
    [State]     NVARCHAR (MAX) NOT NULL,
    [Zip]       NVARCHAR (MAX) NULL,
    [Country]   NVARCHAR (MAX) NOT NULL,
    [GiftWrap]  BIT            NOT NULL,
    [Shipped]   BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [Date] DATETIME NULL, 
    CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED ([OrderID] ASC)
);
