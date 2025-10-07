import { FcGoogle } from 'react-icons/fc';
import { authService } from '../services/authService';

export const LoginButton = () => {
    const handleLogin = () => {
        authService.redirectToGoogle();
    };

    return (
        <button
            onClick={handleLogin}
            className="btn border-2 border-solid border-primary-500 rounded-lg p-4 flex items-center justify-center gap-3 w-full hover:bg-gray-50"
        >
            <FcGoogle className="w-6 h-6" />
            Entrar com Google
        </button>
    );
};
