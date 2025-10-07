import { MdShoppingCart } from 'react-icons/md';
import { UserProfile } from '@/features/auth/components/UserProfile';

export const Header = () => {
    return (
        <header className="bg-white shadow-sm border-b border-gray-200">
            <div className="max-w-7xl mx-auto px-4 py-4">
                <div className="flex items-center justify-between">
                    <div className="flex items-center gap-3">
                        <div className="bg-primary-600 p-2 rounded-lg">
                            <MdShoppingCart className="w-6 h-6 text-white" />
                        </div>
                        <div>
                            <h1 className="text-xl font-bold text-gray-900">OrdersPoc</h1>
                            <p className="text-xs text-gray-500">Sistema de Pedidos</p>
                        </div>
                    </div>
                    <UserProfile />
                </div>
            </div>
        </header>
    );
};
