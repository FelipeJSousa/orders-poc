# OrdersPoc Frontend

Frontend React + TypeScript para sistema de gerenciamento de pedidos.

## Stack

- **React 18** - UI Library
- **TypeScript** - Type Safety
- **Vite** - Build Tool
- **TailwindCSS** - Styling
- **React Router** - Routing
- **TanStack Query** - Server State Management
- **Zustand** - Client State Management
- **React Hook Form** - Form Management
- **Zod** - Validation
- **Axios** - HTTP Client

## Estrutura de Pastas

src/
├── features/ # Features do sistema
│ ├── clientes/ # Feature de clientes
│ ├── pedidos/ # Feature de pedidos
│ └── auth/ # Feature de autenticação
├── shared/ # Código compartilhado
│ ├── components/ # Componentes reutilizáveis
│ ├── hooks/ # Custom hooks
│ ├── utils/ # Utilitários
│ ├── constants/ # Constantes
│ └── types/ # Tipos globais
└── core/ # Core da aplicação
├── api/ # Configuração API
├── config/ # Configurações
└── router/ # Rotas

## Setup

Instalar dependências
```shell 
npm install
```

Configurar variáveis de ambiente
```shell 
cp .env.example .env
```

Rodar em desenvolvimento
```shell 
npm run dev
```

Build para produção
```shell 
npm run build
```

## Scripts Disponíveis

- `npm run dev` - Inicia servidor de desenvolvimento
- `npm run build` - Build para produção
- `npm run lint` - Roda ESLint
- `npm run lint:fix` - Corrige erros de lint automaticamente
- `npm run format` - Formata código com Prettier
- `npm run type-check` - Verifica tipos TypeScript
