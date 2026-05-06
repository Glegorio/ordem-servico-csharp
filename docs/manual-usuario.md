# Manual do Usuario

## 1. Apresentacao

Este manual descreve como utilizar o Sistema de Gestao de Ordens de Servico.

O sistema permite cadastrar clientes, cadastrar servicos, abrir ordens de servico, adicionar itens, alterar status das ordens e gerar relatorios gerenciais.

## 2. Acesso ao Sistema

Ao iniciar o sistema, sera exibida a tela de login.

### Como entrar

1. Informe o nome do usuario.
2. Clique em `Entrar` ou pressione `Enter`.

### Regras do login

- O nome do usuario e obrigatorio.
- O nome deve possuir pelo menos 3 caracteres.
- O login atual nao utiliza senha.

Depois do login, o sistema abre a tela principal.

## 3. Tela Principal

A tela principal apresenta o menu de navegacao do sistema.

Por meio dela, o usuario pode acessar:

- clientes;
- servicos;
- ordens de servico;
- nova ordem de servico;
- relatorios;
- saida do sistema.

A tela tambem exibe informacoes como usuario logado, banco de dados configurado e data/hora atual.

## 4. Cadastro de Clientes

O cadastro de clientes permite registrar pessoas fisicas ou juridicas que poderao ter ordens de servico vinculadas.

### Acessar clientes

1. Na tela principal, acesse o menu de clientes.
2. O sistema exibira a listagem de clientes cadastrados.

### Cadastrar novo cliente

1. Clique na opcao para criar um novo cliente.
2. Preencha os dados solicitados.
3. Salve o cadastro.

### Dados do cliente

| Campo | Descricao |
|---|---|
| Nome | Nome do cliente. |
| Documento | CPF ou CNPJ do cliente. |
| Tipo | Pessoa fisica ou pessoa juridica. |
| E-mail | E-mail de contato. |
| Telefone | Telefone de contato. |
| Ativo | Indica se o cliente esta ativo. |

### Regras importantes

- O nome e obrigatorio.
- O documento e obrigatorio.
- Nao pode existir mais de um cliente com o mesmo documento.
- Clientes inativos nao devem ser usados em novas ordens de servico.

### Editar cliente

1. Localize o cliente na listagem.
2. Abra o cadastro do cliente.
3. Altere os dados desejados.
4. Salve as alteracoes.

### Excluir cliente

1. Localize o cliente na listagem.
2. Selecione a opcao de exclusao.
3. Confirme a operacao.

O sistema nao permite excluir cliente que possua ordem de servico vinculada.

## 5. Cadastro de Servicos

O cadastro de servicos define quais servicos podem ser adicionados a uma ordem de servico.

### Acessar servicos

1. Na tela principal, acesse o menu de servicos.
2. O sistema exibira a listagem de servicos cadastrados.

### Cadastrar novo servico

1. Clique na opcao para criar um novo servico.
2. Preencha os dados solicitados.
3. Salve o cadastro.

### Dados do servico

| Campo | Descricao |
|---|---|
| Nome | Nome do servico oferecido. |
| Valor base | Valor padrao do servico. |
| Percentual de imposto | Percentual de imposto aplicado ao servico. |
| Ativo | Indica se o servico esta disponivel para uso. |

### Regras importantes

- O nome do servico e obrigatorio.
- O valor base deve ser maior que zero.
- O percentual de imposto deve estar entre 0 e 100.
- Apenas servicos ativos podem ser adicionados a uma ordem de servico.

### Editar servico

1. Localize o servico na listagem.
2. Abra o cadastro do servico.
3. Altere os dados desejados.
4. Salve as alteracoes.

### Observacao sobre valores

Quando um servico e adicionado a uma ordem, o sistema copia o valor base e o percentual de imposto para o item da ordem.

Isso significa que, se o cadastro do servico for alterado depois, os itens ja adicionados em ordens anteriores nao terao seus valores modificados automaticamente.

## 6. Ordens de Servico

As ordens de servico representam os atendimentos ou trabalhos realizados para os clientes.

Cada ordem possui cliente, status, observacao, itens, valor total, data de abertura e, quando concluida, data de conclusao.

### Status possiveis

| Status | Significado |
|---|---|
| Aberta | Ordem criada e ainda editavel. |
| Em andamento | Ordem em execucao. |
| Concluida | Ordem finalizada. |
| Cancelada | Ordem cancelada. |

## 7. Abrir Nova Ordem de Servico

### Como abrir uma ordem

1. Na tela principal, acesse a opcao de nova ordem de servico.
2. Selecione o cliente.
3. Preencha a observacao, se necessario.
4. Salve a ordem.

### Regras da abertura

- A ordem e criada com status `Aberta`.
- A data de abertura e preenchida automaticamente.
- O valor total inicial e zero.
- O sistema registra o historico inicial de status.

Depois de aberta, a ordem pode receber itens.

## 8. Consultar Ordens de Servico

### Como consultar

1. Na tela principal, acesse a listagem de ordens de servico.
2. Informe os filtros desejados.
3. Execute a busca.

