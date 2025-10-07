import { ReactNode } from 'react';

interface CardProps {
    children: ReactNode;
    title?: string;
    className?: string;
}

export const Card = ({ children, title, className = '' }: CardProps) => {
    return (
        <div className={`card ${className}`}>
            {title && <h3 className="text-lg font-semibold mb-4 text-gray-900">{title}</h3>}
            {children}
        </div>
    );
};
