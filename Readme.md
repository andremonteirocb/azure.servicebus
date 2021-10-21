
Instale Amazon.Lambda.Tools Global Tools se ainda não estiver instalado.
```
    dotnet tool install -g Amazon.Lambda.Tools
```

Se já instalado, verifique se a nova versão está disponível.
```
    dotnet tool update -g Amazon.Lambda.Tools
```

Execute testes de unidade
```
    cd "Fundamentos.AWS.Lambda/test/Fundamentos.AWS.Lambda.Tests"
    dotnet test
```

Implantar função para AWS Lambda
```
    cd "Fundamentos.AWS.Lambda/src/Fundamentos.AWS.Lambda"
    dotnet lambda deploy-function
```
#   a n d r e m o n t e i r o c b - f u n d a m e n t o s . a z u r e . s e r v i c e b u s  
 