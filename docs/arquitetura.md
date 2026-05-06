# Arquitetura do Sistema

## 1. Visao Geral

O Sistema de Gestao de Ordens de Servico foi desenvolvido como uma aplicacao desktop em C# utilizando Windows Forms, .NET Framework e PostgreSQL.

A arquitetura adotada segue uma separacao em camadas, com responsabilidades bem definidas para interface grafica, regras de negocio, acesso a dados, infraestrutura, entidades e relatorios.

Essa organizacao facilita a manutencao do codigo, reduz o acoplamento entre partes do sistema e permite que as regras de negocio fiquem concentradas fora da interface grafica.

## 2. Estrutura Geral da Solucao

A solucao esta organizada nos seguintes projetos:

| Projeto | Responsabilidade |
|---|---|
| `OrdemServico.Entities` | Define as entidades de dominio e enumeracoes. |
| `OrdemServico.Infra` | Centraliza recursos de infraestrutura, como conexao com banco, sessao e logs. |
| `OrdemServico.Repositories` | Contem o acesso a dados e consultas SQL. |
| `OrdemServico.Services` | Concentra regras de negocio, validacoes e controle transacional. |
| `OrdemServico.Reports` | Contem os relatorios RDLC, filtros, DTOs e servico de relatorio. |
| `OrdemServico.UI` | Contem a interface grafica Windows Forms. |

## 3. Diagrama de Dependencias

Fluxo principal de dependencia entre as camadas:

```text
OrdemServico.UI
    |
    v
OrdemServico.Services
    |
    v
OrdemServico.Repositories
    |
    v
PostgreSQL
```

Dependencias auxiliares:

```text
OrdemServico.UI ---------> OrdemServico.Reports
OrdemServico.UI ---------> OrdemServico.Infra
OrdemServico.Services ---> OrdemServico.Infra
OrdemServico.Services ---> OrdemServico.Entities
OrdemServico.Repositories -> OrdemServico.Entities
OrdemServico.Repositories -> OrdemServico.Infra
OrdemServico.Reports ----> OrdemServico.Infra
OrdemServico.Reports ----> OrdemServico.Entities
```

Regra principal:

- A interface grafica chama os services.
- Os services aplicam regras de negocio e chamam os repositories.
- Os repositories acessam o banco de dados.
- As entidades representam os dados do dominio.
- A infraestrutura oferece recursos compartilhados.

## 4. Camada de Entidades

Projeto:

```text
src/OrdemServico.Entities
```

Essa camada contem as classes que representam os principais conceitos do sistema.

Entidades principais:

| Entidade | Descricao |
|---|---|
| `Cliente` | Representa um cliente pessoa fisica ou juridica. |
| `Servico` | Representa um servico oferecido pela empresa. |
| `OrdemServico` | Representa uma ordem de servico aberta para um cliente. |
| `ItemOrdemServico` | Representa um item vinculado a uma ordem de servico. |
| `HistoricoStatus` | Representa uma mudanca de status da ordem. |
| `RegistroAuditoria` | Representa um registro de auditoria. |

Enumeracoes:

| Enum | Descricao |
|---|---|
| `TipoCliente` | Define se o cliente e pessoa fisica ou juridica. |
| `StatusOS` | Define os status possiveis da ordem de servico. |

Caracteristicas dessa camada:

- Nao deve depender da interface grafica.
- Nao deve acessar banco de dados diretamente.
- Deve representar os dados essenciais do dominio.

## 5. Camada de Infraestrutura

Projeto:

```text
src/OrdemServico.Infra
```

Essa camada contem recursos tecnicos utilizados por outras partes do sistema.

Componentes principais:

| Componente | Responsabilidade |
|---|---|
| `ConnectionFactory` | Cria conexoes abertas com o PostgreSQL usando a connection string configurada. |
| `Logger` | Registra logs da aplicacao em arquivo. |
| `SessionContext` | Mantem o usuario atual da sessao. |

### 5.1 Conexao com Banco

A classe `ConnectionFactory` centraliza a criacao de conexoes com o PostgreSQL.

A connection string e obtida pelo nome:

```text
PostgreSql
```

