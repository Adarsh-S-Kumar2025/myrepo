// Example runtime env file. Copy to server root in production and set apiBaseUrl appropriately.
window.__env = window.__env || {};
// Default to local .NET backend used by the developer (Swagger base URL).
// Replace this file at deploy time to point to staging/production API.
window.__env.apiBaseUrl = window.__env.apiBaseUrl || 'https://localhost:7285';
