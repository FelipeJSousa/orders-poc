import { z } from 'zod';

export const pedidoItemSchema = z.object({
    produto: z.string().min(3, 'Nome do produto deve ter no mínimo 3 caracteres'),
    quantidade: z.number().min(1, 'Quantidade deve ser no mínimo 1'),
    precoUnitario: z.number().min(0.01, 'Preço deve ser maior que zero'),
});

export const createPedidoSchema = z.object({
    clienteId: z.string().uuid('Selecione um cliente válido'),
    observacoes: z.string().max(500, 'Observações muito longas').optional(),
    itens: z.array(pedidoItemSchema).min(1, 'Adicione pelo menos um item'),
});

export type CreatePedidoFormData = z.infer<typeof createPedidoSchema>;
export type PedidoItemFormData = z.infer<typeof pedidoItemSchema>;
