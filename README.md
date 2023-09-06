# Projeto Carrefour

Este é um guia passo a passo para configurar e executar o projeto Carrefour.

## Pré-requisitos

- Certifique-se de que o SQL Server está instalado em sua máquina.

## Configuração

1. Abra o CMD e navegue até a pasta onde deseja clonar o repositório.
2. Execute o comando: `git clone https://github.com/felipefernandescruz/Carrefour.git`
3. Abra o arquivo `Carrefour.sln`.
4. Modifique a `ConnectionString` dentro do arquivo `appsettings.json` para a conexão do seu banco de dados:

```json
"ConnectionStrings": {
    "DefaultSQLConnection": "Server=*;Database=Carrefour;TrustServerCertificate=True;Trusted_Connection=True;MultipleActiveResultSets=true"
}
```
5. Abra o PackageManagerConsole, lembre-se de definir o projeto padrão na camada do repositório e execute o comando: Update-Database.
   
## Execução

Após a configuração, execute o projeto no Visual Studio.

## APIs Desenvolvidas

As seguintes APIs foram desenvolvidas:

- `CreditOrder`: Adiciona dinheiro ao caixa. Ambos possuem o mesmo corpo de solicitação.
- `DebtOrder`: Retira dinheiro do caixa. Ambos possuem o mesmo corpo de solicitação.

A API `BalanceDate` retorna o balanço do dia buscado preenchendo o `balanceDate`. Caso não preencha, irá retornar o último balanço. Ela retorna o início do caixa, total de entradas de crédito e total de entradas de débito.
