import { httpClient } from '@/core/api/httpClient';
import { CreateClienteDto, UpdateClienteDto, ClienteDto } from '../types/cliente.types';

const BASE_URL = '/clientes';

export const clienteService = {
    async getAll(): Promise<ClienteDto[]> {
        const response = await httpClient.get<ClienteDto[]>(BASE_URL);
        return response.data;
    },

    async getById(id: string): Promise<ClienteDto> {
        const response = await httpClient.get<ClienteDto>(`${BASE_URL}/${id}`);
        return response.data;
    },

    async create(data: CreateClienteDto): Promise<ClienteDto> {
        const response = await httpClient.post<ClienteDto>(BASE_URL, data);
        return response.data;
    },

    async update(id: string, data: UpdateClienteDto): Promise<ClienteDto> {
        const response = await httpClient.put<ClienteDto>(`${BASE_URL}/${id}`, data);
        return response.data;
    },

    async delete(id: string): Promise<void> {
        await httpClient.delete(`${BASE_URL}/${id}`);
    },
};