Ela deve estar configurada no arquivo:

```text
src/OrdemServico.UI/App.config
```

Essa abordagem evita espalhar strings de conexao pelo codigo e facilita a manutencao da configuracao.

### 5.2 Logs

A classe `Logger` registra eventos da aplicacao em arquivos de log.

Eventos registrados incluem:

- login;
- abertura de telas;
- cadastros;
- alteracoes;
- exclusoes;
- abertura de ordens;
- alteracoes de status;
- geracao de relatorios;
- erros inesperados.

O logger foi implementado para nao interromper a aplicacao caso ocorra alguma falha durante a gravacao do log.

### 5.3 Sessao do Usuario

A classe `SessionContext` armazena o usuario atual informado no login.

Esse usuario e utilizado em:

- auditoria;
- historico de status;
- logs;
- exibicao na tela principal.

## 6. Camada de Repositorios

Projeto:

```text
src/OrdemServico.Repositories
```

Essa camada e responsavel pelo acesso ao banco de dados.

Repositorios principais:

| Repositorio | Responsabilidade |
|---|---|
| `ClienteRepository` | Consultar, inserir, atualizar e excluir clientes. |
| `ServicoRepository` | Consultar, inserir e atualizar servicos. |
| `OrdemServicoRepository` | Consultar, inserir e atualizar ordens de servico. |
| `ItemOrdemServicoRepository` | Consultar, inserir e excluir itens de ordens. |
| `HistoricoStatusRepository` | Inserir registros de historico de status. |
| `AuditoriaRepository` | Inserir registros de auditoria. |

Classes auxiliares:

| Classe | Descricao |
|---|---|
| `PagedResult` | Representa resultado paginado. |
| `ClienteFiltro` | Representa filtros para busca de clientes. |
| `OrdemServicoFiltro` | Representa filtros para busca de ordens de servico. |

Caracteristicas dessa camada:

- Executa comandos SQL.
- Utiliza Npgsql para comunicacao com PostgreSQL.
- Nao deve conter regra de negocio.
- Recebe conexoes e transacoes abertas pelos services quando necessario.
- Retorna entidades ou objetos de resultado para a camada de servicos.

## 7. Camada de Servicos

Projeto:

```text
src/OrdemServico.Services
```

Essa camada concentra as regras de negocio e a coordenacao das operacoes do sistema.

Services principais:

| Service | Responsabilidade |
|---|---|
| `ClienteService` | Valida e coordena operacoes de clientes. |
| `ServicoService` | Valida e coordena operacoes de servicos. |
| `OrdemServicoService` | Valida e coordena abertura, itens, status, concorrencia e auditoria de ordens. |

Excecoes de negocio:

| Excecao | Uso |
|---|---|
| `RegraNegocioException` | Indica violacao de regra de negocio. |
| `EntidadeNaoEncontradaException` | Indica que um registro esperado nao foi encontrado. |
| `ConcorrenciaException` | Indica conflito de alteracao concorrente. |

Caracteristicas dessa camada:

- Valida dados recebidos da interface.
- Controla transacoes.
- Chama repositories.
- Trata conflitos de concorrencia.
- Registra auditoria quando aplicavel.
- Registra logs de operacoes importantes.

## 8. Camada de Interface Grafica

Projeto:

```text
src/OrdemServico.UI
```

Essa camada contem as telas Windows Forms utilizadas pelo usuario.

Telas principais:

| Tela | Finalidade |
|---|---|
| `FrmLogin` | Entrada do usuario no sistema. |
| `FrmPrincipal` | Tela principal MDI com menus de navegacao. |
| `FrmClientesLista` | Consulta e listagem de clientes. |
| `FrmClienteEdit` | Cadastro e edicao de clientes. |
| `FrmServicosLista` | Consulta e listagem de servicos. |
| `FrmServicoEdit` | Cadastro e edicao de servicos. |
| `FrmOrdensLista` | Consulta e listagem de ordens de servico. |
| `FrmOrdemEdit` | Abertura, edicao, itens e status da ordem de servico. |
| `FrmRelatorio` | Emissao de relatorio de ordens de servico. |

Responsabilidades da UI:

