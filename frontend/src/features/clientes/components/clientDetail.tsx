import { MdPerson, MdEmail, MdPhone, MdLocationOn } from 'react-icons/md';
import { ClienteDto } from '../types/cliente.types';
import { Badge } from '@/shared/components/ui';

interface ClienteDetailProps {
    cliente: ClienteDto;
}

export const ClienteDetail = ({ cliente }: ClienteDetailProps) => {
    return (
        <div className="space-y-4">
            <div className="flex items-start justify-between">
                <div>
                    <h3 className="text-2xl font-bold text-gray-900">{cliente.nome}</h3>
                    <div className="flex items-center gap-2 mt-1">
                        <Badge variant={cliente.tipoPessoa === 1 ? 'info' : 'success'}>
                            {cliente.tipoPessoa === 1 ? 'Pessoa Física' : 'Pessoa Jurídica'}
                        </Badge>
                        <Badge variant={cliente.ativo ? 'success' : 'danger'}>
                            {cliente.ativo ? 'Ativo' : 'Inativo'}
                        </Badge>
                    </div>
                </div>
            </div>

            <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                <div className="flex items-start gap-3">
                    <MdEmail className="w-5 h-5 text-gray-400 mt-0.5" />
                    <div>
                        <p className="text-sm text-gray-500">Email</p>
                        <p className="text-gray-900">{cliente.email}</p>
                    </div>
                </div>

                {cliente.telefone && (
                    <div className="flex items-start gap-3">
                        <MdPhone className="w-5 h-5 text-gray-400 mt-0.5" />
                        <div>
                            <p className="text-sm text-gray-500">Telefone</p>
                            <p className="text-gray-900">{cliente.telefone}</p>
                        </div>
                    </div>
                )}

                {cliente.cpfCnpj && (
                    <div className="flex items-start gap-3">
                        <MdPerson className="w-5 h-5 text-gray-400 mt-0.5" />
                        <div>
                            <p className="text-sm text-gray-500">
                                {cliente.tipoPessoa === 1 ? 'CPF' : 'CNPJ'}
                            </p>
                            <p className="text-gray-900">{cliente.cpfCnpj}</p>
                        </div>
                    </div>
                )}

                {(cliente.endereco || cliente.cidade || cliente.estado || cliente.cep) && (
                    <div className="flex items-start gap-3 md:col-span-2">
                        <MdLocationOn className="w-5 h-5 text-gray-400 mt-0.5" />
                        <div>
                            <p className="text-sm text-gray-500">Endereço</p>
                            <p className="text-gray-900">
                                {cliente.endereco && <>{cliente.endereco}<br /></>}
                                {cliente.cidade && cliente.estado && (
                                    <>{cliente.cidade} - {cliente.estado}</>
                                )}
                                {cliente.cep && <> | CEP: {cliente.cep}</>}
                            </p>
                        </div>
                    </div>
                )}
            </div>

            <div className="pt-4 border-t flex w-full justify-between">
                <p className="text-sm text-gray-500">
                    Cadastrado em: {new Date(cliente.criadoEm).toLocaleDateString('pt-BR')}
                </p>
                <p className="text-sm text-gray-500" hidden={!cliente.atualizadoEm}>
                    Última atualização: {new Date(cliente.atualizadoEm!).toLocaleDateString('pt-BR')}
                </p>
            </div>
        </div>
    );
};
