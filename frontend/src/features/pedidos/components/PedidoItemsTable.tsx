import { Table } from '@/shared/components/ui';
import { formatCurrency } from '@/shared/utils/format';
import {PedidoItemDto} from "@/features/pedidos/types/pedido.type.ts";

interface PedidoItemsTableProps {
    items: PedidoItemDto[];
}

export const PedidoItemsTable = ({ items }: PedidoItemsTableProps) => {
    const columns = [
        {
            key: 'produto',
            title: 'Produto',
        },
        {
            key: 'quantidade',
            title: 'Quantidade',
            render: (value: unknown) => (value as number).toString(),
        },
        {
            key: 'precoUnitario',
            title: 'Preço Unitário',
            render: (value: unknown) => formatCurrency(value as number),
        },
        {
            key: 'subtotal',
            title: 'Subtotal',
            render: (value: unknown) => formatCurrency(value as number),
        },
    ];

    return (
        <div className="space-y-3">
            <h4 className="font-semibold text-gray-900">Itens do Pedido</h4>
            <Table<PedidoItemDto> columns={columns} data={items} emptyMessage="Nenhum item no pedido" />
            <div className="flex justify-end pt-3 border-t">
                <div className="text-right">
                    <p className="text-sm text-gray-600">Total:</p>
                    <p className="text-2xl font-bold text-primary-600">
                        {formatCurrency(items.reduce((sum, item) => sum + item.subtotal, 0))}
                    </p>
                </div>
            </div>
        </div>
    );
};
