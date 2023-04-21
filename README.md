# cache-flow
Solução de lançamento de fluxo de caixa, utilizando API e Works.

# Arquitetura

![Desenho](https://user-images.githubusercontent.com/16910009/233546665-79834aad-1ffb-4cf0-9558-ffd8e84cb815.png)

# Componentes

### CashFlow.RC.API:
API/Borda app utilizada para o registro de fluxo de caixa.

### CashFlow.RC.Work: 
Work service responsável pela persistência no banco de dados e publicação de evento de fluxo de caixa.

### CashFlow.CD.Work: 
Work service responsável pelo processamento dos eventos gerados pela app CashFlow.RC.Work e a sumarização do fluxo de caixa agrupado por data.

### CashFlow.CD.Api: 
Work service responsável por consultar os fluxos de caixa agrupados por data e retornar os dados consolidados via api.

### NATS.io: 
Broker utilizado para comunicação entre os serviços.
Referência https://nats.io/

### MongoDB: 
Banco NoSQL utilizado como repositório na solução.
Referência https://www.mongodb.com/
