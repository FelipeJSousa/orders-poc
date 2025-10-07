import { useEffect, useState } from 'react';
import { useNavigate, useSearchParams } from 'react-router-dom';
import { AiOutlineLoading3Quarters } from 'react-icons/ai';
import { authService } from '../services/authService';
import {useAuth} from "@/features/auth/context/AuthContext.helpers";

export const CallbackPage = () => {
    const [searchParams] = useSearchParams();
    const navigate = useNavigate();
    const { setAuthData } = useAuth();
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        const processCallback = async () => {
            const code = searchParams.get('code');
            const errorParam = searchParams.get('error');

            if (errorParam) {
                setError(`Erro do Google: ${errorParam}`);
                return;
            }

            if (!code) {
                setError('Código de autorização não encontrado');
                return;
            }

            try {
                console.log('Enviando code para backend...');

                const response = await authService.exchangeCodeForToken(code);

                console.log('Autenticação bem-sucedida!', response);

                setAuthData(response.token, response.user);

                navigate('/');
            } catch (err: unknown) {
                const errorMsg = (err as { response?: { data?: { message?: string } } })?.response?.data?.message || 'Erro ao autenticar';
                console.error('Erro ao processar callback:', err);
                setError(errorMsg);
            }
        };

        processCallback();
    }, [searchParams, navigate, setAuthData]);

    if (error) {
        return (
            <div className="min-h-screen flex items-center justify-center bg-gray-50">
                <div className="card max-w-md text-center">
                    <h2 className="text-xl font-semibold mb-2">Erro na autenticação</h2>
                    <p className="text-red-600 mb-4">{error}</p>
                    <button
                        onClick={() => navigate('/login')}
                        className="btn-primary w-full"
                    >
                        Tentar novamente
                    </button>
                </div>
            </div>
        );
    }

    return (
        <div className="min-h-screen flex items-center justify-center bg-gray-50">
            <div className="card max-w-md text-center">
                <AiOutlineLoading3Quarters className="w-12 h-12 animate-spin mx-auto mb-4 text-primary-600" />
                <h2 className="text-xl font-semibold mb-2">Processando login...</h2>
                <p className="text-gray-600">Aguarde enquanto validamos suas credenciais.</p>
            </div>
        </div>
    );
};
