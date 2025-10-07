import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import { MdHome } from 'react-icons/md';
import { LoginPage } from '@/features/auth/pages/LoginPage';
import { CallbackPage } from '@/features/auth/pages/CallbackPage';
import { ProtectedRoute } from '@/shared/components/ProtectedRoute';
import { UserProfile } from '@/features/auth/components/UserProfile';
import { ROUTES } from '@/shared/constants/routes.constants';

const HomePage = () => (
    <div className="min-h-screen bg-gray-50">
        <header className="bg-white shadow">
            <div className="max-w-7xl mx-auto px-4 py-4 flex items-center justify-between">
                <div className="flex items-center gap-2">
                    <MdHome className="w-6 h-6 text-primary-600" />
                    <h1 className="text-2xl font-bold text-gray-900">OrdersPoc</h1>
                </div>
                <UserProfile />
            </div>
        </header>
        <main className="max-w-7xl mx-auto px-4 py-8">
            <div className="card">
                <h2 className="text-xl font-semibold mb-4">Bem-vindo!</h2>
                <p className="text-gray-600">
                    Sistema de gerenciamento de pedidos e clientes está pronto.
                </p>
            </div>
        </main>
    </div>
);

export const AppRouter = () => {
    return (
        <BrowserRouter>
            <Routes>
                <Route path={ROUTES.LOGIN} element={<LoginPage />} />
                <Route path={ROUTES.CALLBACK} element={<CallbackPage />} />

                <Route
                    path={ROUTES.HOME}
                    element={
                        <ProtectedRoute>
                            <HomePage />
                        </ProtectedRoute>
                    }
                />

                <Route path="*" element={<Navigate to={ROUTES.HOME} replace />} />
            </Routes>
        </BrowserRouter>
    );
};
