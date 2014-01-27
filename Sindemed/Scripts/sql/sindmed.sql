use sindmed
-- 12694118291
select * from Chamado order by 1 desc

select email1, usuarioId, cpf, * from Associado where email1 is not null and cpf is null order by 1 desc
select email1, usuarioId, cpf, * from Associado where email1 is not null and cpf is not null order by 1 desc

select email1, usuarioId, cpf, * from Associado where email1 = '' and cpf is null order by 1 desc
select email1, usuarioId, cpf, * from Associado where email1 = '' and cpf is not null order by 1 desc


select email1, * from Associado where associadoId = 9
select * from Medico order by 1 desc

delete from seguranca..Usuario where login = 'zlimasilva@yahoo.com.br'
update associado set usuarioId = null where associadoId = 4149
delete from Chamado where associadoId = 4149

select * from AreaAtendimento

--insert into AreaAtendimento values('Secretaria', null, 1, null)

use seguranca
go

update sessao set dt_desativacao = GETDATE()
update GrupoTransacao set situacao = 'A' where grupoId = 2 and transacaoId = 186

select url,* from Transacao where sistemaId = 2 and transacaoId_pai = 84
select url,* from Transacao where sistemaId = 2 and transacaoId = 84
select * from Grupo

select * from LogAuditoria order by 1 desc
select * from Transacao where sistemaId = 2 and transacaoId_pai is null
select url, * from Transacao where url like '%Register%'

select * From Alerta order by 1 desc



update Alerta set url = '../Atendimento/Create?chamadoId=9&fluxo=2' where alertaId = 3

