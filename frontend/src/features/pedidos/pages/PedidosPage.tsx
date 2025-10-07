import { useState } from 'react';
import { MdAdd } from 'react-icons/md';
import { MainLayout } from '@/shared/components/layout';
import { Button, Card, Loading, Modal } from '@/shared/components/ui';
import { usePedidos, useCreatePedido, useUpdatePedidoStatus } from '../hooks/usePedidos';
import { PedidoList } from '../components/PedidoList';
import { PedidoForm } from '../components/PedidoForm';
import { PedidoDetail } from '../components/PedidoDetail';
import { UpdateStatusModal } from '../components/UpdateStatusModal';
import { CreatePedidoFormData } from '../validation/pedidoSchema';
import { useToastContext } from "@/shared/context/ToastContext.tsx";
import { PedidoDto } from "@/features/pedidos/types/pedido.type.ts";

export const PedidosPage = () => {
    const [isCreateModalOpen, setIsCreateModalOpen] = useState(false);
    const [isViewModalOpen, setIsViewModalOpen] = useState(false);
    const [isStatusModalOpen, setIsStatusModalOpen] = useState(false);
    const [selectedPedido, setSelectedPedido] = useState<PedidoDto | null>(null);

    const { data: pedidos, isLoading } = usePedidos();
    const createMutation = useCreatePedido();
    const updateStatusMutation = useUpdatePedidoStatus();
    const toast = useToastContext();

    const handleCreate = async (data: CreatePedidoFormData) => {
        try {
            await createMutation.mutateAsync(data);
            toast.success('Pedido criado com sucesso!');
            setIsCreateModalOpen(false);
        } catch {
            toast.error('Erro ao criar pedido');
        }
    };

    const handleView = (pedido: PedidoDto) => {
        setSelectedPedido(pedido);
        setIsViewModalOpen(true);
    };

    const handleUpdateStatusClick = (pedido: PedidoDto) => {
        setSelectedPedido(pedido);
        setIsStatusModalOpen(true);
    };

    const handleUpdateStatus = async (newStatus: number) => {
        if (!selectedPedido) return;

        try {
            await updateStatusMutation.mutateAsync({
                id: selectedPedido.id,
                status: newStatus,
            });
            toast.success('Status atualizado com sucesso!');
            setIsStatusModalOpen(false);
            setSelectedPedido(null);
        } catch {
            toast.error('Erro ao atualizar status');
        }
    };

    return (
        <MainLayout>
            <div className="space-y-6">
                <div className="flex items-center justify-between">
                    <div>
                        <h1 className="text-3xl font-bold text-gray-900">Pedidos</h1>
                        <p className="text-gray-600 mt-1">Gerencie seus pedidos</p>
                    </div>
                    <Button onClick={() => setIsCreateModalOpen(true)}>
                        <MdAdd className="w-5 h-5" />
                        Novo Pedido
                    </Button>
                </div>

                <Card>
                    {isLoading ? (
                        <div className="py-12">
                            <Loading text="Carregando pedidos..." />
                        </div>
                    ) : (
                        <PedidoList
                            pedidos={pedidos || []}
                            onView={handleView}
                            onUpdateStatus={handleUpdateStatusClick}
                        />
                    )}
                </Card>
            </div>

            <Modal
                isOpen={isCreateModalOpen}
                onClose={() => setIsCreateModalOpen(false)}
                title="Novo Pedido"
                size="xl"
            >
                <PedidoForm onSubmit={handleCreate} isLoading={createMutation.isPending} />
            </Modal>

            <Modal
                isOpen={isViewModalOpen}
                onClose={() => {
                    setIsViewModalOpen(false);
                    setSelectedPedido(null);
                }}
                title="Detalhes do Pedido"
                size="xl"
            >
                {selectedPedido && <PedidoDetail pedido={selectedPedido} />}
            </Modal>

            <Modal
                isOpen={isStatusModalOpen}
                onClose={() => {
                    setIsStatusModalOpen(false);
                    setSelectedPedido(null);
                }}
                title="Atualizar Status do Pedido"
                size="md"
            >
                {selectedPedido && (
                    <UpdateStatusModal
                        currentStatus={selectedPedido.status}
                        onSubmit={handleUpdateStatus}
                        isLoading={updateStatusMutation.isPending}
                    />
                )}
            </Modal>
        </MainLayout>
    );
};
