export interface NavItem {
  label: string;
  href?: string;
  slug: string;
  isCategory?: boolean;
  featureCat?: Array<{
    title?: string;
    url?: string;
    image?: string;
  }>;
  children?: Array<{
    label: string;
    href: string;
    image?: string;
  }>;
}