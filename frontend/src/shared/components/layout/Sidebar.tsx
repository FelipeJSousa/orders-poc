import { NavLink } from 'react-router-dom';
import { MdHome, MdPeople, MdShoppingBag } from 'react-icons/md';

const navItems = [
    { to: '/', icon: MdHome, label: 'Dashboard' },
    { to: '/clientes', icon: MdPeople, label: 'Clientes' },
    { to: '/pedidos', icon: MdShoppingBag, label: 'Pedidos' },
];

export const Sidebar = () => {
    return (
        <aside className="w-64 bg-white border-r border-gray-200 min-h-screen">
            <nav className="p-4 space-y-2">
                {navItems.map((item) => (
                    <NavLink
                        key={item.to}
                        to={item.to}
                        className={({ isActive }) =>
                            `flex items-center gap-3 px-4 py-3 rounded-lg transition-colors ${
                                isActive
                                    ? 'bg-primary-50 text-primary-700 font-medium'
                                    : 'text-gray-700 hover:bg-gray-50'
                            }`
                        }
                    >
                        <item.icon className="w-5 h-5" />
                        <span>{item.label}</span>
                    </NavLink>
                ))}
            </nav>
        </aside>
    );
};
