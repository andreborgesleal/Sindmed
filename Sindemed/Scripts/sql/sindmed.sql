use seguranca
go

update sessao set dt_desativacao = GETDATE()
update GrupoTransacao set situacao = 'A' where grupoId = 2 and transacaoId = 186

select url, * from Transacao where transacaoId_pai = 97
select url, * from Transacao where url like 'Especialidade%'

select * from Transacao where sistemaId = 2 and nomeCurto like '%Atua%'
