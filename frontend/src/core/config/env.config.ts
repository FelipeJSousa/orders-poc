export const envConfig = {
    apiBaseUrl: import.meta.env.VITE_API_BASE_URL || 'http://localhost:5000/api',
    googleClientId: import.meta.env.VITE_GOOGLE_CLIENT_ID || '',
    googleRedirectUri: import.meta.env.VITE_GOOGLE_REDIRECT_URI || 'http://localhost:5173/callback',
    isDevelopment: import.meta.env.DEV,
    isProduction: import.meta.env.PROD,
} as const;

if (!envConfig.apiBaseUrl) {
    throw new Error('VITE_API_BASE_URL is required');
}
