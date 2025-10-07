export { useAuthContext } from '../context/AuthContext';
import { User } from '../types/auth.types';

export const useAuth = () => {
    const auth = useAuthContext();

    const user: User | null = auth.user
        ? {
            id: auth.user.profile.sub || '',
            name: auth.user.profile.name || '',
            email: auth.user.profile.email || '',
            picture: auth.user.profile.picture,
        }
        : null;

    const login = () => {
        auth.signinRedirect().catch((error) => {
            console.error('Error during signinRedirect:', error);
        });
    };

    const logout = () => {
        auth.signoutRedirect();
    };

    return {
        user,
        isAuthenticated: auth.isAuthenticated,
        isLoading: auth.isLoading,
        accessToken: auth.user?.access_token || null,
        login,
        logout,
        error: auth.error,
    };
};
