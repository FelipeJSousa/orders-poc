export const STATUS_PEDIDO = {
    PENDENTE: 1,
    CONFIRMADO: 2,
    EM_PROCESSAMENTO: 3,
    ENVIADO: 4,
    ENTREGUE: 5,
    CANCELADO: 6,
} as const;

export const STATUS_PEDIDO_LABELS: Record<number, string> = {
    [STATUS_PEDIDO.PENDENTE]: 'Pendente',
    [STATUS_PEDIDO.CONFIRMADO]: 'Confirmado',
    [STATUS_PEDIDO.EM_PROCESSAMENTO]: 'Em Processamento',
    [STATUS_PEDIDO.ENVIADO]: 'Enviado',
    [STATUS_PEDIDO.ENTREGUE]: 'Entregue',
    [STATUS_PEDIDO.CANCELADO]: 'Cancelado',
};

export const STATUS_PEDIDO_COLORS: Record<number, string> = {
    [STATUS_PEDIDO.PENDENTE]: 'badge-warning',
    [STATUS_PEDIDO.CONFIRMADO]: 'badge-info',
    [STATUS_PEDIDO.EM_PROCESSAMENTO]: 'badge-info',
    [STATUS_PEDIDO.ENVIADO]: 'badge-info',
    [STATUS_PEDIDO.ENTREGUE]: 'badge-success',
    [STATUS_PEDIDO.CANCELADO]: 'badge-danger',
};
