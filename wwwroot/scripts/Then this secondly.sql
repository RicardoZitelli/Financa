USE Financa
GO

INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'2B125F00-D89B-4917-BBBD-FFA5A11CB83F', N'Admin', N'ADMIN', NULL)
GO

INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'77C22384-B472-428F-A48C-24EAF9EE5E3D', N'Comum', N'COMUM', NULL)
GO

INSERT INTO AspNetUsers 
VALUES ('d42f5fdb-04d9-4139-bb28-d69b71cf5131'	
		,'master@master.com'	
		,'MASTER@MASTER.COM'
		,'master@master.com'
		,'MASTER@MASTER.COM'
		,1	
		,'AQAAAAEAACcQAAAAEBLfGBkrsCRFbQlZTojdLXYaBTuRTck906sRzrMfDvyEuYIZHeURe42yCTcfc8hnhg=='
		,'EIDGBOJPEHR5RPLVMXJXGERGUXY3Y6GQ'	
		,'d5927cd0-d36e-4495-b138-b23cd16f5d4d'	
		,NULL	
		,0	
		,0	
		,NULL	
		,1	
		,0)


/*
	Usuário: master@master.com
	Senha: 1Q2w3e4r%
*/

GO

INSERT INTO AspNetUserRoles VALUES ('d42f5fdb-04d9-4139-bb28-d69b71cf5131','2B125F00-D89B-4917-BBBD-FFA5A11CB83F')
GO

/*In case of you have already created the user, use the script below*/
/*Caso você já tenha criado o usuário, utilize o script abaixo*/

/*
	UPDATE	AspNetUserRoles 
	SET		RoleId = '2B125F00-D89B-4917-BBBD-FFA5A11CB83F' 
	WHERE	UserId	= 'd42f5fdb-04d9-4139-bb28-d69b71cf5131' 
	GO
*/