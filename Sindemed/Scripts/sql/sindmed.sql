use sindmed

truncate table AssociadoDocumento
select * from AssociadoDocumento

update AssociadoDocumento set fileId = 'e1756448-0c22-436c-adc2-835171cff317.pdf' where nomeArquivoOriginal = 'Segundo teste'