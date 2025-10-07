import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { getApiError } from '@/core/api/httpClient';
import {pedidoService} from "@/features/pedidos/services/pedidoService.ts";
import {CreatePedidoDto} from "@/features/pedidos/types/pedido.type.ts";

export const PEDIDO_KEYS = {
    all: ['pedidos'] as const,
    detail: (id: string) => ['pedidos', id] as const,
};

export const usePedido = (id: string) => {
    return useQuery({
        queryKey: PEDIDO_KEYS.detail(id),
        queryFn: () => pedidoService.getById(id),
        enabled: !!id,
    });
};

export const usePedidos = () => {
    return useQuery({
        queryKey: PEDIDO_KEYS.all,
        queryFn: () => pedidoService.get(),
    });
};

export const useCreatePedido = () => {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: (data: CreatePedidoDto) => pedidoService.create(data),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: PEDIDO_KEYS.all });
        },
        onError: (error) => {
            const apiError = getApiError(error);
            console.error('Erro ao criar pedido:', apiError.message);
        },
    });
};

export const useUpdatePedidoStatus = () => {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: ({ id, status }: { id: string; status: number }) =>
            pedidoService.updateStatus(id, status),
        onSuccess: (data) => {
            queryClient.invalidateQueries({ queryKey: PEDIDO_KEYS.all });
            queryClient.invalidateQueries({ queryKey: PEDIDO_KEYS.detail(data.id) });
        },
        onError: (error) => {
            const apiError = getApiError(error);
            console.error('Erro ao atualizar status:', apiError.message);
        },
    });
};
