-- SCHEMA: Sistema de Gestão de Ordens de Serviço
-- Banco: PostgreSQL 14+

DROP TABLE IF EXISTS auditoria               CASCADE;
DROP TABLE IF EXISTS historico_status_os     CASCADE;
DROP TABLE IF EXISTS itens_ordem_servico     CASCADE;
DROP TABLE IF EXISTS ordens_servico          CASCADE;
DROP TABLE IF EXISTS servicos                CASCADE;
DROP TABLE IF EXISTS clientes                CASCADE;
 
DROP TYPE  IF EXISTS tipo_cliente_enum CASCADE;
DROP TYPE  IF EXISTS status_os_enum    CASCADE;

-- TIPOS ENUMERADOS:
CREATE TYPE tipo_cliente_enum AS ENUM ('FISICA', 'JURIDICA');

CREATE TYPE status_os_enum AS ENUM(
    'ABERTA',
    'EM_ANDAMENTO',
    'CONCLUIDA',
    'CANCELADA'
);

-- Tabela: CLIENTES
CREATE TABLE clientes(
    id              BIGSERIAL         PRIMARY KEY,
    nome            VARCHAR(150)      NOT NULL,
    documento       VARCHAR(20)       NOT NULL,
    tipo            tipo_cliente_enum NOT NULL,
    email           VARCHAR(150),
    telefone        VARCHAR(20),
    data_cadastro   TIMESTAMP         NOT NULL DEFAULT CURRENT_TIMESTAMP,
    ativo           BOOLEAN           NOT NULL DEFAULT TRUE,

    CONSTRAINT uq_clientes_documento            UNIQUE (documento),
    CONSTRAINT ck_clientes_documento_nao_vazio  CHECK (LENGTH(TRIM(documento)) > 0),
    CONSTRAINT ck_clientes_nome_nao_vazio       CHECK(LENGTH(TRIM(nome)) > 0)
);

COMMENT ON TABLE clientes               IS 'Cadastro de clientes (Pessoa Física ou Jurídica)';
COMMENT ON COLUMN clientes.documento    IS 'CPF (11 dig) ou CNPJ (14 dig). Único no banco';
COMMENT ON COLUMN clientes.tipo         IS 'FISICA = pessoa física, JURIDICA = pessoa jurídica';
COMMENT ON COLUMN clientes.ativo        IS 'Soft-flag. Cliente inativo não aparece em novas OS';

-- Tabela: SERVIÇOS
CREATE TABLE servicos(
    id                  BIGSERIAL         PRIMARY KEY,
    nome                VARCHAR(150)      NOT NULL,
    valor_base          NUMERIC(12,2)     NOT NULL,
    percentual_imposto  NUMERIC(5,2)      NOT NULL,
    ativo               BOOLEAN           NOT NULL DEFAULT TRUE,

    CONSTRAINT ck_servicos_valor_positivo       CHECK (valor_base > 0),
    CONSTRAINT ck_servicos_imposto_intervalo    CHECK (percentual_imposto >= 0 AND percentual_imposto <= 100),
    CONSTRAINT ck_servicos_nome_nao_vazio       CHECK (LENGTH(TRIM(nome)) > 0)
);

COMMENT ON TABLE servicos                       IS 'Catálogo de serviços oferecidos';
COMMENT ON COLUMN servicos.valor_base           IS 'Preço atual do serviço.';
COMMENT ON COLUMN servicos.percentual_imposto  IS 'Percentual de impostos entre 0 e 100. Também é congelado nos itens da OS.';

