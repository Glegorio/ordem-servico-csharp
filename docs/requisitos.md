# Requisitos do Sistema

## 1. Visao Geral

O Sistema de Gestao de Ordens de Servico tem como objetivo controlar clientes, servicos oferecidos, ordens de servico, itens vinculados a cada ordem, historico de status, auditoria das operacoes e emissao de relatorios gerenciais.

O sistema e uma aplicacao desktop Windows Forms desenvolvida em C# com .NET Framework, utilizando PostgreSQL como banco de dados.

## 2. Atores

| Ator | Descricao |
|---|---|
| Usuario | Pessoa que acessa o sistema para cadastrar clientes, servicos, ordens de servico e consultar relatorios. |
| Sistema | Aplicacao responsavel por validar regras de negocio, persistir dados, registrar logs, auditoria e historico de status. |

## 3. Requisitos Funcionais

### RF01 - Login do usuario

O sistema deve permitir que o usuario informe um nome de usuario para acessar a aplicacao.

**Detalhes:**

- O nome do usuario deve ser obrigatorio.
- O nome do usuario deve possuir pelo menos 3 caracteres.
- O usuario informado deve ser armazenado na sessao da aplicacao.
- O usuario da sessao deve ser utilizado em registros de auditoria, historico de status e logs.

**Observacao:**  
O login atual e simplificado e nao realiza autenticacao real com senha ou perfis de acesso.

### RF02 - Cadastro de clientes

O sistema deve permitir o cadastro de clientes.

**Dados do cliente:**

- Nome.
- Documento.
- Tipo de cliente: pessoa fisica ou pessoa juridica.
- E-mail.
- Telefone.
- Data de cadastro.
- Situacao: ativo ou inativo.

**Regras principais:**

- Nome e obrigatorio.
- Documento e obrigatorio.
- Documento deve ser unico no banco de dados.
- O cliente deve ser cadastrado como ativo por padrao.

### RF03 - Edicao de clientes

O sistema deve permitir a alteracao dos dados de um cliente ja cadastrado.

**Regras principais:**

- O cliente deve existir.
- Nome e documento continuam obrigatorios.
- Nao deve ser permitido salvar documento duplicado para clientes diferentes.

### RF04 - Exclusao de clientes

O sistema deve permitir a exclusao de clientes, desde que eles nao possuam ordens de servico vinculadas.

**Regras principais:**

- Nao deve ser permitido excluir cliente com uma ou mais ordens de servico vinculadas.
- Caso exista vinculo, o sistema deve informar ao usuario que a exclusao nao e permitida.

### RF05 - Consulta de clientes

O sistema deve permitir consultar clientes cadastrados.

**Recursos esperados:**

- Busca paginada.
- Filtro por dados do cliente conforme os campos disponiveis na tela.
- Visualizacao dos registros encontrados em lista.

### RF06 - Cadastro de servicos

O sistema deve permitir o cadastro de servicos oferecidos.

**Dados do servico:**

- Nome.
- Valor base.
- Percentual de imposto.
- Situacao: ativo ou inativo.

**Regras principais:**

- Nome do servico e obrigatorio.
- Valor base deve ser maior que zero.
- Percentual de imposto deve estar entre 0 e 100.
- O servico deve ser cadastrado como ativo por padrao.

### RF07 - Edicao de servicos

O sistema deve permitir alterar os dados de um servico cadastrado.

**Regras principais:**

- O servico deve existir.
- Nome do servico e obrigatorio.
- Valor base deve ser maior que zero.
- Percentual de imposto deve estar entre 0 e 100.

### RF08 - Consulta de servicos

O sistema deve permitir consultar servicos cadastrados.

**Recursos esperados:**

- Listagem de todos os servicos.
- Listagem apenas de servicos ativos para uso em novas ordens de servico.

### RF09 - Abertura de ordem de servico

O sistema deve permitir abrir uma nova ordem de servico para um cliente.

**Dados da ordem de servico:**

- Cliente.
- Data de abertura.
- Status.
- Observacao.
- Valor total.
- Versao para controle de concorrencia.

**Regras principais:**

- A ordem deve iniciar com status `Aberta`.
- A data de abertura deve ser preenchida automaticamente.
- O valor total inicial deve ser zero.
- A versao inicial deve ser 1.
- Deve ser registrado historico inicial de status.

### RF10 - Consulta de ordens de servico

O sistema deve permitir consultar ordens de servico cadastradas.

**Recursos esperados:**

