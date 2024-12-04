Para rodar este aplicativo faça o seguinte:
Navegue até a pasta: \Infraestutura
1. Verifique o arquivo appsettings.json, e mude a string de conexão, inserindo seu servidor SQL Server, no lugar de: 'ANDRELUCAS\\SQLEXPRESS'
2. Rode o comando para configurar/criar o banco de dados:
   2.1 : dotnet ef --startup-project ../ConversorLog/ database update
