import { ReactNode } from 'react';
import { AuthProvider as OidcAuthProvider } from 'react-oidc-context';
import { oidcConfig } from '@/core/config/oidc.config';

interface AuthProviderProps {
    children: ReactNode;
}

export const AuthProvider = ({ children }: AuthProviderProps) => {
    const onSigninCallback = (): void => {
        console.log('onSigninCallback triggered');
        window.history.replaceState(
            {},
            document.title,
            window.location.pathname
        );
    };

    const onRemoveUser = () => {
        console.log('User removed from storage');
    };

    return (
        <OidcAuthProvider
            {...oidcConfig}
            onSigninCallback={onSigninCallback}
            onRemoveUser={onRemoveUser}
        >
            {children}
        </OidcAuthProvider>
    );
};
