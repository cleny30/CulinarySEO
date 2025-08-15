export interface NavItem {
  label: string;
  href: string;
  isCategory?: boolean;
  image?: string;
  children?: NavItem[];
}

export const menuNav: NavItem[] = [
  {
    label: 'Trang Chủ',
    href: '/',
  },
  {
    label: 'Thực Đơn',
    href: '/menu',
    isCategory: true,
    children: [
      {
        label: 'Món Khai Vị',
        href: '/menu/appetizers',
        isCategory: true,
        image: '/banner_1.webp',
      },
      {
        label: 'Món Chính',
        href: '/menu/main-courses',
        isCategory: true,
        image: '/banner_1.webp',
      },
      {
        label: 'Tráng Miệng',
        href: '/menu/desserts',
        isCategory: true,
        image: '/banner_1.webp',
      },
      {
        label: 'Đồ Uống',
        href: '/menu/drinks',
        isCategory: true,
        image: '/banner_1.webp',
      },
    ],
  },
  {
    label: 'Giới Thiệu',
    href: '/about',
  },
  {
    label: 'Liên Hệ',
    href: '/contact',
  },
];
