import { AiOutlineLoading3Quarters } from 'react-icons/ai';

interface LoadingProps {
    size?: 'sm' | 'md' | 'lg';
    text?: string;
}

export const Loading = ({ size = 'md', text }: LoadingProps) => {
    const sizeClasses = {
        sm: 'w-4 h-4',
        md: 'w-8 h-8',
        lg: 'w-12 h-12',
    };

    return (
        <div className="flex flex-col items-center justify-center gap-3">
            <AiOutlineLoading3Quarters
                className={`${sizeClasses[size]} animate-spin text-primary-600`}
            />
            {text && <p className="text-gray-600 text-sm">{text}</p>}
        </div>
    );
};
