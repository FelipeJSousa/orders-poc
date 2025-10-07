import { WebStorageStateStore } from 'oidc-client-ts';
import { envConfig } from './env.config';

export const oidcConfig = {
    authority: envConfig.googleAuthority,
    client_id: envConfig.googleClientId,
    redirect_uri: envConfig.googleRedirectUri,
    response_type: 'code',
    scope: 'openid profile email',

    userStore: new WebStorageStateStore({ store: window.localStorage }),

    automaticSilentRenew: false,

    loadUserInfo: true,

    post_logout_redirect_uri: window.location.origin,

    metadata: {
        issuer: 'https://accounts.google.com',
        authorization_endpoint: 'https://accounts.google.com/o/oauth2/v2/auth',
        token_endpoint: 'https://oauth2.googleapis.com/token',
        userinfo_endpoint: 'https://openidconnect.googleapis.com/v1/userinfo',
        jwks_uri: 'https://www.googleapis.com/oauth2/v3/certs',
    },
};