### Filtros comuns

- cliente;
- status;
- periodo;
- outros campos disponiveis na tela.

Na listagem, o usuario pode abrir uma ordem para visualizar ou editar seus dados, conforme o status da ordem.

## 9. Adicionar Item a Ordem

Os itens representam os servicos executados dentro de uma ordem de servico.

### Como adicionar item

1. Abra uma ordem de servico.
2. Selecione um servico ativo.
3. Informe a quantidade.
4. Confirme a inclusao do item.

### Regras para adicionar item

- A quantidade deve ser maior que zero.
- A ordem nao pode estar concluida.
- A ordem nao pode estar cancelada.
- O servico selecionado deve estar ativo.

### Calculo do item

O sistema calcula o valor do item com base em:

```text
subtotal = quantidade * valor_unitario
imposto = subtotal * percentual_imposto / 100
valor_total_item = subtotal + imposto
```

Apos adicionar o item, o valor total da ordem e atualizado.

## 10. Remover Item da Ordem

### Como remover item

1. Abra uma ordem de servico.
2. Selecione o item desejado.
3. Acione a opcao de remover.
4. Confirme a operacao.

### Regras para remover item

- A ordem nao pode estar concluida.
- A ordem nao pode estar cancelada.
- O item precisa pertencer a ordem aberta.

Apos remover o item, o valor total da ordem e atualizado.

## 11. Alterar Status da Ordem

### Como alterar status

1. Abra uma ordem de servico.
2. Selecione o novo status.
3. Confirme a alteracao.

### Regras de alteracao

- Nao e permitido alterar para o mesmo status atual.
- Ordem concluida nao pode mudar de status.
- Ordem cancelada nao pode mudar de status.
- Ordem com valor total zero nao pode ser concluida.
- Ao concluir uma ordem, a data de conclusao e preenchida automaticamente.
- Toda alteracao de status gera registro no historico.

## 12. Historico de Status

O sistema registra as mudancas de status da ordem de servico.

Cada registro de historico contem:

- status anterior;
- novo status;
- data e hora da alteracao;
- usuario responsavel.

O primeiro registro e criado automaticamente quando a ordem de servico e aberta.

## 13. Relatorio de Ordens de Servico

O sistema possui relatorio gerencial de ordens de servico.

### Como gerar o relatorio

1. Na tela principal, acesse a opcao de relatorio.
2. Informe os filtros desejados.
3. Gere o relatorio.

### Filtros disponiveis

- periodo;
- cliente;
- status.

### Informacoes exibidas

O relatorio pode exibir:

- numero da ordem;
- data de abertura;
- data de conclusao;
- status;
- cliente;
- documento do cliente;
- valor total;
- valor de imposto.

## 14. Mensagens do Sistema

Durante o uso, o sistema pode exibir mensagens de informacao, alerta ou erro.

Exemplos de mensagens:

| Situacao | Mensagem esperada |
|---|---|
| Cliente sem nome | Informa que o nome e obrigatorio. |
| Cliente sem documento | Informa que o documento e obrigatorio. |
| Documento duplicado | Informa que ja existe cliente com o documento. |
| Servico com valor invalido | Informa que o valor base deve ser maior que zero. |
| Quantidade invalida | Informa que a quantidade deve ser maior que zero. |
| Ordem concluida | Informa que ordem concluida nao pode ser alterada. |
| Ordem cancelada | Informa que ordem cancelada nao pode ser alterada. |
| Conclusao sem itens | Informa que nao e possivel concluir OS com valor total zero. |

## 15. Concorrencia de Alteracoes

O sistema possui controle para evitar que duas alteracoes simultaneas sobrescrevam uma a outra.

Exemplo:

1. Usuario A abre uma ordem de servico.
2. Usuario B abre a mesma ordem.
3. Usuario A altera e salva.
4. Usuario B tenta salvar uma versao antiga.
5. O sistema informa que a ordem foi alterada por outro usuario.

Quando isso ocorrer, o usuario deve recarregar a ordem e tentar novamente.

## 16. Saida do Sistema

Para sair do sistema:

1. Acesse a opcao de sair.
2. Confirme a saida.

Ao sair, o sistema limpa a sessao do usuario e encerra a aplicacao.

## 17. Boas Praticas de Uso

- Verifique se o cliente correto foi selecionado antes de abrir uma ordem.
- Confira os itens antes de concluir a ordem.
- Use o status `Em andamento` para ordens que ja estao em execucao.
- Use o status `Cancelada` apenas quando a ordem nao deve mais ser executada.
- Revise os filtros antes de gerar relatorios.
- Caso receba mensagem de concorrencia, recarregue a ordem antes de tentar salvar novamente.

## 18. Resumo do Fluxo Principal

Fluxo mais comum de uso:

```text
Entrar no sistema
    |
    v
Cadastrar cliente
    |
    v
Cadastrar servicos
    |
    v
Abrir ordem de servico
    |
    v
Adicionar itens
    |
    v
Alterar status
    |
    v
Gerar relatorio
```