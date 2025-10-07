import { Badge } from '@/shared/components/ui';
import { STATUS_PEDIDO_LABELS, STATUS_PEDIDO_COLORS } from '@/shared/constants/status.constants';

interface StatusBadgeProps {
    status: number;
}

export const StatusBadge = ({ status }: StatusBadgeProps) => {
    const label = STATUS_PEDIDO_LABELS[status] || 'Desconhecido';
    const colorClass = STATUS_PEDIDO_COLORS[status] || 'badge';

    const variantMap: Record<string, 'success' | 'warning' | 'danger' | 'info'> = {
        'badge-success': 'success',
        'badge-warning': 'warning',
        'badge-danger': 'danger',
        'badge-info': 'info',
    };

    const variant = variantMap[colorClass] || 'info';

    return <Badge variant={variant}>{label}</Badge>;
};
