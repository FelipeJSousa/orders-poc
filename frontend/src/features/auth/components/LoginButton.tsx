import { FcGoogle } from 'react-icons/fc';
import { authService } from '../services/authService';

export const LoginButton = () => {
    const handleLogin = () => {
        authService.redirectToGoogle();
    };

    return (
        <button
            onClick={handleLogin}
            className="btn-primary flex items-center justify-center gap-3 w-full"
        >
            <FcGoogle className="w-6 h-6" />
            Entrar com Google
        </button>
    );
};
