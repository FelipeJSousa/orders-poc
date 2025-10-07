import { useState } from 'react';
import { MdAdd } from 'react-icons/md';
import { MainLayout } from '@/shared/components/layout';
import { Button, Card, Loading, Modal } from '@/shared/components/ui';
import { ClienteDto } from "@/features/clientes/types/cliente.types.ts";
import { useToastContext } from "@/shared/context/ToastContext.helpers";
import { CreateClienteFormData } from "@/features/clientes/validation/clienteSchema.ts";
import { useClientes, useCreateCliente, useDeleteCliente, useUpdateCliente } from "@/features/clientes/hooks/useClientes.ts";
import { ClienteForm } from "@/features/clientes/components/ClienteForm.tsx";
import { ClienteDetail } from "@/features/clientes/components/clientDetail.tsx";
import { ClienteList } from "@/features/clientes/components/ClientList.tsx";

export const ClientesPage = () => {
    const [isCreateModalOpen, setIsCreateModalOpen] = useState(false);
    const [isEditModalOpen, setIsEditModalOpen] = useState(false);
    const [isViewModalOpen, setIsViewModalOpen] = useState(false);
    const [selectedCliente, setSelectedCliente] = useState<ClienteDto | null>(null);

    const { data: clientes, isLoading } = useClientes();
    const createMutation = useCreateCliente();
    const updateMutation = useUpdateCliente();
    const deleteMutation = useDeleteCliente();
    const toast = useToastContext();

    const handleCreate = async (data: CreateClienteFormData) => {
        try {
            await createMutation.mutateAsync(data);
            toast.success('Cliente criado com sucesso!');
            setIsCreateModalOpen(false);
        } catch {
            toast.error('Erro ao criar cliente');
        }
    };

    const handleUpdate = async (data: CreateClienteFormData) => {
        if (!selectedCliente) return;

        try {
            await updateMutation.mutateAsync({
                id: selectedCliente.id,
                data: {
                    nome: data.nome,
                    email: data.email,
                    telefone: data.telefone,
                    endereco: data.endereco,
                    cidade: data.cidade,
                    estado: data.estado,
                    cep: data.cep,
                },
            });
            toast.success('Cliente atualizado com sucesso!');
            setIsEditModalOpen(false);
            setSelectedCliente(null);
        } catch {
            toast.error('Erro ao atualizar cliente');
        }
    };

    const handleDelete = async (cliente: ClienteDto) => {
        if (!confirm(`Deseja realmente deletar o cliente ${cliente.nome}?`)) return;

        try {
            await deleteMutation.mutateAsync(cliente.id);
            toast.success('Cliente deletado com sucesso!');
        } catch {
            toast.error('Erro ao deletar cliente');
        }
    };

    const handleView = (cliente: ClienteDto) => {
        setSelectedCliente(cliente);
        setIsViewModalOpen(true);
    };

    const handleEdit = (cliente: ClienteDto) => {
        setSelectedCliente(cliente);
        setIsEditModalOpen(true);
    };

    return (
        <MainLayout>
            <div className="space-y-6">
                <div className="flex items-center justify-between">
                    <div>
                        <h1 className="text-3xl font-bold text-gray-900">Clientes</h1>
                        <p className="text-gray-600 mt-1">Gerencie seus clientes</p>
                    </div>
                    <Button onClick={() => setIsCreateModalOpen(true)}>
                        <MdAdd className="w-5 h-5" />
                        Novo Cliente
                    </Button>
                </div>

                <Card>
                    {isLoading ? (
                        <div className="py-12">
                            <Loading text="Carregando clientes..." />
                        </div>
                    ) : (
                        <ClienteList
                            clientes={clientes || []}
                            onView={handleView}
                            onEdit={handleEdit}
                            onDelete={handleDelete}
                        />
                    )}
                </Card>
            </div>

            <Modal
                isOpen={isCreateModalOpen}
                onClose={() => setIsCreateModalOpen(false)}
                title="Novo Cliente"
                size="lg"
            >
                <ClienteForm
                    onSubmit={handleCreate}
                    isLoading={createMutation.isPending}
                />
            </Modal>

            <Modal
                isOpen={isEditModalOpen}
                onClose={() => {
                    setIsEditModalOpen(false);
                    setSelectedCliente(null);
                }}
                title="Editar Cliente"
                size="lg"
            >
                {selectedCliente && (
                    <ClienteForm
                        onSubmit={handleUpdate}
                        isLoading={updateMutation.isPending}
                        initialData={selectedCliente}
                    />
                )}
            </Modal>

            <Modal
                isOpen={isViewModalOpen}
                onClose={() => {
                    setIsViewModalOpen(false);
                    setSelectedCliente(null);
                }}
                title="Detalhes do Cliente"
                size="lg"
            >
                {selectedCliente && <ClienteDetail cliente={selectedCliente} />}
            </Modal>
        </MainLayout>
    );
};
