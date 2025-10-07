import { z } from 'zod';

export const createClienteSchema = z.object({
    nome: z.string()
        .min(3, 'Nome deve ter no mínimo 3 caracteres')
        .max(200, 'Nome deve ter no máximo 200 caracteres'),
    email: z.string()
        .email('Email inválido')
        .max(200, 'Email deve ter no máximo 200 caracteres'),
    tipoPessoa: z.number()
        .min(1, 'Selecione o tipo de pessoa')
        .max(2, 'Tipo de pessoa inválido'),
    cpfCnpj: z.string().optional(),
    telefone: z.string().optional(),
    endereco: z.string().max(500, 'Endereço muito longo').optional(),
    cidade: z.string().max(100, 'Cidade muito longa').optional(),
    estado: z.string().length(2, 'Estado deve ter 2 caracteres').optional().or(z.literal('')),
    cep: z.string().max(10, 'CEP inválido').optional(),
});

export const updateClienteSchema = z.object({
    nome: z.string()
        .min(3, 'Nome deve ter no mínimo 3 caracteres')
        .max(200, 'Nome deve ter no máximo 200 caracteres'),
    email: z.email(),
    telefone: z.string().optional(),
    endereco: z.string().max(500, 'Endereço muito longo').optional(),
    cidade: z.string().max(100, 'Cidade muito longa').optional(),
    estado: z.string().length(2, 'Estado deve ter 2 caracteres').optional().or(z.literal('')),
    cep: z.string().max(10, 'CEP inválido').optional(),
});

export type CreateClienteFormData = z.infer<typeof createClienteSchema>;
export type UpdateClienteFormData = z.infer<typeof updateClienteSchema>;
