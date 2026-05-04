-- SEEDS: Dados de Teste para o Sistema
-- Banco: PostgreSQL 14+
-- Pré-requisito: 01_schema.sql deve ter sido executado primeiro.

TRUNCATE TABLE
    auditoria,
    historico_status_os,
    itens_ordem_servico,
    ordens_servico,
    servicos,
    clientes
RESTART IDENTITY CASCADE;

-- Inserir Clientes
INSERT INTO clientes (nome, documento, tipo, email, telefone, ativo) VALUES
    ('Maria Silva Santos',          '12345678901',     'FISICA',   'maria.silva@email.com',     '(11) 91234-5678', TRUE),
    ('João Pereira da Costa',       '23456789012',     'FISICA',   'joao.pereira@email.com',    '(11) 92345-6789', TRUE),
    ('Ana Costa Almeida',           '34567890123',     'FISICA',   'ana.costa@email.com',       '(11) 93456-7890', FALSE),
    ('Empresa ABC Ltda',            '12345678000190',  'JURIDICA', 'contato@empresaabc.com.br', '(11) 3456-7890',  TRUE),
    ('Tech Solutions S.A.',         '23456789000101',  'JURIDICA', 'sac@techsolutions.com.br',  '(11) 4567-8901',  TRUE);

-- Inserir Serviços
INSERT INTO servicos (nome, valor_base, percentual_imposto, ativo) VALUES
    ('Consultoria Técnica',         250.00,   5.00,  TRUE),
    ('Manutenção Preventiva',       150.00,   5.00,  TRUE),
    ('Instalação de Equipamento',   800.00,  10.00,  TRUE),
    ('Treinamento de Usuários',    1200.00,   0.00,  TRUE),
    ('Suporte Remoto',               80.00,   5.00,  TRUE),
    ('Análise de Sistema',          500.00,   8.00,  FALSE);

-- Inserir Ordens de Serviço (EM ANDAMENTO)
WITH nova_os AS (
    INSERT INTO ordens_servico (cliente_id, status, observacao, valor_total, versao)
    VALUES (1, 'EM_ANDAMENTO', 'Atendimento recorrente — sistema de gestão.', 1218.00, 1)
    RETURNING id
),
itens_inseridos AS (
    INSERT INTO itens_ordem_servico (
        ordem_servico_id, servico_id, quantidade,
        valor_unitario, percentual_imposto_aplicado, valor_total_item
    )
    SELECT id, 1, 4.000, 250.00, 5.00, 1050.00 FROM nova_os
    UNION ALL
    SELECT id, 5, 2.000,  80.00, 5.00,  168.00 FROM nova_os
    RETURNING ordem_servico_id
)
INSERT INTO historico_status_os (ordem_servico_id, status_anterior, status_novo, usuario)
SELECT DISTINCT ordem_servico_id, NULL::status_os_enum, 'ABERTA'::status_os_enum, 'sistema'
FROM itens_inseridos;

-- Registra a mudança de status para EM_ANDAMENTO
INSERT INTO historico_status_os (ordem_servico_id, status_anterior, status_novo, usuario)
SELECT id, 'ABERTA'::status_os_enum, 'EM_ANDAMENTO'::status_os_enum, 'maria.atendente'
FROM ordens_servico WHERE id = 1;

-- Inserir Ordens de Serviço (CONCLUÍDA)
WITH nova_os AS (
    INSERT INTO ordens_servico (
        cliente_id, data_abertura, data_conclusao, status,
        observacao, valor_total, versao
    )
    VALUES (
        4,
        CURRENT_TIMESTAMP - INTERVAL '15 days',
        CURRENT_TIMESTAMP - INTERVAL '5 days',
        'CONCLUIDA',
        'Implantação completa de novo sistema com treinamento de equipe.',
        10795.00,
        3
    )
    RETURNING id
)
INSERT INTO itens_ordem_servico (
    ordem_servico_id, servico_id, quantidade,
    valor_unitario, percentual_imposto_aplicado, valor_total_item
)
SELECT id, 3, 1.000,  800.00, 10.00,  880.00 FROM nova_os
UNION ALL
SELECT id, 4, 8.000, 1200.00,  0.00, 9600.00 FROM nova_os
UNION ALL
SELECT id, 2, 2.000,  150.00,  5.00,  315.00 FROM nova_os;

