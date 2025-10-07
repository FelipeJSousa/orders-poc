import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { clienteService } from '../services/clienteService';
import { CreateClienteDto, UpdateClienteDto } from '../types/cliente.types';
import { getApiError } from '@/core/api/httpClient';

export const CLIENTE_KEYS = {
    all: ['clientes'] as const,
    detail: (id: string) => ['clientes', id] as const,
};

export const useClientes = () => {
    return useQuery({
        queryKey: CLIENTE_KEYS.all,
        queryFn: () => clienteService.getAll(),
    });
};

export const useCliente = (id: string) => {
    return useQuery({
        queryKey: CLIENTE_KEYS.detail(id),
        queryFn: () => clienteService.getById(id),
        enabled: !!id,
    });
};

export const useCreateCliente = () => {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: (data: CreateClienteDto) => clienteService.create(data),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: CLIENTE_KEYS.all });
        },
        onError: (error) => {
            const apiError = getApiError(error);
            console.error('Erro ao criar cliente:', apiError.message);
        },
    });
};

export const useUpdateCliente = () => {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: ({ id, data }: { id: string; data: UpdateClienteDto }) =>
            clienteService.update(id, data),
        onSuccess: (_, variables) => {
            queryClient.invalidateQueries({ queryKey: CLIENTE_KEYS.all });
            queryClient.invalidateQueries({ queryKey: CLIENTE_KEYS.detail(variables.id) });
        },
        onError: (error) => {
            const apiError = getApiError(error);
            console.error('Erro ao atualizar cliente:', apiError.message);
        },
    });
};

export const useDeleteCliente = () => {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: (id: string) => clienteService.delete(id),
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: CLIENTE_KEYS.all });
        },
        onError: (error) => {
            const apiError = getApiError(error);
            console.error('Erro ao deletar cliente:', apiError.message);
        },
    });
};
