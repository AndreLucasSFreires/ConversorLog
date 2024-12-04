Para rodar este aplicativo faça o seguinte:
Navegue até a pasta: \Infraestutura
1. Verifique o arquivo appsettings.json, e mude a string de conexão, inserindo seu servidor SQL Server, no lugar de: 'ANDRELUCAS\\SQLEXPRESS'
2. Rode o comando para configurar/criar o banco de dados:
   2.1 : dotnet ef --startup-project ../ConversorLog/ database update


3. Aqui está um exemplo de chamada do método POST 'Transformar':
{ "logEntrada": "312|200|HIT|\"GET /robots.txt HTTP/1.1\"|100.2\n101|200|MISS|\"POST /myImages HTTP/1.1\"|319.4\n199|404|MISS|\"GET /not-found HTTP/1.1\"|142.9\n312|200|INVALIDATE|\"GET /robots.txt HTTP/1.1\"|245.1" }
