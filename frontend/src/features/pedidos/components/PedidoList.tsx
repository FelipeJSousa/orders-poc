import { MdVisibility, MdEdit } from 'react-icons/md';
import { Table } from '@/shared/components/ui';
import { StatusBadge } from './StatusBadge';
import { formatCurrency, formatDateTime } from '@/shared/utils/format';
import { PedidoDto } from "@/features/pedidos/types/pedido.type.ts";

interface PedidoListProps {
    pedidos: PedidoDto[];
    onView: (pedido: PedidoDto) => void;
    onUpdateStatus: (pedido: PedidoDto) => void;
}

export const PedidoList = ({ pedidos, onView, onUpdateStatus }: PedidoListProps) => {
    const columns = [
        {
            key: 'numeroPedido',
            title: 'Número',
            render: (value: string) => <span className="font-mono">{value}</span>,
        },
        {
            key: 'clienteNome',
            title: 'Cliente',
        },
        {
            key: 'dataPedido',
            title: 'Data',
            render: (value: string) => formatDateTime(value),
        },
        {
            key: 'status',
            title: 'Status',
            render: (value: number) => <StatusBadge status={value} />,
        },
        {
            key: 'valorTotal',
            title: 'Valor Total',
            render: (value: number) => (
                <span className="font-semibold text-primary-600">{formatCurrency(value)}</span>
            ),
        },
        {
            key: 'actions',
            title: 'Ações',
            render: (_: any, row: PedidoDto) => (
                <div className="flex gap-2">
                    <button
                        onClick={() => onView(row)}
                        className="text-blue-600 hover:text-blue-800"
                        title="Visualizar"
                    >
                        <MdVisibility className="w-5 h-5" />
                    </button>
                    <button
                        onClick={() => onUpdateStatus(row)}
                        className="text-yellow-600 hover:text-yellow-800"
                        title="Alterar Status"
                    >
                        <MdEdit className="w-5 h-5" />
                    </button>
                </div>
            ),
        },
    ];

    return <Table columns={columns} data={pedidos} emptyMessage="Nenhum pedido encontrado" />;
};
