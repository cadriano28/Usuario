CREATE TABLE [dbo].[Table]
(
	[pessoaId] INT NOT NULL PRIMARY KEY, 
    [pessoaNome] NVARCHAR(MAX) NULL, 
    [pessoaTel] NVARCHAR(MAX) NULL, 
    [pessoaLogin] NVARCHAR(MAX) NULL, 
    [pessoaSenha] NVARCHAR(MAX) NULL, 
    [pessoaNivel] NVARCHAR(MAX) NULL, 
    [pessoaStatus] NVARCHAR(MAX) NULL
)
