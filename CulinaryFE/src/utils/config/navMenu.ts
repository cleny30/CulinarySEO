import type { NavItem } from "@/types/home";


export const menuNav: NavItem[] = [
  {
    label: "Trang Chủ",
    href: "/",
    slug: "trang-chu",
  },
  {
    label: "Thực Đơn",
    href: "/menu",
    slug: "thuc-don",
    isCategory: true,
    featureCat: [
      {
        title: "Món đang sale",
        url: "/menu/sale",
        image: "/img/featurePic1.jpg",
      },
      {
        title: "Món ăn nổi bật",
        url: "/menu/attract",
        image: "/img/featurePic2.jpg",
      },
    ],
    children: [],
  },
  {
    label: "Giới Thiệu",
    href: "/about",
    slug: "gioi-thieu",
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
        label: "About 4",
        href: "/menu/desserts",
      },
    ],
  },
  {
    label: "Liên Hệ",
    href: "/contact",
    slug: "lien-he",
  },
];


export const footerMenu: NavItem[] =[
  {
    label: "Dịch vụ",
    slug: "dich-vu",
    children:[
      {
        label:"Về chúng tôi",
        href:"/pages/about-us"
      },
      {
        label:"Liên hệ",
        href:"/pages/contact-us"
      },
      {
        label:"Tin tức",
        href:"/blogs/all"
      },
      {
        label:"Sitemap",
        href:"/sitemap.xml"
      },
    ]
  },
  {
    label: "Chính sách",
    slug: "chinh-sach",
    children:[
      {
        label:"Chính sách thanh toán",
        href:"/pages/payment-policy"
      },
      {
        label:"Chính sách bảo mật",
        href:"/pages/privacy-policy"
      },
      {
        label:"Chính sách đổi trả",
        href:"/pages/refund-policy"
      },
      {
        label:"Chính sách giao hàng",
        href:"/pages/shipping-policy"
      },
      {
        label:"Điều khoản & điều kiện",
        href:"/pages/terms-and-conditions"
      },
    ]
  },
  {
    label: "Tài khoản",
    slug: "tai-khoan",
    children:[
      {
        label:"Tài khoản của tôi",
        href:"/account/profile"
      },
      {
        label:"Giỏ hàng",
        href:"/account/cart"
      },
      {
        label:"Lịch sử mua hàng",
        href:"/account/order-history"
      },
    ]
  }
]