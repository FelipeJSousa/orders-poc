import axios, { AxiosError } from 'axios';
import { envConfig } from '../config/env.config';

export const httpClient = axios.create({
    baseURL: envConfig.apiBaseUrl,
    headers: {
        'Content-Type': 'application/json',
    },
    timeout: 30000, // 30 segundos
});

httpClient.interceptors.request.use(
    (config) => {
        const token = localStorage.getItem('auth_token');

        if (token) {
            config.headers.Authorization = `Bearer ${token}`;
        }

        console.log(`[API Request] ${config.method?.toUpperCase()} ${config.url}`);
        return config;
    },
    (error) => {
        console.error('[API Request Error]', error);
        return Promise.reject(error);
    }
);

httpClient.interceptors.response.use(
    (response) => {
        console.log(`[API Response] ${response.config.method?.toUpperCase()} ${response.config.url} - ${response.status}`);
        return response;
    },
    (error: AxiosError) => {
        console.error('[API Response Error]', error);

        if (error.response?.status === 401) {
            console.warn('Token inválido ou expirado. Redirecionando para login...');
            localStorage.removeItem('auth_token');
            localStorage.removeItem('auth_user');
            window.location.href = '/login';
        }

        if (error.response?.status === 403) {
            console.error('Acesso negado');
        }

        if (error.response?.status && error.response.status >= 500) {
            console.error('Erro no servidor');
        }

        return Promise.reject(error);
    }
);

export interface ApiError {
    message: string;
    statusCode?: number;
    errors?: Record<string, string[]>;
}

export const getApiError = (error: unknown): ApiError => {
    if (axios.isAxiosError(error)) {
        const axiosError = error as AxiosError<unknown>;
        const data = axiosError.response?.data as { message?: string; errors?: Record<string, string[]> } | undefined;
        return {
            message: data?.message || axiosError.message || 'Erro desconhecido',
            statusCode: axiosError.response?.status,
            errors: data?.errors,
        };
    }

    return {
        message: 'Erro desconhecido',
    };
};
