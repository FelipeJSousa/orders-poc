import { useForm, useFieldArray } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { MdAdd, MdDelete } from 'react-icons/md';
import { Button, Input, Select } from '@/shared/components/ui';
import { createPedidoSchema, CreatePedidoFormData } from '../validation/pedidoSchema';
import { useClientes } from '@/features/clientes/hooks/useClientes';
import { formatCurrency } from '@/shared/utils/format';

interface PedidoFormProps {
    onSubmit: (data: CreatePedidoFormData) => void;
    isLoading?: boolean;
}

export const PedidoForm = ({ onSubmit, isLoading }: PedidoFormProps) => {
    const { data: clientes = [] } = useClientes();

    const {
        register,
        handleSubmit,
        formState: { errors },
        control,
        watch,
    } = useForm<CreatePedidoFormData>({
        resolver: zodResolver(createPedidoSchema),
        defaultValues: {
            itens: [{ produto: '', quantidade: 1, precoUnitario: 0 }],
        },
    });

    const { fields, append, remove } = useFieldArray({
        control,
        name: 'itens',
    });

    const itens = watch('itens');
    const total = itens.reduce(
        (sum, item) => sum + (item.quantidade || 0) * (item.precoUnitario || 0),
        0
    );

    const clienteOptions = clientes.map((c) => ({
        value: c.id,
        label: c.nome,
    }));

    return (
        <form onSubmit={handleSubmit(onSubmit)} className="space-y-6">
            <div>
                <Select
                    label="Cliente"
                    required
                    {...register('clienteId')}
                    error={errors.clienteId?.message}
                    options={clienteOptions}
                    placeholder="Selecione um cliente..."
                />
            </div>

            <div className="space-y-4">
                <div className="flex items-center justify-between">
                    <h4 className="font-semibold text-gray-900">Itens do Pedido</h4>
                    <Button
                        type="button"
                        size="sm"
                        variant="secondary"
                        onClick={() => append({ produto: '', quantidade: 1, precoUnitario: 0 })}
                    >
                        <MdAdd className="w-4 h-4" />
                        Adicionar Item
                    </Button>
                </div>

                {fields.map((field, index) => (
                    <div key={field.id} className="p-4 bg-gray-50 rounded-lg space-y-3">
                        <div className="flex items-start justify-between">
                            <h5 className="font-medium text-gray-700">Item {index + 1}</h5>
                            {fields.length > 1 && (
                                <button
                                    type="button"
                                    onClick={() => remove(index)}
                                    className="text-red-600 hover:text-red-800"
                                >
                                    <MdDelete className="w-5 h-5" />
                                </button>
                            )}
                        </div>

                        <Input
                            label="Produto"
                            {...register(`itens.${index}.produto`)}
                            error={errors.itens?.[index]?.produto?.message}
                            placeholder="Nome do produto"
                        />

                        <div className="grid grid-cols-2 gap-3">
                            <Input
                                label="Quantidade"
                                type="number"
                                min="1"
                                {...register(`itens.${index}.quantidade`, { valueAsNumber: true })}
                                error={errors.itens?.[index]?.quantidade?.message}
                            />

                            <Input
                                label="Preço Unitário"
                                type="number"
                                step="0.01"
                                min="0"
                                {...register(`itens.${index}.precoUnitario`, { valueAsNumber: true })}
                                error={errors.itens?.[index]?.precoUnitario?.message}
                            />
                        </div>

                        {itens[index]?.quantidade > 0 && itens[index]?.precoUnitario > 0 && (
                            <div className="text-right">
                                <p className="text-sm text-gray-600">Subtotal:</p>
                                <p className="text-lg font-semibold text-primary-600">
                                    {formatCurrency(itens[index].quantidade * itens[index].precoUnitario)}
                                </p>
                            </div>
                        )}
                    </div>
                ))}

                {errors.itens?.message && (
                    <p className="text-sm text-red-600">{errors.itens.message}</p>
                )}
            </div>

            <div>
                <label className="label-field">Observações</label>
                <textarea
                    {...register('observacoes')}
                    className="input-field resize-none"
                    rows={3}
                    placeholder="Observações sobre o pedido (opcional)"
                />
                {errors.observacoes && (
                    <p className="mt-1 text-sm text-red-600">{errors.observacoes.message}</p>
                )}
            </div>

            <div className="flex justify-between items-center p-4 bg-primary-50 rounded-lg">
                <span className="text-lg font-semibold text-gray-900">Total do Pedido:</span>
                <span className="text-2xl font-bold text-primary-600">{formatCurrency(total)}</span>
            </div>

            <div className="flex justify-end gap-3 pt-4">
                <Button type="submit" isLoading={isLoading}>
                    Criar Pedido
                </Button>
            </div>
        </form>
    );
};
