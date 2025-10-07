import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { Button, Input, Select } from '@/shared/components/ui';
import { createClienteSchema, CreateClienteFormData } from '../validation/clienteSchema';

interface ClienteFormProps {
    onSubmit: (data: CreateClienteFormData) => void;
    isLoading?: boolean;
    initialData?: Partial<CreateClienteFormData>;
}

const tipoPessoaOptions = [
    { value: 1, label: 'Pessoa Física' },
    { value: 2, label: 'Pessoa Jurídica' },
];

export const ClienteForm = ({ onSubmit, isLoading, initialData }: ClienteFormProps) => {
    const {
        register,
        handleSubmit,
        formState: { errors },
        watch,
    } = useForm<CreateClienteFormData>({
        resolver: zodResolver(createClienteSchema),
        defaultValues: initialData,
    });

    const tipoPessoa = watch('tipoPessoa');

    return (
        <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
            <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                <Input
                    label="Nome"
                    required
                    {...register('nome')}
                    error={errors.nome?.message}
                    placeholder="Nome completo"
                />

                <Input
                    label="Email"
                    type="email"
                    required
                    {...register('email')}
                    error={errors.email?.message}
                    placeholder="exemplo@email.com"
                />
            </div>

            <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                <Select
                    label="Tipo de Pessoa"
                    required
                    {...register('tipoPessoa', { valueAsNumber: true })}
                    error={errors.tipoPessoa?.message}
                    options={tipoPessoaOptions}
                    placeholder="Selecione..."
                />

                <Input
                    label={tipoPessoa === 1 ? 'CPF' : 'CNPJ'}
                    {...register('cpfCnpj')}
                    error={errors.cpfCnpj?.message}
                    placeholder={tipoPessoa === 1 ? '000.000.000-00' : '00.000.000/0000-00'}
                />
            </div>

            <Input
                label="Telefone"
                {...register('telefone')}
                error={errors.telefone?.message}
                placeholder="(00) 00000-0000"
            />

            <Input
                label="Endereço"
                {...register('endereco')}
                error={errors.endereco?.message}
                placeholder="Rua, número, complemento"
            />

            <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
                <Input
                    label="Cidade"
                    {...register('cidade')}
                    error={errors.cidade?.message}
                    placeholder="Cidade"
                />

                <Input
                    label="Estado"
                    {...register('estado')}
                    error={errors.estado?.message}
                    placeholder="UF"
                    maxLength={2}
                />

                <Input
                    label="CEP"
                    {...register('cep')}
                    error={errors.cep?.message}
                    placeholder="00000-000"
                />
            </div>

            <div className="flex justify-end gap-3 pt-4">
                <Button type="submit" isLoading={isLoading}>
                    Salvar Cliente
                </Button>
            </div>
        </form>
    );
};
