Passo 1:

Executar scripts de criação de banco de dados e tabelas presentes na pasta SQL em TaskControl.Infrastructure\SQL:

1 - CREATE_DB.txt
2 - CREATE_TABLES.txt


Passo 2:

docker run -d -p 8080:80 --name container TaskControl