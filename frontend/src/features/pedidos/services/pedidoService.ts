import { httpClient } from '@/core/api/httpClient';
import {CreatePedidoDto, PedidoDto} from "@/features/pedidos/types/pedido.type.ts";

const BASE_URL = '/pedidos';

export const pedidoService = {
    async getById(id: string): Promise<PedidoDto> {
        const response = await httpClient.get<PedidoDto>(`${BASE_URL}/${id}`);
        return response.data;
    },

    async get(): Promise<PedidoDto[]> {
        const response = await httpClient.get<PedidoDto[]>(BASE_URL);
        return response.data;
    },

    async create(data: CreatePedidoDto): Promise<PedidoDto> {
        const response = await httpClient.post<PedidoDto>(BASE_URL, data);
        return response.data;
    },

    async updateStatus(id: string, status: number): Promise<PedidoDto> {
        const response = await httpClient.put<PedidoDto>(`${BASE_URL}/${id}/status/${status}`);
        return response.data;
    },
};
