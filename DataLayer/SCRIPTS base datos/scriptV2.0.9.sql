IF COL_LENGTH('dbo.tbParametrosEmpresa', 'validaCabys') IS NULL
BEGIN
    ALTER TABLE tbParametrosEmpresa add validaCabys bit;
END
go
update tbParametrosEmpresa 	set validaCabys=1 where validaCabys is null;
go