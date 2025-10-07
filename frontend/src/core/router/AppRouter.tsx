import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import { LoginPage } from '@/features/auth/pages/LoginPage';
import { CallbackPage } from '@/features/auth/pages/CallbackPage';
import { ProtectedRoute } from '@/shared/components/ProtectedRoute';
import { ROUTES } from '@/shared/constants/routes.constants';
import { MainLayout } from "@/shared/components/layout";
import { Card } from "@/shared/components/ui";
import { ClientesPage } from "@/features/clientes/pages/ClientesPage.tsx";
import { PedidosPage } from "@/features/pedidos/pages/PedidosPage.tsx";

const HomePage = () => (
    <MainLayout>
        <Card title="Bem-vindo ao OrdersPoc!">
            <p className="text-gray-600">
                Sistema de gerenciamento de pedidos e clientes está pronto.
            </p>
        </Card>
    </MainLayout>
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

                <Route
                    path="/clientes"
                    element={
                        <ProtectedRoute>
                            <ClientesPage />
                        </ProtectedRoute>
                    }
                />

                <Route
                    path="/pedidos"
                    element={
                        <ProtectedRoute>
                            <PedidosPage />
                        </ProtectedRoute>
                    }
                />

                <Route path="*" element={<Navigate to={ROUTES.HOME} replace />} />
            </Routes>
        </BrowserRouter>
    );
};
