export interface User {
    id: string;
    name: string;
    email: string;
    picture?: string;
}

export interface AuthState {
    user: User | null;
    isAuthenticated: boolean;
    isLoading: boolean;
    accessToken: string | null;
}

export interface LoginResponse {
    token: string;
    username: string;
    expiresIn: number;
}