INSERT INTO historico_status_os (ordem_servico_id, status_anterior, status_novo, data_hora, usuario)
VALUES
    (2, NULL::status_os_enum,           'ABERTA'::status_os_enum,       CURRENT_TIMESTAMP - INTERVAL '15 days', 'sistema'),
    (2, 'ABERTA'::status_os_enum,       'EM_ANDAMENTO'::status_os_enum, CURRENT_TIMESTAMP - INTERVAL '14 days', 'joao.tecnico'),
    (2, 'EM_ANDAMENTO'::status_os_enum, 'CONCLUIDA'::status_os_enum,    CURRENT_TIMESTAMP - INTERVAL '5 days',  'joao.tecnico');

-- Inserir Ordens de Serviço (ABERTA)
WITH nova_os AS (
    INSERT INTO ordens_servico (cliente_id, status, observacao, valor_total, versao)
    VALUES (2, 'ABERTA', 'Aguardando aprovação do cliente.', 262.50, 1)
    RETURNING id
)
INSERT INTO itens_ordem_servico (
    ordem_servico_id, servico_id, quantidade,
    valor_unitario, percentual_imposto_aplicado, valor_total_item
)
SELECT id, 1, 1.000, 250.00, 5.00, 262.50 FROM nova_os;

INSERT INTO historico_status_os (ordem_servico_id, status_anterior, status_novo, usuario)
VALUES (3, NULL::status_os_enum, 'ABERTA'::status_os_enum, 'sistema');

-- Inserir Auditoria
INSERT INTO auditoria (entidade, id_registro, operacao, usuario, snapshot)
VALUES
    -- Auditoria da criação da OS #1
    (
        'ordens_servico',
        1,
        'INSERT',
        'maria.atendente',
        '{
            "id": 1,
            "cliente_id": 1,
            "status": "ABERTA",
            "valor_total": 0.00,
            "versao": 1
        }'::jsonb
    ),
    -- Auditoria da mudança de status para EM_ANDAMENTO
    (
        'ordens_servico',
        1,
        'UPDATE',
        'maria.atendente',
        '{
            "id": 1,
            "cliente_id": 1,
            "status": "EM_ANDAMENTO",
            "valor_total": 1218.00,
            "versao": 2,
            "operacao_realizada": "alteracao_status"
        }'::jsonb
    ),
    -- Auditoria da conclusão da OS #2
    (
        'ordens_servico',
        2,
        'UPDATE',
        'joao.tecnico',
        '{
            "id": 2,
            "cliente_id": 4,
            "status": "CONCLUIDA",
            "valor_total": 10795.00,
            "versao": 3,
            "operacao_realizada": "conclusao"
        }'::jsonb
    );

-- Validação dos Dados
SELECT
    'Total clientes' AS metrica,
    COUNT(*)::text AS valor
FROM clientes
UNION ALL
SELECT 'Clientes ativos',          COUNT(*)::text FROM clientes WHERE ativo = TRUE
UNION ALL
SELECT 'Total servicos',           COUNT(*)::text FROM servicos
UNION ALL
SELECT 'Servicos ativos',          COUNT(*)::text FROM servicos WHERE ativo = TRUE
UNION ALL
SELECT 'Total OS',                 COUNT(*)::text FROM ordens_servico
UNION ALL
SELECT 'OS abertas',               COUNT(*)::text FROM ordens_servico WHERE status = 'ABERTA'
UNION ALL
SELECT 'OS em andamento',          COUNT(*)::text FROM ordens_servico WHERE status = 'EM_ANDAMENTO'
UNION ALL
SELECT 'OS concluidas',            COUNT(*)::text FROM ordens_servico WHERE status = 'CONCLUIDA'
UNION ALL
SELECT 'Total itens',              COUNT(*)::text FROM itens_ordem_servico
UNION ALL
SELECT 'Total historico status',   COUNT(*)::text FROM historico_status_os
UNION ALL
SELECT 'Total auditoria',          COUNT(*)::text FROM auditoria
UNION ALL
SELECT 'Valor total faturado',     'R$ ' || TO_CHAR(SUM(valor_total), 'FM999G990D00') FROM ordens_servico;