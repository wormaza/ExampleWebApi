DECLARE @TABLE VARCHAR(100) = 'table'
DECLARE @COLUMN VARCHAR(100) = 'column'
DECLARE @DESCRIPCION VARCHAR(100) = 'MS_Description'
DECLARE @CurrentUser sysname
select @CurrentUser = schema_name()

CREATE TABLE Colores(
	CodigoColor varchar(3) NOT NULL,
	Nombre varchar(100) NOT NULL
)

ALTER TABLE Colores
ADD CONSTRAINT PK_Colores
PRIMARY KEY (CodigoColor)

ALTER TABLE Colores
ADD Activo INT NOT NULL 
CONSTRAINT D_Colores_Activo
DEFAULT (1)

execute sp_addextendedproperty @DESCRIPCION, 
   'Tabla que alamcena los colores a utilizar',
   'user', @CurrentUser, @TABLE, 'Colores'

CREATE TABLE Formas(
	CodigoForma varchar(3) NOT NULL,
	Nombre varchar(100) NOT NULL
)

ALTER TABLE Formas
ADD CONSTRAINT PK_Formas
PRIMARY KEY (CodigoForma)

ALTER TABLE Formas
ADD Activo INT NOT NULL 
CONSTRAINT D_Formas_Activo
DEFAULT (1)

execute sp_addextendedproperty @DESCRIPCION, 
   'Tabla que alamcena los tipos de formas',
   'user', @CurrentUser, @TABLE, 'Formas'

CREATE TABLE Portes(
	CodigoPorte varchar(3) NOT NULL,
	Nombre varchar(100) NOT NULL
)

ALTER TABLE Portes
ADD CONSTRAINT PK_Portes
PRIMARY KEY (CodigoPorte)

ALTER TABLE Portes
ADD Activo INT NOT NULL 
CONSTRAINT D_Portes_Activo
DEFAULT (1)

execute sp_addextendedproperty @DESCRIPCION, 
   'Tabla que alamcena los tipos de tamaños',
   'user', @CurrentUser, @TABLE, 'Portes'


CREATE TABLE Bordes(
	CodigoBorde int NOT NULL,
	Nombre varchar(100) NOT NULL
)

ALTER TABLE Bordes
ADD CONSTRAINT PK_Bordes
PRIMARY KEY (CodigoBorde)

ALTER TABLE Bordes
ADD Activo INT NOT NULL 
CONSTRAINT D_Bordes_Activo
DEFAULT (1)

execute sp_addextendedproperty @DESCRIPCION, 
   'Tabla que alamcena los tipos de bordes',
   'user', @CurrentUser, @TABLE, 'Bordes'