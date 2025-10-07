import { ToastContext } from './ToastContext.helpers';
export { useToastContext } from './ToastContext.helpers';
import { ReactNode } from 'react';
import { ToastContainer } from '@/shared/components/ToastContainer';
import { useToast } from '@/shared/hooks/useToast';

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