- Busca paginada.
- Filtros por cliente, status, periodo ou outros campos disponiveis na tela.
- Visualizacao das ordens em lista.
- Abertura de uma ordem para visualizacao ou edicao.

### RF11 - Adicao de item na ordem de servico

O sistema deve permitir adicionar itens a uma ordem de servico.

**Dados do item:**

- Servico.
- Quantidade.
- Valor unitario aplicado.
- Percentual de imposto aplicado.
- Valor total do item.

**Regras principais:**

- A quantidade deve ser maior que zero.
- A ordem de servico deve existir.
- A ordem de servico deve estar em status editavel.
- Nao deve ser permitido adicionar item em ordem concluida.
- Nao deve ser permitido adicionar item em ordem cancelada.
- O servico deve existir.
- O servico deve estar ativo.
- O valor unitario e o percentual de imposto devem ser copiados do cadastro do servico no momento da adicao.
- O valor unitario e o percentual de imposto do item devem permanecer congelados, mesmo que o cadastro do servico seja alterado depois.
- O valor total da ordem deve ser recalculado ou atualizado apos a adicao do item.

### RF12 - Remocao de item da ordem de servico

O sistema deve permitir remover itens de uma ordem de servico.

**Regras principais:**

- A ordem de servico deve existir.
- A ordem deve estar em status editavel.
- Nao deve ser permitido remover item de ordem concluida.
- Nao deve ser permitido remover item de ordem cancelada.
- O item deve pertencer a ordem de servico informada.
- O valor total da ordem deve ser atualizado apos a remocao.
- O valor total da ordem nao deve ficar negativo.

### RF13 - Alteracao de status da ordem de servico

O sistema deve permitir alterar o status de uma ordem de servico.

**Status disponiveis:**

- Aberta.
- Em andamento.
- Concluida.
- Cancelada.

**Regras principais:**

- A ordem de servico deve existir.
- Nao deve ser permitido alterar para o mesmo status atual.
- Ordem concluida nao pode mudar de status.
- Ordem cancelada nao pode mudar de status.
- Ordem com valor total zero nao pode ser concluida.
- Ao concluir uma ordem, a data de conclusao deve ser preenchida.
- Toda alteracao de status deve gerar registro no historico de status.

### RF14 - Historico de status da ordem de servico

O sistema deve registrar o historico de mudancas de status de cada ordem de servico.

**Dados registrados:**

- Ordem de servico.
- Status anterior.
- Novo status.
- Data e hora da alteracao.
- Usuario responsavel.

**Regras principais:**

- Ao abrir uma ordem, deve ser criado um registro inicial no historico.
- Ao alterar o status, deve ser criado novo registro com status anterior e novo status.

### RF15 - Auditoria de operacoes

O sistema deve registrar auditoria das principais operacoes realizadas.

**Dados registrados:**

- Entidade auditada.
- Identificador do registro.
- Operacao realizada.
- Data e hora.
- Usuario responsavel.
- Snapshot dos dados.

**Operacoes previstas:**

- Inclusao.
- Alteracao.
- Exclusao, quando aplicavel.

**Observacao:**  
O sistema possui auditoria por aplicacao e tambem estrutura de trigger de auditoria no banco de dados para algumas tabelas.

### RF16 - Relatorio de ordens de servico

O sistema deve permitir gerar relatorio gerencial de ordens de servico.

**Filtros esperados:**

- Periodo de abertura.
- Cliente.
- Status da ordem de servico.

**Dados exibidos:**

- Numero da ordem.
- Data de abertura.
- Data de conclusao.
- Status.
- Cliente.
- Documento do cliente.
- Valor total.
- Valor de imposto.

**Regras principais:**

- O relatorio deve considerar os filtros informados pelo usuario.
- O valor de imposto deve ser calculado com base nos itens da ordem de servico.

### RF17 - Logs da aplicacao

O sistema deve registrar logs de eventos relevantes da aplicacao.

**Eventos esperados:**

- Login efetuado.
- Abertura de telas principais.
- Cadastro, edicao e exclusao de registros.
- Abertura de ordens de servico.
- Adicao e remocao de itens.
- Alteracao de status.
- Geracao de relatorios.
- Erros inesperados.

## 4. Requisitos Nao Funcionais

### RNF01 - Plataforma

O sistema deve ser executado em ambiente Windows.

### RNF02 - Interface grafica

O sistema deve possuir interface grafica desktop desenvolvida em Windows Forms.

### RNF03 - Banco de dados

O sistema deve utilizar PostgreSQL como banco de dados relacional.

### RNF04 - Conexao com banco

