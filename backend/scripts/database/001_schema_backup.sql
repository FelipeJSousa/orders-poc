
CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE public."Clientes" (
    "Id" uuid NOT NULL,
    "Nome" character varying(200) NOT NULL,
    "Email" character varying(255) NOT NULL,
    "TipoPessoa" integer NOT NULL,
    "CpfCnpj" character varying(14),
    "Telefone" character varying(20),
    "Endereco" character varying(300),
    "Cidade" character varying(100),
    "Estado" character varying(2),
    "Cep" character varying(8),
    "CriadoEm" timestamp with time zone NOT NULL,
    "AtualizadoEm" timestamp with time zone,
    "Ativo" boolean NOT NULL DEFAULT TRUE,
    CONSTRAINT "PK_Clientes" PRIMARY KEY ("Id")
);

CREATE TABLE public."Pedidos" (
    "Id" uuid NOT NULL,
    "NumeroPedido" character varying(50) NOT NULL,
    "ClienteId" uuid NOT NULL,
    "DataPedido" timestamp with time zone NOT NULL,
    "Status" integer NOT NULL,
    "ValorTotal" numeric(18,2) NOT NULL,
    "Moeda" character varying(3) NOT NULL DEFAULT 'BRL',
    "Observacoes" character varying(500),
    "CriadoEm" timestamp with time zone NOT NULL,
    "AtualizadoEm" timestamp with time zone,
    "Ativo" boolean NOT NULL DEFAULT TRUE,
    CONSTRAINT "PK_Pedidos" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_Pedidos_Clientes_ClienteId" FOREIGN KEY ("ClienteId") REFERENCES public."Clientes" ("Id") ON DELETE RESTRICT
);

CREATE TABLE public."PedidoItens" (
    "Id" uuid NOT NULL,
    "PedidoId" uuid NOT NULL,
    "Produto" character varying(200) NOT NULL,
    "Quantidade" integer NOT NULL,
    "PrecoUnitario" numeric(18,2) NOT NULL,
    "MoedaPreco" character varying(3) NOT NULL DEFAULT 'BRL',
    "Subtotal" numeric(18,2) NOT NULL,
    "MoedaSubtotal" character varying(3) NOT NULL DEFAULT 'BRL',
    "CriadoEm" timestamp with time zone NOT NULL,
    "AtualizadoEm" timestamp with time zone,
    "Ativo" boolean NOT NULL DEFAULT TRUE,
    CONSTRAINT "PK_PedidoItens" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_PedidoItens_Pedidos_PedidoId" FOREIGN KEY ("PedidoId") REFERENCES public."Pedidos" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_Clientes_Ativo" ON public."Clientes" ("Ativo");

CREATE INDEX "IX_Clientes_CpfCnpj" ON public."Clientes" ("CpfCnpj");

CREATE UNIQUE INDEX "IX_Clientes_Email" ON public."Clientes" ("Email");

CREATE INDEX "IX_PedidoItens_PedidoId" ON public."PedidoItens" ("PedidoId");

CREATE INDEX "IX_Pedidos_ClienteId" ON public."Pedidos" ("ClienteId");

CREATE INDEX "IX_Pedidos_DataPedido" ON public."Pedidos" ("DataPedido");

CREATE UNIQUE INDEX "IX_Pedidos_NumeroPedido" ON public."Pedidos" ("NumeroPedido");

CREATE INDEX "IX_Pedidos_Status" ON public."Pedidos" ("Status");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20251007003305_InitialCreate', '8.0.0');

COMMIT;