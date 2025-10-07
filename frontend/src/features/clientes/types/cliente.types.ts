export interface ClienteDto {
    id: string;
    nome: string;
    email: string;
    tipoPessoa: number;
    cpfCnpj?: string;
    telefone?: string;
    endereco?: string;
    cidade?: string;
    estado?: string;
    cep?: string;
    ativo: boolean;
    criadoEm: string;
    atualizadoEm: string | null;
}

export interface CreateClienteDto {
    nome: string;
    email: string;
    tipoPessoa: number;
    cpfCnpj?: string;
    telefone?: string;
    endereco?: string;
    cidade?: string;
    estado?: string;
    cep?: string;
}

export interface UpdateClienteDto {
    nome: string;
    email: string;
    telefone?: string;
    endereco?: string;
    cidade?: string;
    estado?: string;
    cep?: string;
}

export enum TipoPessoa {
    Fisica = 1,
    Juridica = 2,
}
