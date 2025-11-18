// Runtime configuration helper.
// In production set `window.__env = { apiBaseUrl: 'https://api.example.com' }` before loading the app.
// Default to the local .NET backend used during development (Swagger base URL).
export const API_BASE_URL = (window as any).__env?.apiBaseUrl || 'https://localhost:7285';
