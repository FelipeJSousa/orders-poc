import { BiLogOut } from 'react-icons/bi';
import { useAuth } from "@/features/auth/context/AuthContext.tsx";

export const UserProfile = () => {
    const { user, logout } = useAuth();

    if (!user) return null;

    return (
        <div className="flex items-center gap-3">
            {user.picture ? (
                <img
                    src={user.picture}
                    alt={user.name}
                    className="w-10 h-10 rounded-full border-2 border-gray-200"
                />
            ) : (
                <div className="w-10 h-10 rounded-full bg-primary-500 flex items-center justify-center text-white font-semibold">
                    {user.name.charAt(0).toUpperCase()}
                </div>
            )}
            <div className="flex flex-col">
                <span className="text-sm font-medium text-gray-900">{user.name}</span>
                <span className="text-xs text-gray-500">{user.email}</span>
            </div>
            <button
                onClick={logout}
                className="btn-secondary text-sm flex items-center gap-2 ml-2"
                title="Sair"
            >
                <BiLogOut className="w-4 h-4" />
                Sair
            </button>
        </div>
    );
};
