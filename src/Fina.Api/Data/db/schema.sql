drop database if exists "fina-dev";
create database "fina-dev";

drop table if exists users cascade;
drop table if exists contas cascade;
drop table if exists categorias cascade;
drop table if exists sub_categorias cascade;
drop table if exists transacoes cascade;

create table users (
	id serial primary key,
	username varchar(50) not null unique,
	password_hash varchar(255) not null,
	email varchar(100) not null unique,
	created_at timestamp default current_timestamp
);

CREATE TABLE contas (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    nome VARCHAR(100) NOT NULL,
    tipo_conta VARCHAR(20) NOT NULL,
    saldo NUMERIC(18, 2) NOT NULL DEFAULT 0.00,
    limite NUMERIC(18, 2) NOT NULL DEFAULT 0.00,
    ativo BOOLEAN NOT NULL DEFAULT TRUE,
    usuario_id UUID NOT NULL,
    criado_em TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT (timezone('utc', now()))
);

CREATE TABLE categorias (
	id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    nome VARCHAR(100) NOT NULL,
    descricao VARCHAR(255) NOT NULL,
    tipo_categoria VARCHAR(20) NOT NULL,
    icon VARCHAR(20) NOT NULL,
    color VARCHAR(20) NOT NULL,
    is_default BOOLEAN NOT NULL DEFAULT FALSE,
    ativo BOOLEAN NOT NULL DEFAULT TRUE,
	usuario_id UUID NULL,
	criado_em TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT (timezone('utc', now()))
);

CREATE TABLE sub_categorias (
	id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    nome VARCHAR(100) NOT NULL,
	usuario_id UUID NULL,
	criado_em TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT (timezone('utc', now()))
);

CREATE TABLE transacoes (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    titulo VARCHAR(255) NOT NULL,
    descricao TEXT NULL,
    pago_recebido_em TIMESTAMP WITH TIME ZONE, -- DateTime? (Opcional)
    tipo_transacao VARCHAR(20) NOT NULL,
    valor NUMERIC(18, 2) NOT NULL,              -- Mapeamento ideal para decimal
    status_transacao VARCHAR(20) NOT NULL DEFAULT 'Pendente',
    categoria_id UUID NOT NULL,
    sub_categoria_id UUID,                      -- Guid? (Opcional)
    forma_pagamento_recebimento VARCHAR(100),   -- string? (Opcional)
    origem_pagamento_recebimento VARCHAR(100),  -- string? (Opcional)
    usuario_id UUID NOT NULL,
    criado_em TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT (timezone('utc', now())),
    
    -- 3. Definição das Chaves Estrangeiras (Constraints)
    CONSTRAINT fk_transacoes_categoria 
        FOREIGN KEY (categoria_id) 
        REFERENCES categorias(id) 
        ON DELETE RESTRICT,
        
    CONSTRAINT fk_transacoes_sub_categoria 
        FOREIGN KEY (sub_categoria_id) 
        REFERENCES sub_categorias(id) 
        ON DELETE SET NULL
);

-- 4. Criação de Índices para Otimização de Busca
CREATE INDEX idx_transacoes_usuario_id ON transacoes(usuario_id);
CREATE INDEX idx_transacoes_categoria_id ON transacoes(categoria_id);