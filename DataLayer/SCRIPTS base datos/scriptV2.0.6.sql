IF COL_LENGTH('dbo.tbParametrosEmpresa', 'cantComandas') IS NULL
BEGIN
    ALTER TABLE tbParametrosEmpresa add cantComandas int;	
END
go
IF COL_LENGTH('dbo.tbParametrosEmpresa', 'correoCierre') IS NULL
BEGIN
    ALTER TABLE tbParametrosEmpresa add correoCierre bit;	
END
go
IF COL_LENGTH('dbo.tbClientes', 'aplicaDescAuto') IS NULL
BEGIN
    ALTER TABLE tbClientes add aplicaDescAuto bit;	
END
GO
update tbClientes 	set aplicaDescAuto=0 where aplicaDescAuto is null;
GO
update tbParametrosEmpresa 	set cantComandas=1 where cantComandas is null;
GO
update tbParametrosEmpresa 	set correoCierre=0 where correoCierre is null;
GO
update tbPersona set barrio=1 where barrio=0
go
update tbPersona set distrito=1 where distrito=0
go
update tbPersona set canton=1 where canton=0
go
update tbPersona set provincia=1 where provincia=0
go
insert into tbBarrios values(6,2,1,1,'Esparza Centro')
go
insert into tbBarrios values(5,5,1,1,'Filadelfia Centro')
go
insert into tbBarrios values(4,7,1,1,'Belen Centro')
go