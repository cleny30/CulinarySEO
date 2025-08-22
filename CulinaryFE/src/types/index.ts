import type { FC, ReactNode } from 'react';

export interface RouteConfig {
  path: string;
  name: string;
  component: FC;
  layout?: FC<{ children: ReactNode }>;
  allowPermissions?: string[];
}
