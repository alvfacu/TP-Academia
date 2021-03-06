USE [tp2_net]
GO
/****** Object:  ForeignKey [FK_alumnos_inscripciones_cursos]    Script Date: 02/22/2016 00:07:13 ******/
ALTER TABLE [dbo].[alumnos_inscripciones] DROP CONSTRAINT [FK_alumnos_inscripciones_cursos]
GO
/****** Object:  ForeignKey [FK_alumnos_inscripciones_personas]    Script Date: 02/22/2016 00:07:13 ******/
ALTER TABLE [dbo].[alumnos_inscripciones] DROP CONSTRAINT [FK_alumnos_inscripciones_personas]
GO
/****** Object:  ForeignKey [FK_comisiones_planes]    Script Date: 02/22/2016 00:07:13 ******/
ALTER TABLE [dbo].[comisiones] DROP CONSTRAINT [FK_comisiones_planes]
GO
/****** Object:  ForeignKey [FK_cursos_comisiones]    Script Date: 02/22/2016 00:07:13 ******/
ALTER TABLE [dbo].[cursos] DROP CONSTRAINT [FK_cursos_comisiones]
GO
/****** Object:  ForeignKey [FK_cursos_materias]    Script Date: 02/22/2016 00:07:13 ******/
ALTER TABLE [dbo].[cursos] DROP CONSTRAINT [FK_cursos_materias]
GO
/****** Object:  ForeignKey [FK_docentes_cursos_cursos]    Script Date: 02/22/2016 00:07:13 ******/
ALTER TABLE [dbo].[docentes_cursos] DROP CONSTRAINT [FK_docentes_cursos_cursos]
GO
/****** Object:  ForeignKey [FK_docentes_cursos_personas]    Script Date: 02/22/2016 00:07:13 ******/
ALTER TABLE [dbo].[docentes_cursos] DROP CONSTRAINT [FK_docentes_cursos_personas]
GO
/****** Object:  ForeignKey [FK_materias_planes]    Script Date: 02/22/2016 00:07:14 ******/
ALTER TABLE [dbo].[materias] DROP CONSTRAINT [FK_materias_planes]
GO
/****** Object:  ForeignKey [FK_modulos_usuarios_modulos]    Script Date: 02/22/2016 00:07:14 ******/
ALTER TABLE [dbo].[modulos_usuarios] DROP CONSTRAINT [FK_modulos_usuarios_modulos]
GO
/****** Object:  ForeignKey [FK_modulos_usuarios_usuarios]    Script Date: 02/22/2016 00:07:14 ******/
ALTER TABLE [dbo].[modulos_usuarios] DROP CONSTRAINT [FK_modulos_usuarios_usuarios]
GO
/****** Object:  ForeignKey [FK_personas_planes]    Script Date: 02/22/2016 00:07:14 ******/
ALTER TABLE [dbo].[personas] DROP CONSTRAINT [FK_personas_planes]
GO
/****** Object:  ForeignKey [FK_planes_especialidades]    Script Date: 02/22/2016 00:07:14 ******/
ALTER TABLE [dbo].[planes] DROP CONSTRAINT [FK_planes_especialidades]
GO
/****** Object:  ForeignKey [FK_usuarios_personas]    Script Date: 02/22/2016 00:07:14 ******/
ALTER TABLE [dbo].[usuarios] DROP CONSTRAINT [FK_usuarios_personas]
GO
/****** Object:  Table [dbo].[alumnos_inscripciones]    Script Date: 02/22/2016 00:07:13 ******/
ALTER TABLE [dbo].[alumnos_inscripciones] DROP CONSTRAINT [FK_alumnos_inscripciones_cursos]
GO
ALTER TABLE [dbo].[alumnos_inscripciones] DROP CONSTRAINT [FK_alumnos_inscripciones_personas]
GO
DROP TABLE [dbo].[alumnos_inscripciones]
GO
/****** Object:  Table [dbo].[modulos_usuarios]    Script Date: 02/22/2016 00:07:14 ******/
ALTER TABLE [dbo].[modulos_usuarios] DROP CONSTRAINT [FK_modulos_usuarios_modulos]
GO
ALTER TABLE [dbo].[modulos_usuarios] DROP CONSTRAINT [FK_modulos_usuarios_usuarios]
GO
DROP TABLE [dbo].[modulos_usuarios]
GO
/****** Object:  Table [dbo].[docentes_cursos]    Script Date: 02/22/2016 00:07:13 ******/
ALTER TABLE [dbo].[docentes_cursos] DROP CONSTRAINT [FK_docentes_cursos_cursos]
GO
ALTER TABLE [dbo].[docentes_cursos] DROP CONSTRAINT [FK_docentes_cursos_personas]
GO
DROP TABLE [dbo].[docentes_cursos]
GO
/****** Object:  Table [dbo].[usuarios]    Script Date: 02/22/2016 00:07:14 ******/
ALTER TABLE [dbo].[usuarios] DROP CONSTRAINT [FK_usuarios_personas]
GO
DROP TABLE [dbo].[usuarios]
GO
/****** Object:  Table [dbo].[cursos]    Script Date: 02/22/2016 00:07:13 ******/
ALTER TABLE [dbo].[cursos] DROP CONSTRAINT [FK_cursos_comisiones]
GO
ALTER TABLE [dbo].[cursos] DROP CONSTRAINT [FK_cursos_materias]
GO
DROP TABLE [dbo].[cursos]
GO
/****** Object:  Table [dbo].[comisiones]    Script Date: 02/22/2016 00:07:13 ******/
ALTER TABLE [dbo].[comisiones] DROP CONSTRAINT [FK_comisiones_planes]
GO
DROP TABLE [dbo].[comisiones]
GO
/****** Object:  Table [dbo].[materias]    Script Date: 02/22/2016 00:07:14 ******/
ALTER TABLE [dbo].[materias] DROP CONSTRAINT [FK_materias_planes]
GO
DROP TABLE [dbo].[materias]
GO
/****** Object:  Table [dbo].[personas]    Script Date: 02/22/2016 00:07:14 ******/
ALTER TABLE [dbo].[personas] DROP CONSTRAINT [FK_personas_planes]
GO
DROP TABLE [dbo].[personas]
GO
/****** Object:  Table [dbo].[planes]    Script Date: 02/22/2016 00:07:14 ******/
ALTER TABLE [dbo].[planes] DROP CONSTRAINT [FK_planes_especialidades]
GO
DROP TABLE [dbo].[planes]
GO
/****** Object:  Table [dbo].[modulos]    Script Date: 02/22/2016 00:07:14 ******/
DROP TABLE [dbo].[modulos]
GO
/****** Object:  Table [dbo].[especialidades]    Script Date: 02/22/2016 00:07:14 ******/
DROP TABLE [dbo].[especialidades]
GO
/****** Object:  User [net]    Script Date: 02/22/2016 00:07:14 ******/
DROP USER [net]
GO
/****** Object:  User [net]    Script Date: 02/22/2016 00:07:14 ******/
CREATE USER [net] FOR LOGIN [net] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[especialidades]    Script Date: 02/22/2016 00:07:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[especialidades](
	[id_especialidad] [int] IDENTITY(1,1) NOT NULL,
	[desc_especialidad] [varchar](50) NOT NULL,
 CONSTRAINT [PK_especialidades] PRIMARY KEY CLUSTERED 
(
	[id_especialidad] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[especialidades] ON
INSERT [dbo].[especialidades] ([id_especialidad], [desc_especialidad]) VALUES (1, N'Sistemas')
INSERT [dbo].[especialidades] ([id_especialidad], [desc_especialidad]) VALUES (2, N'Analista de Sistemas')
INSERT [dbo].[especialidades] ([id_especialidad], [desc_especialidad]) VALUES (3, N'Química')
INSERT [dbo].[especialidades] ([id_especialidad], [desc_especialidad]) VALUES (4, N'Mecanica')
INSERT [dbo].[especialidades] ([id_especialidad], [desc_especialidad]) VALUES (7, N'Eléctrica')
SET IDENTITY_INSERT [dbo].[especialidades] OFF
/****** Object:  Table [dbo].[modulos]    Script Date: 02/22/2016 00:07:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[modulos](
	[id_modulo] [int] IDENTITY(1,1) NOT NULL,
	[desc_modulo] [varchar](50) NULL,
	[ejecuta] [varchar](50) NULL,
 CONSTRAINT [PK_modulos] PRIMARY KEY CLUSTERED 
(
	[id_modulo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[modulos] ON
INSERT [dbo].[modulos] ([id_modulo], [desc_modulo], [ejecuta]) VALUES (1, N'Modulo Alumno', NULL)
INSERT [dbo].[modulos] ([id_modulo], [desc_modulo], [ejecuta]) VALUES (4, N'Modulo Administrador', NULL)
INSERT [dbo].[modulos] ([id_modulo], [desc_modulo], [ejecuta]) VALUES (6, N'Módulo Prueba', NULL)
SET IDENTITY_INSERT [dbo].[modulos] OFF
/****** Object:  Table [dbo].[planes]    Script Date: 02/22/2016 00:07:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[planes](
	[id_plan] [int] IDENTITY(1,1) NOT NULL,
	[desc_plan] [varchar](50) NOT NULL,
	[id_especialidad] [int] NOT NULL,
 CONSTRAINT [PK_planes] PRIMARY KEY CLUSTERED 
(
	[id_plan] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[planes] ON
INSERT [dbo].[planes] ([id_plan], [desc_plan], [id_especialidad]) VALUES (1, N'Plan 2008', 1)
INSERT [dbo].[planes] ([id_plan], [desc_plan], [id_especialidad]) VALUES (3, N'Plan 1995', 2)
INSERT [dbo].[planes] ([id_plan], [desc_plan], [id_especialidad]) VALUES (5, N'Plan 2013', 2)
INSERT [dbo].[planes] ([id_plan], [desc_plan], [id_especialidad]) VALUES (6, N'Test', 1)
INSERT [dbo].[planes] ([id_plan], [desc_plan], [id_especialidad]) VALUES (9, N'Plan 2014', 1)
INSERT [dbo].[planes] ([id_plan], [desc_plan], [id_especialidad]) VALUES (12, N'Tengo un plan', 3)
INSERT [dbo].[planes] ([id_plan], [desc_plan], [id_especialidad]) VALUES (13, N'Plan 2005', 7)
INSERT [dbo].[planes] ([id_plan], [desc_plan], [id_especialidad]) VALUES (14, N'Prueba', 3)
SET IDENTITY_INSERT [dbo].[planes] OFF
/****** Object:  Table [dbo].[personas]    Script Date: 02/22/2016 00:07:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[personas](
	[id_persona] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[apellido] [varchar](50) NOT NULL,
	[direccion] [varchar](50) NULL,
	[email] [varchar](50) NULL,
	[telefono] [varchar](50) NULL,
	[fecha_nac] [datetime] NOT NULL,
	[legajo] [int] NULL,
	[tipo_persona] [int] NOT NULL,
	[id_plan] [int] NOT NULL,
 CONSTRAINT [PK_personas] PRIMARY KEY CLUSTERED 
(
	[id_persona] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[personas] ON
INSERT [dbo].[personas] ([id_persona], [nombre], [apellido], [direccion], [email], [telefono], [fecha_nac], [legajo], [tipo_persona], [id_plan]) VALUES (2, N'Facundo', N'Alvarez', N'CA', N'alv.facundo@gmail.com', N'123456', CAST(0x0000860E00000000 AS DateTime), 3333, 2, 1)
INSERT [dbo].[personas] ([id_persona], [nombre], [apellido], [direccion], [email], [telefono], [fecha_nac], [legajo], [tipo_persona], [id_plan]) VALUES (4, N'Matias', N'Gentileti', N'MA', N'm@ge.com', N'123123', CAST(0x00009C8200000000 AS DateTime), 1111, 2, 5)
INSERT [dbo].[personas] ([id_persona], [nombre], [apellido], [direccion], [email], [telefono], [fecha_nac], [legajo], [tipo_persona], [id_plan]) VALUES (6, N'Alejandro', N'Tulipan', N'TU', N'ale@tu.com', N'666666', CAST(0x00006B2000000000 AS DateTime), 1, 1, 1)
INSERT [dbo].[personas] ([id_persona], [nombre], [apellido], [direccion], [email], [telefono], [fecha_nac], [legajo], [tipo_persona], [id_plan]) VALUES (7, N'Admin', N' ', N'JL', N'jorge@lopez.com', N'123456789', CAST(0x0000802B00000000 AS DateTime), 9999, 0, 3)
INSERT [dbo].[personas] ([id_persona], [nombre], [apellido], [direccion], [email], [telefono], [fecha_nac], [legajo], [tipo_persona], [id_plan]) VALUES (8, N'Martin', N'Guereta', N'LA', N'm@g.com', N'1111', CAST(0x000081C100000000 AS DateTime), 1111, 1, 5)
INSERT [dbo].[personas] ([id_persona], [nombre], [apellido], [direccion], [email], [telefono], [fecha_nac], [legajo], [tipo_persona], [id_plan]) VALUES (10, N'Juana', N'Lopez', N'Cerca', N'aaa@aaa.com', N'111111111111111', CAST(0x0000737D00000000 AS DateTime), 123456, 1, 1)
INSERT [dbo].[personas] ([id_persona], [nombre], [apellido], [direccion], [email], [telefono], [fecha_nac], [legajo], [tipo_persona], [id_plan]) VALUES (11, N'Susana', N'Oria', N'Lejos', N'susana@gaga.com', N'111111111111', CAST(0x00009E2B00000000 AS DateTime), 1121213131, 1, 1)
INSERT [dbo].[personas] ([id_persona], [nombre], [apellido], [direccion], [email], [telefono], [fecha_nac], [legajo], [tipo_persona], [id_plan]) VALUES (19, N'Guillermina', N'Vescovo', N'Rioja 4033', N'guilleves@gmail.com', N'66666666', CAST(0x0000841C00000000 AS DateTime), 40223, 2, 1)
INSERT [dbo].[personas] ([id_persona], [nombre], [apellido], [direccion], [email], [telefono], [fecha_nac], [legajo], [tipo_persona], [id_plan]) VALUES (20, N'Nicolas', N'Giordano', N'Zeballos 1233', N'nicocda@hotmail.com', N'11111111', CAST(0x0000851100000000 AS DateTime), 40236, 2, 1)
INSERT [dbo].[personas] ([id_persona], [nombre], [apellido], [direccion], [email], [telefono], [fecha_nac], [legajo], [tipo_persona], [id_plan]) VALUES (21, N'Zulema', N'Ricardi', N'Cordoba 1212', N'zule.ricar@gmail.com', N'156789098', CAST(0x00005FB700000000 AS DateTime), 40145, 2, 14)
INSERT [dbo].[personas] ([id_persona], [nombre], [apellido], [direccion], [email], [telefono], [fecha_nac], [legajo], [tipo_persona], [id_plan]) VALUES (23, N'Silvana', N'Lopez', N'SL', N'lopez@susana.com', N'1111111111', CAST(0x000055C400000000 AS DateTime), 8282828, 1, 14)
INSERT [dbo].[personas] ([id_persona], [nombre], [apellido], [direccion], [email], [telefono], [fecha_nac], [legajo], [tipo_persona], [id_plan]) VALUES (24, N'Juan', N'Flamini', N'AS', N'j.flaimini@outlook.com', N'1111111111111', CAST(0x00006D7600000000 AS DateTime), 990101, 1, 5)
INSERT [dbo].[personas] ([id_persona], [nombre], [apellido], [direccion], [email], [telefono], [fecha_nac], [legajo], [tipo_persona], [id_plan]) VALUES (25, N'Roberto', N'Carlos', N'RO', N'robert@gmail.com', N'341001919', CAST(0x0000778E00000000 AS DateTime), 99999, 1, 12)
INSERT [dbo].[personas] ([id_persona], [nombre], [apellido], [direccion], [email], [telefono], [fecha_nac], [legajo], [tipo_persona], [id_plan]) VALUES (26, N'Federico', N'Rojas', N'FE', N'fede@rojas.com', N'11111', CAST(0x000081C100000000 AS DateTime), 111311, 2, 12)
SET IDENTITY_INSERT [dbo].[personas] OFF
/****** Object:  Table [dbo].[materias]    Script Date: 02/22/2016 00:07:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[materias](
	[id_materia] [int] IDENTITY(1,1) NOT NULL,
	[desc_materia] [varchar](50) NOT NULL,
	[hs_semanales] [int] NOT NULL,
	[hs_totales] [int] NOT NULL,
	[id_plan] [int] NOT NULL,
 CONSTRAINT [PK_materias] PRIMARY KEY CLUSTERED 
(
	[id_materia] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[materias] ON
INSERT [dbo].[materias] ([id_materia], [desc_materia], [hs_semanales], [hs_totales], [id_plan]) VALUES (1, N'Lenguaje de Programación JAVA', 4, 42, 1)
INSERT [dbo].[materias] ([id_materia], [desc_materia], [hs_semanales], [hs_totales], [id_plan]) VALUES (6, N'HTML', 3, 20, 1)
INSERT [dbo].[materias] ([id_materia], [desc_materia], [hs_semanales], [hs_totales], [id_plan]) VALUES (10, N'Matematica Superior', 2, 40, 5)
INSERT [dbo].[materias] ([id_materia], [desc_materia], [hs_semanales], [hs_totales], [id_plan]) VALUES (11, N'Probabilidad Y Estadistica', 3, 60, 1)
INSERT [dbo].[materias] ([id_materia], [desc_materia], [hs_semanales], [hs_totales], [id_plan]) VALUES (14, N'Quimica Inorganica', 5, 40, 14)
INSERT [dbo].[materias] ([id_materia], [desc_materia], [hs_semanales], [hs_totales], [id_plan]) VALUES (15, N'.NET', 5, 60, 5)
INSERT [dbo].[materias] ([id_materia], [desc_materia], [hs_semanales], [hs_totales], [id_plan]) VALUES (16, N'Quimica Organica III', 1, 15, 12)
SET IDENTITY_INSERT [dbo].[materias] OFF
/****** Object:  Table [dbo].[comisiones]    Script Date: 02/22/2016 00:07:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[comisiones](
	[id_comision] [int] IDENTITY(1,1) NOT NULL,
	[desc_comision] [varchar](50) NOT NULL,
	[anio_especialidad] [int] NOT NULL,
	[id_plan] [int] NOT NULL,
 CONSTRAINT [PK_comisiones] PRIMARY KEY CLUSTERED 
(
	[id_comision] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[comisiones] ON
INSERT [dbo].[comisiones] ([id_comision], [desc_comision], [anio_especialidad], [id_plan]) VALUES (2, N'304', 2014, 12)
INSERT [dbo].[comisiones] ([id_comision], [desc_comision], [anio_especialidad], [id_plan]) VALUES (3, N'406', 2014, 1)
INSERT [dbo].[comisiones] ([id_comision], [desc_comision], [anio_especialidad], [id_plan]) VALUES (4, N'406', 2014, 1)
INSERT [dbo].[comisiones] ([id_comision], [desc_comision], [anio_especialidad], [id_plan]) VALUES (7, N'306', 2015, 1)
INSERT [dbo].[comisiones] ([id_comision], [desc_comision], [anio_especialidad], [id_plan]) VALUES (9, N'Hola', 2015, 3)
INSERT [dbo].[comisiones] ([id_comision], [desc_comision], [anio_especialidad], [id_plan]) VALUES (10, N'404', 2015, 5)
INSERT [dbo].[comisiones] ([id_comision], [desc_comision], [anio_especialidad], [id_plan]) VALUES (11, N'XXX', 2015, 14)
INSERT [dbo].[comisiones] ([id_comision], [desc_comision], [anio_especialidad], [id_plan]) VALUES (12, N'PRUEBA Quimica Inorganica I', 2016, 14)
INSERT [dbo].[comisiones] ([id_comision], [desc_comision], [anio_especialidad], [id_plan]) VALUES (13, N'5K01', 2016, 5)
INSERT [dbo].[comisiones] ([id_comision], [desc_comision], [anio_especialidad], [id_plan]) VALUES (14, N'3Q01', 2016, 12)
SET IDENTITY_INSERT [dbo].[comisiones] OFF
/****** Object:  Table [dbo].[cursos]    Script Date: 02/22/2016 00:07:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cursos](
	[id_curso] [int] IDENTITY(1,1) NOT NULL,
	[id_materia] [int] NOT NULL,
	[id_comision] [int] NOT NULL,
	[anio_calendario] [int] NOT NULL,
	[cupo] [int] NOT NULL,
	[desc_curso] [varchar](50) NULL,
 CONSTRAINT [PK_cursos] PRIMARY KEY CLUSTERED 
(
	[id_curso] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[cursos] ON
INSERT [dbo].[cursos] ([id_curso], [id_materia], [id_comision], [anio_calendario], [cupo], [desc_curso]) VALUES (1, 1, 2, 2016, 20, N'JAVA - 304')
INSERT [dbo].[cursos] ([id_curso], [id_materia], [id_comision], [anio_calendario], [cupo], [desc_curso]) VALUES (3, 6, 3, 2016, 30, N'HTML - 405')
INSERT [dbo].[cursos] ([id_curso], [id_materia], [id_comision], [anio_calendario], [cupo], [desc_curso]) VALUES (4, 11, 2, 2015, 20, N'HOLA')
INSERT [dbo].[cursos] ([id_curso], [id_materia], [id_comision], [anio_calendario], [cupo], [desc_curso]) VALUES (15, 1, 2, 2015, 20, N'Prueba')
INSERT [dbo].[cursos] ([id_curso], [id_materia], [id_comision], [anio_calendario], [cupo], [desc_curso]) VALUES (22, 14, 12, 22, 22, N'Sfassf')
INSERT [dbo].[cursos] ([id_curso], [id_materia], [id_comision], [anio_calendario], [cupo], [desc_curso]) VALUES (23, 15, 13, 2015, 15, N'.NET TARDE')
INSERT [dbo].[cursos] ([id_curso], [id_materia], [id_comision], [anio_calendario], [cupo], [desc_curso]) VALUES (24, 10, 10, 2016, 30, N'MAT SUP FLAMINI')
INSERT [dbo].[cursos] ([id_curso], [id_materia], [id_comision], [anio_calendario], [cupo], [desc_curso]) VALUES (25, 16, 2, 2016, 10, N'Quimica Inorganica III 3q01')
INSERT [dbo].[cursos] ([id_curso], [id_materia], [id_comision], [anio_calendario], [cupo], [desc_curso]) VALUES (26, 11, 3, 2015, 4, N'PyE Promocion Directa')
SET IDENTITY_INSERT [dbo].[cursos] OFF
/****** Object:  Table [dbo].[usuarios]    Script Date: 02/22/2016 00:07:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[usuarios](
	[id_usuario] [int] IDENTITY(1,1) NOT NULL,
	[nombre_usuario] [varchar](50) NOT NULL,
	[clave] [varchar](50) NOT NULL,
	[habilitado] [bit] NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[apellido] [varchar](50) NOT NULL,
	[email] [varchar](50) NULL,
	[cambia_clave] [bit] NULL,
	[id_persona] [int] NULL,
 CONSTRAINT [PK_usuarios] PRIMARY KEY CLUSTERED 
(
	[id_usuario] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[usuarios] ON
INSERT [dbo].[usuarios] ([id_usuario], [nombre_usuario], [clave], [habilitado], [nombre], [apellido], [email], [cambia_clave], [id_persona]) VALUES (11, N'alvfacu', N'facundofa', 1, N'Facundo', N'Alvarez', N'alvfacu@hotmail.com', NULL, 2)
INSERT [dbo].[usuarios] ([id_usuario], [nombre_usuario], [clave], [habilitado], [nombre], [apellido], [email], [cambia_clave], [id_persona]) VALUES (12, N'tincho', N'pokemon', 1, N'Martin', N'Guereta', N'guereta_martin@gmail.com', NULL, 8)
INSERT [dbo].[usuarios] ([id_usuario], [nombre_usuario], [clave], [habilitado], [nombre], [apellido], [email], [cambia_clave], [id_persona]) VALUES (47, N'admin', N'123', 1, N'Admin', N' ', N'admin@admin.com', 0, 7)
INSERT [dbo].[usuarios] ([id_usuario], [nombre_usuario], [clave], [habilitado], [nombre], [apellido], [email], [cambia_clave], [id_persona]) VALUES (49, N'guilleves', N'holacomova', 0, N'Guillermina', N'Vescovo', N'guille_vesco@hotmail.com', NULL, 19)
INSERT [dbo].[usuarios] ([id_usuario], [nombre_usuario], [clave], [habilitado], [nombre], [apellido], [email], [cambia_clave], [id_persona]) VALUES (62, N'sawyer', N'nicogiordaneta', 1, N'Nicolas', N'Giordano', N'nico@gmail.com', 0, 20)
INSERT [dbo].[usuarios] ([id_usuario], [nombre_usuario], [clave], [habilitado], [nombre], [apellido], [email], [cambia_clave], [id_persona]) VALUES (63, N'susana', N'gimenez', 1, N'Susana', N'Oria', N'susana@oria.com', NULL, 11)
INSERT [dbo].[usuarios] ([id_usuario], [nombre_usuario], [clave], [habilitado], [nombre], [apellido], [email], [cambia_clave], [id_persona]) VALUES (65, N'ale', N'tulipanale', 1, N'Alejandro', N'Tulipan', N'aletulipan@outlook.com', NULL, 6)
INSERT [dbo].[usuarios] ([id_usuario], [nombre_usuario], [clave], [habilitado], [nombre], [apellido], [email], [cambia_clave], [id_persona]) VALUES (67, N'matias', N'matiasas', 1, N'Matias', N'Gentileti', N'm@ge.com', NULL, 4)
INSERT [dbo].[usuarios] ([id_usuario], [nombre_usuario], [clave], [habilitado], [nombre], [apellido], [email], [cambia_clave], [id_persona]) VALUES (68, N'robert', N'carlosrobert', 1, N'Roberto', N'Carlos', N'robert@gmail.com', NULL, 25)
INSERT [dbo].[usuarios] ([id_usuario], [nombre_usuario], [clave], [habilitado], [nombre], [apellido], [email], [cambia_clave], [id_persona]) VALUES (69, N'rojas', N'rojasrojas', 1, N'Federico', N'Rojas', N'fede@rojas.com', NULL, 26)
INSERT [dbo].[usuarios] ([id_usuario], [nombre_usuario], [clave], [habilitado], [nombre], [apellido], [email], [cambia_clave], [id_persona]) VALUES (70, N'jflamini', N'superiorlove', 1, N'Juan', N'Flamini', N'j.flaimini@outlook.com', NULL, 24)
INSERT [dbo].[usuarios] ([id_usuario], [nombre_usuario], [clave], [habilitado], [nombre], [apellido], [email], [cambia_clave], [id_persona]) VALUES (71, N'juana', N'lopezjuana', 1, N'Juana', N'Lopez', N'aaa@aaa.com', NULL, 10)
SET IDENTITY_INSERT [dbo].[usuarios] OFF
/****** Object:  Table [dbo].[docentes_cursos]    Script Date: 02/22/2016 00:07:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[docentes_cursos](
	[id_dictado] [int] IDENTITY(1,1) NOT NULL,
	[id_curso] [int] NOT NULL,
	[id_docente] [int] NOT NULL,
	[cargo] [int] NOT NULL,
 CONSTRAINT [PK_docentes_cursos] PRIMARY KEY CLUSTERED 
(
	[id_dictado] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[docentes_cursos] ON
INSERT [dbo].[docentes_cursos] ([id_dictado], [id_curso], [id_docente], [cargo]) VALUES (2, 3, 6, 0)
INSERT [dbo].[docentes_cursos] ([id_dictado], [id_curso], [id_docente], [cargo]) VALUES (7, 4, 6, 1)
INSERT [dbo].[docentes_cursos] ([id_dictado], [id_curso], [id_docente], [cargo]) VALUES (25, 22, 23, 1)
INSERT [dbo].[docentes_cursos] ([id_dictado], [id_curso], [id_docente], [cargo]) VALUES (26, 23, 8, 0)
INSERT [dbo].[docentes_cursos] ([id_dictado], [id_curso], [id_docente], [cargo]) VALUES (28, 24, 24, 2)
INSERT [dbo].[docentes_cursos] ([id_dictado], [id_curso], [id_docente], [cargo]) VALUES (29, 25, 25, 0)
INSERT [dbo].[docentes_cursos] ([id_dictado], [id_curso], [id_docente], [cargo]) VALUES (30, 15, 10, 0)
INSERT [dbo].[docentes_cursos] ([id_dictado], [id_curso], [id_docente], [cargo]) VALUES (31, 26, 11, 1)
SET IDENTITY_INSERT [dbo].[docentes_cursos] OFF
/****** Object:  Table [dbo].[modulos_usuarios]    Script Date: 02/22/2016 00:07:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[modulos_usuarios](
	[id_modulo_usuario] [int] IDENTITY(1,1) NOT NULL,
	[id_modulo] [int] NOT NULL,
	[id_usuario] [int] NOT NULL,
	[alta] [bit] NULL,
	[baja] [bit] NULL,
	[modificacion] [bit] NULL,
	[consulta] [bit] NULL,
 CONSTRAINT [PK_modulos_usuarios] PRIMARY KEY CLUSTERED 
(
	[id_modulo_usuario] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[modulos_usuarios] ON
INSERT [dbo].[modulos_usuarios] ([id_modulo_usuario], [id_modulo], [id_usuario], [alta], [baja], [modificacion], [consulta]) VALUES (4, 1, 12, 0, 0, 1, 1)
INSERT [dbo].[modulos_usuarios] ([id_modulo_usuario], [id_modulo], [id_usuario], [alta], [baja], [modificacion], [consulta]) VALUES (8, 4, 47, 1, 1, 1, 1)
INSERT [dbo].[modulos_usuarios] ([id_modulo_usuario], [id_modulo], [id_usuario], [alta], [baja], [modificacion], [consulta]) VALUES (9, 1, 63, 1, 0, 1, 1)
SET IDENTITY_INSERT [dbo].[modulos_usuarios] OFF
/****** Object:  Table [dbo].[alumnos_inscripciones]    Script Date: 02/22/2016 00:07:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[alumnos_inscripciones](
	[id_inscripcion] [int] IDENTITY(1,1) NOT NULL,
	[id_alumno] [int] NOT NULL,
	[id_curso] [int] NOT NULL,
	[condicion] [varchar](50) NOT NULL,
	[nota] [int] NULL,
 CONSTRAINT [PK_alumnos_inscripciones] PRIMARY KEY CLUSTERED 
(
	[id_inscripcion] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[alumnos_inscripciones] ON
INSERT [dbo].[alumnos_inscripciones] ([id_inscripcion], [id_alumno], [id_curso], [condicion], [nota]) VALUES (23, 19, 4, N'PROMOVIDO', 10)
INSERT [dbo].[alumnos_inscripciones] ([id_inscripcion], [id_alumno], [id_curso], [condicion], [nota]) VALUES (48, 4, 24, N'REGULAR', 10)
INSERT [dbo].[alumnos_inscripciones] ([id_inscripcion], [id_alumno], [id_curso], [condicion], [nota]) VALUES (49, 4, 23, N'INSCRIPTO', 0)
INSERT [dbo].[alumnos_inscripciones] ([id_inscripcion], [id_alumno], [id_curso], [condicion], [nota]) VALUES (50, 2, 4, N'REGULAR', 7)
INSERT [dbo].[alumnos_inscripciones] ([id_inscripcion], [id_alumno], [id_curso], [condicion], [nota]) VALUES (52, 2, 26, N'PROMOVIDO', 10)
INSERT [dbo].[alumnos_inscripciones] ([id_inscripcion], [id_alumno], [id_curso], [condicion], [nota]) VALUES (53, 19, 26, N'PROMOVIDO', 9)
INSERT [dbo].[alumnos_inscripciones] ([id_inscripcion], [id_alumno], [id_curso], [condicion], [nota]) VALUES (54, 20, 26, N'PROMOVIDO', 9)
INSERT [dbo].[alumnos_inscripciones] ([id_inscripcion], [id_alumno], [id_curso], [condicion], [nota]) VALUES (63, 2, 1, N'INSCRIPTO', 0)
SET IDENTITY_INSERT [dbo].[alumnos_inscripciones] OFF
/****** Object:  ForeignKey [FK_alumnos_inscripciones_cursos]    Script Date: 02/22/2016 00:07:13 ******/
ALTER TABLE [dbo].[alumnos_inscripciones]  WITH CHECK ADD  CONSTRAINT [FK_alumnos_inscripciones_cursos] FOREIGN KEY([id_curso])
REFERENCES [dbo].[cursos] ([id_curso])
GO
ALTER TABLE [dbo].[alumnos_inscripciones] CHECK CONSTRAINT [FK_alumnos_inscripciones_cursos]
GO
/****** Object:  ForeignKey [FK_alumnos_inscripciones_personas]    Script Date: 02/22/2016 00:07:13 ******/
ALTER TABLE [dbo].[alumnos_inscripciones]  WITH CHECK ADD  CONSTRAINT [FK_alumnos_inscripciones_personas] FOREIGN KEY([id_alumno])
REFERENCES [dbo].[personas] ([id_persona])
GO
ALTER TABLE [dbo].[alumnos_inscripciones] CHECK CONSTRAINT [FK_alumnos_inscripciones_personas]
GO
/****** Object:  ForeignKey [FK_comisiones_planes]    Script Date: 02/22/2016 00:07:13 ******/
ALTER TABLE [dbo].[comisiones]  WITH CHECK ADD  CONSTRAINT [FK_comisiones_planes] FOREIGN KEY([id_plan])
REFERENCES [dbo].[planes] ([id_plan])
GO
ALTER TABLE [dbo].[comisiones] CHECK CONSTRAINT [FK_comisiones_planes]
GO
/****** Object:  ForeignKey [FK_cursos_comisiones]    Script Date: 02/22/2016 00:07:13 ******/
ALTER TABLE [dbo].[cursos]  WITH CHECK ADD  CONSTRAINT [FK_cursos_comisiones] FOREIGN KEY([id_comision])
REFERENCES [dbo].[comisiones] ([id_comision])
GO
ALTER TABLE [dbo].[cursos] CHECK CONSTRAINT [FK_cursos_comisiones]
GO
/****** Object:  ForeignKey [FK_cursos_materias]    Script Date: 02/22/2016 00:07:13 ******/
ALTER TABLE [dbo].[cursos]  WITH CHECK ADD  CONSTRAINT [FK_cursos_materias] FOREIGN KEY([id_materia])
REFERENCES [dbo].[materias] ([id_materia])
GO
ALTER TABLE [dbo].[cursos] CHECK CONSTRAINT [FK_cursos_materias]
GO
/****** Object:  ForeignKey [FK_docentes_cursos_cursos]    Script Date: 02/22/2016 00:07:13 ******/
ALTER TABLE [dbo].[docentes_cursos]  WITH CHECK ADD  CONSTRAINT [FK_docentes_cursos_cursos] FOREIGN KEY([id_curso])
REFERENCES [dbo].[cursos] ([id_curso])
GO
ALTER TABLE [dbo].[docentes_cursos] CHECK CONSTRAINT [FK_docentes_cursos_cursos]
GO
/****** Object:  ForeignKey [FK_docentes_cursos_personas]    Script Date: 02/22/2016 00:07:13 ******/
ALTER TABLE [dbo].[docentes_cursos]  WITH CHECK ADD  CONSTRAINT [FK_docentes_cursos_personas] FOREIGN KEY([id_docente])
REFERENCES [dbo].[personas] ([id_persona])
GO
ALTER TABLE [dbo].[docentes_cursos] CHECK CONSTRAINT [FK_docentes_cursos_personas]
GO
/****** Object:  ForeignKey [FK_materias_planes]    Script Date: 02/22/2016 00:07:14 ******/
ALTER TABLE [dbo].[materias]  WITH CHECK ADD  CONSTRAINT [FK_materias_planes] FOREIGN KEY([id_plan])
REFERENCES [dbo].[planes] ([id_plan])
GO
ALTER TABLE [dbo].[materias] CHECK CONSTRAINT [FK_materias_planes]
GO
/****** Object:  ForeignKey [FK_modulos_usuarios_modulos]    Script Date: 02/22/2016 00:07:14 ******/
ALTER TABLE [dbo].[modulos_usuarios]  WITH CHECK ADD  CONSTRAINT [FK_modulos_usuarios_modulos] FOREIGN KEY([id_modulo])
REFERENCES [dbo].[modulos] ([id_modulo])
GO
ALTER TABLE [dbo].[modulos_usuarios] CHECK CONSTRAINT [FK_modulos_usuarios_modulos]
GO
/****** Object:  ForeignKey [FK_modulos_usuarios_usuarios]    Script Date: 02/22/2016 00:07:14 ******/
ALTER TABLE [dbo].[modulos_usuarios]  WITH CHECK ADD  CONSTRAINT [FK_modulos_usuarios_usuarios] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[usuarios] ([id_usuario])
GO
ALTER TABLE [dbo].[modulos_usuarios] CHECK CONSTRAINT [FK_modulos_usuarios_usuarios]
GO
/****** Object:  ForeignKey [FK_personas_planes]    Script Date: 02/22/2016 00:07:14 ******/
ALTER TABLE [dbo].[personas]  WITH CHECK ADD  CONSTRAINT [FK_personas_planes] FOREIGN KEY([id_plan])
REFERENCES [dbo].[planes] ([id_plan])
GO
ALTER TABLE [dbo].[personas] CHECK CONSTRAINT [FK_personas_planes]
GO
/****** Object:  ForeignKey [FK_planes_especialidades]    Script Date: 02/22/2016 00:07:14 ******/
ALTER TABLE [dbo].[planes]  WITH CHECK ADD  CONSTRAINT [FK_planes_especialidades] FOREIGN KEY([id_especialidad])
REFERENCES [dbo].[especialidades] ([id_especialidad])
GO
ALTER TABLE [dbo].[planes] CHECK CONSTRAINT [FK_planes_especialidades]
GO
/****** Object:  ForeignKey [FK_usuarios_personas]    Script Date: 02/22/2016 00:07:14 ******/
ALTER TABLE [dbo].[usuarios]  WITH CHECK ADD  CONSTRAINT [FK_usuarios_personas] FOREIGN KEY([id_persona])
REFERENCES [dbo].[personas] ([id_persona])
GO
ALTER TABLE [dbo].[usuarios] CHECK CONSTRAINT [FK_usuarios_personas]
GO