- Exibir dados ao usuario.
- Capturar entradas.
- Validar aspectos basicos de interface.
- Chamar os services.
- Exibir mensagens de sucesso, alerta ou erro.

A UI nao deve concentrar regras de negocio complexas. Essas regras devem permanecer na camada de servicos.

## 9. Camada de Relatorios

Projeto:

```text
src/OrdemServico.Reports
```

Essa camada contem os recursos de relatorio do sistema.

Componentes principais:

| Componente | Responsabilidade |
|---|---|
| `RelatorioOrdens.rdlc` | Layout visual do relatorio. |
| `RelatorioService` | Consulta os dados usados no relatorio. |
| `RelatorioFiltro` | Representa os filtros usados na emissao. |
| `RelatorioOrdemDto` | Representa os dados exibidos no relatorio. |

O relatorio de ordens de servico permite filtrar dados por periodo, cliente e status.

O valor de imposto e calculado com base nos itens da ordem:

```text
quantidade * valor_unitario * percentual_imposto_aplicado / 100
```

## 10. Banco de Dados

Scripts:

```text
database/01_schema.sql
database/02_seeds.sql
```

Banco utilizado:

```text
PostgreSQL
```

Tabelas principais:

| Tabela | Finalidade |
|---|---|
| `clientes` | Armazena clientes. |
| `servicos` | Armazena servicos disponiveis. |
| `ordens_servico` | Armazena ordens de servico. |
| `itens_ordem_servico` | Armazena itens das ordens. |
| `historico_status_os` | Armazena historico de status das ordens. |
| `auditoria` | Armazena registros de auditoria. |

Tipos enumerados:

| Tipo | Valores |
|---|---|
| `tipo_cliente_enum` | `FISICA`, `JURIDICA` |
| `status_os_enum` | `ABERTA`, `EM_ANDAMENTO`, `CONCLUIDA`, `CANCELADA` |

O banco tambem possui:

- chaves primarias;
- chaves estrangeiras;
- constraints de validacao;
- indices para consultas;
- trigger generica de auditoria para algumas tabelas.

## 11. Fluxo de Abertura de Ordem de Servico

Fluxo simplificado:

```text
Usuario
    |
    v
FrmOrdemEdit
    |
    v
OrdemServicoService.Abrir
    |
    v
OrdemServicoRepository.Inserir
    |
    v
HistoricoStatusRepository.Inserir
    |
    v
AuditoriaRepository.Inserir
    |
    v
PostgreSQL
```

Passos principais:

1. O usuario informa os dados da ordem.
2. A UI chama o service.
3. O service cria a ordem com status inicial `Aberta`.
4. O repository insere a ordem no banco.
5. O service registra o historico inicial.
6. O service registra auditoria.
7. A transacao e confirmada.

## 12. Fluxo de Adicao de Item

Fluxo simplificado:

```text
Usuario
    |
    v
FrmOrdemEdit
    |
    v
OrdemServicoService.AdicionarItem
    |
    v
ServicoRepository.ObterPorId
    |
    v
ItemOrdemServicoRepository.Inserir
    |
    v
OrdemServicoRepository.Atualizar
    |
    v
AuditoriaRepository.Inserir
    |
    v
PostgreSQL
```

Passos principais:

1. O usuario escolhe um servico e informa a quantidade.
2. O service valida a quantidade.
3. O service verifica se a ordem existe.
4. O service verifica se a ordem esta editavel.
5. O service verifica se o servico existe e esta ativo.
6. O service calcula o subtotal, imposto e valor total do item.
7. O item e inserido com valor unitario e percentual de imposto congelados.
8. O valor total da ordem e atualizado.
9. A versao da ordem e validada para controle de concorrencia.
10. A auditoria e registrada.
11. A transacao e confirmada.

## 13. Controle Transacional

Operacoes de escrita sao executadas dentro de transacoes.

Exemplos:

- cadastrar cliente;
- atualizar cliente;
- excluir cliente;
- cadastrar servico;
- atualizar servico;
- abrir ordem de servico;
- adicionar item;
- remover item;
- alterar status.

Quando a operacao e concluida com sucesso, a transacao recebe `Commit`.