O sistema deve obter a string de conexao a partir do arquivo de configuracao da aplicacao.

### RNF05 - Arquitetura em camadas

O sistema deve seguir separacao em camadas, mantendo responsabilidades distintas entre apresentacao, servicos, repositorios, infraestrutura, entidades e relatorios.

### RNF06 - Integridade dos dados

O sistema deve utilizar constraints, chaves primarias, chaves estrangeiras e validacoes para preservar a integridade dos dados.

### RNF07 - Controle transacional

Operacoes que alteram mais de uma tabela devem ser executadas dentro de transacao.

### RNF08 - Concorrencia

O sistema deve utilizar controle de concorrencia otimista para evitar sobrescrita indevida de alteracoes em ordens de servico.

### RNF09 - Auditoria

O sistema deve manter trilha de auditoria das operacoes relevantes para permitir rastreabilidade.

### RNF10 - Logs

O sistema deve registrar logs em arquivo e nao deve interromper a aplicacao caso ocorra falha na escrita do log.

### RNF11 - Paginacao

Consultas de listagem devem utilizar paginacao para evitar carregamento excessivo de dados.

### RNF12 - Manutenibilidade

O codigo deve manter baixo acoplamento entre camadas e concentrar regras de negocio na camada de servicos.

### RNF13 - Relatorios

O sistema deve gerar relatorios usando RDLC e dados consultados no banco PostgreSQL.

## 5. Regras de Negocio Consolidadas

| Codigo | Regra |
|---|---|
| RN01 | Cliente deve possuir nome e documento. |
| RN02 | Documento de cliente deve ser unico. |
| RN03 | Cliente com ordem de servico vinculada nao pode ser excluido. |
| RN04 | Servico deve possuir nome. |
| RN05 | Valor base do servico deve ser maior que zero. |
| RN06 | Percentual de imposto deve estar entre 0 e 100. |
| RN07 | Ordem de servico deve iniciar com status Aberta. |
| RN08 | Ordem de servico deve iniciar com valor total zero. |
| RN09 | Ordem concluida nao pode ser alterada. |
| RN10 | Ordem cancelada nao pode ser alterada. |
| RN11 | Nao e permitido concluir ordem de servico com valor total zero. |
| RN12 | Quantidade do item deve ser maior que zero. |
| RN13 | Apenas servicos ativos podem ser adicionados a uma ordem de servico. |
| RN14 | Valor unitario e percentual de imposto do item devem ser congelados no momento da inclusao. |
| RN15 | Toda alteracao de status deve gerar historico. |
| RN16 | Alteracoes concorrentes em ordem de servico devem ser detectadas pela versao. |

## 6. Criterios de Aceite

### Cadastro de cliente

- O sistema deve salvar cliente com dados obrigatorios preenchidos.
- O sistema deve impedir cliente sem nome.
- O sistema deve impedir cliente sem documento.
- O sistema deve impedir documento duplicado.
- O sistema deve impedir exclusao de cliente com ordem vinculada.

### Cadastro de servico

- O sistema deve salvar servico com nome, valor base e percentual de imposto validos.
- O sistema deve impedir valor base menor ou igual a zero.
- O sistema deve impedir percentual de imposto menor que 0 ou maior que 100.

### Ordem de servico

- O sistema deve abrir ordem com status Aberta, valor zero e versao 1.
- O sistema deve permitir adicionar item valido a uma ordem editavel.
- O sistema deve impedir item com quantidade menor ou igual a zero.
- O sistema deve impedir adicionar servico inativo.
- O sistema deve atualizar o valor total da ordem apos adicionar ou remover item.
- O sistema deve impedir edicao de ordem concluida ou cancelada.
- O sistema deve impedir conclusao de ordem com valor zero.
- O sistema deve registrar historico ao alterar status.

### Relatorio

- O sistema deve gerar relatorio com base nos filtros informados.
- O sistema deve exibir valor total e valor de imposto das ordens.

### Auditoria e logs

- O sistema deve registrar usuario, data, operacao e snapshot nas auditorias previstas.
- O sistema deve registrar eventos relevantes em arquivo de log.

## 7. Escopo Atual

Faz parte do escopo atual:

- Login simplificado por nome de usuario.
- Cadastro e manutencao de clientes.
- Cadastro e manutencao de servicos.
- Abertura e gerenciamento de ordens de servico.
- Inclusao e remocao de itens da ordem.
- Controle de status da ordem.
- Historico de status.
- Auditoria.
- Logs em arquivo.
- Relatorio gerencial de ordens de servico.
