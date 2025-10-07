import { useState } from 'react';
import { Button, Select } from '@/shared/components/ui';
import { STATUS_PEDIDO_LABELS } from '@/shared/constants/status.constants';

interface UpdateStatusModalProps {
    currentStatus: number;
    onSubmit: (newStatus: number) => void;
    isLoading?: boolean;
}

export const UpdateStatusModal = ({ currentStatus, onSubmit, isLoading }: UpdateStatusModalProps) => {
    const [selectedStatus, setSelectedStatus] = useState(currentStatus);

    const statusOptions = Object.entries(STATUS_PEDIDO_LABELS).map(([value, label]) => ({
        value: parseInt(value),
        label,
    }));

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        onSubmit(selectedStatus);
    };

    return (
        <form onSubmit={handleSubmit} className="space-y-4">
            <Select
                label="Novo Status"
                value={selectedStatus}
                onChange={(e) => setSelectedStatus(parseInt(e.target.value))}
                options={statusOptions}
            />

            <div className="flex justify-end gap-3 pt-4">
                <Button type="submit" isLoading={isLoading}>
                    Atualizar Status
                </Button>
            </div>
        </form>
    );
};
