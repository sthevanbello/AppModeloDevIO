/****** Script for SelectTopNRows command from SSMS  ******/
select * from AspNetRoles

--insert into AspNetRoles(Id, [Name], NormalizedName)
--Values (1, 'Admin', 'ADMIN')

select * from AspNetUsers

select * from AspNetUserRoles

select * from AspNetUserClaims

--insert into AspNetUserRoles(UserId, RoleId)
--values ('00fd2e1e-249c-4751-8cfd-6da1c0794137', 1).

--insert into AspNetUserClaims(UserId, ClaimType, ClaimValue)
--values ('00fd2e1e-249c-4751-8cfd-6da1c0794137', 'Permissao', 'PodeExcluir, PodeEscrever, PodeLer')

--update AspNetUserClaims set ClaimType = 'PodeExcluir' where id = 1