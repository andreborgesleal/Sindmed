use seguranca
go
select * from Usuario where login = 'fjathias@globo.com'

update sessao set dt_desativacao = GETDATE()

select * from UsuarioGrupo where usuarioId = 31
select * From Grupo where empresaId = 1
select * from Transacao where sistemaId = 2 and url like '%List'
select * from Transacao where sistemaId = 2 and url like '%Associado/Create'
select * from UsuarioGrupo where usuarioId = 31
select * From GrupoTransacao where grupoId = 3 and transacaoId = 129
select * From GrupoTransacao where grupoId = 3 and transacaoId = 141
--delete from GrupoTransacao where grupoId = 3 and transacaoId = 129

begin tran
select b.nomeCurto, * From GrupoTransacao a join Transacao b on a.transacaoId = b.transacaoId where grupoId = 3 and sistemaId = 2
select * from grupo where empresaId = 1
declare @transacaoId int
select @transacaoId = transacaoId from Transacao where sistemaId = 2 and nomeCurto = 'Página Inicial'

insert into GrupoTransacao(grupoId, transacaoId, situacao) values (3, @transacaoId, 'A')

delete from GrupoTransacao where grupoId = 3 and transacaoId = 129


select b.nomeCurto, * From GrupoTransacao a join Transacao b on a.transacaoId = b.transacaoId where grupoId = 3 and sistemaId = 2 order by 1 desc

--rollback tran
commit

begin tran
declare @transacaoId int
declare @transacaoId_pai int
declare @grupoId int

select @transacaoId = max(transacaoId)+1 from Transacao

select @grupoId = grupoId from Grupo where sistemaId = 2 and empresaId = 1 and descricao = 'Administração'

select @transacaoId_pai = a.transacaoId from Transacao a join GrupoTransacao b on a.transacaoId = b.transacaoId
where b.grupoId = @grupoId and nomeCurto = 'Especialidades'

insert into Transacao(transacaoId, sistemaId, transacaoId_pai, nomeCurto, nome, descricao, referencia, exibir, posicao, url, glyph)
values(@transacaoId, 2, @transacaoId_pai, 'Pesquisar Especialidade 1', 'Pesquisa da primeira especialidade do médico', 'Formulário modal de pesquisa da especialidade do médico que é exibida no cadastro do associado - Especialidade 1', 'Formulário Modal', 'N', null, 'Home/LovEspecialidadeMedica1Modal', null)

insert into GrupoTransacao(grupoId, transacaoId, situacao) values (@grupoId, @transacaoId, 'A')

set @transacaoId = @transacaoId + 1

insert into Transacao(transacaoId, sistemaId, transacaoId_pai, nomeCurto, nome, descricao, referencia, exibir, posicao, url, glyph)
values(@transacaoId, 2, @transacaoId_pai, 'Pesquisar Especialidade 2', 'Pesquisa da segunda especialidade do médico', 'Formulário modal de pesquisa da especialidade do médico que é exibida no cadastro do associado - Especialidade 2', 'Formulário Modal', 'N', null, 'Home/LovEspecialidadeMedica2Modal', null)

insert into GrupoTransacao(grupoId, transacaoId, situacao) values (@grupoId, @transacaoId, 'A')

set @transacaoId = @transacaoId + 1

insert into Transacao(transacaoId, sistemaId, transacaoId_pai, nomeCurto, nome, descricao, referencia, exibir, posicao, url, glyph)
values(@transacaoId, 2, @transacaoId_pai, 'Pesquisar Especialidade 3', 'Pesquisa da terceira especialidade do médico', 'Formulário modal de pesquisa da especialidade do médico que é exibida no cadastro do associado - Especialidade 3', 'Formulário Modal', 'N', null, 'Home/LovEspecialidadeMedica3Modal', null)

insert into GrupoTransacao(grupoId, transacaoId, situacao) values (@grupoId, @transacaoId, 'A')

select distinct a.* from Transacao a join GrupoTransacao b on a.transacaoId = b.transacaoId
where transacaoId_pai = @transacaoId_pai

--rollback tran
commit

