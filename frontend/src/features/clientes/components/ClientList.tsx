import { MdEdit, MdDelete, MdVisibility } from 'react-icons/md';
import { Table, Badge } from '@/shared/components/ui';
import { ClienteDto } from '../types/cliente.types';

interface ClienteListProps {
    clientes: ClienteDto[];
    onView: (cliente: ClienteDto) => void;
    onEdit: (cliente: ClienteDto) => void;
    onDelete: (cliente: ClienteDto) => void;
}

export const ClienteList = ({ clientes, onView, onEdit, onDelete }: ClienteListProps) => {
    const columns = [
        {
            key: 'nome',
            title: 'Nome',
        },
        {
            key: 'email',
            title: 'Email',
        },
        {
            key: 'tipoPessoa',
            title: 'Tipo',
            render: (value: unknown) => (
                <Badge variant={value === 1 ? 'info' : 'success'}>
                    {value === 1 ? 'PF' : 'PJ'}
                </Badge>
            ),
        },
        {
            key: 'telefone',
            title: 'Telefone',
            render: (value: unknown) => (value as string) || '-',
        },
        {
            key: 'ativo',
            title: 'Status',
            render: (value: unknown) => (
                <Badge variant={value ? 'success' : 'danger'}>
                    {value ? 'Ativo' : 'Inativo'}
                </Badge>
            ),
        },
        {
            key: 'actions',
            title: 'Ações',
            render: (_: unknown, row: ClienteDto) => (
                <div className="flex gap-2">
                    <button
                        onClick={() => onView(row)}
                        className="text-blue-600 hover:text-blue-800"
                        title="Visualizar"
                    >
                        <MdVisibility className="w-5 h-5" />
                    </button>
                    <button
                        onClick={() => onEdit(row)}
                        className="text-yellow-600 hover:text-yellow-800"
                        title="Editar"
                    >
                        <MdEdit className="w-5 h-5" />
                    </button>
                    <button
                        onClick={() => onDelete(row)}
                        className="text-red-600 hover:text-red-800"
                        title="Excluir"
                    >
                        <MdDelete className="w-5 h-5" />
                    </button>
                </div>
            ),
        },
    ];

    return <Table<ClienteDto> columns={columns} data={clientes} emptyMessage="Nenhum cliente encontrado" />;
};
