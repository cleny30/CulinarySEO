export interface Product {
  productId: string;
  productName: string;
  price: number;
  finalPrice: number;
  discount: number;
  totalQuantity: number;
  totalSold: number;
  reviewCount: number;
  averageRating: number;
  productImages: string[];
}

export interface ProductResult {
  items: Product[];
  totalItems: number;
  page: number;
  pageSize: number;
}