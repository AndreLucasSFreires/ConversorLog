Para rodar este aplicativo faça o seguinte:

1. Verifique o arquivo appsettings.json, e mude a string de conexão, inserindo seu servidor SQL Server, no lugar de: 'ANDRELUCAS\\SQLEXPRESS'
2. Rode o comando para configurar/criar o banco de dados:
   2.1 : Navegue até a pasta: \Infraestutura
   2.2 :  dotnet ef --startup-project ../ConversorLog/ database update

4. Aqui está um exemplo de chamada do método POST 'Transformar' do Formato 'Minha CDN' para o formato 'Agora':
{ "logEntrada": "312|200|HIT|\\"GET /robots.txt HTTP/1.1\\"|100.2\n101|200|MISS|\\"POST /myImages HTTP/1.1\\"|319.4\n199|404|MISS|\\"GET /not-found HTTP/1.1\\"|142.9\n312|200|INVALIDATE|\\"GET /robots.txt HTTP/1.1\\"|245.1" }

5. Aqui está um exemplo de chamada do método POST 'Transformar' do formato 'Agora' para o formato 'Minha CDN':
{
  "logEntrada": "#Version: 1.0 #Date: 04/12/2024 14:13:22\n#Fields: provider http-method status-code uri-path time-taken response-size cache-status\n\\"MINHA CDN\\" GET 200 /robots.txt 100 312 HIT\n\\"MINHA CDN\\" POST 200 /myImages 319 101 MISS\n\\"MINHA CDN\\" GET 404 /not-found 143 199 MISS\n\\"MINHA CDN\\" GET 200 /robots.txt 245 312 REFRESH_HIT"
}
