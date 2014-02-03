use seguranca

select * from Transacao where transacaoId_pai is null and sistemaId = 2
select * from Transacao where (transacaoId_pai = 85 or transacaoId = 85) and sistemaId = 2
select * from Transacao where (transacaoId_pai = 139 or transacaoId = 139) and sistemaId = 2
select * from Transacao where (transacaoId_pai = 143 or transacaoId = 143) and sistemaId = 2

select * from grupo where empresaId = 1 and sistemaId = 2

select * from Transacao where nomeCurto = 'Atendimento'
select * from Transacao where nomeCurto = 'Detalhar Chamado'
select * from Transacao where nomeCurto = 'Listar Chamados'
select * from Transacao where url = 'Associado/Edit'

select distinct referencia from Transacao

select * from GrupoTransacao where transacaoId = 143
select * from Grupo


use sindmed
select * from Associado where cpf is not null

select * from seguranca..usuario where login like '%aramis%'

delete from seguranca..usuario where login like '%aramis%'
update Associado set usuarioId = null where associadoId = 425

select * from seguranca..Grupo
select * from seguranca..usuariogrupo
select usuarioId From seguranca..Usuario where login = 'aramis@uol.com.br'

insert into seguranca..UsuarioGrupo values((select usuarioId From seguranca..Usuario where login = 'aramis@uol.com.br'), 3, 'A')

update seguranca..Usuario set situacao = 'A' where usuarioId = (select usuarioId From seguranca..Usuario where login = 'aramis@uol.com.br')

425-ARAMIS FRANCISCO MENDONCA DE MORAES
00051390230

select * from seguranca..Sessao where value1 is not null