import { ReactNode } from 'react';

/* eslint-disable @typescript-eslint/no-explicit-any */
interface Column<T extends Record<string, any>> {
    key: string;
    title: string;
    render?: (value: unknown, row: T) => ReactNode;
}

interface TableProps<T extends Record<string, any>> {
    columns: Column<T>[];
    data: T[];
    emptyMessage?: string;
    onRowClick?: (row: T) => void;
}

export const Table = <T extends Record<string, any>>({ columns, data, emptyMessage = 'Nenhum dado encontrado', onRowClick }: TableProps<T>) => {
    return (
        <div className="overflow-x-auto">
            <table className="min-w-full divide-y divide-gray-200">
                <thead className="bg-gray-50">
                <tr>
                    {columns.map((column) => (
                        <th
                            key={column.key}
                            className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider"
                        >
                            {column.title}
                        </th>
                    ))}
                </tr>
                </thead>
                <tbody className="bg-white divide-y divide-gray-200">
                {data.length === 0 ? (
                    <tr>
                        <td colSpan={columns.length} className="px-6 py-4 text-center text-gray-500">
                            {emptyMessage}
                        </td>
                    </tr>
                ) : (
                    data.map((row, rowIndex) => (
                        <tr
                            key={rowIndex}
                            onClick={() => onRowClick?.(row)}
                            className={onRowClick ? 'hover:bg-gray-50 cursor-pointer' : ''}
                        >
                            {columns.map((column) => (
                                <td key={column.key} className="px-6 py-4 whitespace-nowrap text-sm text-gray-900">
                                    {column.render ? column.render(row[column.key], row) : row[column.key]}
                                </td>
                            ))}
                        </tr>
                    ))
                )}
                </tbody>
            </table>
        </div>
    );
};
