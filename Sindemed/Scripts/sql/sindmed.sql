﻿use sindmed

truncate table AssociadoDocumento
select * from AssociadoDocumento



select * from associadodocumento

insert into AssociadoDocumento values(1895, 'e1756448-0c22-436c-adc2-835171cff315.pdf', 'Seguro Celular', GETDATE())


insert into AssociadoDocumento values(1895, 'e1756448-0c22-436c-adc2-835171cff314.png', 'Cadastro de Usuário', GETDATE())
update AssociadoDocumento set fileId = 'e1756448-0c22-436c-adc2-835171cff317.png' where nomeArquivoOriginal = 'Foto da empresa'

update AssociadoDocumento set nomeArquivoOriginal = 'Chave de Segurança' where fileId = '7f57e4f3-4420-4147-9fc8-6795262eb636.txt'
