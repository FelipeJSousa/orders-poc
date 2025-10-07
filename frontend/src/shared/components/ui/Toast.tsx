import { useEffect } from 'react';
import { MdClose, MdCheckCircle, MdError, MdWarning, MdInfo } from 'react-icons/md';

type ToastType = 'success' | 'error' | 'warning' | 'info';

interface ToastProps {
    message: string;
    type: ToastType;
    onClose: () => void;
    duration?: number;
}

export const Toast = ({ message, type, onClose, duration = 5000 }: ToastProps) => {
    useEffect(() => {
        const timer = setTimeout(() => {
            onClose();
        }, duration);

        return () => clearTimeout(timer);
    }, [duration, onClose]);

    const icons = {
        success: <MdCheckCircle className="w-5 h-5" />,
        error: <MdError className="w-5 h-5" />,
        warning: <MdWarning className="w-5 h-5" />,
        info: <MdInfo className="w-5 h-5" />,
    };

    const styles = {
        success: 'bg-green-50 text-green-800 border-green-200',
        error: 'bg-red-50 text-red-800 border-red-200',
        warning: 'bg-yellow-50 text-yellow-800 border-yellow-200',
        info: 'bg-blue-50 text-blue-800 border-blue-200',
    };

    return (
        <div className={`flex items-center gap-3 p-4 rounded-lg border ${styles[type]} shadow-lg`}>
            {icons[type]}
            <p className="flex-1 text-sm font-medium">{message}</p>
            <button onClick={onClose} className="text-current hover:opacity-70">
                <MdClose className="w-5 h-5" />
            </button>
        </div>
    );
};
