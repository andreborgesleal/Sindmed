use seguranca
go

update sessao set dt_desativacao = GETDATE()
update GrupoTransacao set situacao = 'A' where grupoId = 2 and transacaoId = 186

use sindmed

select email1, * from Associado where email1 is not null order by 1 desc
delete from seguranca..Usuario where login = 'zumerosergio@gmail.com'


use seguranca
select url,* from Transacao where sistemaId = 2 and transacaoId_pai = 84
select url,* from Transacao where sistemaId = 2 and transacaoId = 84
select * from Grupo

select * from LogAuditoria order by 1 desc