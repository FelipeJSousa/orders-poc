import { MdPerson, MdCalendarToday } from 'react-icons/md';
import { StatusBadge } from './StatusBadge';
import { PedidoItemsTable } from './PedidoItemsTable';
import { formatCurrency, formatDateTime } from '@/shared/utils/format';
import { PedidoDto } from "@/features/pedidos/types/pedido.type.ts";

interface PedidoDetailProps {
    pedido: PedidoDto;
}

export const PedidoDetail = ({ pedido }: PedidoDetailProps) => {
    return (
        <div className="space-y-6">
            <div className="flex items-start justify-between">
                <div>
                    <h3 className="text-2xl font-bold text-gray-900">Pedido #{pedido.numeroPedido}</h3>
                    <div className="flex items-center gap-2 mt-2">
                        <StatusBadge status={pedido.status} />
                    </div>
                </div>
                <div className="text-right">
                    <p className="text-sm text-gray-500">Valor Total</p>
                    <p className="text-2xl font-bold text-primary-600">
                        {formatCurrency(pedido.valorTotal)}
                    </p>
                </div>
            </div>

            <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                <div className="flex items-start gap-3">
                    <MdPerson className="w-5 h-5 text-gray-400 mt-0.5" />
                    <div>
                        <p className="text-sm text-gray-500">Cliente</p>
                        <p className="text-gray-900 font-medium">{pedido.clienteNome}</p>
                    </div>
                </div>

                <div className="flex items-start gap-3">
                    <MdCalendarToday className="w-5 h-5 text-gray-400 mt-0.5" />
                    <div>
                        <p className="text-sm text-gray-500">Data do Pedido</p>
                        <p className="text-gray-900">{formatDateTime(pedido.dataPedido)}</p>
                    </div>
                </div>
            </div>

            {pedido.observacoes && (
                <div>
                    <h4 className="font-semibold text-gray-900 mb-2">Observações</h4>
                    <p className="text-gray-600 bg-gray-50 p-3 rounded-lg">{pedido.observacoes}</p>
                </div>
            )}

            <PedidoItemsTable items={pedido.itens} />

            <div className="pt-4 border-t w-full flex justify-between items-center">
                <p className="text-sm text-gray-500">
                    Criado em: {formatDateTime(pedido.criadoEm)}
                </p>
                {pedido.atualizadoEm && (
                    <p className="text-sm text-gray-500">
                        Última atualização: {formatDateTime(pedido.atualizadoEm)}
                    </p>
                )}
            </div>
        </div>
    );
};
