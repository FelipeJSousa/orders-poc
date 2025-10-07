import axios from 'axios';
import { envConfig } from '@/core/config/env.config';

interface GoogleCallbackResponse {
    token: string;
    user: {
        id: string;
        name: string;
        email: string;
        picture: string;
    };
    expiresIn: number;
}

export const authService = {
    redirectToGoogle() {
        const params = new URLSearchParams({
            client_id: envConfig.googleClientId,
            redirect_uri: envConfig.googleRedirectUri,
            response_type: 'code',
            scope: 'openid profile email',
            access_type: 'offline',
            prompt: 'consent',
        });

        window.location.href = `https://accounts.google.com/o/oauth2/v2/auth?${params.toString()}`;
    },

    async exchangeCodeForToken(code: string): Promise<GoogleCallbackResponse> {
        const response = await axios.post(
            `${envConfig.apiBaseUrl}/auth`,
            { code }
        );
        return response.data;
    },
};
