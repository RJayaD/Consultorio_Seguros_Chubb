USE [DbConsultorio_Seguros]
GO
/****** Object:  Table [dbo].[Asegurado]    Script Date: 22/10/2023 11:00:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Asegurado](
	[AseguradoId] [int] IDENTITY(1,1) NOT NULL,
	[cedula] [varchar](10) NOT NULL,
	[nombre_cliente] [varchar](55) NOT NULL,
	[fecha_creacion] [date] NOT NULL,
	[telefono] [varchar](10) NOT NULL,
	[edad] [int] NULL,
	[estado] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[AseguradoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Seguro]    Script Date: 22/10/2023 11:00:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Seguro](
	[SeguroId] [int] IDENTITY(1,1) NOT NULL,
	[nombre_seguro] [varchar](50) NOT NULL,
	[codigo] [varchar](10) NOT NULL,
	[fecha_creacion] [date] NOT NULL,
	[suma] [decimal](10, 2) NOT NULL,
	[prima] [decimal](10, 2) NOT NULL,
	[estado] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[SeguroId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SeguroCliente]    Script Date: 22/10/2023 11:00:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SeguroCliente](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[SeguroId] [int] NOT NULL,
	[AseguradoId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[SeguroCliente]  WITH CHECK ADD  CONSTRAINT [FK_SeguroCliente_Asegurado] FOREIGN KEY([AseguradoId])
REFERENCES [dbo].[Asegurado] ([AseguradoId])
GO
ALTER TABLE [dbo].[SeguroCliente] CHECK CONSTRAINT [FK_SeguroCliente_Asegurado]
GO
ALTER TABLE [dbo].[SeguroCliente]  WITH CHECK ADD  CONSTRAINT [FK_SeguroCliente_Seguro] FOREIGN KEY([SeguroId])
REFERENCES [dbo].[Seguro] ([SeguroId])
GO
ALTER TABLE [dbo].[SeguroCliente] CHECK CONSTRAINT [FK_SeguroCliente_Seguro]
GO
/****** Object:  StoredProcedure [dbo].[spCargaComboAsegurado]    Script Date: 22/10/2023 11:00:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spCargaComboAsegurado]
AS
BEGIN
    SELECT AseguradoId,nombre_cliente From Asegurado 
	WHERE estado=1;
END
GO
/****** Object:  StoredProcedure [dbo].[spCargaComboSeguro]    Script Date: 22/10/2023 11:00:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spCargaComboSeguro]
AS
BEGIN
    SELECT SeguroId,nombre_seguro From Seguro 
	WHERE estado=1;
END
GO
/****** Object:  StoredProcedure [dbo].[spEditarAsegurado]    Script Date: 22/10/2023 11:00:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spEditarAsegurado]
    @AseguradoId INT,
    @cedula VARCHAR(10),
    @nombre_cliente VARCHAR(55),
    @telefono VARCHAR(10),
    @edad INT
AS
BEGIN
 IF NOT EXISTS (SELECT 1 FROM Asegurado WHERE cedula = @cedula AND estado=1)
    BEGIN
    UPDATE Asegurado
    SET cedula = @cedula,
        nombre_cliente = @nombre_cliente,
        telefono = @telefono,
        edad = @edad
    WHERE AseguradoId = @AseguradoId;
	END
END
GO
/****** Object:  StoredProcedure [dbo].[spEditarSeguro]    Script Date: 22/10/2023 11:00:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spEditarSeguro]
    @SeguroId INT,
    @nombre_seguro VARCHAR(50),
    @codigo VARCHAR(10),
    @suma DECIMAL(10,2),
    @prima DECIMAL(10,2)
AS
BEGIN
IF NOT EXISTS (SELECT 1 FROM Seguro WHERE codigo = @codigo and estado=1)
    BEGIN
    UPDATE Seguro
    SET nombre_seguro = @nombre_seguro,
        codigo = @codigo,
        suma = @suma,
        prima = @prima
    WHERE SeguroId = @SeguroId;
	END
END
GO
/****** Object:  StoredProcedure [dbo].[spEditarSeguroCliente]    Script Date: 22/10/2023 11:00:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spEditarSeguroCliente]
    @id INT,
    @SeguroId Int,
    @AseguradoId int
AS
BEGIN
    UPDATE SeguroCliente
    SET SeguroId = @SeguroId,
        AseguradoId = @AseguradoId
    WHERE id = @id;
END
GO
/****** Object:  StoredProcedure [dbo].[spEliminarAsegurado]    Script Date: 22/10/2023 11:00:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spEliminarAsegurado]
    @AseguradoId INT
AS
BEGIN
    UPDATE Asegurado
	SET estado = 0
    WHERE AseguradoId = @AseguradoId;
END
GO
/****** Object:  StoredProcedure [dbo].[spEliminarSeguro]    Script Date: 22/10/2023 11:00:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spEliminarSeguro]
	@SeguroId INT
AS
BEGIN
    UPDATE Seguro
	SET estado = 0
    WHERE SeguroId = @SeguroId;
END
GO
/****** Object:  StoredProcedure [dbo].[spEliminarSeguroCliente]    Script Date: 22/10/2023 11:00:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spEliminarSeguroCliente]
	@id INT
AS
BEGIN
    DELETE SeguroCliente
    WHERE id = @id;
END
GO
/****** Object:  StoredProcedure [dbo].[spGuardarAsegurado]    Script Date: 22/10/2023 11:00:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spGuardarAsegurado]
    @cedula VARCHAR(10),
    @nombre_cliente VARCHAR(55),
    @fecha_creacion DATE,
    @telefono VARCHAR(10),
    @edad INT
AS
BEGIN
    -- Comprobar si la cédula ya existe en la tabla
    IF NOT EXISTS (SELECT 1 FROM Asegurado WHERE cedula = @cedula and estado=1)
    BEGIN
        -- La cédula no existe, se puede realizar la inserción
        INSERT INTO Asegurado (cedula, nombre_cliente, fecha_creacion, telefono, edad, estado)
        VALUES (@cedula, @nombre_cliente, @fecha_creacion, @telefono, @edad, 1);
        -- Puedes agregar aquí código adicional si es necesario después de la inserción
    END
END
GO
/****** Object:  StoredProcedure [dbo].[spGuardarSeguro]    Script Date: 22/10/2023 11:00:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spGuardarSeguro]
    @nombre_seguro VARCHAR(50),
    @codigo VARCHAR(10),
    @fecha_creacion DATE,
    @suma DECIMAL(10,2),
    @prima DECIMAL(10,2)
AS
BEGIN
	IF NOT EXISTS (SELECT 1 FROM Seguro WHERE codigo = @codigo and estado=1)
    BEGIN
    INSERT INTO Seguro (nombre_seguro, codigo, fecha_creacion, suma, prima, estado)
    VALUES (@nombre_seguro, @codigo, @fecha_creacion, @suma, @prima, 1);
 END
END
GO
/****** Object:  StoredProcedure [dbo].[spGuardarSeguroCliente]    Script Date: 22/10/2023 11:00:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spGuardarSeguroCliente]
    @SeguroId Int,
    @AseguradoId int
AS
BEGIN
	IF NOT EXISTS (SELECT 1 FROM SeguroCliente WHERE SeguroId = @SeguroId and AseguradoId=@AseguradoId)
    BEGIN
    INSERT INTO SeguroCliente(SeguroId, AseguradoId)
    VALUES (@SeguroId, @AseguradoId);
	END
END
GO
/****** Object:  StoredProcedure [dbo].[spVerAsegurado]    Script Date: 22/10/2023 11:00:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spVerAsegurado]
AS
BEGIN
    SELECT * FROM Asegurado
	WHERE estado = 1
END
GO
/****** Object:  StoredProcedure [dbo].[spVerAseguradoId]    Script Date: 22/10/2023 11:00:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spVerAseguradoId]
    @AseguradoId INT
AS
BEGIN
    SELECT * FROM Asegurado
    WHERE AseguradoId = @AseguradoId;
END
GO
/****** Object:  StoredProcedure [dbo].[spVerAseguradosxCodigo]    Script Date: 22/10/2023 11:00:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spVerAseguradosxCodigo]
    @codigo VARCHAR(10)
AS
BEGIN
select A.nombre_cliente, S.nombre_seguro,S.codigo,A.telefono,A.edad
from Seguro S
inner join SeguroCliente SC
on s.SeguroId=SC.SeguroId
inner join Asegurado A
on A.AseguradoId=SC.AseguradoId
where S.codigo =@codigo
END
GO
/****** Object:  StoredProcedure [dbo].[spVerAseguradoxCondicion]    Script Date: 22/10/2023 11:00:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spVerAseguradoxCondicion]
@condicion VARCHAR(10)
AS
BEGIN
    SELECT * FROM Asegurado
	WHERE estado = 1 and (cedula like '%'+@condicion+'%' or nombre_cliente like '%'+@condicion+'%')
END
GO
/****** Object:  StoredProcedure [dbo].[spVerSeguro]    Script Date: 22/10/2023 11:00:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spVerSeguro]
AS
BEGIN
    SELECT * FROM Seguro
	WHERE estado = 1
END
GO
/****** Object:  StoredProcedure [dbo].[spVerSeguroId]    Script Date: 22/10/2023 11:00:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spVerSeguroId]
    @SeguroId INT
AS
BEGIN
    SELECT SeguroId,nombre_seguro,codigo,CONVERT(DATE, fecha_creacion, 23) AS fecha_creacion,suma,prima,estado FROM Seguro
    WHERE SeguroId = @SeguroId;
END
GO
/****** Object:  StoredProcedure [dbo].[spVerSegurosClientes]    Script Date: 22/10/2023 11:00:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spVerSegurosClientes]
AS
BEGIN
    select SC.id, A.AseguradoId,A.nombre_cliente,A.cedula, S.SeguroId,S.nombre_seguro,S.codigo
from Asegurado A
inner join SeguroCliente SC
on a.AseguradoId=SC.AseguradoId
inner join Seguro S
on S.SeguroId=SC.SeguroId
END
GO
/****** Object:  StoredProcedure [dbo].[spVerSegurosClientesCondicional]    Script Date: 22/10/2023 11:00:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spVerSegurosClientesCondicional]
    @condicion VARCHAR(10)
AS
BEGIN
    select A.nombre_cliente,S.nombre_seguro,S.codigo,A.telefono,A.edad
from Asegurado as A
inner join SeguroCliente SC
on a.AseguradoId=SC.AseguradoId
inner join Seguro S
on S.SeguroId=SC.SeguroId
where A.cedula like '%'+@condicion+'%' or A.nombre_cliente like '%'+@condicion+'%' or S.codigo like '%'+@condicion+'%'
and (A.estado=1 and S.estado=1)
END 
GO
/****** Object:  StoredProcedure [dbo].[spVerSegurosxCedula]    Script Date: 22/10/2023 11:00:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spVerSegurosxCedula]
    @cedula VARCHAR(10)
AS
BEGIN
    select A.nombre_cliente, S.nombre_seguro,S.codigo,A.telefono,A.edad
from Asegurado A
inner join SeguroCliente SC
on a.AseguradoId=SC.AseguradoId
inner join Seguro S
on S.SeguroId=SC.SeguroId
where A.cedula =@cedula
END
GO
/****** Object:  StoredProcedure [dbo].[spVerSeguroxCondicion]    Script Date: 22/10/2023 11:00:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spVerSeguroxCondicion]
@condicion VARCHAR(10)
AS
BEGIN
    SELECT * FROM Seguro
	WHERE estado = 1 and (codigo like '%'+@condicion+'%' or nombre_seguro like '%'+@condicion+'%')
END

GO
