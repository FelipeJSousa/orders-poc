import { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { MdShoppingCart } from 'react-icons/md';
import { LoginButton } from "@/features/auth/components/LoginButton.tsx";
import { useAuthContext } from "@/features/auth/hooks/useAuth.ts";

export const LoginPage = () => {
    const { isAuthenticated } = useAuthContext();
    const navigate = useNavigate();

    useEffect(() => {
        if (isAuthenticated) {
            navigate('/');
        }
    }, [isAuthenticated, navigate]);

    return (
        <div className="min-h-screen flex items-center justify-center bg-gradient-to-br from-primary-50 to-primary-100">
            <div className="card max-w-md w-full mx-4">
                <div className="text-center mb-8">
                    <div className="inline-flex items-center justify-center w-16 h-16 bg-primary-600 rounded-full mb-4">
                        <MdShoppingCart className="w-8 h-8 text-white" />
                    </div>
                    <h1 className="text-3xl font-bold text-gray-900 mb-2">OrdersPoc</h1>
                    <p className="text-gray-600">
                        Sistema de gerenciamento de pedidos e clientes
                    </p>
                </div>
                <LoginButton />
            </div>
        </div>
    );
};
