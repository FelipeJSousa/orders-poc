import { createContext, useContext, ReactNode } from 'react';
import { useToast } from '@/shared/hooks/useToast';
import { ToastContainer } from '@/shared/components/ToastContainer';

interface ToastContextValue {
    success: (message: string) => void;
    error: (message: string) => void;
    warning: (message: string) => void;
    info: (message: string) => void;
}

const ToastContext = createContext<ToastContextValue | undefined>(undefined);

export const useToastContext = () => {
    const context = useContext(ToastContext);
    if (!context) {
        throw new Error('useToastContext must be used within ToastProvider');
    }
    return context;
};

interface ToastProviderProps {
    children: ReactNode;
}

export const ToastProvider = ({ children }: ToastProviderProps) => {
    const toast = useToast();

    return (
        <ToastContext.Provider
            value={{
                success: toast.success,
                error: toast.error,
                warning: toast.warning,
                info: toast.info,
            }}
        >
            {children}
            <ToastContainer />
        </ToastContext.Provider>
    );
};
