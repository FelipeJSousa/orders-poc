import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import { ReactQueryDevtools } from '@tanstack/react-query-devtools';

const queryClient = new QueryClient({
    defaultOptions: {
        queries: {
            retry: 1,
            refetchOnWindowFocus: false,
            staleTime: 5 * 60 * 1000, // 5 minutes
        },
    },
});

function App() {
    return (
        <QueryClientProvider client={queryClient}>
            <div className="min-h-screen bg-gray-50">
                <header className="bg-white shadow">
                    <div className="max-w-7xl mx-auto px-4 py-6">
                        <h1 className="text-3xl font-bold text-gray-900">Orders Poc</h1>
                    </div>
                </header>
                <main className="max-w-7xl mx-auto px-4 py-6">
                    <div className="card">
                        <h2 className="text-2xl font-semibold mb-4">
                            Bem-vindo ao Orders Poc
                        </h2>
                        <p className="text-gray-600">
                            Sistema de gerenciamento de pedidos e clientes
                        </p>
                    </div>
                </main>
            </div>
            <ReactQueryDevtools initialIsOpen={false} />
        </QueryClientProvider>
    );
}

export default App;