Quando ocorre erro, a transacao recebe `Rollback`.

Essa estrategia evita gravacoes parciais em operacoes que envolvem mais de uma tabela.

## 14. Controle de Concorrencia

O sistema utiliza concorrencia otimista nas ordens de servico por meio do campo:

```text
versao
```

Funcionamento geral:

1. A ordem e carregada com uma versao atual.
2. O usuario realiza uma alteracao.
3. Ao salvar, o sistema tenta atualizar a ordem usando a versao conhecida.
4. Se outro usuario ja alterou a mesma ordem antes, a versao no banco sera diferente.
5. Nesse caso, a atualizacao nao e aplicada e o sistema gera uma excecao de concorrencia.

Esse mecanismo evita que uma alteracao sobrescreva outra sem que o usuario perceba.

## 15. Auditoria

O sistema possui auditoria para rastrear alteracoes relevantes.

Dados registrados:

- entidade;
- identificador do registro;
- operacao;
- data e hora;
- usuario;
- snapshot dos dados.

A auditoria e usada principalmente para manter rastreabilidade sobre alteracoes em ordens de servico e itens.

Existe tambem uma funcao de auditoria generica no banco de dados, definida no script de schema, com triggers associadas a algumas tabelas.

## 16. Logs

O sistema registra logs em arquivos diarios.

Formato geral do arquivo:

```text
app-AAAA-MM-DD.log
```

Os logs registram:

- nivel do evento;
- data e hora;
- origem;
- mensagem;
- detalhes de excecao, quando houver.

O logger foi projetado para nao quebrar a aplicacao caso ocorra erro ao escrever o arquivo de log.

## 17. Decisoes Arquiteturais

### 17.1 Uso de Windows Forms

Windows Forms foi utilizado por ser uma tecnologia simples e adequada para aplicacoes desktop administrativas em ambiente Windows.

### 17.2 Uso de PostgreSQL

PostgreSQL foi utilizado por oferecer banco relacional robusto, suporte a constraints, indices, enums, transacoes e campos JSONB para auditoria.

### 17.3 Separacao em camadas

A separacao em camadas permite dividir responsabilidades e evitar que a interface grafica acesse diretamente o banco ou concentre regras de negocio.

### 17.4 Services como ponto central das regras

As regras de negocio ficam concentradas nos services para facilitar manutencao, leitura e testes futuros.

### 17.5 Repositories com SQL direto

Os repositories usam Npgsql e SQL direto para acesso ao PostgreSQL.

Essa abordagem reduz dependencias externas e deixa as consultas explicitas.

### 17.6 Concorrencia otimista

A concorrencia otimista foi escolhida para detectar conflitos de alteracao sem bloquear registros durante todo o tempo de edicao pelo usuario.

### 17.7 Valores congelados nos itens

O valor unitario e o percentual de imposto sao copiados do servico para o item da ordem no momento da inclusao.

Isso garante que alteracoes futuras no cadastro do servico nao mudem o valor historico de ordens ja registradas.

## 18. Pontos de Atencao

| Ponto | Observacao |
|---|---|
| Login | O login atual e simplificado e nao possui senha. |
| Auditoria | Ha auditoria feita pela aplicacao e tambem trigger no banco para algumas tabelas. |
| Testes | Nao ha uma camada de testes automatizados na estrutura atual. |
| UI | A interface grafica concentra a experiencia do usuario, mas regras importantes devem ficar nos services. |
| Relatorios | O relatorio depende do RDLC e do ReportViewer. |

## 19. Resumo

A arquitetura do sistema segue um modelo em camadas tradicional para aplicacoes desktop:

```text
Interface Grafica -> Servicos -> Repositorios -> Banco de Dados
```

As entidades representam o dominio, a infraestrutura fornece recursos compartilhados, os services concentram regras de negocio e transacoes, os repositories isolam o acesso a dados e a camada de relatorios cuida da geracao das informacoes gerenciais.

Essa estrutura e adequada para o porte do sistema e facilita futuras evolucoes, como inclusao de testes automatizados, autenticacao real, novos relatorios ou novas regras de negocio.