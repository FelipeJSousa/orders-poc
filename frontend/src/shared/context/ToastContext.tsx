import { ToastContext } from './ToastContext.helpers';
import { ToastContainer } from '@/shared/components/ToastContainer';

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
