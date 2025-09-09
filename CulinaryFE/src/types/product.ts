export interface Product {
  productId: string;
  productName: string;
  shortDescription: string;
  slug: string;
  pageTitle: string;
  metaDescription: string;
  price: number;
  finalPrice: number;
  discount: number;
  totalQuantity: number;
  totalSold: number;
  reviewCount: number;
  averageRating: number;
  productImages: string[];
  categoryIds: number[];
  stocks: Record<string, number>; // key: warehouseId (UUID), value: stock quantity
  currentWarehouseQuantity: number;
}

export interface ProductResult {
  items: Product[];
  totalItems: number;
  page: number;
  pageSize: number;
}