-- Tabela: ORDENS DE SERVIÇOS
CREATE TABLE ordens_servico(
    id                  BIGSERIAL         PRIMARY KEY,
    cliente_id          BIGINT            NOT NULL,
    data_abertura       TIMESTAMP         NOT NULL DEFAULT CURRENT_TIMESTAMP,
    data_conclusao      TIMESTAMP         NULL,
    status              status_os_enum    NOT NULL DEFAULT 'ABERTA',
    observacao          TEXT              NULL,
    valor_total         NUMERIC(12,2)     NOT NULL DEFAULT 0,
    versao              INTEGER           NOT NULL DEFAULT 1,

    CONSTRAINT fk_os_cliente
        FOREIGN KEY (cliente_id)
        REFERENCES clientes(id)
        ON DELETE RESTRICT
        ON UPDATE CASCADE,

    CONSTRAINT ck_os_valor_total_nao_negativo           CHECK (valor_total >= 0),
    CONSTRAINT ck_os_data_conclusao_valida              CHECK (data_conclusao IS NULL OR data_conclusao >= data_abertura),
    CONSTRAINT ck_os_status_conclusao_consistencia      CHECK(
                                                             (status = 'CONCLUIDA' AND data_conclusao IS NOT NULL)
                                                              OR
                                                             (status <> 'CONCLUIDA' AND data_conclusao IS NULL)
                                                              OR
                                                             (status = 'CANCELADA') -- cancelada pode ou não ter data
                        )
);

COMMENT ON TABLE ordens_servico                 IS 'Ordens de serviços';
COMMENT ON COLUMN ordens_servico.versao         IS 'Controle de concorrência otimista';
COMMENT ON COLUMN ordens_servico.valor_total    IS 'Soma do itens. Recalcula pela cama Service a cada alteração';

-- Tabela: ITENS ORDEM DE SERVIÇO
CREATE TABLE itens_ordem_servico(
    id                              BIGSERIAL         PRIMARY KEY,
    ordem_servico_id                BIGINT            NOT NULL,
    servico_id                      BIGINT            NOT NULL,
    quantidade                      NUMERIC(10, 3)    NOT NULL,
    valor_unitario                  NUMERIC(12, 2)    NOT NULL, -- CONGELADO
    percentual_imposto_aplicado     NUMERIC(5, 2)     NOT NULL, -- CONGELADO
    valor_total_item                NUMERIC(12, 2)    NOT NULL,

    CONSTRAINT fk_item_ordem
        FOREIGN KEY (ordem_servico_id)
        REFERENCES ordens_servico(id)
        ON DELETE CASCADE
        ON UPDATE CASCADE,

    CONSTRAINT fk_item_servico
        FOREIGN KEY (servico_id)
        REFERENCES servicos(id)
        ON DELETE RESTRICT 
        ON UPDATE CASCADE,

    CONSTRAINT ck_item_quantidade_positiva          CHECK (quantidade > 0),
    CONSTRAINT ck_item_valor_unitario_positivo      CHECK (valor_unitario > 0),
    CONSTRAINT ck_item_imposto_intervalo            CHECK (percentual_imposto_aplicado >= 0 AND percentual_imposto_aplicado <= 100),
    CONSTRAINT ck_item_valor_total_nao_negativo     CHECK (valor_total_item >= 0)
);

COMMENT ON TABLE itens_ordem_servico                                IS 'Itens (linhas) de uma OS';
COMMENT ON COLUMN itens_ordem_servico.valor_unitario                IS 'Valor copiado de servicos.valor_base. CONGELADO.';
COMMENT ON COLUMN itens_ordem_servico.percentual_imposto_aplicado   IS 'Percentual copiado de servicos.percentual_imposto. CONGELADO';
COMMENT ON COLUMN itens_ordem_servico.valor_total_item              IS 'Calculo: (quantidade * valor_unitario) + imposto.';

-- Tabela: HISTORICO STATUS OS
CREATE TABLE historico_status_os(
    id                  BIGSERIAL         PRIMARY KEY,
    ordem_servico_id    BIGINT            NOT NULL,
    status_anterior     status_os_enum    NULL, -- primeiro registro (criação)
    status_novo         status_os_enum    NOT NULL,
    data_hora           TIMESTAMP         NOT NULL DEFAULT CURRENT_TIMESTAMP,
    usuario             VARCHAR(100)      NOT NULL,

    CONSTRAINT fk_historico_ordem
        FOREIGN KEY (ordem_servico_id)
        REFERENCES ordens_servico(id)
        ON DELETE CASCADE
        ON UPDATE CASCADE,

    CONSTRAINT ck_historico_usuario_nao_vazio CHECK (LENGTH(TRIM(usuario)) > 0)
);

