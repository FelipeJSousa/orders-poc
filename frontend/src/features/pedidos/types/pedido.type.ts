export interface PedidoDto {
    id: string;
    numeroPedido: string;
    clienteId: string;
    clienteNome: string;
    dataPedido: string;
    status: number;
    statusDescricao: string;
    valorTotal: number;
    observacoes?: string;
    itens: PedidoItemDto[];
    criadoEm: string;
    atualizadoEm: string | null;
}

export interface PedidoItemDto {
    id: string;
    produto: string;
    quantidade: number;
    precoUnitario: number;
    subtotal: number;
}

export interface CreatePedidoDto {
    clienteId: string;
    observacoes?: string;
    itens: CreatePedidoItemDto[];
}

export interface CreatePedidoItemDto {
    produto: string;
    quantidade: number;
    precoUnitario: number;
}

export enum StatusPedido {
    Pendente = 1,
    Confirmado = 2,
    EmProcessamento = 3,
    Enviado = 4,
    Entregue = 5,
    Cancelado = 6,
}
