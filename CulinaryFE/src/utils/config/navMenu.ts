export interface NavItem {
  label: string;
  href: string;
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

export const menuNav: NavItem[] = [
  {
    label: "Trang Chủ",
    href: "/",
  },
  {
    label: "Thực Đơn",
    href: "/menu",
    isCategory: true,
    featureCat:[
      {
        title: "Món đang sale",
        url: "/menu/sale",
        image: "/img/featurePic1.jpg"
      },
      {
        title: "Món ăn nổi bật",
        url: "/menu/attract",
        image: "/img/featurePic2.jpg"
      },
    ],
    children: [
      {
        label: "Món Khai Vị",
        href: "/menu/appetizers",
        image: "/img/banner_1.webp",
      },
      {
        label: "Món Chính",
        href: "/menu/main-courses",
        image: "/img/banner_1.webp",
      },
      {
        label: "Tráng Miệng",
        href: "/menu/desserts",
        image: "/img/banner_1.webp",
      },
      {
        label: "Đồ Uống",
        href: "/menu/drinks",
        image: "/img/banner_1.webp",
      },
      {
        label: "Đồ Uống",
        href: "/menu/drinks",
        image: "/img/banner_1.webp",
      },
      {
        label: "Đồ Uống",
        href: "/menu/drinks",
        image: "/img/banner_1.webp",
      },
      {
        label: "Đồ Uống",
        href: "/menu/drinks",
        image: "/img/banner_1.webp",
      },
      {
        label: "Đồ Uống",
        href: "/menu/drinks",
        image: "/img/banner_1.webp",
      },
      {
        label: "Đồ Uống",
        href: "/menu/drinks",
        image: "/img/banner_1.webp",
      },
      {
        label: "Đồ Uống",
        href: "/menu/drinks",
        image: "/img/banner_1.webp",
      },
    ],
  },
  {
    label: "Giới Thiệu",
    href: "/about",
    children: [
      {
        label: "About 1",
        href: "/menu/appetizers",
      },
      {
        label: "Trang nay trang kia",
        href: "/menu/main-courses",
      },
      {
        label: "About 3",
        href: "/menu/desserts",
      },
      {
        label: "trang dang nhap",
        href: "/menu/desserts",
      },
      {
        label: "Trang tinh toan",
        href: "/menu/desserts",
      },
      {
        label: "About 3",
        href: "/menu/desserts",
      },
    ],
  },
  {
    label: "Liên Hệ",
    href: "/contact",
  },
];