COMMENT ON TABLE historico_status_os IS 'Timeline de mudanças de status de cada OS';

-- Tabela: AUDITORIA
CREATE TABLE auditoria(
    id                  BIGSERIAL         PRIMARY KEY,
    entidade            VARCHAR(50)       NOT NULL,
    id_registro         BIGINT            NOT NULL,
    operacao            VARCHAR(10)       NOT NULL,
    data_hora           TIMESTAMP         NOT NULL DEFAULT CURRENT_TIMESTAMP,
    usuario             VARCHAR(100)      NOT NULL,
    snapshot            JSONB             NOT NULL,

    CONSTRAINT ck_auditoria_operacao_valida         CHECK (operacao IN ('INSERT', 'UPDATE', 'DELETE')),
    CONSTRAINT ck_auditoria_usuario_nao_vazio       CHECK (LENGTH(TRIM(usuario)) > 0),
    CONSTRAINT ck_auditoria_entidade_nao_vazio      CHECK (LENGTH(TRIM(entidade)) > 0)
);

COMMENT ON TABLE auditoria IS 'Trilha de auditoria polimórfica.';
COMMENT ON COLUMN auditoria.entidade IS 'Nome da tabela auditada';
COMMENT ON COLUMN auditoria.snapshot IS 'Estado completo do registro após a operação';

-- Índices:
CREATE INDEX idx_clientes_nome
    ON clientes (nome);

CREATE INDEX idx_clientes_data_cadastro
    ON clientes(data_cadastro DESC);

CREATE INDEX idx_clientes_ativos_nome
    ON clientes (nome)
    WHERE ativo = TRUE;

CREATE INDEX idx_os_data_abertura
    ON ordens_servico (data_abertura DESC);

CREATE INDEX idx_os_status
    ON ordens_servico (status);

CREATE INDEX idx_os_cliente
    ON ordens_servico (cliente_id);

CREATE INDEX idx_os_relatorio
    ON ordens_servico (cliente_id, data_abertura, status);

CREATE INDEX idx_itens_os
    ON itens_ordem_servico (ordem_servico_id);

CREATE INDEX idx_historico_os
    ON historico_status_os (ordem_servico_id, data_hora DESC);

CREATE INDEX idx_auditoria_entidade_registro
    ON auditoria (entidade, id_registro, data_hora DESC);

CREATE INDEX idx_auditoria_snapshot_gin
    ON auditoria
    USING GIN (snapshot);

-- TRIGGER DE AUDITORIA
CREATE OR REPLACE FUNCTION fn_auditoria_generica()
RETURNS TRIGGER AS $$
DECLARE
    v_usuario VARCHAR(100);
    v_snapshot JSONB;
    v_id_registro BIGINT;
BEGIN
    v_usuario := COALESCE(current_setting('app.usuario', TRUE), CURRENT_USER);

     IF (TG_OP = 'DELETE') THEN
        v_snapshot := to_jsonb(OLD);
        v_id_registro := OLD.id;
    ELSE
        v_snapshot := to_jsonb(NEW);
        v_id_registro := NEW.id;
    END IF;
 
    INSERT INTO auditoria (entidade, id_registro, operacao, usuario, snapshot)
    VALUES (TG_TABLE_NAME, v_id_registro, TG_OP, v_usuario, v_snapshot);

    IF (TG_OP = 'DELETE') THEN
        RETURN OLD;
    ELSE
        RETURN NEW;
    END IF;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER tg_auditoria_ordens_servico
    AFTER INSERT OR UPDATE OR DELETE ON ordens_servico
    FOR EACH ROW EXECUTE FUNCTION fn_auditoria_generica();
 
CREATE TRIGGER tg_auditoria_itens_os
    AFTER INSERT OR UPDATE OR DELETE ON itens_ordem_servico
    FOR EACH ROW EXECUTE FUNCTION fn_auditoria_generica();

-- Validação rápida: liste as tabelas criadas
--   \dt
-- Liste os índices:
--   \di
-- Veja o detalhe de uma tabela:
--   \d ordens_servico